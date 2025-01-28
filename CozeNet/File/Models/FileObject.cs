using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CozeNet.File.Models
{
    public class FileObject
    {
        /// <summary>
        /// 已上传的文件 ID。
        /// </summary>
        [JsonPropertyName("id")]
        public string? ID { get; set; }

        /// <summary>
        /// 文件的总字节数。
        /// </summary>
        [JsonPropertyName("bytes")]
        public int Bytes { get; set; }

        /// <summary>
        /// 文件的上传时间，格式为 10 位的 Unixtime 时间戳，单位为秒（s）。
        /// </summary>
        [JsonPropertyName("created_at")]
        public int CreatedAt { get; set; }

        /// <summary>
        /// 文件名称。
        /// </summary>
        [JsonPropertyName("file_name")]
        public string? FileName { get; set; }
    }
}
