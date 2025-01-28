using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozeNet.Chat.Models
{
    public class StreamMessage
    {
        public StreamEvents Event { get; set; }
        /// <summary>
        /// Chat事件为ChatObject，Message事件为MessageObject
        /// </summary>
        public object? Data { get; set; }
    }
}
