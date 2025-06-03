using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozeNet.Workflow.Models
{
    public class StreamMessage
    {
        /// <summary>
        /// 此消息在接口响应中的事件 ID。以 0 为开始。
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 当前流式返回的数据包事件。
        /// </summary>
        public StreamEvents Event { get; set; }

        /// <summary>
        /// 事件内容。各个 event 类型的事件内容格式不同。
        /// </summary>
        public object? Data { get; set; }
    }
}
