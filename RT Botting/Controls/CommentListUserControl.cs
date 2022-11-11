using CsvHelper;
using CsvHelper.Configuration;
using Newtonsoft.Json;
using Tolstoy_Toolkit.Classes;
using Tolstoy_Toolkit.CSV;
using Tolstoy_Toolkit.Json;
using Tolstoy_Toolkit.Properties;
using Tolstoy_Toolkit.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using static System.Windows.Forms.ListViewItem;
using Tolstoy_Toolkit.Reader;
using System.Net.Http;

namespace Tolstoy_Toolkit
{
    public partial class CommentListUserControl : UserControl, IMessageFilter
    {
        string previousUrl;

        private const int WM_XBUTTONDOWN = 0x020B;
        private const int MK_XBUTTON1 = 0x0020;
        private const int MK_XBUTTON2 = 0x0040;

        bool isRunning, canGoBack;

        private ListViewColumnSorter lvwColumnSorter;

        HistoryReader historyHelper;

        public CommentListUserControl(HistoryReader _history)
        {
            Application.AddMessageFilter(this);
            // this.FormClosed += (s, e) => Application.RemoveMessageFilter(this);

            InitializeComponent();

            // Create an instance of a ListView column sorter and assign it
            // to the ListView control.
            lvwColumnSorter = new ListViewColumnSorter();

            rtUrl.Text = Settings.Default.RtUrl;
            setMaxVote.Checked = Settings.Default.SetMaxVoting;
            maxVote.Value = Settings.Default.MaxVoting;
            parallelVotingChkBox.Checked = Settings.Default.ParallelVoting;
            parallelUpDown.Value = Settings.Default.Parallel;
            setDelay.Checked = Settings.Default.SetDelay;
            delay.Value = Settings.Default.Delay;
            downvoteBetween.Checked = Settings.Default.SetMinusRatingBetween;
            minusRatingMin.Value = Settings.Default.MinusRatingMin;
            minusRatingMax.Value = Settings.Default.MinusRatingMax;
            upvoteBetween.Checked = Settings.Default.SetPlusRatingBetween;
            plusRatingMin.Value = Settings.Default.PlusRatingMin;
            plusRatingMax.Value = Settings.Default.PlusRatingMax;

            historyHelper = _history;
        }

        public bool PreFilterMessage(ref Message m)
        {
            if (m.Msg == WM_XBUTTONDOWN)
            {
                int lowWord = (m.WParam.ToInt32() << 16) >> 16;
                switch (lowWord)
                {
                    case MK_XBUTTON1:
                        // navigate backward
                        if (canGoBack)
                        {
                            if (!String.IsNullOrEmpty(previousUrl))
                                LoadComments(previousUrl);
                            goBackBtn.Enabled = false;
                            canGoBack = false;
                            commListView.ListViewItemSorter = null;
                        }
                        break;
                    case MK_XBUTTON2:
                        // navigate forward

                        break;
                }
            }
            return false; // dispatch further
        }

        #region Click handler
        private async void loadHistory_Click(object sender, EventArgs e)
        {
            if (File.Exists(Program.historyFilePath))
            {
                commListView.Items.Clear();
                using (var reader = new StreamReader(Program.historyFilePath))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    var records = csv.GetRecords<CsvVoteHistory>();
                    foreach (var record in records.Reverse())
                    {
                        int totalRating = Convert.ToInt32(record.Rating);
                        if (Settings.Default.ReadRatingFromServer)
                        {
                            try
                            {
                                string[] tokens = TxtUtils.LoadToken();
                                if (tokens != null || tokens.Length != 0)
                                {
                                    string comms = null;
                                    comms = await Web.RequestGetAsync("https://web.tolstoycomments.com/api/raiting/stats?commentid=" + record.CommentId + "&token=" + HttpUtility.UrlEncode(tokens[0], UnicodeEncoding.UTF8));

                                    //Web.GET("https://web.tolstoycomments.com/api/raiting/stats?commentid=" + record.CommentId + "&token=" + HttpUtility.UrlEncode(tokens[0], UnicodeEncoding.UTF8));

                                    var usrInfo = JsonConvert.DeserializeObject<RatingStats.Root>(comms);

                                    int plus = usrInfo.Data.Good.Count;
                                    int minus = usrInfo.Data.Bad.Count;
                                    totalRating = plus - minus;
                                }
                            }
                            catch
                            {

                            }
                        }

                        ListViewItem list = new ListViewItem(record.DateTime);
                        list.SubItems.Add(record.UserId);
                        list.SubItems.Add(record.User);
                        list.SubItems.Add(totalRating.ToString());
                        list.SubItems.Add(record.Comment);
                        list.SubItems.Add(record.CommentId);

                        if (record.Vote == "up")
                            list.ForeColor = Color.ForestGreen;
                        else if (record.Vote == "down")
                            list.ForeColor = Color.Firebrick;

                        commListView.Items.Add(list);
                    }
                }
            }
            else
                MessageBox.Show("History file does not exist", base.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void exportToCsv_Click(object sender, EventArgs e)
        {
            if (commListView.Items.Count > 0)
            {
                using (var sfd = new SaveFileDialog())
                {
                    sfd.FileName = "List - " + DateTime.Now.ToString("dd MMMM yyyy HH.mm");
                    sfd.Filter = "CSV (*.csv)|*.csv";
                    sfd.FilterIndex = 2;

                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        using (TextWriter TW = new StreamWriter(sfd.FileName))
                        {
                            for (int i = 0; i < commListView.Items.Count; i++)
                            {
                                string dateTime = Csv.Escape(commListView.Items[i].SubItems[0].Text);
                                string userId = Csv.Escape(commListView.Items[i].SubItems[1].Text);
                                string user = Csv.Escape(commListView.Items[i].SubItems[2].Text);
                                string vote = Csv.Escape(commListView.Items[i].SubItems[3].Text);
                                string comm = Csv.Escape(commListView.Items[i].SubItems[4].Text);
                                string commId = Csv.Escape(commListView.Items[i].SubItems[5].Text);
                                if (i == 0)
                                    TW.WriteLine($"Date & time,User ID,User,Rating,Comment,Comment ID");
                                TW.WriteLine($"{dateTime},{userId},{user},{vote},{comm},{commId}");
                            }
                        }
                        Form1.instance.Log("CSV saved to " + sfd.FileName);
                    }
                }
            }
            else
            {
                Form1.instance.Log("There is nothing in the list");
            }
        }


        private void DownVote_Click(object sender, EventArgs e)
        {
            VoteComments(Vote.down);
        }

        private void Upvote_Click(object sender, EventArgs e)
        {
            VoteComments(Vote.up);
        }

        private async void VoteComments(Vote vote)
        {
            isRunning = true;
            try
            {
                bool selected = false;
                var dateTimes = new List<string>();
                var usrIds = new List<string>();
                var usrs = new List<string>();
                var ratings = new List<int>();
                var comms = new List<string>();
                var commIds = new List<string>();
                var listPos = new List<int>();

                if (commListView.Items.Count > 0)
                {
                    for (int i = 0; i < commListView.Items.Count; i++)
                    {
                        if (commListView.Items[i].Selected == true)
                        {
                            dateTimes.Add(commListView.Items[i].SubItems[0].Text);
                            usrIds.Add(commListView.Items[i].SubItems[1].Text);
                            usrs.Add(commListView.Items[i].SubItems[2].Text);
                            ratings.Add(Convert.ToInt32(commListView.Items[i].SubItems[3].Text));
                            comms.Add(commListView.Items[i].SubItems[4].Text);
                            commIds.Add(commListView.Items[i].SubItems[5].Text);
                            listPos.Add(i);
                            selected = true;
                        }
                    }
                }

                if (selected == false)
                {
                    MessageBox.Show("No comment selected", Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Done();
                    return;
                }

                string[] tokens = TxtUtils.LoadToken();
                if (tokens == null || tokens.Length == 0)
                {
                    MessageBox.Show("Cannot vote without a token", Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int parallelLimit = 0;
                int ii = 0;
                foreach (string commid in commIds)
                {
                    if (!isRunning)
                        break;

                    Run($"Voting comment: {commid} ({commIds.FindIndex(x => x.StartsWith(commid)) + 1} out of {commIds.Count})");

                    var rnd = new Random();
                    tokens = tokens.OrderBy(line => rnd.Next()).ToArray();

                    await Task.Factory.StartNew(() =>
                    {
                        if (parallelVotingChkBox.Checked)
                        {
                            parallelLimit++;
                            if (parallelLimit == parallelUpDown.Value)
                            {
                                VotePost(tokens, dateTimes[ii], usrIds[ii], usrs[ii], ratings[ii], comms[ii], commid, listPos[ii], vote);
                                parallelLimit = 0;
                            }
                            else if (commIds.FindIndex(x => x.StartsWith(commid)) + 1 == commIds.Count)
                            {
                                VotePost(tokens, dateTimes[ii], usrIds[ii], usrs[ii], ratings[ii], comms[ii], commid, listPos[ii], vote);
                            }
                            else
                            {
                                VotePost(tokens, dateTimes[ii], usrIds[ii], usrs[ii], ratings[ii], comms[ii], commid, listPos[ii], vote, false);
                            }
                        }
                        else
                            VotePost(tokens, dateTimes[ii], usrIds[ii], usrs[ii], ratings[ii], comms[ii], commid, listPos[ii], vote);
                    });

                    ii++;
                }

                if (Settings.Default.SaveHistory)
                    historyHelper.Save();

                Done();
            }
            catch (ThreadAbortException ex)
            {
                Done(ex.Message);
            }
            catch (Exception ex)
            {
                //MessageBox.Show("An error occured\n" + ex.Message);
                Done("An error occured: " + ex.Message);
            }
        }

        private async Task VotePost(string[] tokens, string dateTime, string usrId, string usr, int currentRating, string comment, string commId, int listPos, Vote vote, bool wait = true)
        {
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/103.0.0.0 Safari/537.36");

            int numToStop = Num.Rand(1, setMaxVote.Checked ? (int)maxVote.Value : tokens.Length);
            int upNumOfRatingToStop = Num.Rand((int)plusRatingMin.Value, (int)plusRatingMax.Value);
            int downNumOfRatingToStop = Num.Rand((int)minusRatingMin.Value, (int)minusRatingMax.Value);

            int exceptionCount = 0;
            int voteCount = 0;
            for (int i = 0; i < tokens.Length; i++)
            {
                if (upNumOfRatingToStop <= currentRating && upvoteBetween.Checked && vote == Vote.up)
                    break;

                if (downNumOfRatingToStop >= currentRating && downvoteBetween.Checked && vote == Vote.down)
                    break;

                if (maxVote.Value == i && setMaxVote.Checked)
                    break;

                if (!isRunning)
                    break;

                try
                {
                    string v = (vote == Vote.down) ? "down" : "up";

                    var url = "https://web.tolstoycomments.com/api/raiting/" + v + "?token=" + HttpUtility.UrlEncode(tokens[i], UnicodeEncoding.UTF8);

                    var postData = "{\"comment_id\":" + Uri.EscapeDataString(commId);
                    postData += ",\"token\":\"" + HttpUtility.UrlDecode(tokens[i], UnicodeEncoding.UTF8) + "\"}";

                    if (Settings.Default.NoAwaiting)
                        Web.RequestPostAsync(url, postData);
                    else
                    {
                        if (wait)
                            Web.POST(url, postData);
                        else
                        {
                            await Web.RequestPostAsync(url, postData);
                        }
                    }

                    if (vote == Vote.up)
                    {
                        if (Settings.Default.LogVotes)
                            Form1.instance.Log($"Upvoted {commId} with token {Str.Truncate(tokens[i], 15)}...");
                        currentRating++;
                        voteCount++;
                        exceptionCount = 0;
                    }
                    else
                    {
                        if (Settings.Default.LogVotes)
                            Form1.instance.Log($"Downvoted {commId} with token {Str.Truncate(tokens[i], 15)}...");
                        exceptionCount = 0;
                        currentRating--;
                    }

                    //Form1.instance.Log($"Time: " + DateTime.Now);

                    if (setDelay.Checked)
                        Thread.Sleep(Convert.ToInt32(delay.Value));
                }
                catch (WebException ex)
                {
                    Debug.WriteLine("Total votes: " + voteCount);
                    if (exceptionCount == 10)
                    {
                        Form1.instance.Log($"Too many errors. Stopped");
                        break;
                    }

                    exceptionCount++;
                    //Debug.WriteLine(ex.Message);
                    using (var stream = ex.Response.GetResponseStream())
                    using (var reader = new StreamReader(stream))
                    {
                        string error = Str.TranslateError(reader.ReadToEnd());
                        if (String.IsNullOrEmpty(error))
                            error = ex.Message;
                        Form1.instance.Log($"Error voting comment {commId} with token {Str.Truncate(tokens[i], 15)}... {error}");
                        //Debug.WriteLine(reader.ReadToEnd());
                    }
                }
                catch (Exception ex)
                {
                    if (exceptionCount == 7)
                        break;

                    Debug.WriteLine(ex.Message);
                    //Form1.instance.Log($"Time: " + DateTime.Now);
                }
            }

            try
            {
                string comms = null;
                comms = await Web.RequestGetAsync("https://web.tolstoycomments.com/api/raiting/stats?commentid=" + commId + "&token=" + HttpUtility.UrlEncode(tokens[0], UnicodeEncoding.UTF8));

                var usrInfo = JsonConvert.DeserializeObject<RatingStats.Root>(comms);

                int plus = usrInfo.Data.Good.Count;
                int minus = usrInfo.Data.Bad.Count;
                int total = plus - minus;

                Invoke(new Action(delegate ()
                {
                    commListView.Items[listPos].SubItems[3].Text = total.ToString();
                    if (vote == Vote.up)
                        commListView.Items[listPos].ForeColor = Color.ForestGreen;
                    else if (vote == Vote.down)
                        commListView.Items[listPos].ForeColor = Color.Firebrick;
                }));

                if (Settings.Default.SaveHistory /*&& !FileUtils.IsFileLocked(Program.historyFilePath)*/)
                    SaveHistory(dateTime, usrId, usr, total.ToString(), comment, commId, vote);
            }
            catch
            {

            }
        }

        private void LoadComments_Click(object sender, EventArgs e)
        {
            if (Settings.Default.SaveHistory)
                historyHelper.Save();

            commListView.ListViewItemSorter = null;

            isRunning = true;

            string url = "https://web.tolstoycomments.com/api/chatpage/first?siteid=" + Web.GetSiteId(rtUrl.Text) + "&hash=null&url=" + HttpUtility.UrlEncode(HttpUtility.UrlDecode(rtUrl.Text.Replace("89.191.237.192", "rt.com")), Encoding.UTF8) + "&sort=1&format=1";
            if (rtUrl.Text.Contains("rt.com") || rtUrl.Text.Contains("89.191.237.192"))
            {
                LoadComments(url);
                previousUrl = url;
                return;
            }

            LoadProfileComments(rtUrl.Text);

            isRunning = false;
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            isRunning = false;
        }

        private void GoBackBtn_Click(object sender, EventArgs e)
        {
            commListView.ListViewItemSorter = null;
            if (!String.IsNullOrEmpty(previousUrl))
                LoadComments(previousUrl);
            goBackBtn.Enabled = false;
            canGoBack = false;
        }

        private void PasteBtn_Click(object sender, EventArgs e)
        {
            //Paste from clipboard
            rtUrl.Text = Clipboard.GetText(TextDataFormat.Text);
        }
        #endregion

        #region Load comments
        private async void LoadComments(string url)
        {
            Run("Loading comments...");
            try
            {
                commListView.Items.Clear();

            LoadMore:
                string sort = null;

                string comms = await Web.RequestGetAsync(url);

                var rtcomms = JsonConvert.DeserializeObject<ArticleComments.Root>(comms);
                var commentsList = rtcomms.Data.Comments;
                var commCounts = rtcomms.Data.Chat.CountCommentLoad;

                string[] historyList = null;
                try
                {
                    if (File.Exists(Program.historyFilePath))
                        historyList = File.ReadAllLines(Program.historyFilePath);
                }
                catch
                {
                    //Skip reading history
                }

                foreach (var c in commentsList)
                {
                    if (!isRunning)
                        break;

                    try
                    {
                        string name = c.User.Name;
                        if (c.User.Name != c.User.Nick) //Show nick name if name doesn't match with nick name
                            name = c.User.Name + " (" + c.User.Nick + ")";

                        AddToList(historyList, c.DataCreate, c.User.Id, name, c.Raiting.Val, c.TextTemplate, c.Id);
                        sort = c.Sort;
                        try
                        {
                            //Get answer comments
                            if (c.AnswerCommentCount == 1)
                            {
                                string name2 = c.AnswerComment.User.Name;
                                if (c.AnswerComment.User.Name != c.AnswerComment.User.Nick)
                                    name2 = c.AnswerComment.User.Name + " (" + c.AnswerComment.User.Nick + ")";

                                AddToList(historyList, c.AnswerComment.DataCreate, c.AnswerComment.User.Id, name2, c.AnswerComment.Raiting.Val, "   @" + c.AnswerComment.TextTemplate, c.AnswerComment.Id);
                            }
                            else if (c.AnswerCommentCount > 1)
                            {
                                string anscomms = await Web.RequestGetAsync("https://web.tolstoycomments.com/api/chatpage/first?siteid=" + Web.GetSiteId(rtUrl.Text) + "&hash=" + rtcomms.Data.Chat.Hash + "&url=" + HttpUtility.UrlEncode(HttpUtility.UrlDecode(rtUrl.Text.Replace("89.191.237.192", "rt.com")), Encoding.UTF8) + "&rootid=" + c.Id + "&sort=1&format=1");


                                var rtanscomms = JsonConvert.DeserializeObject<ArticleComments.Root>(anscomms);
                                var ansCommentsList = rtanscomms.Data.Comments;
                                for (int i = ansCommentsList.Length - 2; i > 1; i--)
                                {
                                    var cc = ansCommentsList[i];
                                    AddToList(historyList, cc.DataCreate, cc.User.Id, cc.User.Name, cc.Raiting.Val, "   @" + cc.TextTemplate, cc.Id);
                                    sort = c.Sort;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine(ex);
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex);
                    }
                }
                if (commCounts == 50 && isRunning && !String.IsNullOrEmpty(sort))
                {
                    url = "https://web.tolstoycomments.com/api/chatpage/page?siteid=" + Web.GetSiteId(rtUrl.Text) + "&hash=null&url=" + HttpUtility.UrlEncode(HttpUtility.UrlDecode(rtUrl.Text.Replace("89.191.237.192", "rt.com")), Encoding.UTF8) + "&page=" + sort + "&down=true&sort=1&format=1";
                    goto LoadMore;
                }

                Done();
            }
            catch (WebException ex)
            {
                //Debug.WriteLine(ex.Message);
                using (var stream = ex.Response.GetResponseStream())
                using (var reader = new StreamReader(stream))
                {
                    string error = Str.TranslateError(reader.ReadToEnd());
                    if (String.IsNullOrEmpty(error))
                        error = ex.Message;
                    Form1.instance.Log($"An error occured: {error}");
                    //Debug.WriteLine(reader.ReadToEnd());

                }
                Done();
            }
            catch (Exception ex)
            {
                Done("An error occured: " + ex.Message);
                Debug.WriteLine(ex);
            }
        }

        private async void LoadProfileComments(string userId)
        {
            try
            {
                string[] tokens = TxtUtils.LoadToken();
                if (tokens == null || tokens.Length == 0)
                {
                    MessageBox.Show("Profile comments cannot be loaded without a token", Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Run($"Loading profile comments...");

                string url = "https://web.tolstoycomments.com/api/user/profile?userid=" + userId + "&siteid=4194&token=" + HttpUtility.UrlEncode(tokens[0], UnicodeEncoding.UTF8) + "&lang=en";

                string comms = await Web.RequestGetAsync(url);

                var usrComms = JsonConvert.DeserializeObject<ProfileComments.Root>(comms);
                var comments = usrComms.Data.Comments;

                commListView.Items.Clear();
                isRunning = true;

                string[] historyList = null;
                if (File.Exists(Program.historyFilePath))
                    historyList = File.ReadAllLines(Program.historyFilePath);

                foreach (var c in comments)
                {
                    if (!isRunning)
                        break;

                    try
                    {
                        string name = c.User.Name;
                        if (c.User.Name != c.User.Nick) //Show nick name if name doesn't match with nick name
                            name = c.User.Name + " (" + c.User.Nick + ")";

                        AddToList(historyList, c.DataCreate, c.User.Id, name, c.Raiting.Val, c.TextTemplate, c.Id);
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex);
                    }
                }

                Done($"Loaded comments from {usrComms.Data.User.Name}. Overall rating {usrComms.Data.UserDetails.CountRaiting}. Last online: {usrComms.Data.UserDetails.DateLastVisit.ToString("dd MMM yyyy HH:mm")}");
            }
            catch (WebException ex)
            {
                //Debug.WriteLine(ex.Message);
                using (var stream = ex.Response.GetResponseStream())
                using (var reader = new StreamReader(stream))
                {
                    string error = Str.TranslateError(reader.ReadToEnd());
                    if (String.IsNullOrEmpty(error))
                        error = ex.Message;
                    Form1.instance.Log($"An error occured: {error}");
                    //Debug.WriteLine(reader.ReadToEnd());

                }
                Done();
            }
            catch (Exception ex)
            {
                Done("An error occured: " + ex.Message);
                Debug.WriteLine(ex);
            }
        }
        #endregion

        #region ListView Related
        private void SaveHistory(string dateTime, string usrId, string usr, string rating, string comment, string commId, Vote vote)
        {
            try
            {
                string str = $"{Csv.Escape(dateTime)},{usrId},{Csv.Escape(usr)},{rating},{Csv.Escape(comment)},{commId},{vote}\n";

                //var newHistoryFile = new List<string>();
                bool alreadyAdded = false;

                StringBuilder sb = new StringBuilder();

                sb.Append("Date & Time,User ID,User,Rating,Comment,Comment ID,Vote" + Environment.NewLine);

                using (var reader = new StringReader(historyHelper.historyList))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    var records = csv.GetRecords<CsvVoteHistory>();
                    foreach (var record in records)
                    {
                        //if (record.CommentId == commId && record.Vote == vote.ToString())
                        //{
                        //    return;
                        //}
                        if (record.CommentId == commId)
                        {
                            sb.Append(str + Environment.NewLine);
                            alreadyAdded = true;
                            continue;
                        }

                        sb.Append($"{Csv.Escape(record.DateTime)},{record.UserId},{Csv.Escape(record.User)},{record.Rating},{Csv.Escape(record.Comment)},{record.CommentId},{record.Vote}" + Environment.NewLine);
                    }
                    reader.Close();
                }
                if (!alreadyAdded)
                    sb.Append(str + Environment.NewLine);

                //Debug.WriteLine("Append history " + DateTime.Now);
                historyHelper.historyList = sb.ToString();

                //File.WriteAllLines(Program.historyFilePath, newHistoryFile.ToArray());
            }
            catch (Exception ex)
            {
                Form1.instance.Log(ex.Message);
            }
        }

        private void AddToList(string[] historyList, DateTime dateTime, int id, string nick, int vote, string comm, int commid)
        {
            // ListViewItem list = new ListViewItem(dateTime.ToString("dd MMM yy HH:mm"));
            ListViewItem list = new ListViewItem(dateTime.ToString("yyyy-MM-dd HH:mm"));
            list.SubItems.Add(id.ToString());
            list.SubItems.Add(nick);
            list.SubItems.Add(vote.ToString());
            list.SubItems.Add(HttpUtility.HtmlDecode(Str.StripHTML(comm)));
            list.SubItems.Add(commid.ToString());

            if (historyList != null && Settings.Default.LoadHistory)
            {
                foreach (string s in historyList)
                {
                    if (s.Contains("," + commid.ToString() + ","))
                    {
                        if (s.Contains("," + commid.ToString() + ",up"))
                            list.ForeColor = Color.ForestGreen;
                        else if (s.Contains("," + commid.ToString() + ",down"))
                            list.ForeColor = Color.Firebrick;
                    }
                }
            }

            commListView.Items.Add(list);
        }

        private void viewComment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (commListView.SelectedItems.Count == 1)
            {
                if (commListView.Items.Count > 0)
                {
                    for (int i = 0; i < commListView.Items.Count; i++)
                    {
                        if (commListView.Items[i].Selected == true)
                        {
                            commentViewer.Text = commListView.Items[i].SubItems[2].Text + ": " + commListView.Items[i].SubItems[4].Text;
                            string commId = commListView.Items[i].SubItems[5].Text;
                            string userId = commListView.Items[i].SubItems[1].Text;

                            if (commId != null)
                            {
                                LoadRatingInfo(commId, commListView.Items[i].SubItems[3]);
                                LoadUserInfo(userId);
                            }
                        }
                    }
                }
            }
        }

        //View profile
        private void viewProfileComments_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (Settings.Default.SaveHistory)
                historyHelper.Save();

            isRunning = false;
            goBackBtn.Enabled = true;
            canGoBack = true;

            string userId = null;

            if (commListView.Items.Count > 0)
            {
                for (int i = 0; i < commListView.Items.Count; i++)
                {
                    if (commListView.Items[i].Selected == true)
                    {
                        userId = commListView.Items[i].SubItems[1].Text;
                    }
                }
            }

            LoadProfileComments(userId);
        }


        private void commListView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A && e.Control)
            {
                commListView.MultiSelect = true;
                foreach (ListViewItem item in commListView.Items)
                {
                    item.Selected = true;
                }
            }
        }
        #endregion

        #region Load user stuff
        private async void LoadRatingInfo(string commId, ListViewSubItem subItem)
        {
            try
            {
                string[] tokens = TxtUtils.LoadToken();
                if (tokens == null || tokens.Length == 0)
                {
                    ratingUsers.Text = "Rating cannot be loaded without a token";
                    return;
                }

                string url = "https://web.tolstoycomments.com/api/raiting/stats?commentid=" + commId + "&token=" + HttpUtility.UrlEncode(tokens[0], UnicodeEncoding.UTF8);

                string comms = await Web.RequestGetAsync(url);

                var usrInfo = JsonConvert.DeserializeObject<RatingStats.Root>(comms);
                var upvotes = usrInfo.Data.Good.Items;
                var downvotes = usrInfo.Data.Bad.Items;

                int plus = usrInfo.Data.Good.Count;
                int minus = usrInfo.Data.Bad.Count;
                int total = plus - minus;
                totalRatingLbl.Text = total + $" (+{plus} / -{minus})";

                ratingUsers.Text = "";
                ratingUsers.SelectionColor = Color.ForestGreen;
                ratingUsers.AppendText($"===[ {plus} upvotes ]===\n");
                ratingUsers.SelectionColor = Color.Black;
                foreach (var up in upvotes)
                {
                    string name = up.Name;
                    if (up.Name != up.Nick)
                        name = up.Name + " (" + up.Nick + ") ";
                    ratingUsers.AppendText("[ID: " + up.Id + "] " + name + "\n");
                }
                ratingUsers.SelectionColor = Color.DarkRed;
                ratingUsers.AppendText($"\n===[ {minus} downvotes ]===\n");
                ratingUsers.SelectionColor = Color.Black;
                foreach (var down in downvotes)
                {
                    string name = down.Name;
                    if (down.Name != down.Nick)
                        name = down.Name + " (" + down.Nick + ") ";
                    ratingUsers.AppendText("[ID: " + down.Id + "] " + name + "\n");
                }

                subItem.Text = total.ToString();
            }
            catch
            {

            }
        }

        private void commListView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            commListView.ListViewItemSorter = lvwColumnSorter;

            // Determine if clicked column is already the column that is being sorted.
            if (e.Column == lvwColumnSorter.SortColumn)
            {
                // Reverse the current sort direction for this column.
                if (lvwColumnSorter.Order == SortOrder.Ascending)
                {
                    lvwColumnSorter.Order = SortOrder.Descending;
                }
                else
                {
                    lvwColumnSorter.Order = SortOrder.Ascending;
                }
            }
            else
            {
                // Set the column number that is to be sorted; default to ascending.
                lvwColumnSorter.SortColumn = e.Column;
                lvwColumnSorter.Order = SortOrder.Ascending;
            }

            // Perform the sort with these new sort options.
            this.commListView.Sort();
        }

        private async void LoadUserInfo(string userId)
        {
            try
            {
                if (profilePic.Image != null)
                {
                    profilePic.Image = null;
                    // profilePic.Dispose();
                }

                string[] tokens = TxtUtils.LoadToken();
                if (tokens == null || tokens.Length == 0)
                {
                    nameLbl.Text = "User info cannot be loaded without a token";
                    userNameLbl.Text = "";
                    idLbl.Text = "";
                    lastVisitLbl.Text = "";
                    ratingLbl.Text = "";
                    return;
                }

                string url = "https://web.tolstoycomments.com/api/user/profile?userid=" + userId + "&siteid=4194&token=" + HttpUtility.UrlEncode(tokens[0], UnicodeEncoding.UTF8) + "&lang=en";

                string comms = await Web.RequestGetAsync(url);
                ProfileComments.Root usrInfo = JsonConvert.DeserializeObject<ProfileComments.Root>(comms);
                string imgPath = null;

                await Task.Factory.StartNew(() =>
                {
                    using (WebClient client = new WebClient())
                    {
                        if (!String.IsNullOrEmpty(usrInfo.Data.User.Ava))
                        {
                            imgPath = Path.Combine(Program.tempDir, usrInfo.Data.User.Id.ToString() + Str.RandStr(10) + ".png");
                            Directory.CreateDirectory(Program.tempDir);
                            client.DownloadFile(new Uri(usrInfo.Data.User.Ava), imgPath);
                        }
                    }
                });

                if (usrInfo != null)
                {
                    if (File.Exists(imgPath))
                        profilePic.Image = Image.FromFile(imgPath);
                    nameLbl.Text = "Name: " + usrInfo.Data.User.Name;
                    userNameLbl.Text = "User: " + usrInfo.Data.User.Nick;
                    idLbl.Text = "ID: " + usrInfo.Data.User.Id;
                    lastVisitLbl.Text = "Last visit: " + usrInfo.Data.UserDetails.DateLastVisit.ToString("dd MMM yyyy HH:mm");
                    ratingLbl.Text = "Rating: " + usrInfo.Data.UserDetails.CountRaiting.ToString();
                }
            }
            catch (Exception ex)
            {
#if DEBUG
                Form1.instance.Log("An error occured: " + ex.ToString());
#else
                Form1.instance.Log("An error occured: " + ex.Message);
#endif
                Debug.WriteLine(ex);
            }
        }
        #endregion

        #region Form handlers
        private void Run(string text)
        {
            isRunning = true;
            UpVote.Enabled = false;
            DownVote.Enabled = false;
            loadComms.Enabled = false;
            Form1.instance.Run(text);
        }

        private void Done(string text = "Done")
        {
            isRunning = false;
            UpVote.Enabled = true;
            DownVote.Enabled = true;
            loadComms.Enabled = true;
            Form1.instance.Done(text);
        }
        #endregion
    }
}
