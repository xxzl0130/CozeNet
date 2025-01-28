using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CozeNet.Workflow.Models
{
    public class ErrorEvent
    {
        /// <summary>
        /// 调用状态码。 
        /// </summary>
        [JsonPropertyName("error_code")]
        public int Code { get; set; }

        /// <summary>
        /// 状态信息。API 调用失败时可通过此字段查看详细错误信息。
        /// </summary>
        [JsonPropertyName("error_message")]
        public string? Message { get; set; }
    }
}
