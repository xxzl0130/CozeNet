using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CozeNet.Message.Models
{
    public class MessageModifyResponse
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
        /// 修改后的消息详细信息。详细说明可参考 Message Object。
        /// </summary>
        [JsonPropertyName("message")]
        public MessageObject? MessageObject { get; set; }
    }
}
