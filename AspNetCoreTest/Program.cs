using System.Runtime.CompilerServices;
using System.Text.Encodings.Web;
using System.Text.Json.Serialization;
using CozeNet.AspNetCore;
using CozeNet.Chat;
using CozeNet.Chat.Models;
using CozeNet.Conversation;
using CozeNet.Core.Models;
using CozeNet.Message;
using CozeNet.Message.Models;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateSlimBuilder(args);
builder.Services.AddCozeNet(builder.Configuration);
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default);
    options.SerializerOptions.Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
});
builder.Services.AddHttpClient();
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
    options.InstanceName = "CozeTest:";
});
var app = builder.Build();

var chatApi = app.MapGroup("/chat");
chatApi.MapGet("/{msg}", async (string msg, ConversationService conversationService, ChatService chatService,
    MessageService messageService, IOptionsMonitor<CozeOptions> options, CancellationToken cancellationToken) =>
{
    var c = await conversationService.CreateAsync();
    var chatResponse = await chatService.SendNoneStreamAsync(c.Data.ID, new ChatRequest
    {
        AdditionalMessages =
        [
            new BaseMessageObject
            {
                Content = msg, ContentType = ContentType.Text, Role = RoleType.User, Type = MessageType.Question,
            }
        ],
        BotID = options.CurrentValue.BotId,
        UserID = Guid.NewGuid().ToString(),
    });
    do
    {
        var chatResult = await chatService.RetrieveAsync(c.Data.ID, chatResponse.Data.ID);
        if (chatResult is not { Data: { Status: "completed" } })
        {
            Console.WriteLine(chatResult.Data.Status);
            await Task.Delay(TimeSpan.FromSeconds(3), cancellationToken);
            continue;
        }

        var messages =
            await messageService.ListAsync(c.Data.ID, new MessageListRequest { ChatID = chatResponse.Data.ID });
        foreach (var m in messages!.Data!)
        {
            Console.WriteLine($"{m.Role} {m.Type}: {m.Content}");
        }

        return messages.Data.First().Content;
    } while (true);
}).WithSummary("普通对话");

chatApi.MapGet("/complete/{msg}", async (string msg, ChatService chatService, ConversationService conversationService,
    IOptionsMonitor<CozeOptions> options, CancellationToken cancellationToken) =>
{
    var c = await conversationService.CreateAsync();

    async IAsyncEnumerable<KeyValuePair<string, string>> ChatCompleteAsync(
        string msg, ChatService chatService,
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        var r = chatService.SendStreamAsync(c.Data.ID, new ChatRequest
        {
            BotID = options.CurrentValue.BotId,
            UserID = Guid.NewGuid().ToString(),
            AutoSaveHistory = true,
            Stream = true,
            AdditionalMessages =
            [
                new BaseMessageObject
                {
                    Content = msg, ContentType = ContentType.Text, Role = RoleType.User, Type = MessageType.Question,
                }
            ]
        }, cancellationToken);
        await foreach (var completionUpdate in r)
        {
            if (completionUpdate.Event is StreamEvents.DeltaMessage)
            {
                var message = completionUpdate.Data as MessageObject;
                if (message!.Type is not (MessageType.Answer or MessageType.FollowUp)) continue;
                yield return new KeyValuePair<string, string>(message.Content!, message.Type!);
            }
            else if (completionUpdate.Event is StreamEvents.MessageComplete)
            {
                var message = completionUpdate.Data as MessageObject;
                if (message!.Type is MessageType.Answer) //MessageComplete时会完整返回回答内容
                {
                    //保存内容到本地对话历史记录
                }
                else if (message!.Type is MessageType.FollowUp)
                {
                    //follow up是建议问题，用于前端展示
                    yield return new KeyValuePair<string, string>(message!.Content!, message.Type!);
                }
            }
            else if (completionUpdate.Event is StreamEvents.ChatComplete)
            {
                var chat = completionUpdate.Data as ChatObject;
                //chat!.Usage?.InputCount
                //chat!.Usage?.OutputCount
                //记录本次对话所消耗的token数量
            }
            else if (completionUpdate.Event is StreamEvents.ChatFailed)
            {
                var error = completionUpdate.Data as ChatObject;
                yield return new KeyValuePair<string, string>(error!.LastError?.Message ?? "", "Error");
                yield break; //出现错误，中断流生成
            }
        }

    }
    return TypedResults.ServerSentEvents(ChatCompleteAsync(msg, chatService, cancellationToken));

});

app.Run();


[JsonSerializable(typeof(CozeResult<ChatObject>))]
[JsonSerializable(typeof(OAuthToken))]
[JsonSerializable(typeof(TokenRequestBody))]
[JsonSerializable(typeof(KeyValuePair<string, string>))]
internal partial class AppJsonSerializerContext : JsonSerializerContext
{
}