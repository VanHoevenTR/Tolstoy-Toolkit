using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tolstoy_Toolkit
{
    internal class ArticleComments
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
            [JsonProperty("site_id")]
            public int SiteId { get; set; }

            [JsonProperty("title")]
            public string Title { get; set; }

            [JsonProperty("hash")]
            public string Hash { get; set; }

            [JsonProperty("identity")]
            public object Identity { get; set; }

            [JsonProperty("url")]
            public Uri Url { get; set; }

            [JsonProperty("count_comment_all")]
            public int CountCommentAll { get; set; }

            [JsonProperty("count_comment_load")]
            public int CountCommentLoad { get; set; }

            [JsonProperty("closed")]
            public bool Closed { get; set; }

            [JsonProperty("format")]
            public int Format { get; set; }

            [JsonProperty("root_id")]
            public int RootId { get; set; }

            [JsonProperty("fixed_comment")]
            public object FixedComment { get; set; }
        }
        public class Comment
        {
            [JsonProperty("answer_comment_count")]
            public int AnswerCommentCount { get; set; }

            [JsonProperty("answer_comment")]
            public AnswerComment AnswerComment { get; set; }

            [JsonProperty("id")]
            public int Id { get; set; }

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
            public string Sort { get; set; }

            [JsonProperty("edited")]
            public bool Edited { get; set; }

            [JsonProperty("fixed")]
            public bool Fixed { get; set; }

            [JsonProperty("comment_type")]
            public int CommentType { get; set; }

            [JsonProperty("answer_comment_root_id")]
            public int AnswerCommentRootId { get; set; }
        }

        public partial class AnswerComment
        {
            [JsonProperty("id")]
            public int Id { get; set; }

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
            public int CommentType { get; set; }

            [JsonProperty("answer_comment_root_id")]
            public int AnswerCommentRootId { get; set; }
        }

        public partial class Raiting
        {
            [JsonProperty("id")]
            public int Id { get; set; }

            [JsonProperty("val")]
            public int Val { get; set; }

            [JsonProperty("user_val")]
            public int UserVal { get; set; }
        }

        public partial class User
        {
            [JsonProperty("id")]
            public int Id { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("nick")]
            public string Nick { get; set; }

            [JsonProperty("ava")]
            public string Ava { get; set; }

            [JsonProperty("online")]
            public bool Online { get; set; }

            [JsonProperty("data_last_visit")]
            public DateTimeOffset DataLastVisit { get; set; }

            [JsonProperty("admin")]
            public bool Admin { get; set; }
        }
    }
}
