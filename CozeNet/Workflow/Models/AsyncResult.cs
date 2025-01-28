using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using CozeNet.Core.Models;

namespace CozeNet.Workflow.Models
{
    public class AsyncResult : CozeResult<WorkflowExecuteHistory[]>
    {
        public class DetailObject
        {
            /// <summary>
            /// 本次请求的 Log ID。如果工作流执行异常，可以联系服务团队通过 Log ID 排查问题。
            /// </summary>
            [JsonPropertyName("logid")]
            public string? LogID { get; set; }
        }

        /// <summary>
        /// 本次请求的执行详情。
        /// </summary>
        [JsonPropertyName("detail")]
        public DetailObject Detail { get; set; }
    }
}
