using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CozeNet.Core.Models
{
    internal class TokenRequestBody
    {

        [JsonPropertyName("duration_seconds")]
        public int DurationSeconds { get; set; } = 86399;

        [JsonPropertyName("grant_type")]
        public string GrantType { get; set; } = "urn:ietf:params:oauth:grant-type:jwt-bearer";
    }
}
