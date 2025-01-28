using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CozeNet.Workflow.Models
{
    public class MessageEvent
    {
        /// <summary>
        /// 流式输出的消息内容。
        /// </summary>
        [JsonPropertyName("content")]
        public string? Content { get; set; }

        /// <summary>
        /// 输出消息的节点名称，例如消息节点、结束节点。
        /// </summary>
        [JsonPropertyName("node_title")]
        public string? NodeTitle {  get; set; }

        /// <summary>
        /// 此消息在节点中的消息 ID，从 0 开始计数，例如消息节点的第 5 条消息。
        /// </summary>
        [JsonPropertyName("node_seq_id")]
        public string? NodeSeqID { get; set; }

        /// <summary>
        /// node_is_finish
        /// </summary>
        [JsonPropertyName("node_is_finish")]
        public bool NodeIsFinish {  get; set; }

        /// <summary>
        /// 额外字段。
        /// </summary>
        [JsonPropertyName("ext")]
        public Dictionary<string, string>? Extra {  get; set; }

        /// <summary>
        /// 预留字段，无需关注。
        /// </summary>
        [JsonPropertyName("cost")]
        public string? Cost { get; set; }
    }
}
