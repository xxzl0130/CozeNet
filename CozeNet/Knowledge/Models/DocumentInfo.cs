using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CozeNet.Knowledge.Models
{
    public class DocumentInfo
    {
        /// <summary>
        /// 文件内容的总字符数量。
        /// </summary>
        [JsonPropertyName("char_count")]
        public int CharCount { get; set; }

        /// <summary>
        /// 分段规则。详细说明可参考 chunk_strategy object。
        /// </summary>
        [JsonPropertyName("chunk_strategy")]
        public Chunkstrategy? ChunkStrategy { get; set; }

        /// <summary>
        /// 文件的上传时间，格式为 10 位的 Unixtime 时间戳。
        /// </summary>
        [JsonPropertyName("create_time")]
        public long CreateTime { get; set; }

        /// <summary>
        /// 文件的 ID。
        /// </summary>
        [JsonPropertyName("document_id")]
        public string? DocumentID { get; set; }

        /// <summary>
        /// 文件的格式类型。
        /// </summary>
        [JsonPropertyName("format_type")]
        public DataSetType FormatType { get; set; }

        /// <summary>
        /// 被对话命中的次数。
        /// </summary>
        [JsonPropertyName("hit_count")]
        public int HitCount { get; set; }

        /// <summary>
        /// 文件的名称。
        /// </summary>
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        /// <summary>
        /// 文件的大小，单位为字节。
        /// </summary>
        [JsonPropertyName("size")]
        public int Size { get; set; }

        /// <summary>
        /// 文件的分段数量。
        /// </summary>
        [JsonPropertyName("slice_count")]
        public int SliceCount { get; set; }

        /// <summary>
        /// 文件的上传方式。取值包括：
        /// 0：上传本地文件。
        /// 1：上传在线网页。
        /// </summary>
        [JsonPropertyName("source_type")]
        public int SourceType { get; set; }

        /// <summary>
        /// 文件的处理状态。取值包括：
        /// 0：处理中
        /// 1：处理完毕
        /// 9：处理失败，建议重新上传
        /// </summary>
        [JsonPropertyName("status")]
        public int Status { get; set; }

        /// <summary>
        /// 本地文件格式，即文件后缀，例如 txt。格式支持 pdf、txt、doc、docx 类型。
        /// </summary>
        [JsonPropertyName("type")]
        public string? Type { get; set; }

        /// <summary>
        /// 在线网页自动更新的频率。单位为小时。
        /// </summary>
        [JsonPropertyName("update_interval")]
        public int UpdateInterval { get; set; }

        /// <summary>
        /// 文件的最近一次修改时间，格式为 10 位的 Unixtime 时间戳。
        /// </summary>
        [JsonPropertyName("update_time")]
        public long UpdateTime { get; set; }

        /// <summary>
        /// 在线网页是否自动更新。取值包括：0：不自动更新1：自动更新
        /// </summary>
        [JsonPropertyName("update_type")]
        public int UpdateType { get; set; }

        /// <summary>
        /// 上传的本地文档的唯一标识。
        /// </summary>
        [JsonPropertyName("tos_uri")]
        public string? TosURI { get; set; }
    }
}
