using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozeNet.Chat.Models
{
    /// <summary>
    /// 流式响应事件
    /// </summary>
    public static class StreamEventStrings
    {
        /// <summary>
        /// 创建对话的事件，表示对话开始。
        /// </summary>
        public const string Created = "conversation.chat.created";

        /// <summary>
        /// 服务端正在处理对话。
        /// </summary>
        public const string InProgress = "conversation.chat.in_progress";

        /// <summary>
        /// 增量消息，通常是 type=answer 时的增量消息。
        /// </summary>
        public const string DeltaMessage = "conversation.message.delta";

        /// <summary>
        /// 增量语音消息，通常是 type=answer 时的增量消息。
        /// </summary>
        public const string DeltaAudio = "conversation.audio.delta";

        /// <summary>
        /// message 已回复完成。此时流式包中带有所有 message.delta 的拼接结果，且每个消息均为 completed 状态。
        /// </summary>
        public const string MessageComplete = "conversation.message.completed";

        /// <summary>
        /// 对话完成。
        /// </summary>
        public const string ChatComplete = "conversation.chat.completed";

        /// <summary>
        /// 此事件用于标识对话失败。
        /// </summary>
        public const string ChatFailed = "conversation.chat.failed";

        /// <summary>
        /// 对话中断，需要使用方上报工具的执行结果。
        /// </summary>
        public const string RequireAction = "conversation.chat.requires_action";

        /// <summary>
        /// 流式响应过程中的错误事件。关于 code 和 msg 的详细说明，可参考错误码。
        /// </summary>
        public const string Error = "error";

        /// <summary>
        /// 本次会话的流式返回正常结束。
        /// </summary>
        public const string Done = "done";

        public static StreamEvents ToChatStreamEvents(this string? str)
        {
            switch (str)
            {
                case Created:
                    return StreamEvents.Created;
                case InProgress:
                    return StreamEvents.InProgress;
                case DeltaMessage:
                    return StreamEvents.DeltaMessage;
                case DeltaAudio:
                    return StreamEvents.DeltaAudio;
                case MessageComplete:
                    return StreamEvents.MessageComplete;
                case ChatFailed:
                    return StreamEvents.ChatFailed;
                case ChatComplete:
                    return StreamEvents.ChatComplete;
                case RequireAction:
                    return StreamEvents.RequireAction;
                case Error:
                    return StreamEvents.Error;
                case Done:
                    return StreamEvents.Done;
                default:
                    return StreamEvents.None;
            }
        }
    }

    public enum StreamEvents
    {
        None = 0,
        Created,
        InProgress,
        DeltaMessage,
        DeltaAudio,
        MessageComplete,
        ChatComplete,
        ChatFailed,
        RequireAction,
        Error,
        Done
    }
}
