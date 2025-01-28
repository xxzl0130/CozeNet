using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CozeNet.Chat.Models
{
    public class ToolOutput
    {
        /// <summary>
        /// 上报运行结果的 ID。你可以在发起对话（V3）接口响应的 tool_calls 字段下查看此 ID。
        /// </summary>
        [JsonPropertyName("tool_call_id")]
        public string? ToolCallID {  get; set; }

        /// <summary>
        /// 工具的执行结果。
        /// </summary>
        [JsonPropertyName("output")]
        public string? Output {  get; set; }
    }
}
