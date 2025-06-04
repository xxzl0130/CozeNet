using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using CozeNet.Message.Models;

namespace CozeNet.Workflow.Models
{
    public class ChatFlowRequest
    {
        /// <summary>
        /// 待执行的对话流 ID，此对话流应已发布。
        /// </summary>
        [JsonPropertyName("workflow_id")]
        public string? WorkflowID { get; set; }

        /// <summary>
        /// 对话中用户问题和历史消息。数组长度限制为 50，即最多传入 50 条消息
        /// </summary>
        [JsonPropertyName("additional_messages")]
        public List<BaseMessageObject>? AdditionalMessages { get; set; }

        /// <summary>
        /// 设置对话流的输入参数。
        /// </summary>
        [JsonPropertyName("parameters")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Dictionary<string, string>? Parameters { get; set; }

        /// <summary>
        /// 需要关联的扣子应用 ID
        /// </summary>
        [JsonPropertyName("app_id")]
        public string? AppID { get; set; }

        /// <summary>
        /// 需要关联的智能体 ID。
        /// </summary>
        [JsonPropertyName("bot_id")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? BotID { get; set; }

        /// <summary>
        /// 对话流对应的会话 ID，对话流产生的消息会保存到此对话中
        /// </summary>
        [JsonPropertyName("conversation_id")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? ConversationId { get; set; }

        /// <summary>
        /// 用于指定一些额外的字段，以 Map[String][String] 格式传入。例如某些插件会隐式用到的经纬度等字段。
        /// </summary>
        [JsonPropertyName("ext")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Dictionary<string, string>? Extra { get; set; }
    }
}
