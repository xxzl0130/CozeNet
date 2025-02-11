
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using CozeNet.Core.Models;

namespace CozeNet.Workflow.Models
{
    public class RunResponse : CozeResult<string>
    {
        /// <summary>
        /// 工作流试运行调试页面。访问此页面可查看每个工作流节点的运行结果、输入输出等信息。
        /// </summary>
        [JsonPropertyName("debug_url")]
        public string? DebugUrl {  get; set; }

        /// <summary>
        /// 异步执行的执行 ID。仅在异步执行工作流（is_async=true）时返回。
        /// 可通过 execute_id 调用查询工作流异步执行结果API 获取工作流的最终执行结果。
        /// </summary>
        [JsonPropertyName("execute_id")]
        public string? ExecutionID {  get; set; }

        /// <summary>
        /// 预留字段，无需关注。
        /// </summary>
        [JsonPropertyName("token")]
        public int Token {  get; set; }

        /// <summary>
        /// 预留字段，无需关注。
        /// </summary>
        [JsonPropertyName("cost")]
        public string? Cost { get; set; }
    }
}
