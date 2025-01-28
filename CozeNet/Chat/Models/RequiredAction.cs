using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CozeNet.Chat.Models
{
    public class RequiredAction
    {
        /// <summary>
        /// 额外操作的类型，枚举值为 submit_tool_outputs。
        /// </summary>
        [JsonPropertyName("type")]
        public string? Type { get; set; }

        /// <summary>
        /// 需要提交的结果详情，通过提交接口上传，并可以继续聊天
        /// </summary>
        [JsonPropertyName("submit_tool_outputs")]
        public SubmitToolOutputs? SubmitToolOutputs { get; set; }
    }
}
