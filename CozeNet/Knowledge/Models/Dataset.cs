using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CozeNet.Knowledge.Models
{
    public class Dataset
    {
        /// <summary>
        /// 知识库名称。
        /// </summary>
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        public enum EStatus
        {
            Active = 1,
            Inactive = 3
        }

        /// <summary>
        /// 知识库状态
        /// </summary>
        [JsonPropertyName("status")]
        public EStatus Status { get; set; }

        /// <summary>
        /// 当前用户是否为该知识库的所有者
        /// </summary>
        [JsonPropertyName("can_edit")]
        public bool CanEdit { get; set; }

        /// <summary>
        /// 知识库图标的 uri。
        /// </summary>
        [JsonPropertyName("icon_uri")]
        public string? IconURI { get; set; }

        /// <summary>
        /// 知识库图标的 URL。
        /// </summary>
        [JsonPropertyName("icon_url")]
        public string? IconURL { get; set; }

        /// <summary>
        /// 知识库所在空间的空间 ID。
        /// </summary>
        [JsonPropertyName("space_id")]
        public string? SpaceID { get; set; }

        /// <summary>
        /// 知识库中的文件数量。
        /// </summary>
        [JsonPropertyName("doc_count")]
        public int DocumnentCount { get; set; }

        /// <summary>
        /// 知识库中的文件列表。
        /// </summary>
        [JsonPropertyName("file_list")]
        public string[]? FileList { get; set; }

        /// <summary>
        /// 知识库命中总次数。
        /// </summary>
        [JsonPropertyName("hit_count")]
        public int HitCount { get; set; }

        /// <summary>
        /// 知识库创建者的头像 url。
        /// </summary>
        [JsonPropertyName("avatar_url")]
        public string? AvatarURL { get; set; }

        /// <summary>
        /// 知识库创建者的扣子 ID。
        /// </summary>
        [JsonPropertyName("creator_id")]
        public string? CreatorID { get; set; }

        /// <summary>
        /// creator_name
        /// </summary>
        [JsonPropertyName("creator_name")]
        public string? CreaterName { get; set; }

        /// <summary>
        /// 知识库 ID。
        /// </summary>
        [JsonPropertyName("dataset_id")]
        public string? DatasetID { get; set; }

        /// <summary>
        /// 知识库创建时间，秒级时间戳。
        /// </summary>
        [JsonPropertyName("create_time")]
        public long CreateTime { get; set; }

        /// <summary>
        /// 知识库的更新时间，秒级时间戳。
        /// </summary>
        [JsonPropertyName("update_time")]
        public long UpdateTime { get; set; }

        /// <summary>
        /// 知识库描述信息。
        /// </summary>
        [JsonPropertyName("description")]
        public string? Description { get; set; }

        /// <summary>
        /// 知识库类型
        /// </summary>
        [JsonPropertyName("format_type")]
        public DataSetType FormatType { get; set; }

        /// <summary>
        /// 知识库分段总数。
        /// </summary>
        [JsonPropertyName("slice_count")]
        public int SliceCount { get; set; }

        /// <summary>
        /// 知识库中已存文件的总大小。
        /// </summary>
        [JsonPropertyName("all_file_size")]
        public long AllFileSize { get; set; }

        /// <summary>
        /// 知识库已绑定的智能体数量。
        /// </summary>
        [JsonPropertyName("bot_used_count")]
        public int BotUsedCount { get; set; }

        /// <summary>
        /// 知识库的切片规则。
        /// </summary>
        [JsonPropertyName("chunk_strategy")]
        public Chunkstrategy? Chunkstrategy { get; set; }

        /// <summary>
        /// 处理失败的文件列表。
        /// </summary>
        [JsonPropertyName("failed_file_list")]
        public string[]? FailedFileList { get; set; }

        /// <summary>
        /// 处理中的文件名。
        /// </summary>
        [JsonPropertyName("processing_file_list")]
        public string[]? ProcessingFileList { get; set; }

        /// <summary>
        /// 处理中的文件 ID。
        /// </summary>
        [JsonPropertyName("processing_file_id_list")]
        public string[]? ProcessingFileIDList { get; set; }
    }
}
