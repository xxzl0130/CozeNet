using System.Net.Http.Json;
using CozeNet.Core;
using CozeNet.Core.Models;
using CozeNet.Message.Models;

namespace CozeNet.Message
{
    public class MessageService(Context context)
    {
        /// <summary>
        /// 创建消息
        /// </summary>
        /// <param name="conversationId">会话ID</param>
        /// <param name="createRequest"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<CozeResult<MessageObject>?> CreateAsync(string conversationId, MessageCreateRequest createRequest, CancellationToken cancellationToken = default)
        {
            const string api = "/v1/conversation/message/create";
            return await context.GetJsonAsync<CozeResult<MessageObject>>(api, HttpMethod.Post, JsonContent.Create(createRequest, options: context.JsonOptions),
                parameters: new Dictionary<string, string> { { "conversation_id", conversationId } }, cancellationToken: cancellationToken);
        }

        /// <summary>
        /// 查看消息列表
        /// </summary>
        /// <param name="conversationId"></param>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<MessageListResponse?> ListAsync(string conversationId, MessageListRequest request, CancellationToken cancellationToken = default)
        {
            const string api = "/v1/conversation/message/list";
            return await context.GetJsonAsync<MessageListResponse>(api, HttpMethod.Post,
                parameters: new Dictionary<string, string> { { "conversation_id", conversationId } }, cancellationToken: cancellationToken);
        }

        /// <summary>
        /// 查看消息详情
        /// </summary>
        /// <param name="conversationId"></param>
        /// <param name="messageId"></param>
        /// <returns></returns>
        public async Task<CozeResult<MessageObject>?> RetrieveAsync(string conversationId, string messageId, CancellationToken cancellationToken = default)
        {
            const string api = "/v1/conversation/message/retrieve";
            return await context.GetJsonAsync<CozeResult<MessageObject>>(api, HttpMethod.Get,
                parameters: new Dictionary<string, string> { { "conversation_id", conversationId }, { "message_id", messageId } }, cancellationToken: cancellationToken);
        }

        /// <summary>
        /// 修改一条消息，支持修改消息内容、附加内容和消息类型
        /// </summary>
        /// <param name="conversationId"></param>
        /// <param name="messageId"></param>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<MessageModifyResponse?> ModifyAsync(string conversationId, string messageId, MessageModifyRequest request, CancellationToken cancellationToken = default)
        {
            const string api = "/v1/conversation/message/modify";
            return await context.GetJsonAsync<MessageModifyResponse>(api, HttpMethod.Post, JsonContent.Create(request, options: context.JsonOptions),
                parameters: new Dictionary<string, string> { { "conversation_id", conversationId }, { "message_id", messageId } }, cancellationToken: cancellationToken);
        }

        /// <summary>
        /// 调用接口在指定会话中删除消息。
        /// 暂不支持批量操作，需要逐条删除。
        /// 无法手动选择仅删除某一条中间态消息。当删除会话中生成的回复时，系统会自动删除与该回复相关的所有中间状态消息。中间态消息指除 type = answer 之外，通过发起对话接口生成的其他类型消息，如 type 为 verbose、function_call 等；会话中自动生成的回复指调用发起对话接口生成的 type = answer 类型消息。
        /// 删除消息后，无法通过查看消息列表或查看对话消息详情接口查看已删除的这些消息。
        /// </summary>
        /// <param name="conversationId"></param>
        /// <param name="messageId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<CozeResult<MessageObject>?> DeleteAsync(string conversationId, string messageId, CancellationToken cancellationToken = default)
        {
            const string api = "/v1/conversation/message/delete";
            return await context.GetJsonAsync<CozeResult<MessageObject>>(api, HttpMethod.Post,
                parameters: new Dictionary<string, string> { { "conversation_id", conversationId }, { "message_id", messageId } }, cancellationToken: cancellationToken);
        }
    }
}
