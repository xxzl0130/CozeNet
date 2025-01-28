using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozeNet.Message.Models
{
    public static class MessageType
    {
        /// <summary>
        /// 用户输入内容。
        /// </summary>
        public const string Question = "question";
        /// <summary>
        /// 智能体返回给用户的消息内容，支持增量返回。如果工作流绑定了 messge 节点，可能会存在多 answer 场景，此时可以用流式返回的结束标志来判断所有 answer 完成。
        /// </summary>
        public const string Answer = "answer";
        /// <summary>
        /// 智能体对话过程中调用函数（function call）的中间结果。
        /// </summary>
        public const string FunctionCall = "function_call";
        /// <summary>
        /// 调用工具 （function call）后返回的结果。
        /// </summary>
        public const string ToolOutput = "tool_output";
        /// <summary>
        /// 调用工具 （function call）后返回的结果。
        /// </summary>
        public const string ToolResponse = "tool_response";
        /// <summary>
        /// 如果在智能体上配置打开了用户问题建议开关，则会返回推荐问题相关的回复内容。
        /// </summary>
        public const string FollowUp = "follow_up";
        /// <summary>
        /// 多 answer 场景下，服务端会返回一个 verbose 包，对应的 content 为 JSON 格式，content.msg_type =generate_answer_finish 代表全部 answer 回复完成。
        /// </summary>
        public const string Verbose = "verbose";
    }
}
