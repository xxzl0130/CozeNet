using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CozeNet.Workflow.Models
{
    public class WorkflowExecuteHistory
    {
        /// <summary>
        /// 执行 ID。
        /// </summary>
        [JsonPropertyName("execute_id")]
        public string? ExecuteID { get; set; }

        /// <summary>
        /// 执行状态。
        /// Success：执行成功。
        /// Running：执行中。
        /// Fail：执行失败。
        /// </summary>
        [JsonPropertyName("execute_status")]
        public string? ExecuteStatus { get; set; }

        /// <summary>
        /// 执行工作流时指定的 Bot ID。返回 0 表示未指定智能体 ID。
        /// </summary>
        [JsonPropertyName("bot_id")]
        public string? BotID { get; set; }

        /// <summary>
        /// 智能体的发布渠道 ID，默认仅显示 Agent as API 渠道，渠道 ID 为 1024。
        /// </summary>
        [JsonPropertyName("connector_id")]
        public string? ConnectorID { get; set; }

        /// <summary>
        /// 用户 ID，执行工作流时通过 ext 字段指定的 user_id。如果未指定，则返回 Token 申请人的扣子 ID
        /// </summary>
        [JsonPropertyName("connector_uid")]
        public string? ConnectorUID { get; set; }

        public enum ERunMode
        {
            Sync = 0,
            Stream = 1,
            Async = 2,
        };

        /// <summary>
        /// 工作流的运行方式：
        /// </summary>
        [JsonPropertyName("run_mode")]
        public ERunMode RunMode { get; set; }

        /// <summary>
        /// 工作流异步运行的 Log ID。如果工作流执行异常，可以联系服务团队通过 Log ID 排查问题。
        /// </summary>
        [JsonPropertyName("logid")]
        public string? LogID { get; set; }

        /// <summary>
        /// 工作流运行开始时间，Unixtime 时间戳格式，单位为秒。
        /// </summary>
        [JsonPropertyName("create_time")]
        public long CreateTime { get; set; }

        /// <summary>
        /// 工作流的恢复运行时间，Unixtime 时间戳格式，单位为秒。
        /// </summary>
        [JsonPropertyName("update_time")]
        public long UpdateTime { get; set; }

        /// <summary>
        /// 工作流的输出，通常为 JSON 序列化字符串，也有可能是非 JSON 结构的字符串。
        /// </summary>
        [JsonPropertyName("output")]
        public string? Output { get; set; }

        /// <summary>
        /// 预留字段，无需关注。
        /// </summary>
        [JsonPropertyName("token")]
        public string? Token { get; set; }

        /// <summary>
        /// 预留字段，无需关注。
        /// </summary>
        [JsonPropertyName("cost")]
        public string? Cost { get; set; }

        /// <summary>
        /// 调用状态码。 
        /// </summary>
        [JsonPropertyName("error_code")]
        public string? ErrorCode { get; set; }

        /// <summary>
        /// 状态信息。为 API 调用失败时可通过此字段查看详细错误信息。
        /// </summary>
        [JsonPropertyName("error_message")]
        public string? ErrorMessage { get; set; }

        /// <summary>
        /// 工作流试运行调试页面。访问此页面可查看每个工作流节点的运行结果、输入输出等信息。
        /// </summary>
        [JsonPropertyName("debug_url")]
        public string? DebugUrl { get; set; }
    }
}
