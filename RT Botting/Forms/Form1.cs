using Newtonsoft.Json;
using Tolstoy_Toolkit.Classes;
using Tolstoy_Toolkit.CSV;
using Tolstoy_Toolkit.Enums;
using Tolstoy_Toolkit.Json;
using Tolstoy_Toolkit.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using Tolstoy_Toolkit.Reader;

namespace Tolstoy_Toolkit
{
    public partial class Form1 : Form
    {
        CommentListUserControl userControl;

        public static Form1 instance = null;
        bool isUserInfoRunning, isBfRunning;

        DateTime autoReloadTime;

        string[] storeUserId;

        HistoryReader history;

        public Form1()
        {
            instance = this;

            InitializeComponent();
            CheckUpdate();

            commentsTabControl.TabPages[commentsTabControl.TabCount - 1].Text = "";
            commentsTabControl.Padding = new Point(12, 4);
            commentsTabControl.DrawMode = TabDrawMode.OwnerDrawFixed;

            commentsTabControl.DrawItem += commentsTabControl_DrawItem;
            commentsTabControl.MouseDown += commentsTabControl_MouseDown;
            commentsTabControl.Selecting += commentsTabControl_Selecting;
            commentsTabControl.HandleCreated += commentsTabControl_HandleCreated;

            history = new HistoryReader();
            userControl = new CommentListUserControl(history);
            userControl.Dock = DockStyle.Fill;
            tabPage1.Controls.Add(userControl);

            if (File.Exists(Program.tokensFilePath))
                tokensTextBox.Text = File.ReadAllText(Program.tokensFilePath);

            if (File.Exists(Program.friendsFilePath))
            {
                var friends = File.ReadAllLines(Program.friendsFilePath);
                foreach (var friend in friends)
                {
                    friendsList.Items.Add(friend);
                }
            }
        }

        #region TabControl
        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);
        private const int TCM_SETMINTABWIDTH = 0x1300 + 49;
        private void commentsTabControl_HandleCreated(object sender, EventArgs e)
        {
            SendMessage(commentsTabControl.Handle, TCM_SETMINTABWIDTH, IntPtr.Zero, (IntPtr)16);
        }

        private void commentsTabControl_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (e.TabPageIndex == commentsTabControl.TabCount - 1)
                e.Cancel = true;
        }

        private void commentsTabControl_MouseDown(object sender, MouseEventArgs e)
        {
            SaveConfig();
            var lastIndex = commentsTabControl.TabCount - 1;
            if (commentsTabControl.GetTabRect(lastIndex).Contains(e.Location))
            {
                commentsTabControl.TabPages.Insert(lastIndex, "Instance " + commentsTabControl.TabPages.Count);
                commentsTabControl.SelectedIndex = lastIndex;
                commentsTabControl.TabPages[lastIndex].UseVisualStyleBackColor = true;

                userControl = new CommentListUserControl(history);
                userControl.Dock = DockStyle.Fill;
                commentsTabControl.TabPages[lastIndex].Controls.Add(userControl);
            }
            else
            {
                for (var i = 0; i < commentsTabControl.TabPages.Count; i++)
                {
                    var tabRect = commentsTabControl.GetTabRect(i);
                    tabRect.Inflate(-2, -2);
                    var closeImage = Resources.Close;
                    var imageRect = new Rectangle(
                        (tabRect.Right - closeImage.Width),
                        tabRect.Top + (tabRect.Height - closeImage.Height) / 2,
                        closeImage.Width,
                        closeImage.Height);
                    if (imageRect.Contains(e.Location))
                    {
                        commentsTabControl.TabPages.RemoveAt(i);
                        break;
                    }
                }
            }
        }

        private void commentsTabControl_DrawItem(object sender, DrawItemEventArgs e)
        {
            var tabPage = commentsTabControl.TabPages[e.Index];
            var tabRect = commentsTabControl.GetTabRect(e.Index);
            tabRect.Inflate(-2, -2);
            if (e.Index == commentsTabControl.TabCount - 1)
            {
                var addImage = Resources.Add;
                e.Graphics.DrawImage(addImage,
                    tabRect.Left + (tabRect.Width - addImage.Width) / 2,
                    tabRect.Top + (tabRect.Height - addImage.Height) / 2);
            }
            else
            {
                var closeImage = Resources.Close;
                e.Graphics.DrawImage(closeImage,
                    (tabRect.Right - closeImage.Width),
                    tabRect.Top + (tabRect.Height - closeImage.Height) / 2);
                TextRenderer.DrawText(e.Graphics, tabPage.Text, tabPage.Font,
                    tabRect, tabPage.ForeColor, TextFormatFlags.Left);
            }
        }
        #endregion

        #region Update
        private async void CheckUpdate()
        {
            string remoteVer = null;
            await Task.Factory.StartNew(() =>
            {
                remoteVer = UpdateUtils.RemoteVersion();
            });

            if (!UpdateUtils.RemoteVersion().Contains(ProductVersion))
                Log("There is a new update available. https://mega.nz/folder/PUJ12T6Z#0MrSFBO8QTMGqMDGdNxk5g\nFor more infomation, contact on:\n- Discord: VanHoeven#5150\n- Email: vanhoeventr@proton.me");
            else
                Log("No update available");
        }
        #endregion

        #region Form control handlers
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Settings.Default.SaveHistory)
                history.Save();
            SaveConfig();
        }

        public void Run(string text)
        {
            statusLbl.Text = text;
            Log(text);
            progressBar.Style = ProgressBarStyle.Marquee;
        }

        public void Done(string text = "Done")
        {
            statusLbl.Text = text;
            Log(text);
            progressBar.Style = ProgressBarStyle.Continuous;
        }

        private void logTextBox_TextChanged(object sender, EventArgs e)
        {
            logTextBox.SelectionStart = logTextBox.Text.Length;
            logTextBox.ScrollToCaret();
        }

        private void logTextBox_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            Process.Start(e.LinkText);
        }


        public void Log(string text)
        {
            if (logTextBox.InvokeRequired)
            {
                Invoke(new Action(delegate ()
                {
                    logTextBox.AppendText(text + Environment.NewLine);
                }));
            }
            else
            {
                logTextBox.AppendText(text + Environment.NewLine);
            }
        }
        #endregion

        #region Brute force user info
        private async void bfDescending_Click(object sender, EventArgs e)
        {
            isBfRunning = true;
            if (String.IsNullOrEmpty(bfFileOutput.Text))
            {
                MessageBox.Show("File not selected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (String.IsNullOrEmpty(bfStartUserId.Text))
            {
                MessageBox.Show("Input start user ID first", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                string[] tokens = TxtUtils.LoadToken();
                if (tokens == null || tokens.Length == 0)
                {
                    MessageBox.Show("User info cannot be loaded without a token", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                for (int i = Convert.ToInt32(bfStartUserId.Text); i > 0; i--)
                {
                    if (!isBfRunning)
                        break;

                    try
                    {
                        string url = "https://web.tolstoycomments.com/api/user/profile?userid=" + i + "&siteid=4194&token=" + HttpUtility.UrlEncode(tokens[0], UnicodeEncoding.UTF8) + "&lang=en";
                        await Task.Factory.StartNew(() =>
                        {
                            CollectUserInfo(url);
                        });

                        bfStatus.Text = "Saved user: " + i;
                    }
                    catch
                    {
                        bfStatus.Text = "Error getting info from " + i;
                    }
                }

                Done("All done");
            }
            catch (Exception ex)
            {
                Log("An error occured: " + ex.Message);
                Debug.WriteLine(ex);
            }
        }

        private async void bfAscending_Click(object sender, EventArgs e)
        {
            isBfRunning = true;
            if (String.IsNullOrEmpty(bfFileOutput.Text))
            {
                MessageBox.Show("File not selected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (String.IsNullOrEmpty(bfStartUserId.Text))
            {
                MessageBox.Show("Input start user ID first", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                string[] tokens = TxtUtils.LoadToken();
                if (tokens == null || tokens.Length == 0)
                {
                    MessageBox.Show("User info cannot be loaded without a token", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!File.Exists(bfFileOutput.Text))
                    File.AppendAllText(bfFileOutput.Text, $"User ID,Username,Name,Social,Rating,Comments,Registration,Last visit,Role,Awards,About,Avatar URL\n");

                for (int i = Convert.ToInt32(bfStartUserId.Text); i > 0; i++)
                {
                    if (!isBfRunning)
                        break;

                    try
                    {
                        string url = "https://web.tolstoycomments.com/api/user/profile?userid=" + i + "&siteid=4194&token=" + HttpUtility.UrlEncode(tokens[0], UnicodeEncoding.UTF8) + "&lang=en";
                        await Task.Factory.StartNew(() =>
                        {
                            CollectUserInfo(url);
                        });

                        bfStatus.Text = "Saved user: " + i;
                    }
                    catch
                    {
                        bfStatus.Text = "Error getting info from " + i;
                    }
                }

                Done("All done");
            }
            catch (Exception ex)
            {
                Log("An error occured: " + ex.Message);
                Debug.WriteLine(ex);
            }
        }

        private void bfSelFile_Click(object sender, EventArgs e)
        {
            using (var sfd = new SaveFileDialog())
            {
                sfd.Filter = "CSV (*.csv)|*.csv";
                sfd.FilterIndex = 2;

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    bfFileOutput.Text = sfd.FileName;
                }
            }
        }

        private void bfStop_Click(object sender, EventArgs e)
        {
            isBfRunning = false;
        }
        #endregion

        #region User info
        private void loadZZZBotsBtn_Click(object sender, EventArgs e)
        {
            Log("Loading Z___Z___Z bots... NOTE: Most of his bots are banned");
            string[] lines = Resources.Botsid.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            LoadUserInfo(lines);
        }

        private void loadAdmins_Click(object sender, EventArgs e)
        {
            Log("Loading admins and mods... NOTE: All their roles are default after we exposed them");
            string[] lines = Resources.Adminsid.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            LoadUserInfo(lines);
        }

        private async void LoadUserInfo(string[] userId)
        {
            storeUserId = userId;
            isUserInfoRunning = true;
            try
            {
                userListView.Items.Clear();
                string[] tokens = TxtUtils.LoadToken();
                if (tokens == null || tokens.Length == 0)
                {
                    MessageBox.Show("User info cannot be loaded without a token", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                foreach (string id in userId)
                {
                    if (String.IsNullOrEmpty(id))
                        break;

                    if (!isUserInfoRunning)
                        break;

                    string url = "https://web.tolstoycomments.com/api/user/profile?userid=" + id + "&siteid=4194&token=" + HttpUtility.UrlEncode(tokens[0], UnicodeEncoding.UTF8) + "&lang=en";

                    try
                    {
                        string comms = await Web.RequestGetAsync(url);

                        var usrComms = JsonConvert.DeserializeObject<ProfileComments.Root>(comms);
                        var comments = usrComms.Data.Comments;

                        ListViewItem list = new ListViewItem(usrComms.Data.User.Id.ToString());
                        list.SubItems.Add(usrComms.Data.User.Nick);
                        list.SubItems.Add(usrComms.Data.User.Name);
                        var social = usrComms.Data.UserDetails.Social;
                        foreach (var s in social)
                        {
                            list.SubItems.Add(s.Name + " (" + s.Type + ")");
                            break;
                        }
                        if (social.Length == 0)
                            list.SubItems.Add("");

                        list.SubItems.Add(usrComms.Data.UserDetails.CountRaiting.ToString());
                        list.SubItems.Add(usrComms.Data.UserDetails.CountComment.ToString());
                        list.SubItems.Add(usrComms.Data.UserDetails.DateRegistration.ToString("dd MMM yy HH:mm"));
                        list.SubItems.Add(usrComms.Data.UserDetails.DateLastVisit.ToString("dd MMM yy HH:mm"));
                        list.SubItems.Add(usrComms.Data.UserDetails.Role);
                        list.SubItems.Add(usrComms.Data.UserDetails.AwardsCountUser.ToString());
                        list.SubItems.Add(usrComms.Data.UserDetails.About);
                        list.SubItems.Add(usrComms.Data.User.Ava);

                        userListView.Items.Add(list);
                    }
                    catch (Exception ex)
                    {
                        Done("An error occured: " + ex.Message);
                        Debug.WriteLine(ex);
                    }
                }

                if (Settings.Default.UsrInfoAutoSave)
                {
                    string usrInfoPath = Path.Combine(Program.usrInfoDir, "User info - " + DateTime.Now.ToString("dd MMMM yyyy HH.mm.ss") + ".csv");
                    Directory.CreateDirectory(Path.GetDirectoryName(usrInfoPath));
                    UsrInfoSaveCsv(usrInfoPath);
                }

                Done("All done");
            }
            catch (Exception ex)
            {
                Done("An error occured: " + ex.Message);
                Debug.WriteLine(ex);
            }
        }

        private void CollectUserInfo(string url)
        {
            string comms = Web.GET(url);

            var usrComms = JsonConvert.DeserializeObject<ProfileComments.Root>(comms);

            if (!File.Exists(bfFileOutput.Text))
                File.AppendAllText(bfFileOutput.Text, "User ID,Username,Name,Social,Rating,Comments,Registration,Last visit,Role,Awards,About,Avatar URL\n");

            using (TextWriter TW = new StreamWriter(bfFileOutput.Text, append: true))
            {
                string id = Csv.Escape(usrComms.Data.User.Id.ToString());
                string nick = Csv.Escape(usrComms.Data.User.Nick);
                string name = Csv.Escape(usrComms.Data.User.Name);

                var social = usrComms.Data.UserDetails.Social;
                string soc = "";

                foreach (var s in social)
                {
                    soc = Csv.Escape(s.Name + " (" + s.Type + ")");
                    break;
                }

                string countRating = Csv.Escape(usrComms.Data.UserDetails.CountRaiting.ToString());
                string countComment = Csv.Escape(usrComms.Data.UserDetails.CountComment.ToString());
                string dateRegistration = Csv.Escape(usrComms.Data.UserDetails.DateRegistration.ToString("dd MMM yy HH:mm"));
                string dateLastVisit = Csv.Escape(usrComms.Data.UserDetails.DateLastVisit.ToString("dd MMM yy HH:mm"));
                string role = Csv.Escape(usrComms.Data.UserDetails.Role);
                string awardsCountUser = Csv.Escape(usrComms.Data.UserDetails.AwardsCountUser.ToString());
                string about = Csv.Escape(usrComms.Data.UserDetails.About);
                string avatar = Csv.Escape(usrComms.Data.User.Ava);

                TW.WriteLine($"{id},{nick},{name},{soc},{countRating},{countComment},{dateRegistration},{dateLastVisit},{role},{awardsCountUser},{about},{avatar}");
            }
        }

        private void usrInfoStopBtn_Click(object sender, EventArgs e)
        {
            isUserInfoRunning = false;
            autoReloadTimer.Stop();
        }

        private void usrInfoEditFavUserId_Click(object sender, EventArgs e)
        {
            if (!File.Exists(Program.favUsersFilePath))
                File.WriteAllText(Program.favUsersFilePath, "# Add user ID each lines");
            Process.Start("explorer.exe", Program.favUsersFilePath);
        }

        private void usrInfoLoadFavUserId_Click(object sender, EventArgs e)
        {
            var users = TxtUtils.LoadFavUsers();
            if (users != null)
                LoadUserInfo(users);
            else
                Log("No users in list");
        }

        private void usrInfoExportToCsv_Click(object sender, EventArgs e)
        {
            if (userListView.Items.Count > 0)
            {
                using (var sfd = new SaveFileDialog())
                {
                    sfd.FileName = "User info - " + DateTime.Now.ToString("dd MMMM yyyy HH.mm.ss");
                    sfd.Filter = "CSV (*.csv)|*.csv";
                    sfd.FilterIndex = 2;

                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        UsrInfoSaveCsv(sfd.FileName);
                    }
                }
            }
            else
            {
                Form1.instance.Log("There is nothing in the list");
            }
        }

        private void UsrInfoSaveCsv(string path)
        {
            using (TextWriter TW = new StreamWriter(path))
            {
                for (int i = 0; i < userListView.Items.Count; i++)
                {
                    string id = Csv.Escape(userListView.Items[i].SubItems[0].Text);
                    string nick = Csv.Escape(userListView.Items[i].SubItems[1].Text);
                    string name = Csv.Escape(userListView.Items[i].SubItems[2].Text);
                    string social = Csv.Escape(userListView.Items[i].SubItems[3].Text);
                    string countRating = Csv.Escape(userListView.Items[i].SubItems[4].Text);
                    string countComment = Csv.Escape(userListView.Items[i].SubItems[5].Text);
                    string dateRegistration = Csv.Escape(userListView.Items[i].SubItems[6].Text);
                    string dateLastVisit = Csv.Escape(userListView.Items[i].SubItems[7].Text);
                    string role = Csv.Escape(userListView.Items[i].SubItems[8].Text);
                    string awardsCountUser = Csv.Escape(userListView.Items[i].SubItems[9].Text);
                    string about = Csv.Escape(userListView.Items[i].SubItems[10].Text);
                    string avatar = Csv.Escape(userListView.Items[i].SubItems[11].Text);

                    if (i == 0)
                        TW.WriteLine("User ID,Username,Name,Social,Rating,Comments,Registration,Last visit,Role,Awards,About,Avatar URL");
                    TW.WriteLine($"{id},{nick},{name},{social},{countRating},{countComment},{dateRegistration},{dateLastVisit},{role},{awardsCountUser},{about},{avatar}");
                }
            }
            Form1.instance.Log("CSV saved to " + path);
        }

        private void autoReloadTimer_Tick(object sender, EventArgs e)
        {
            var timeleft = autoReloadTime - DateTime.Now;
            if (timeleft.Ticks < 0)
            {
                if (storeUserId != null)
                    LoadUserInfo(storeUserId);
                var hour1 = Int32.Parse(usrInfoAutoReloadH.Text);
                var min1 = Int32.Parse(usrInfoAutoReloadM.Text);
                var sec1 = Int32.Parse(usrInfoAutoReloadS.Text);
                if (hour1 != 0 || min1 != 0 || sec1 != 0)
                {
                    var start = DateTime.Now;
                    var timeSpan = new TimeSpan(0, hour1, min1, sec1);
                    autoReloadTime = start.Add(timeSpan);
                    autoReloadTimer.Start();
                }
            }
            else
            {
                //usrInfoTimerLbl.Text = string.Format("{0:D2}:{0:D2}:{0:D2}", timeleft.Hours, timeleft.Minutes, timeleft.Seconds);
                usrInfoTimerLbl.Text = new DateTime(timeleft.Ticks).ToString("HH:mm:ss");
            }
        }

        private void usrInfoStartTimer_Click(object sender, EventArgs e)
        {
            try
            {
                var hour1 = Int32.Parse(usrInfoAutoReloadH.Text);
                var min1 = Int32.Parse(usrInfoAutoReloadM.Text);
                var sec1 = Int32.Parse(usrInfoAutoReloadS.Text);
                if (hour1 != 0 || min1 != 0 || sec1 != 0)
                {
                    var start = DateTime.Now;
                    var timeSpan = new TimeSpan(0, hour1, min1, sec1);
                    autoReloadTime = start.Add(timeSpan);
                    autoReloadTimer.Start();
                }
            }
            catch
            {

            }
        }
        #endregion

        #region Message
        private async void addFriend_Click(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(messengerToken.Text))
                {
                    string token = messengerToken.Text;

                    string url = "https://web.tolstoycomments.com/api/user/profile?userid=" + messengerToUserId.Text + "&siteid=4657&token=" + HttpUtility.UrlEncode(token, UnicodeEncoding.UTF8) + "&lang=en";

                    string comms = await Web.RequestGetAsync(url);

                    var usrComms = JsonConvert.DeserializeObject<ProfileComments.Root>(comms);

                    if (usrComms.Data.User.Name != usrComms.Data.User.Nick)
                        friendsList.Items.Add($"{usrComms.Data.User.Id}|{usrComms.Data.User.Name} ({usrComms.Data.User.Nick})");
                    else
                        friendsList.Items.Add($"{usrComms.Data.User.Id}|{usrComms.Data.User.Name}");
                }
                else
                {
                    Log("Could not get user info without token");

                    friendsList.Items.Add(messengerToUserId.Text);
                }
                UpdateFriendsFile();
            }
            catch (WebException ex)
            {
                using (var stream = ex.Response.GetResponseStream())
                using (var reader = new StreamReader(stream))
                {
                    Form1.instance.Log($"Error adding user info... {Str.TranslateError(reader.ReadToEnd())}");
                    Debug.WriteLine(reader.ReadToEnd());
                }
            }
            catch (Exception ex)
            {
                Log(ex.Message);
            }
        }

        private void removeFriend_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (string s in friendsList.SelectedItems.OfType<string>().ToList())
                    friendsList.Items.Remove(s);

                UpdateFriendsFile();
            }
            catch (Exception ex)
            {
                Log(ex.Message);
            }
        }

        private void UpdateFriendsFile()
        {
            StreamWriter SaveFile = new StreamWriter(Program.friendsFilePath);
            foreach (var item in friendsList.Items)
            {
                SaveFile.WriteLine(item.ToString());
            }
            SaveFile.Close();
        }

        private void loadMessage_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(messengerToken.Text))
            {
                MessageBox.Show("Token text box is empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (String.IsNullOrEmpty(messengerToUserId.Text))
            {
                MessageBox.Show("User ID text box is empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            loadMsg();
        }

        private async void sendMessageBtn_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(messengerToken.Text))
            {
                MessageBox.Show("Token text box is empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (String.IsNullOrEmpty(messengerToUserId.Text))
            {
                MessageBox.Show("User ID text box is empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                var url = "https://web.tolstoycomments.com/api/chatmessage/create?token=" + HttpUtility.UrlEncode(messengerToken.Text, UnicodeEncoding.UTF8) + "&lang=en";

                var postData = @"{""user_id"":" + messengerToUserId.Text;
                postData += @",""site_id"":4657,""text"":""" + writtenMessage.Text;
                postData += @""",""token"":""" + HttpUtility.UrlEncode(messengerToken.Text, UnicodeEncoding.UTF8);
                postData += @""",""answer_comment_id"":null,""attache"":[],""lang"":""en"",""uuid"":""bq44uDo40Uqp9fHvCo-FNw"",""time_zone_client"":120}""";

                await Web.RequestPostAsync(url, postData);

                loadMsg();

                writtenMessage.Text = "";
            }
            catch (WebException ex)
            {
                using (var stream = ex.Response.GetResponseStream())
                using (var reader = new StreamReader(stream))
                {
                    Form1.instance.Log($"Error sending a message.. {Str.TranslateError(reader.ReadToEnd())}");
                    Debug.WriteLine(reader.ReadToEnd());
                }
            }
            catch (Exception ex)
            {
                Log(ex.Message);
            }
        }

        private async void loadMsg()
        {
            try
            {
                string url = "https://web.tolstoycomments.com/api/chatmessage/last?userid=" + messengerToUserId.Text + "&token=" + HttpUtility.UrlEncode(messengerToken.Text, UnicodeEncoding.UTF8) + "&siteId=4657";

                string comms = await Web.RequestGetAsync(url);

                var usrComms = JsonConvert.DeserializeObject<PrivateMessages.Root>(comms);
                var comments = usrComms.Data.Comments;

                messages.Text = "";

                foreach (var c in comments)
                {
                    try
                    {
                        string name = c.User.Name;
                        if (c.User.Name != c.User.Nick)
                            name = c.User.Name + " (" + c.User.Nick + ")";

                        messages.AppendText($"[{name}] {HttpUtility.HtmlDecode(c.TextTemplate)} \n\n");
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex);
                    }
                }
            }
            catch (Exception ex)
            {
                Done(ex.Message);
            }
        }

        private void friendsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            string item = friendsList.GetItemText(friendsList.SelectedItem);
            string[] i = item.Split('|');
            if (i.Length > -1)
            {
                messengerToUserId.Text = i[0];
                loadMsg();
            }
        }

        private void messages_TextChanged(object sender, EventArgs e)
        {
            // set the current caret position to the end
            messages.SelectionStart = messages.Text.Length;
            // scroll it automatically
            messages.ScrollToCaret();
        }
        #endregion

        #region Token
        private void saveTokensBtn_Click(object sender, EventArgs e)
        {
            File.WriteAllText(Path.Combine(Program.exePath, "tokens.txt"), tokensTextBox.Text);
        }

        private void openExtEditor_Click(object sender, EventArgs e)
        {
            if (!File.Exists(Program.tokensFilePath))
                File.WriteAllText(Program.tokensFilePath, "# Add tokens each lines");
            Process.Start("explorer.exe", Program.tokensFilePath);
        }

        bool isLoadingTokenInfo = false;
        private async void loadTokenInfo_Click(object sender, EventArgs e)
        {
            if (!isLoadingTokenInfo)
            {
                isLoadingTokenInfo = true;
                tokenInfoList.Items.Clear();
                try
                {
                    string[] tokenList = File.ReadAllLines(Program.tokensFilePath);
                    foreach (string token in tokenList)
                    {
                        if (!isLoadingTokenInfo)
                            break;
                        try
                        {
                            string userInfo = await Web.RequestGetAsync("https://web.tolstoycomments.com/api/widget/293f8d4ddfb340fe8bce9217c593deae/events/4659?token=" + HttpUtility.UrlEncode(token, UnicodeEncoding.UTF8));

                            var usrInfo = JsonConvert.DeserializeObject<UserInfoWidget.Root>(userInfo);

                            string name = usrInfo.Data.Name;
                            if (usrInfo.Data.Name != usrInfo.Data.Nick)
                                name = usrInfo.Data.Name + " (" + usrInfo.Data.Nick + ")";

                            ListViewItem list = new ListViewItem(token);
                            list.SubItems.Add($"ID: {usrInfo.Data.Id}");
                            list.SubItems.Add(name);

                            tokenInfoList.Items.Add(list);
                        }
                        catch (Exception ex)
                        {
                            ListViewItem list = new ListViewItem(token);
                            list.SubItems.Add("");
                            list.SubItems.Add("Token is invalid. It might be invalid, expired or banned");

                            tokenInfoList.Items.Add(list);
                            //Log("Token is invalid. It might be invalid, expired or banned");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Done(ex.Message);
                }
            }
            else
                isLoadingTokenInfo = false;
        }

        private void massSendEditUsers_Click(object sender, EventArgs e)
        {
            if (!File.Exists(Program.usrIdMassMsgFilePath))
                File.WriteAllText(Program.usrIdMassMsgFilePath, "");
            Process.Start("explorer.exe", Program.usrIdMassMsgFilePath);
        }

        bool massSendRunning;
        private async void massSendStart_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(massSendToken.Text))
            {
                MessageBox.Show("Token text box is empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (String.IsNullOrEmpty(massSendMsg.Text))
            {
                MessageBox.Show("Message is empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (File.Exists(Program.usrIdMassMsgFilePath))
            {
                massSendRunning = true;
                string[] users = File.ReadAllLines(Program.usrIdMassMsgFilePath);
                foreach (string user in users)
                {
                    massSendStatus.Text = $"Sending message to {user} ({Array.IndexOf(users, user) + 1} out of {users.Count()})";

                    if (!massSendRunning)
                        break;

                    try
                    {
                        try
                        {
                            var url = "https://web.tolstoycomments.com/api/chatmessage/create?token=" + HttpUtility.UrlEncode(massSendToken.Text, UnicodeEncoding.UTF8) + "&lang=en";

                            var postData = @"{""user_id"":" + user;
                            postData += @",""site_id"":4657,""text"":""" + massSendMsg.Text;
                            postData += @""",""token"":""" + HttpUtility.UrlEncode(massSendToken.Text, UnicodeEncoding.UTF8);
                            postData += @""",""answer_comment_id"":null,""attache"":[],""lang"":""en"",""uuid"":""bq44uDo40Uqp9fHvCo-FNw"",""time_zone_client"":120}""";

                            await Web.RequestPostAsync(url, postData);
                            await Task.Delay(2000);
                        }
                        catch (WebException ex)
                        {
                            using (var stream = ex.Response.GetResponseStream())
                            using (var reader = new StreamReader(stream))
                            {
                                Form1.instance.Log($"Error sending a message.. {Str.TranslateError(reader.ReadToEnd())}");
                                Debug.WriteLine(reader.ReadToEnd());
                            }
                        }
                        catch (Exception ex)
                        {
                            Log(ex.Message);
                        }
                    }
                    catch
                    {

                    }
                }

                massSendStatus.Text = "Done";
            }
            else
                MessageBox.Show("File " + Program.usrIdMassMsgFilePath + " does not exist", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void massSendStop_Click(object sender, EventArgs e)
        {
            massSendRunning = false;
        }

        private void exportTokenInfo_Click(object sender, EventArgs e)
        {
            if (tokenInfoList.Items.Count > 0)
            {
                using (var sfd = new SaveFileDialog())
                {
                    sfd.FileName = "Token List - " + DateTime.Now.ToString("dd MMMM yyyy HH.mm");
                    sfd.Filter = "CSV (*.csv)|*.csv";

                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        using (TextWriter TW = new StreamWriter(sfd.FileName))
                        {
                            for (int i = 0; i < tokenInfoList.Items.Count; i++)
                            {
                                string token = Csv.Escape(tokenInfoList.Items[i].SubItems[0].Text);
                                string userId = Csv.Escape(tokenInfoList.Items[i].SubItems[1].Text);
                                string name = Csv.Escape(tokenInfoList.Items[i].SubItems[2].Text);
                                if (i == 0)
                                    TW.WriteLine($"Token,User ID,Name");
                                TW.WriteLine($"{token},{userId},{name}");
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
        #endregion

        #region Config
        private void SaveConfig()
        {
            if (userControl != null)
            {
                Settings.Default.RtUrl = userControl.rtUrl.Text;
                Settings.Default.SetMaxVoting = userControl.setMaxVote.Checked;
                Settings.Default.MaxVoting = userControl.maxVote.Value;
                Settings.Default.ParallelVoting = userControl.parallelVotingChkBox.Checked;
                Settings.Default.Parallel = userControl.parallelUpDown.Value;
                Settings.Default.SetDelay = userControl.setDelay.Checked;
                Settings.Default.Delay = userControl.delay.Value;
                Settings.Default.SetMinusRatingBetween = userControl.downvoteBetween.Checked;
                Settings.Default.MinusRatingMin = userControl.minusRatingMin.Value;
                Settings.Default.MinusRatingMax = userControl.minusRatingMax.Value;
                Settings.Default.SetPlusRatingBetween = userControl.upvoteBetween.Checked;
                Settings.Default.PlusRatingMin = userControl.plusRatingMin.Value;
                Settings.Default.PlusRatingMax = userControl.plusRatingMax.Value;
                Settings.Default.Save();
            }
        }
        #endregion
    }
}
