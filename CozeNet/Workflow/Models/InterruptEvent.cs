using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CozeNet.Workflow.Models
{
    public class InterruptData
    {
        /// <summary>
        /// 工作流中断事件 ID，恢复运行时应回传此字段。
        /// </summary>
        [JsonPropertyName("event_id")]
        public string? ID { get; set; }

        /// <summary>
        /// 工作流中断类型，恢复运行时应回传此字段。
        /// </summary>
        [JsonPropertyName("type")]
        public int Type { get; set; }
    }

    public class InterruptEvent
    {
        /// <summary>
        /// 输出消息的节点名称，例如消息节点、结束节点。
        /// </summary>
        [JsonPropertyName("node_title")]
        public string? NodeTitle { get; set; }

        /// <summary>
        /// 中断控制内容。
        /// </summary>
        [JsonPropertyName("interrupt_data")]
        public InterruptData? Data { get; set; }
    }
}
