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
        public ChatObject? ChatObject { get; set; }
    }
}
