using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using CozeNet.Core.Models;

namespace CozeNet.Knowledge.Models
{
    public class DocumentListResponse : CozeResult<DocumentInfo[]>
    {
        /// <summary>
        /// 指定知识库中的文件总数。
        /// </summary>
        [JsonPropertyName("total")]
        public int Total { get; set; }
    }
}
