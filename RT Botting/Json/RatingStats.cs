using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tolstoy_Toolkit.Json
{
    internal class RatingStats
    {
        public partial class Root
        {
            [JsonProperty("data")]
            public Data Data { get; set; }
        }

        public partial class Data
        {
            [JsonProperty("raiting")]
            public Raiting Raiting { get; set; }

            [JsonProperty("good")]
            public VotedUsers Good { get; set; }

            [JsonProperty("bad")]
            public VotedUsers Bad { get; set; }

            [JsonProperty("comment")]
            public Comment Comment { get; set; }
        }

        public partial class VotedUsers
        {
            [JsonProperty("count")]
            public int Count { get; set; }

            [JsonProperty("items")]
            public Item[] Items { get; set; }
        }

        public partial class Item
        {
            [JsonProperty("id")]
            public long Id { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("nick")]
            public string Nick { get; set; }

            [JsonProperty("ava")]
            public string Ava { get; set; }

            [JsonProperty("online")]
            public bool Online { get; set; }

            [JsonProperty("data_last_visit")]
            public DateTime DataLastVisit { get; set; }

            [JsonProperty("admin")]
            public bool Admin { get; set; }
        }

        public partial class Comment
        {
            [JsonProperty("user")]
            public User User { get; set; }
        }

        public partial class User
        {
            [JsonProperty("id")]
            public long Id { get; set; }
        }

        public partial class Raiting
        {
            [JsonProperty("id")]
            public long Id { get; set; }

            [JsonProperty("val")]
            public long Val { get; set; }

            [JsonProperty("user_val")]
            public long UserVal { get; set; }
        }
    }
}
