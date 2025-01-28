using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CozeNet.Workflow.Models
{
    public class RunRequest
    {
        /// <summary>
        /// 待执行的 Workflow ID，此工作流应已发布。
        /// 进入 Workflow 编排页面，在页面 URL 中，workflow 参数后的数字就是 Workflow ID。例如 https://www.coze.com/work_flow?space_id=42463***&workflow_id=73505836754923***，Workflow ID 为 73505836754923***
        /// </summary>
        [JsonPropertyName("workflow_id")]
        public string? WorkflowID {  get; set; }

        /// <summary>
        /// 工作流开始节点的输入参数及取值，你可以在指定工作流的编排页面查看参数列表。
        /// </summary>
        [JsonPropertyName("parameters")]
        public Dictionary<string, object>? Parameters { get; set; }

        /// <summary>
        /// 需要关联的智能体ID。 部分工作流执行时需要指定关联的 Bot，例如存在数据库节点、变量节点等节点的工作流。
        /// </summary>
        [JsonPropertyName("bot_id")]
        public string? BotID {  get; set; }

        /// <summary>
        /// 用于指定一些额外的字段
        /// 目前仅支持以下字段：
        /// latitude：String 类型，表示经度。
        /// longitude：String 类型，表示纬度。
        /// user_id：String 类型，表示用户 ID。
        /// </summary>
        [JsonPropertyName("ext")]
        public Dictionary<string, string>? Extra {  get; set; }

        /// <summary>
        /// 是否异步运行。异步运行后可通过本接口返回的 execute_id 调用查询工作流异步执行结果API 获取工作流的最终执行结果。
        /// true：异步运行。
        /// false：（默认）同步运行。
        /// </summary>
        [JsonPropertyName("is_async")]
        public bool IsAsync { get; set; } = false;

        /// <summary>
        /// 工作流所在的应用 ID。
        /// 仅运行扣子应用中的工作流时，才需要设置 app_id。智能体绑定的工作流、空间资源库中的工作流无需设置 app_id。
        /// </summary>
        [JsonPropertyName("app_id")]
        public string? AppID { get; set; }
    }
}
