using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using CozeNet.Chat.Models;
using CozeNet.Conversation.Models;
using CozeNet.Core;
using CozeNet.Core.Authorization;
using CozeNet.Core.Models;
using CozeNet.Utils;

namespace CozeNet.Conversation
{
    public class ConversationService
    {
        private Context _context;

        public ConversationService(Context context) {
            _context = context;
        }

        /// <summary>
        /// 创建一个会话。
        /// <see cref="https://www.coze.cn/docs/developer_guides/create_conversation#48f17c5f"/>
        /// </summary>
        /// <param name="enterMessage">会话中的消息内容</param>
        /// <param name="metaData">创建消息时的附加消息，获取消息时也会返回此附加消息</param>
        /// <returns></returns>
        public async Task<CozeResult<ConversationObject>?> CreateAsync(EnterMessageObject[]? enterMessage = null, object? metaData = null)
        {
            var body = new
            {
                messages = enterMessage,
                meta_data = metaData
            };
            var api = "/v1/conversation/create";
            return await _context.GetJsonAsync<CozeResult<ConversationObject>>(api, HttpMethod.Post, JsonContent.Create(body));
        }

        public async Task<CozeResult<ConversationObject>?> GetInfoAsync(string conversationId)
        {
            return await _context.GetJsonAsync<CozeResult<ConversationObject>>($"/v1/conversation/retrieve?conversation_id={conversationId}", HttpMethod.Get);
        }
    }
}
