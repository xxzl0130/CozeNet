using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozeNet.Workflow.Models
{
    public static class StreamEventStrings
    {
        public const string Message = "Message";

        public const string Error = "Error";

        public const string Done = "Done";

        public const string Interrupt = "Interrupt";

        public static StreamEvents ToWorkflowStreamEvent(this string? str)
        {
            if (string.IsNullOrEmpty(str))
                return StreamEvents.None;
            switch (str)
            {
                case Message:
                    return StreamEvents.Message;
                case Error:
                    return StreamEvents.Error;
                case Done:
                    return StreamEvents.Done;
                case Interrupt: 
                    return StreamEvents.Interrupt;
                default:
                    return StreamEvents.None;
            }
        }
    }

    public enum StreamEvents
    {
        None = 0,
        Message,
        Error,
        Done,
        Interrupt
    }
}
