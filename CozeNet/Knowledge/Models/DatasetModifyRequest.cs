using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CozeNet.Knowledge.Models
{
    public class DatasetModifyRequest
    {
        /// <summary>
        /// 知识库名称，长度不超过 100 个字符。
        /// </summary>
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        /// <summary>
        /// 知识库图标，应传入上传文件接口中获取的 file_id。
        /// </summary>
        [JsonPropertyName("file_id")]
        public string? FileID { get; set; }

        /// <summary>
        /// 知识库描述信息。
        /// </summary>
        [JsonPropertyName("description")]
        public string? Description { get; set; }
    }
}
