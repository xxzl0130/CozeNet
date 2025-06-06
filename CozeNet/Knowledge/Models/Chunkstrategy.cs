﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CozeNet.Knowledge.Models
{
    public class Chunkstrategy
    {
        /// <summary>
        /// 分段设置。取值包括：
        /// 0：自动分段与清洗。采用扣子预置规则进行数据分段与处理。
        /// 1：自定义。此时需要通过 separator、max_tokens、remove_extra_spaces 和 remove_urls_emails 分段规则细节。
        /// </summary>
        [JsonPropertyName("chunk_type")]
        public int ChunkType { get; set; }

        /// <summary>
        /// 分段标识符。在 chunk_type=1 时必选。
        /// </summary>
        [JsonPropertyName("separator")]
        public string? Separator { get; set; }

        /// <summary>
        /// 最大分段长度，取值范围为 100~2000。在 chunk_type=1 时必选。
        /// </summary>
        [JsonPropertyName("max_tokens")]
        public int MaxTokens { get; set; }

        /// <summary>
        /// 是否自动过滤连续的空格、换行符和制表符。取值包括：
        /// true：自动过滤
        /// false：（默认）不自动过滤在 chunk_type=1 时生效。
        /// </summary>
        [JsonPropertyName("remove_extra_spaces")]
        public bool RemoveExtraSpaces { get; set; }

        /// <summary>
        /// 是否自动过滤所有 URL 和电子邮箱地址。取值包括：
        /// true：自动过滤
        /// false：（默认）不自动过滤在 chunk_type=1 时生效。
        /// </summary>
        [JsonPropertyName("remove_urls_emails")]
        public bool RemoveUrlsEmails { get; set; }

        /// <summary>
        /// 图片知识库的标注方式：
        /// 0：（默认）系统自动标注描述信息
        /// 1：手工标注
        /// </summary>
        [JsonPropertyName("caption_type")]
        public int CaptionType { get; set; }
    }
}
