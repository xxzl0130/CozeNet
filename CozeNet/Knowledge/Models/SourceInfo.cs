using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CozeNet.Knowledge.Models
{
    public class SourceInfo
    {
        /// <summary>
        /// 本地文件的 Base64 编码。
        /// </summary>
        [JsonPropertyName("file_base64")]
        public string? FileBase64 { get; set; }

        /// <summary>
        /// 本地文件格式，即文件后缀，例如 txt。格式支持 pdf、txt、doc、docx 类型。
        /// 上传的文件类型应与知识库类型匹配，例如 txt 文件只能上传到文档类型的知识库中。
        /// 上传本地文件时必选。
        /// </summary>
        [JsonPropertyName("file_type")]
        public string? FileType { get; set; }

        /// <summary>
        /// 网页的 URL 地址。
        /// 上传在线网页时必选。
        /// </summary>
        [JsonPropertyName("web_url")]
        public string? WebUrl { get; set; }

        /// <summary>
        /// 文件的上传方式。
        /// 支持设置为:
        /// 1，表示上传在线网页。
        /// 上传本地文件时必选。
        /// </summary>
        [JsonPropertyName("document_source")]
        public int DocumentSource { get; set; }

        /// <summary>
        /// 通过上传文件接口获取的文件 ID。
        /// </summary>
        [JsonPropertyName("source_file_id")]
        public long SourceFileID {  get; set; }
    }
}
