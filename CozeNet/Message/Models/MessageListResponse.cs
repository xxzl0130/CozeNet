using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CozeNet.Message.Models
{
    public class MessageListResponse
    {
        /// <summary>
        /// 状态码。0 代表调用成功。
        /// </summary>
        [JsonPropertyName("code")]
        public int Code { get; set; }

        /// <summary>
        /// 状态信息。API 调用失败时可通过此字段查看详细错误信息。
        /// </summary>
        [JsonPropertyName("msg")]
        public string? Message { get; set; }

        /// <summary>
        /// 消息详情。详细说明可参考 Message Object。
        /// </summary>
        [JsonPropertyName("data")]
        public MessageObject[] Data { get; set; }

        /// <summary>
        /// 返回的消息列表中，第一条消息的 Message ID。
        /// </summary>
        [JsonPropertyName("first_id")]
        public string? FirstID { get; set; }

        /// <summary>
        /// 返回的消息列表中，最后一条消息的 Message ID。
        /// </summary>
        [JsonPropertyName("last_id")]
        public string? LastID { get; set; }

        /// <summary>
        /// 是否已返回全部消息。true：已返回全部消息。false：未返回全部消息，可再次调用此接口查看其他分页。
        /// </summary>
        [JsonPropertyName("has_more")]
        public bool HasMore { get; set; }
    }
}
