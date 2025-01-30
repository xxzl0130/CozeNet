using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CozeNet.Knowledge.Models
{
    public class DocumentProgress
    {
        /// <summary>
        /// 文件地址。
        /// </summary>
        [JsonPropertyName("url")]
        public string? Url { get; set; }

        /// <summary>
        /// 文件的大小，单位为字节。
        /// </summary>
        [JsonPropertyName("size")]
        public long Size { get; set; }

        /// <summary>
        /// 本地文件格式，即文件后缀，例如 txt。格式支持 pdf、txt、doc、docx 类型。
        /// </summary>
        [JsonPropertyName("type")]
        public string? Type { get; set; }

        /// <summary>
        /// 文件的处理状态。取值包括：
        /// 0：处理中
        /// 1：处理完毕
        /// 9：处理失败，建议重新上传
        /// </summary>
        [JsonPropertyName("status")]
        public int Status { get; set; }

        /// <summary>
        /// 文件上传的进度。单位为百分比。
        /// </summary>
        [JsonPropertyName("progress")]
        public int Progress { get; set; }

        /// <summary>
        /// 文件的 ID。
        /// </summary>
        [JsonPropertyName("document_id")]
        public string? DocumentID {  get; set; }

        /// <summary>
        /// 在线网页是否自动更新。取值包括：
        /// 0：不自动更新
        /// 1：自动更新
        /// </summary>
        [JsonPropertyName("update_type")]
        public int UpdateType { get; set; }

        /// <summary>
        /// 文件名称。
        /// </summary>
        [JsonPropertyName("document_name")]
        public string? DocumentName {  get; set; }

        /// <summary>
        /// 预期剩余时间，单位为秒。
        /// </summary>
        [JsonPropertyName("remaining_time")]
        public long RemainingTime { get; set; }

        /// <summary>
        /// 失败状态的详细描述，例如切片失败时返回失败信息。
        /// 仅文档处理失败时会返回此参数。
        /// </summary>
        [JsonPropertyName("status_descript")]
        public string? StatusDescript {  get; set; }

        /// <summary>
        /// 在线网页自动更新的频率。单位为小时。
        /// </summary>
        [JsonPropertyName("update_interval")]
        public int UpdateInterval { get; set; }
    }
}
