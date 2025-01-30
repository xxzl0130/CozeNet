using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CozeNet.Knowledge.Models
{
    public class PhotoInfo
    {
        /// <summary>
        /// 图片链接。
        /// </summary>
        [JsonPropertyName("url")]
        public string? Url { get; set; }

        /// <summary>
        /// 图片名。
        /// </summary>
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        /// <summary>
        /// 图片大小，单位为字节。
        /// </summary>
        [JsonPropertyName("size")]
        public int Size { get; set; }

        /// <summary>
        /// 文件格式，即文件后缀，例如 jpg、png。
        /// </summary>
        [JsonPropertyName("type")]
        public string? Type { get; set; }

        /// <summary>
        /// 文件的状态。取值包括：
        /// 0：处理中
        /// 1：处理完毕
        /// 9：处理失败，建议重新上传
        /// </summary>
        [JsonPropertyName("status")]
        public int Status { get; set; }

        /// <summary>
        /// 图片描述信息。
        /// </summary>
        [JsonPropertyName("caption")]
        public string? Caption { get; set; }

        /// <summary>
        /// 创建人的扣子 ID。
        /// </summary>
        [JsonPropertyName("creator_id")]
        public string? CreatorID { get; set; }

        /// <summary>
        /// 图片的上传时间，格式为 10 位的 Unixtime 时间戳。
        /// </summary>
        [JsonPropertyName("create_time")]
        public long CreateTime { get; set; }

        /// <summary>
        /// 更新时间，格式为 10 位的 Unixtime 时间戳。
        /// </summary>
        [JsonPropertyName("update_time")]
        public long UpdateTime { get; set; }

        /// <summary>
        /// 图片的 ID。
        /// </summary>
        [JsonPropertyName("document_id")]
        public string? DocumentID { get; set; }

        /// <summary>
        /// 上传方式。取值包括：
        /// 0：上传本地文件。
        /// 1：上传在线网页。
        /// 5：上传 file_id。
        /// </summary>
        [JsonPropertyName("source_type")]
        public int SourceType { get; set; }
    }

    public class PhotoInfoList
    {
        /// <summary>
        /// 图片的详细信息。
        /// </summary>
        [JsonPropertyName("photo_infos")]
        public PhotoInfo[]? PhotoInfos { get; set; }

        /// <summary>
        /// 符合查询条件的图片总数量。
        /// </summary>
        [JsonPropertyName("total_count")]
        public int TotalCount { get; set; }
    }
}
