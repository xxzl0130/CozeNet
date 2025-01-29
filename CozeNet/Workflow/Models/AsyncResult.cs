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
    }
}
