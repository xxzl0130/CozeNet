using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CozeNet.Chat.Models;
using CozeNet.Core;
using CozeNet.Core.Models;
using CozeNet.Message.Models;
using System.Net.Http.Json;
using CozeNet.Utils;
using System.Runtime.CompilerServices;
using System.Net.Http;
using System.Text.Json;
using System.IO;
using System.Threading;

namespace CozeNet.Chat
{
    public class ChatService
    {
        private Context _context;

        public ChatService(Context context)
        {
            _context = context;
        }

        internal static async IAsyncEnumerable<StreamMessage> ProcessStreamResponce(HttpResponseMessage response, [EnumeratorCancellation] CancellationToken cancellationToken)
        {
            using var stream = await response.Content.ReadAsStreamAsync(cancellationToken);
            using var reader = new StreamReader(stream);

            StreamMessage message = new StreamMessage();

            while (!reader.EndOfStream)
            {
                if (cancellationToken.IsCancellationRequested)
                    yield break;

                var line = await reader.ReadLineAsync();
                if (string.IsNullOrEmpty(line))
                    continue;

                if (line.StartsWith("event:"))
                {
                    var eventStr = line.Substring("event:".Length).Trim();
                    var eventEnum = eventStr.ToChatStreamEvents();
                    if (eventEnum == StreamEvents.None)
                        yield break;
                    message = new StreamMessage { Event = eventEnum };
                }
                else if (line.StartsWith("data:"))
                {
                    var dataStr = line.Substring("data:".Length).Trim();
                    if (message.Event == StreamEvents.DeltaMessage || message.Event == StreamEvents.DeltaAudio || message.Event == StreamEvents.MessageComplete)
                        message.Data = JsonSerializer.Deserialize<MessageObject>(dataStr);
                    else if (message.Event != StreamEvents.None && message.Event != StreamEvents.Done)
                        message.Data = JsonSerializer.Deserialize<ChatObject>(dataStr);
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
        /// <returns></returns>
        public async Task<CozeResult<ChatObject>?> SendNoneStreamAsync(string conversationId, ChatRequest request)
        {
            var api = "/v3/chat";
            request.Stream = false;
            var ret = await _context.GetJsonAsync<CozeResult<ChatObject>>(api, HttpMethod.Post, JsonContent.Create(request),
                parameters: new Dictionary<string, string> { { "conversation_id", conversationId } });
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
            using var request = _context.GenerateRequest(api, HttpMethod.Post, JsonContent.Create(chatRequest),
                parameters: new Dictionary<string, string> { { "conversation_id", conversationId } });
            using var response = await _context.HttpClient!.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
            response.EnsureSuccessStatusCode();
            await foreach (var item in ProcessStreamResponce(response, cancellationToken))
            {
                yield return item;
            }
        }

        /// <summary>
        /// 查询对话详情
        /// </summary>
        /// <param name="conversationId"></param>
        /// <param name="chatId"></param>
        /// <returns></returns>
        public async Task<CozeResult<ChatObject>?> RetrieveAsync(string conversationId, string chatId)
        {
            var api = "/v3/chat/retrieve";
            var parameters = new Dictionary<string, string> { { "conversation_id", conversationId }, { "chat_id", chatId } };
            return await _context.GetJsonAsync<CozeResult<ChatObject>>(api, HttpMethod.Get,
                parameters: parameters);
        }

        /// <summary>
        /// 查看对话消息详情
        /// </summary>
        /// <param name="conversationId"></param>
        /// <param name="chatId"></param>
        /// <returns></returns>
        public async Task<CozeResult<MessageObject[]>?> MessageListAsync(string conversationId, string chatId)
        {
            var api = "/v3/chat/message/list";
            var parameters = new Dictionary<string, string> { { "conversation_id", conversationId }, { "chat_id", chatId } };
            return await _context.GetJsonAsync<CozeResult<MessageObject[]>>(api, HttpMethod.Get,
                parameters: parameters);
        }

        /// <summary>
        /// 提交工具执行结果，非流式响应
        /// </summary>
        /// <param name="conversationId"></param>
        /// <param name="chatId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<CozeResult<ChatObject>?> SubmitToolOutputNoneStreamAsync(string conversationId, string chatId, IEnumerable<ToolOutput> toolOutputs)
        {
            var api = "/v3/chat/submit_tool_outputs";
            var request = new
            {
                tool_outputs = toolOutputs.ToArray(),
                stream = false,
            };
            return await _context.GetJsonAsync<CozeResult<ChatObject>>(api, HttpMethod.Get, JsonContent.Create(request));
        }

        /// <summary>
        /// 提交工具执行结果，流式响应
        /// </summary>
        /// <param name="conversationId"></param>
        /// <param name="chatId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public async IAsyncEnumerable<StreamMessage> SubmitToolOutputStreamAsync(string conversationId, string chatId, IEnumerable<ToolOutput> toolOutputs, [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            var api = "/v3/chat/submit_tool_outputs";
            var parameters = new Dictionary<string, string> { { "conversation_id", conversationId }, { "chat_id", chatId } };
            var requestBody = new
            {
                tool_outputs = toolOutputs.ToArray(),
                stream = true,
            };
            using var request = _context.GenerateRequest(api, HttpMethod.Post, JsonContent.Create(requestBody),
                parameters: parameters);
            using var response = await _context.HttpClient!.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
            response.EnsureSuccessStatusCode();
            await foreach (var item in ProcessStreamResponce(response, cancellationToken))
            {
                yield return item;
            }
        }

        /// <summary>
        /// 调用此接口取消进行中的对话。
        /// </summary>
        /// <param name="conversationId"></param>
        /// <param name="chatId"></param>
        /// <returns></returns>
        public async Task<CozeResult<ChatObject>?> CancleChat(string conversationId, string chatId)
        {
            var api = "/v3/chat/cancel";
            var body = new
            {
                conversation_id = conversationId,
                chat_id = chatId
            };
            return await _context.GetJsonAsync<CozeResult<ChatObject>>(api, HttpMethod.Get, JsonContent.Create(body));
        }
    }
}
