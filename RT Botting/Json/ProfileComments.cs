using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tolstoy_Toolkit
{
    internal class ProfileComments
    {
        public partial class Root
        {
            [JsonProperty("data")]
            public Data Data { get; set; }
        }

        public partial class Data
        {
            [JsonProperty("user")]
            public User User { get; set; }

            [JsonProperty("user_details")]
            public UserDetails UserDetails { get; set; }

            [JsonProperty("comments")]
            public Comment[] Comments { get; set; }
        }

        public partial class Comment
        {
            [JsonProperty("site_id")]
            public long SiteId { get; set; }

            [JsonProperty("hidden")]
            public bool Hidden { get; set; }

            [JsonProperty("hash")]
            public object Hash { get; set; }

            [JsonProperty("url")]
            public object Url { get; set; }

            [JsonProperty("title")]
            public object Title { get; set; }

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
            public long Sort { get; set; }

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
            public DateTime DataLastVisit { get; set; }

            [JsonProperty("admin")]
            public bool Admin { get; set; } //Unused?
        }

        public partial class UserDetails
        {
            [JsonProperty("ava_big")]
            public string AvaBig { get; set; }

            [JsonProperty("date_last_visit")]
            public DateTime DateLastVisit { get; set; }

            [JsonProperty("date_registration")]
            public DateTime DateRegistration { get; set; }

            [JsonProperty("role")]
            public string Role { get; set; }

            [JsonProperty("about")]
            public string About { get; set; }

            [JsonProperty("count_raiting")]
            public int CountRaiting { get; set; }

            [JsonProperty("count_comment")]
            public int CountComment { get; set; }

            [JsonProperty("social")]
            public Social[] Social { get; set; }

            [JsonProperty("awards_count_user")]
            public long AwardsCountUser { get; set; }

            [JsonProperty("awards_count_all")]
            public long AwardsCountAll { get; set; }

            [JsonProperty("awards_view")]
            public AwardsView[] AwardsView { get; set; }

            [JsonProperty("position_rating")]
            public long PositionRating { get; set; }
        }
        public partial class Social
        {
            [JsonProperty("type")]
            public string Type { get; set; }

            [JsonProperty("url")]
            public string Url { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }
        }

        public partial class AwardsView
        {
            [JsonProperty("id")]
            public long Id { get; set; }

            [JsonProperty("image_original")]
            public Uri ImageOriginal { get; set; }
        }
    }
}
