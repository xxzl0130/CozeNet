using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CozeNet.Conversation.Models
{
    /// <summary>
    /// https://www.coze.cn/open/docs/developer_guides/create_conversation#48f17c5f
    /// </summary>
    public class ConversationObject
    {
        /// <summary>
        /// Conversation ID，即会话的唯一标识。
        /// </summary>
        [JsonPropertyName("id")]
        public string ID { get; set; }

        /// <summary>
        /// 会话创建的时间。格式为 10 位的 Unixtime 时间戳，单位为秒。
        /// </summary>
        [JsonPropertyName("created_at")]
        public int CreatedAt { get; set; }

        /// <summary>
        /// 创建消息时的附加消息，获取消息时也会返回此附加消息。自定义键值对，应指定为 Map 对象格式。长度为 16 对键值对，其中键（key）的长度范围为 1～64 个字符，值（value）的长度范围为 1～512 个字符。
        /// </summary>
        [JsonPropertyName("meta_data")]
        public object? MetaData { get; set; }
    }
}
