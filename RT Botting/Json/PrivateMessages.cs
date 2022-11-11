using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tolstoy_Toolkit.Json
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    internal class PrivateMessages
    {
        public partial class Root
        {
            [JsonProperty("data")]
            public Data Data { get; set; }
        }

        public partial class Data
        {
            [JsonProperty("chat")]
            public Chat Chat { get; set; }

            [JsonProperty("comments")]
            public Comment[] Comments { get; set; }
        }

        public partial class Chat
        {
            [JsonProperty("user_first")]
            public User UserFirst { get; set; }

            [JsonProperty("user_second")]
            public User UserSecond { get; set; }

            [JsonProperty("count_comment_all")]
            public long CountCommentAll { get; set; }

            [JsonProperty("count_comment_load")]
            public long CountCommentLoad { get; set; }
        }

        public partial class User
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
            [JsonProperty("id")]
            public long Id { get; set; }

            [JsonProperty("text_template")]
            public string TextTemplate { get; set; }

            [JsonProperty("data_create")]
            public DateTime DataCreate { get; set; }

            [JsonProperty("user")]
            public User User { get; set; }

            [JsonProperty("raiting")]
            public Raiting Raiting { get; set; }

            [JsonProperty("attaches")]
            public object[] Attaches { get; set; }

            [JsonProperty("attaches_icons")]
            public object[] AttachesIcons { get; set; }

            [JsonProperty("attaches_text")]
            public string AttachesText { get; set; }

            [JsonProperty("sort")]
            public double Sort { get; set; }

            [JsonProperty("edited")]
            public bool Edited { get; set; }

            [JsonProperty("fixed")]
            public bool Fixed { get; set; }

            [JsonProperty("comment_type")]
            public long CommentType { get; set; }

            [JsonProperty("answer_comment_root_id")]
            public long AnswerCommentRootId { get; set; }
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
