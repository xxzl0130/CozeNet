using CozeNet.Chat.Models;
using CozeNet.Core;
using CozeNet.Core.Models;
using CozeNet.Message.Models;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace CozeNet.Chat
{
    /// <summary>
    /// 对话服务
    /// </summary>
    /// <param name="context"></param>
    public class ChatService(Context context)
    {
        internal async IAsyncEnumerable<StreamMessage> ProcessStreamResponse(HttpResponseMessage response, [EnumeratorCancellation] CancellationToken cancellationToken)
        {
            await using var stream = await response.Content.ReadAsStreamAsync(cancellationToken);
            using var reader = new StreamReader(stream);

            var message = new StreamMessage();

            while (!reader.EndOfStream)
            {
                if (cancellationToken.IsCancellationRequested)
                    yield break;

                var line = await reader.ReadLineAsync(cancellationToken);
                if (string.IsNullOrEmpty(line))
                    continue;

                if (line.StartsWith("event:"))
                {
                    var eventStr = line["event:".Length..].Trim();
                    var eventEnum = eventStr.ToChatStreamEvents();
                    if (eventEnum == StreamEvents.None)
                        yield break;
                    message = new StreamMessage { Event = eventEnum };
                }
                else if (line.StartsWith("data:"))
                {
                    var dataStr = line["data:".Length..].Trim();
                    if (message.Event is StreamEvents.DeltaMessage or StreamEvents.DeltaAudio or StreamEvents.MessageComplete)
                        message.Data = JsonSerializer.Deserialize<MessageObject>(dataStr, options: context.JsonOptions);
                    else if (message.Event != StreamEvents.None && message.Event != StreamEvents.Done)
                        message.Data = JsonSerializer.Deserialize<ChatObject>(dataStr, options: context.JsonOptions);
                    else
                        message.Data = null;
                    yield return message;
                }
            }
        }

        /// <summary>
        /// 发起非流式响应对话
        /// </summary>
        /// <param name="conversationId"></param>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<CozeResult<ChatObject>?> SendNoneStreamAsync(string conversationId, ChatRequest request, CancellationToken cancellationToken = default)
        {
            var api = "/v3/chat";
            request.Stream = false;
            request.AutoSaveHistory = true;// 非流式传输时需将AutoSaveHistory设置为true
            var ret = await context.GetJsonAsync<CozeResult<ChatObject>>(api, HttpMethod.Post, JsonContent.Create(request, options: context.JsonOptions),
                parameters: new Dictionary<string, string> { { "conversation_id", conversationId } }, cancellationToken: cancellationToken);
            return ret;
        }

        /// <summary>
        /// 发起流式响应对话
        /// </summary>
        /// <param name="conversationId"></param>
        /// <param name="chatRequest"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async IAsyncEnumerable<StreamMessage> SendStreamAsync(string conversationId, ChatRequest chatRequest, [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            var api = "/v3/chat";
            chatRequest.Stream = true;
            using var request = context.GenerateRequest(api, HttpMethod.Post, JsonContent.Create(chatRequest, options: context.JsonOptions),
                parameters: new Dictionary<string, string> { { "conversation_id", conversationId } });
            using var response = await context.HttpClient!.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
            response.EnsureSuccessStatusCode();
            await foreach (var item in ProcessStreamResponse(response, cancellationToken))
            {
                yield return item;
            }
        }

        /// <summary>
        /// 查询对话详情
        /// </summary>
        /// <param name="conversationId"></param>
        /// <param name="chatId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<CozeResult<ChatObject>?> RetrieveAsync(string conversationId, string chatId, CancellationToken cancellationToken = default)
        {
            var api = "/v3/chat/retrieve";
            var parameters = new Dictionary<string, string> { { "conversation_id", conversationId }, { "chat_id", chatId } };
            return await context.GetJsonAsync<CozeResult<ChatObject>>(api, HttpMethod.Get,
                parameters: parameters, cancellationToken: cancellationToken);
        }

        /// <summary>
        /// 查看对话消息详情
        /// </summary>
        /// <param name="conversationId"></param>
        /// <param name="chatId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<CozeResult<MessageObject[]>?> MessageListAsync(string conversationId, string chatId, CancellationToken cancellationToken = default)
        {
            var api = "/v3/chat/message/list";
            var parameters = new Dictionary<string, string> { { "conversation_id", conversationId }, { "chat_id", chatId } };
            return await context.GetJsonAsync<CozeResult<MessageObject[]>>(api, HttpMethod.Get,
                parameters: parameters, cancellationToken: cancellationToken);
        }

        /// <summary>
        /// 提交工具执行结果，非流式响应
        /// </summary>
        /// <param name="conversationId">会话id</param>
        /// <param name="chatId">对话id</param>
        /// <param name="toolOutputs">工具执行结果</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns></returns>
        public async Task<CozeResult<ChatObject>?> SubmitToolOutputNoneStreamAsync(string conversationId, string chatId, IEnumerable<ToolOutput> toolOutputs, CancellationToken cancellationToken = default)
        {
            var api = "/v3/chat/submit_tool_outputs";
            SubmitToolOutput request = new()
            {
                ToolOutputs = toolOutputs.ToArray(),
                Stream = false,
            };
            return await context.GetJsonAsync<CozeResult<ChatObject>>(api, HttpMethod.Get, JsonContent.Create(request, options: context.JsonOptions), cancellationToken: cancellationToken);
        }

        /// <summary>
        /// 提交工具执行结果，流式响应
        /// </summary>
        /// <param name="conversationId">会话id</param>
        /// <param name="chatId">对话id</param>
        /// <param name="toolOutputs">工具执行结果</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns></returns>
        public async IAsyncEnumerable<StreamMessage> SubmitToolOutputStreamAsync(string conversationId, string chatId, IEnumerable<ToolOutput> toolOutputs, [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            var api = "/v3/chat/submit_tool_outputs";
            var parameters = new Dictionary<string, string> { { "conversation_id", conversationId }, { "chat_id", chatId } };
            SubmitToolOutput requestBody = new()
            {
                ToolOutputs = toolOutputs.ToArray(),
                Stream = true,
            };
            using var request = context.GenerateRequest(api, HttpMethod.Post, JsonContent.Create(requestBody, options: context.JsonOptions),
                parameters: parameters);
            using var response = await context.HttpClient!.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
            response.EnsureSuccessStatusCode();
            await foreach (var item in ProcessStreamResponse(response, cancellationToken))
            {
                yield return item;
            }
        }

        /// <summary>
        /// 调用此接口取消进行中的对话。
        /// </summary>
        /// <param name="conversationId">会话id</param>
        /// <param name="chatId">对话id</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns></returns>
        public async Task<CozeResult<ChatObject>?> CancelChat(string conversationId, string chatId, CancellationToken cancellationToken = default)
        {
            var api = "/v3/chat/cancel";
            BaseChatObject body = new()
            {
                ConversationID = conversationId,
                ChatID = chatId
            };
            return await context.GetJsonAsync<CozeResult<ChatObject>>(api, HttpMethod.Get, JsonContent.Create(body, options: context.JsonOptions), cancellationToken: cancellationToken);
        }
    }
}
