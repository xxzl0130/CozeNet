using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using CozeNet.Core;
using CozeNet.Core.Models;
using CozeNet.Message.Models;
using CozeNet.Utils;

namespace CozeNet.Message
{
    public class MessageService
    {
        private Context _context;

        public MessageService(Context context)
        {
            _context = context;
        }

        /// <summary>
        /// 创建消息
        /// </summary>
        /// <param name="conversationId">会话ID</param>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<CozeResult<MessageObject>?> CreateAsync(string conversationId, MessageCreateRequest createRequest)
        {
            var api = "/v1/conversation/message/create";
            var request = _context.GenerateRequest(api, HttpMethod.Post, JsonContent.Create(createRequest),
                parameters: new Dictionary<string, string> { { "conversation_id", conversationId } });
            var response = _context.HttpClient!.Send(request);
            return await response.GetJsonObjectAsync<CozeResult<MessageObject>>();
        }

        /// <summary>
        /// 查看消息列表
        /// </summary>
        /// <param name="conversationId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<MessageListResponse?> ListAsync(string conversationId, MessageListRequest request)
        {
            var api = "/v1/conversation/message/list";
            var response = await _context.SendRequestAsync(api, HttpMethod.Post, JsonContent.Create(request),
                parameters: new Dictionary<string, string> { { "conversation_id", conversationId } });
            return await response.GetJsonObjectAsync<MessageListResponse>();
        }

        /// <summary>
        /// 查看消息详情
        /// </summary>
        /// <param name="conversationId"></param>
        /// <param name="messageId"></param>
        /// <returns></returns>
        public async Task<CozeResult<MessageObject>?> RetrieveAsync(string conversationId, string messageId)
        {
            var api = "/v1/conversation/message/retrieve";
            var response = await _context.SendRequestAsync(api, HttpMethod.Get,
                parameters: new Dictionary<string, string> { { "conversation_id", conversationId }, { "message_id", messageId } });
            return await response.GetJsonObjectAsync<CozeResult<MessageObject>>();
        }

        /// <summary>
        /// 修改一条消息，支持修改消息内容、附加内容和消息类型
        /// </summary>
        /// <param name="conversationId"></param>
        /// <param name="messageId"></param>
        /// <returns></returns>
        public async Task<MessageModifyResponse?> ModifyAsync(string conversationId, string messageId, MessageModifyRequest request)
        {
            var api = "/v1/conversation/message/modify";
            var response = await _context.SendRequestAsync(api, HttpMethod.Post,
                parameters: new Dictionary<string, string> { { "conversation_id", conversationId }, { "message_id", messageId } });
            return await response.GetJsonObjectAsync<MessageModifyResponse>();
        }

        /// <summary>
        /// 调用接口在指定会话中删除消息。
        /// 暂不支持批量操作，需要逐条删除。
        /// 无法手动选择仅删除某一条中间态消息。当删除会话中生成的回复时，系统会自动删除与该回复相关的所有中间状态消息。中间态消息指除 type = answer 之外，通过发起对话接口生成的其他类型消息，如 type 为 verbose、function_call 等；会话中自动生成的回复指调用发起对话接口生成的 type = answer 类型消息。
        /// 删除消息后，无法通过查看消息列表或查看对话消息详情接口查看已删除的这些消息。
        /// </summary>
        /// <param name="conversationId"></param>
        /// <param name="messageId"></param>
        /// <returns></returns>
        public async Task<CozeResult<MessageObject>?> DeleteAsync(string conversationId, string messageId)
        {
            var api = "/v1/conversation/message/delete";
            var response = await _context.SendRequestAsync(api, HttpMethod.Post,
                parameters: new Dictionary<string, string> { { "conversation_id", conversationId }, { "message_id", messageId } });
            return await response.GetJsonObjectAsync<CozeResult<MessageObject>>();
        }
    }
}
