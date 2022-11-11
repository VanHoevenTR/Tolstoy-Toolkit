using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tolstoy_Toolkit.CSV
{
    internal class CsvVoteHistory
    {
        [Name("Date & Time")]
        public string DateTime { get; set; }

        [Name("User ID")]
        public string UserId { get; set; }

        [Name("User")]
        public string User { get; set; }

        [Name("Rating")]
        public string Rating { get; set; }

        [Name("Comment")]
        public string Comment { get; set; }

        [Name("Comment ID")]
        public string CommentId { get; set; }

        [Name("Vote")]
        public string Vote { get; set; }
    }
}
