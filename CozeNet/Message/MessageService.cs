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
    }
}
