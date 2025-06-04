using System.Net.Http.Json;
using CozeNet.Conversation.Models;
using CozeNet.Core;
using CozeNet.Core.Models;

namespace CozeNet.Conversation
{
    /// <summary>
    /// 会话服务
    /// </summary>
    /// <param name="context"></param>
    public class ConversationService(Context context)
    {
        /// <summary>
        /// 创建一个会话
        /// <para><see href="https://www.coze.cn/docs/developer_guides/create_conversation#48f17c5f">创建会话文档</see></para>
        /// </summary>
        /// <param name="enterMessage">会话中的消息内容</param>
        /// <param name="metaData">创建消息时的附加消息，获取消息时也会返回此附加消息</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns></returns>
        public async Task<CozeResult<ConversationObject>?> CreateAsync(EnterMessageObject[]? enterMessage = null, object? metaData = null, CancellationToken cancellationToken = default)
        {
            const string api = "/v1/conversation/create";
            return await context.GetJsonAsync<CozeResult<ConversationObject>>(api, HttpMethod.Post, JsonContent.Create(new CreateConversationObject(enterMessage, metaData), options: context.JsonOptions), cancellationToken: cancellationToken);
        }

        /// <summary>
        /// 获取会话信息
        /// </summary>
        /// <param name="conversationId">会话id</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns></returns>
        public async Task<CozeResult<ConversationObject>?> GetInfoAsync(string conversationId, CancellationToken cancellationToken = default)
        {
            return await context.GetJsonAsync<CozeResult<ConversationObject>>($"/v1/conversation/retrieve?conversation_id={conversationId}", HttpMethod.Get, cancellationToken: cancellationToken);
        }
    }
}
