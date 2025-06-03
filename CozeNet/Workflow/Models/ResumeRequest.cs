using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CozeNet.Workflow.Models
{
    public class ResumeRequest
    {
        /// <summary>
        /// 待执行的 Workflow ID，此工作流应已发布。
        /// </summary>
        [JsonPropertyName("workflow_id")]
        public string? WorkflowID { get; set; }

        /// <summary>
        /// 工作流执行中断事件 ID。你可以从执行工作流（流式响应）的响应信息中获得中断事件 ID。
        /// </summary>
        [JsonPropertyName("event_id")]
        public string? EventID { get; set; }

        /// <summary>
        /// 恢复执行时，用户对智能体指定问题的回复。回复中应包含问答节点中的必选参数，否则工作流会再次中断并提问。
        /// </summary>
        [JsonPropertyName("resume_data")]
        public string? ResumeData { get; set; }

        /// <summary>
        /// 中断类型，你可以从执行工作流（流式响应）的响应信息中获得中断时间的中断类型。
        /// </summary>
        [JsonPropertyName("interrupt_type")]
        public int InterruptType { get; set; }
    }
}
