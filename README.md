# Coze NET

[Coze API](https://www.coze.cn/open/docs/developer_guides/coze_api_overview)的.net实现。

实现了大多数接口，并重点测试了对话相关接口。

## 已实现

- 鉴权
  - 个人访问令牌
  - 开发者JWT
- 智能体
- 会话
- 消息
- 对话
- 文件
- 工作流
- 知识库

## 未实现

- 空间
- 渠道
- 语音

## 使用示例

详见[Test.cs](https://github.com/xxzl0130/CozeNet/blob/main/Test/Test.cs)

## ASP.NET Core 集成
安装nuget包
`dotnet add package CozeNet.AspNetCore`

### appsettings.json
```json
"Coze": {
  "Endpoint": "api.coze.cn",
  "AppId": "1111111111",
  "PublicKey": "111-222222222222222",
  "PrivateKey": "private_key.pem",
  "BotID": "777777777",
  "PersonalAccessToken": "",
  "HttpClientName": "",
  "TokenExpire": null
},
```
注意：
PersonalAccessToken和PublicKey、PrivateKey为二选一的关系。
HttpClientName用于获取注入自定义配置的HttpClient。
比如注入了一个名为CozeClient的httpclient，其中使用一个自定义的用于记录请求和响应日志的handler。则配置中需要设置为`"HttpClientName":"CozeClient"`.

`builder.Services.AddHttpClient("CozeClient").ConfigureHttpClientDefaults(defaults => defaults.AddHttpMessageHandler<LoggingHandler>());`


### Program.cs
```csharp
builder.Services.AddCozeNet();
```

由于使用了IDistributedCache来对accessToken进行缓存，所以需要安装[Redis包](https://www.nuget.org/packages/Microsoft.Extensions.Caching.StackExchangeRedis)配置Redis连接。如果有用其他的IDistributedCache实现也可以。
```
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
    options.InstanceName = "MyProjectName:";
});
```

### 通过依赖注入使用
注意：TypedResults.ServerSentEvents 为.net 10新增内容
```csharp
public ChatController(ConversationService conversationService, ChatService chatService,IOptionsMonitor<CozeOptions> options)
{
    [HttpPost("complete")]
    public async Task<> Chat(string message, CancellationToken cancellationToken)
    {
      async IAsyncEnumerable<StreamChatResponse> ChatCompleteAsync(
          string message, ChatService chatService,
          [EnumeratorCancellation] CancellationToken cancellationToken)
      {
          var r = chatService.SendStreamAsync(conversation!.Id, new()
          {
              BotID = options.CurrentValue.BotId,
              AutoSaveHistory = true,
              Stream = true,
              AdditionalMessages = [new() { Role = RoleType.User, Content = message, ContentType =      ContentType.Text, Type = MessageType.Question }]
          }, cancellationToken);
          await foreach (var completionUpdate in r)
          {
              if (completionUpdate.Event is CozeNet.Chat.Models.StreamEvents.DeltaMessage)
              {
                  var message = completionUpdate.Data as MessageObject;
                  if (message!.Type is not (MessageType.Answer or MessageType.FollowUp)) continue;
                  yield return new StreamChatResponse(message.Content!, message.Type!);
              }
              else if (completionUpdate.Event is CozeNet.Chat.Models.StreamEvents.MessageComplete)
              {
                  var message = completionUpdate.Data as MessageObject;
                  if (message!.Type is MessageType.Answer)//MessageComplete时会完整返回回答内容
                  {
                      //保存对话历史                      
                  }
                  else if (message!.Type is MessageType.FollowUp)
                  {
                      yield return new StreamChatResponse(message!.Content!, message.Type!);
                  }
              }
              else if (completionUpdate.Event is CozeNet.Chat.Models.StreamEvents.ChatComplete)
              {
                  var chat = completionUpdate.Data as CozeNet.Chat.Models.ChatObject;
                  //对话完毕，会返回本次对话消耗的token数量等信息
              }
              else if (completionUpdate.Event is CozeNet.Chat.Models.StreamEvents.ChatFailed)
              {
                  var error = completionUpdate.Data as CozeNet.Chat.Models.ChatObject;
                  yield return new StreamChatResponse(error!.LastError?.Message ?? "", "Error");
                  yield break;
              }
              //None, Created, InProgress, DeltaMessage, DeltaAudio, MessageComplete, ChatComplete,       ChatFailed, RequireAction, Error, Done
          }
      }

      return TypedResults.ServerSentEvents(ChatCompleteAsync(message, _chatService,  cancellationToken));
    }

}
```

