using System.Text.Json.Serialization;
using CozeNet.Core.Models;

namespace CozeNet.Chat.Models
{
    public class ChatObject
    {
        /// <summary>
        /// 对话 ID，即对话的唯一标识。
        /// </summary>
        [JsonPropertyName("id")]
        public string? ID { get; set; }

        /// <summary>
        /// 会话 ID，即会话的唯一标识。
        /// </summary>
        [JsonPropertyName("conversation_id")]
        public string? ConversationID { get; set; }

        /// <summary>
        /// 要进行会话聊天的 Bot ID。
        /// </summary>
        [JsonPropertyName("bot_id")]
        public string? BotID { get; set; }

        /// <summary>
        /// 消息的内容，支持纯文本、多模态（文本、图片、文件混合输入）、卡片等多种类型的内容。
        /// 只在流式响应时有。
        /// </summary>
        [JsonPropertyName("content")]
        public string? Content { get; set; }

        /// <summary>
        /// 消息内容的类型，取值包括：text：文本。object_string：多模态内容，即文本和文件的组合、文本和图片的组合。card：卡片。此枚举值仅在接口响应中出现，不支持作为入参。
        /// 只在流式响应时有。
        /// </summary>
        [JsonPropertyName("content_type")]
        public string? ContentType { get; set; }

        /// <summary>
        /// 对话创建的时间。格式为 10 位的 Unixtime 时间戳，单位为秒。
        /// </summary>
        [JsonPropertyName("created_at")]
        public long CreatedAt { get; set; }

        /// <summary>
        /// 对话结束的时间。格式为 10 位的 Unixtime 时间戳，单位为秒。
        /// </summary>
        [JsonPropertyName("completed_at")]
        public long CompletedAt { get; set; }

        /// <summary>
        /// 对话失败的时间。格式为 10 位的 Unixtime 时间戳，单位为秒。
        /// </summary>
        [JsonPropertyName("failed_at")]
        public long FailedAt { get; set; }

        /// <summary>
        /// 创建消息时的附加消息，用于传入使用方的自定义数据，获取消息时也会返回此附加消息。自定义键值对，应指定为 Map 对象格式。长度为 16 对键值对，其中键（key）的长度范围为 1～64 个字符，值（value）的长度范围为 1～512 个字符。
        /// </summary>
        [JsonPropertyName("meta_data")]
        public object? MetaData { get; set; }

        /// <summary>
        /// 对话运行异常时，此字段中返回详细的错误信息，包括：Code：错误码。Integer 类型。0 表示成功，其他值表示失败。Msg：错误信息。String 类型。对话正常运行时，此字段返回 null。suggestion 失败不会被标记为运行异常，不计入 last_error。
        /// </summary>
        [JsonPropertyName("last_error")]
        public CozeResult? LastError { get; set; }

        /// <summary>
        /// 会话的运行状态。取值为：created：会话已创建。in_progress：Bot 正在处理中。completed：Bot 已完成处理，本次会话结束。failed：会话失败。requires_action：会话中断，需要进一步处理。
        /// </summary>
        [JsonPropertyName("status")]
        public string? Status { get; set; }

        /// <summary>
        /// 需要运行的信息详情。
        /// </summary>
        [JsonPropertyName("required_action")]
        public RequiredAction? RequiredAction { get; set; }

        /// <summary>
        /// Token 消耗的详细信息。实际的 Token 消耗以对话结束后返回的值为准。
        /// </summary>
        [JsonPropertyName("usage")]
        public Usage? Usage { get; set; }
    }
}
