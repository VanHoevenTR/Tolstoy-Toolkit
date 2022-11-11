using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tolstoy_Toolkit.Json
{
    internal class UserInfoWidget
    {
        public partial class Root
        {
            [JsonProperty("data")]
            public Data Data { get; set; }
        }

        public partial class Data
        {
            [JsonProperty("id")]
            public int Id { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("nick")]
            public string Nick { get; set; }

            [JsonProperty("ava")]
            public string Ava { get; set; }

            [JsonProperty("answer_count")]
            public long AnswerCount { get; set; }
        }
    }
}
