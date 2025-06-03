using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CozeNet.Chat.Models
{
    public class ToolCall
    {
        /// <summary>
        /// 上报运行结果的 ID。
        /// </summary>
        [JsonPropertyName("id")]
        public string? ID { get; set; }

        /// <summary>
        /// 工具类型，枚举值为 function。
        /// </summary>
        [JsonPropertyName("type")]
        public string? Type { get; set; }

        /// <summary>
        /// 执行方法 function 的定义。
        /// </summary>
        [JsonPropertyName("function")]
        public ToolCallFunction? Function { get; set; }
    }
}
