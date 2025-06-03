using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using CozeNet.Core.Models;

namespace CozeNet.Knowledge.Models
{
    public class DocumentProgressResponseData
    {
        /// <summary>
        /// 文件的上传进度详情。
        /// </summary>
        [JsonPropertyName("data")]
        public DocumentProgress[]? Data { get; set; }
    }

    public class DocumentProgressResponse : CozeResult<DocumentProgressResponseData>
    {
    }
}
