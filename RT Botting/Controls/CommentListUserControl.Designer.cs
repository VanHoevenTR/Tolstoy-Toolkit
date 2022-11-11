namespace Tolstoy_Toolkit
{
    partial class CommentListUserControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.PasteBtn = new System.Windows.Forms.Button();
            this.commentViewer = new System.Windows.Forms.RichTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.goBackBtn = new System.Windows.Forms.Button();
            this.DownVote = new System.Windows.Forms.Button();
            this.UpVote = new System.Windows.Forms.Button();
            this.loadComms = new System.Windows.Forms.Button();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.commListView = new System.Windows.Forms.ListView();
            this.dateTimeCol = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.userIdCol = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.userCol = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.votesCol = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.commsCol = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.commIdCol = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.total = new System.Windows.Forms.Label();
            this.ratingUsers = new System.Windows.Forms.RichTextBox();
            this.profilePic = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label8 = new System.Windows.Forms.Label();
            this.ratingLbl = new System.Windows.Forms.Label();
            this.lastVisitLbl = new System.Windows.Forms.Label();
            this.idLbl = new System.Windows.Forms.Label();
            this.userNameLbl = new System.Windows.Forms.Label();
            this.nameLbl = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.totalRatingLbl = new System.Windows.Forms.Label();
            this.parallelUpDown = new System.Windows.Forms.NumericUpDown();
            this.parallelVotingChkBox = new System.Windows.Forms.CheckBox();
            this.plusRatingMax = new System.Windows.Forms.NumericUpDown();
            this.minusRatingMax = new System.Windows.Forms.NumericUpDown();
            this.plusRatingMin = new System.Windows.Forms.NumericUpDown();
            this.minusRatingMin = new System.Windows.Forms.NumericUpDown();
            this.upvoteBetween = new System.Windows.Forms.CheckBox();
            this.downvoteBetween = new System.Windows.Forms.CheckBox();
            this.setDelay = new System.Windows.Forms.CheckBox();
            this.delay = new System.Windows.Forms.NumericUpDown();
            this.setMaxVote = new System.Windows.Forms.CheckBox();
            this.maxVote = new System.Windows.Forms.NumericUpDown();
            this.rtUrl = new System.Windows.Forms.TextBox();
            this.exportToCsv = new System.Windows.Forms.Button();
            this.loadHistory = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.profilePic)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.parallelUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.plusRatingMax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.minusRatingMax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.plusRatingMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.minusRatingMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.delay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxVote)).BeginInit();
            this.SuspendLayout();
            // 
            // PasteBtn
            // 
            this.PasteBtn.Font = new System.Drawing.Font("Segoe UI Emoji", 8.25F);
            this.PasteBtn.Location = new System.Drawing.Point(626, 1);
            this.PasteBtn.Name = "PasteBtn";
            this.PasteBtn.Size = new System.Drawing.Size(31, 23);
            this.PasteBtn.TabIndex = 25;
            this.PasteBtn.Text = "📋";
            this.PasteBtn.UseVisualStyleBackColor = true;
            this.PasteBtn.Click += new System.EventHandler(this.PasteBtn_Click);
            // 
            // commentViewer
            // 
            this.commentViewer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.commentViewer.Location = new System.Drawing.Point(2, 99);
            this.commentViewer.Name = "commentViewer";
            this.commentViewer.ReadOnly = true;
            this.commentViewer.Size = new System.Drawing.Size(213, 286);
            this.commentViewer.TabIndex = 0;
            this.commentViewer.Text = "";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(559, 32);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(24, 13);
            this.label4.TabIndex = 23;
            this.label4.Text = "ms.";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(4, 5);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(148, 13);
            this.label7.TabIndex = 17;
            this.label7.Text = "Full article url or profile ID: ";
            // 
            // goBackBtn
            // 
            this.goBackBtn.Enabled = false;
            this.goBackBtn.Location = new System.Drawing.Point(7, 87);
            this.goBackBtn.Name = "goBackBtn";
            this.goBackBtn.Size = new System.Drawing.Size(75, 23);
            this.goBackBtn.TabIndex = 24;
            this.goBackBtn.Text = "Go back";
            this.goBackBtn.UseVisualStyleBackColor = true;
            this.goBackBtn.Click += new System.EventHandler(this.GoBackBtn_Click);
            // 
            // DownVote
            // 
            this.DownVote.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DownVote.ForeColor = System.Drawing.Color.DarkRed;
            this.DownVote.Location = new System.Drawing.Point(381, 87);
            this.DownVote.Name = "DownVote";
            this.DownVote.Size = new System.Drawing.Size(127, 23);
            this.DownVote.TabIndex = 15;
            this.DownVote.Text = "Mass downvote";
            this.DownVote.UseVisualStyleBackColor = true;
            this.DownVote.Click += new System.EventHandler(this.DownVote_Click);
            // 
            // UpVote
            // 
            this.UpVote.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UpVote.ForeColor = System.Drawing.Color.ForestGreen;
            this.UpVote.Location = new System.Drawing.Point(513, 87);
            this.UpVote.Name = "UpVote";
            this.UpVote.Size = new System.Drawing.Size(127, 23);
            this.UpVote.TabIndex = 16;
            this.UpVote.Text = "Mass upvote";
            this.UpVote.UseVisualStyleBackColor = true;
            this.UpVote.Click += new System.EventHandler(this.Upvote_Click);
            // 
            // loadComms
            // 
            this.loadComms.Location = new System.Drawing.Point(663, 1);
            this.loadComms.Name = "loadComms";
            this.loadComms.Size = new System.Drawing.Size(75, 23);
            this.loadComms.TabIndex = 20;
            this.loadComms.Text = "Load";
            this.loadComms.UseVisualStyleBackColor = true;
            this.loadComms.Click += new System.EventHandler(this.LoadComments_Click);
            // 
            // cancelBtn
            // 
            this.cancelBtn.ForeColor = System.Drawing.Color.Maroon;
            this.cancelBtn.Location = new System.Drawing.Point(663, 87);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(75, 23);
            this.cancelBtn.TabIndex = 18;
            this.cancelBtn.Text = "STOP";
            this.cancelBtn.UseVisualStyleBackColor = true;
            this.cancelBtn.Click += new System.EventHandler(this.CancelBtn_Click);
            // 
            // commListView
            // 
            this.commListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.dateTimeCol,
            this.userIdCol,
            this.userCol,
            this.votesCol,
            this.commsCol,
            this.commIdCol});
            this.commListView.FullRowSelect = true;
            this.commListView.HideSelection = false;
            this.commListView.Location = new System.Drawing.Point(5, 116);
            this.commListView.Name = "commListView";
            this.commListView.Size = new System.Drawing.Size(734, 309);
            this.commListView.TabIndex = 22;
            this.commListView.UseCompatibleStateImageBehavior = false;
            this.commListView.View = System.Windows.Forms.View.Details;
            this.commListView.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.commListView_ColumnClick);
            this.commListView.SelectedIndexChanged += new System.EventHandler(this.viewComment_SelectedIndexChanged);
            this.commListView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.commListView_KeyDown);
            this.commListView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.viewProfileComments_MouseDoubleClick);
            // 
            // dateTimeCol
            // 
            this.dateTimeCol.Text = "Date & time";
            this.dateTimeCol.Width = 100;
            // 
            // userIdCol
            // 
            this.userIdCol.Text = "User ID";
            this.userIdCol.Width = 54;
            // 
            // userCol
            // 
            this.userCol.Text = "User";
            this.userCol.Width = 110;
            // 
            // votesCol
            // 
            this.votesCol.Tag = "Numeric";
            this.votesCol.Text = "👍";
            this.votesCol.Width = 37;
            // 
            // commsCol
            // 
            this.commsCol.Text = "Comment";
            this.commsCol.Width = 345;
            // 
            // commIdCol
            // 
            this.commIdCol.Text = "Comment ID";
            this.commIdCol.Width = 77;
            // 
            // total
            // 
            this.total.AutoSize = true;
            this.total.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.total.Location = new System.Drawing.Point(6, 9);
            this.total.Name = "total";
            this.total.Size = new System.Drawing.Size(69, 13);
            this.total.TabIndex = 31;
            this.total.Text = "Total rating:";
            // 
            // ratingUsers
            // 
            this.ratingUsers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.ratingUsers.Location = new System.Drawing.Point(3, 32);
            this.ratingUsers.Name = "ratingUsers";
            this.ratingUsers.ReadOnly = true;
            this.ratingUsers.Size = new System.Drawing.Size(213, 353);
            this.ratingUsers.TabIndex = 1;
            this.ratingUsers.Text = "";
            // 
            // profilePic
            // 
            this.profilePic.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.profilePic.Location = new System.Drawing.Point(170, 5);
            this.profilePic.Name = "profilePic";
            this.profilePic.Size = new System.Drawing.Size(45, 45);
            this.profilePic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.profilePic.TabIndex = 32;
            this.profilePic.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(225, 62);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(27, 13);
            this.label3.TabIndex = 38;
            this.label3.Text = "and";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(607, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 13);
            this.label1.TabIndex = 38;
            this.label1.Text = "and";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(743, 2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.Padding = new System.Drawing.Point(6, 6);
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(227, 423);
            this.tabControl1.TabIndex = 40;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.commentViewer);
            this.tabPage1.Controls.Add(this.profilePic);
            this.tabPage1.Controls.Add(this.label8);
            this.tabPage1.Controls.Add(this.ratingLbl);
            this.tabPage1.Controls.Add(this.lastVisitLbl);
            this.tabPage1.Controls.Add(this.idLbl);
            this.tabPage1.Controls.Add(this.userNameLbl);
            this.tabPage1.Controls.Add(this.nameLbl);
            this.tabPage1.Location = new System.Drawing.Point(4, 28);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(219, 391);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Comment viewer";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(4, 82);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(95, 13);
            this.label8.TabIndex = 31;
            this.label8.Text = "Comment viewer:";
            // 
            // ratingLbl
            // 
            this.ratingLbl.AutoSize = true;
            this.ratingLbl.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ratingLbl.Location = new System.Drawing.Point(4, 63);
            this.ratingLbl.Name = "ratingLbl";
            this.ratingLbl.Size = new System.Drawing.Size(44, 13);
            this.ratingLbl.TabIndex = 31;
            this.ratingLbl.Text = "Rating:";
            // 
            // lastVisitLbl
            // 
            this.lastVisitLbl.AutoSize = true;
            this.lastVisitLbl.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lastVisitLbl.Location = new System.Drawing.Point(4, 43);
            this.lastVisitLbl.Name = "lastVisitLbl";
            this.lastVisitLbl.Size = new System.Drawing.Size(53, 13);
            this.lastVisitLbl.TabIndex = 31;
            this.lastVisitLbl.Text = "Last visit:";
            // 
            // idLbl
            // 
            this.idLbl.AutoSize = true;
            this.idLbl.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.idLbl.Location = new System.Drawing.Point(112, 63);
            this.idLbl.Name = "idLbl";
            this.idLbl.Size = new System.Drawing.Size(21, 13);
            this.idLbl.TabIndex = 31;
            this.idLbl.Text = "ID:";
            // 
            // userNameLbl
            // 
            this.userNameLbl.AutoSize = true;
            this.userNameLbl.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.userNameLbl.Location = new System.Drawing.Point(4, 24);
            this.userNameLbl.Name = "userNameLbl";
            this.userNameLbl.Size = new System.Drawing.Size(33, 13);
            this.userNameLbl.TabIndex = 31;
            this.userNameLbl.Text = "User:";
            // 
            // nameLbl
            // 
            this.nameLbl.AutoSize = true;
            this.nameLbl.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nameLbl.Location = new System.Drawing.Point(4, 5);
            this.nameLbl.Name = "nameLbl";
            this.nameLbl.Size = new System.Drawing.Size(39, 13);
            this.nameLbl.TabIndex = 31;
            this.nameLbl.Text = "Name:";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.totalRatingLbl);
            this.tabPage2.Controls.Add(this.ratingUsers);
            this.tabPage2.Controls.Add(this.total);
            this.tabPage2.Location = new System.Drawing.Point(4, 28);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(219, 391);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Total rating";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // totalRatingLbl
            // 
            this.totalRatingLbl.AutoSize = true;
            this.totalRatingLbl.Location = new System.Drawing.Point(77, 9);
            this.totalRatingLbl.Name = "totalRatingLbl";
            this.totalRatingLbl.Size = new System.Drawing.Size(19, 13);
            this.totalRatingLbl.TabIndex = 32;
            this.totalRatingLbl.Text = "---";
            // 
            // parallelUpDown
            // 
            this.parallelUpDown.Location = new System.Drawing.Point(310, 28);
            this.parallelUpDown.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.parallelUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.parallelUpDown.Name = "parallelUpDown";
            this.parallelUpDown.Size = new System.Drawing.Size(44, 22);
            this.parallelUpDown.TabIndex = 42;
            this.parallelUpDown.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            // 
            // parallelVotingChkBox
            // 
            this.parallelVotingChkBox.AutoSize = true;
            this.parallelVotingChkBox.Location = new System.Drawing.Point(208, 30);
            this.parallelVotingChkBox.Name = "parallelVotingChkBox";
            this.parallelVotingChkBox.Size = new System.Drawing.Size(99, 17);
            this.parallelVotingChkBox.TabIndex = 41;
            this.parallelVotingChkBox.Text = "Parallel voting";
            this.parallelVotingChkBox.UseVisualStyleBackColor = true;
            // 
            // plusRatingMax
            // 
            this.plusRatingMax.Location = new System.Drawing.Point(643, 59);
            this.plusRatingMax.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.plusRatingMax.Name = "plusRatingMax";
            this.plusRatingMax.Size = new System.Drawing.Size(45, 22);
            this.plusRatingMax.TabIndex = 36;
            // 
            // minusRatingMax
            // 
            this.minusRatingMax.Location = new System.Drawing.Point(262, 59);
            this.minusRatingMax.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.minusRatingMax.Minimum = new decimal(new int[] {
            1000000,
            0,
            0,
            -2147483648});
            this.minusRatingMax.Name = "minusRatingMax";
            this.minusRatingMax.Size = new System.Drawing.Size(45, 22);
            this.minusRatingMax.TabIndex = 36;
            // 
            // plusRatingMin
            // 
            this.plusRatingMin.Location = new System.Drawing.Point(554, 59);
            this.plusRatingMin.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.plusRatingMin.Name = "plusRatingMin";
            this.plusRatingMin.Size = new System.Drawing.Size(45, 22);
            this.plusRatingMin.TabIndex = 36;
            // 
            // minusRatingMin
            // 
            this.minusRatingMin.Location = new System.Drawing.Point(174, 59);
            this.minusRatingMin.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.minusRatingMin.Minimum = new decimal(new int[] {
            1000000,
            0,
            0,
            -2147483648});
            this.minusRatingMin.Name = "minusRatingMin";
            this.minusRatingMin.Size = new System.Drawing.Size(45, 22);
            this.minusRatingMin.TabIndex = 36;
            // 
            // upvoteBetween
            // 
            this.upvoteBetween.AutoSize = true;
            this.upvoteBetween.Location = new System.Drawing.Point(403, 62);
            this.upvoteBetween.Name = "upvoteBetween";
            this.upvoteBetween.Size = new System.Drawing.Size(145, 17);
            this.upvoteBetween.TabIndex = 35;
            this.upvoteBetween.Text = "Upvote rating between";
            this.upvoteBetween.UseVisualStyleBackColor = true;
            // 
            // downvoteBetween
            // 
            this.downvoteBetween.AutoSize = true;
            this.downvoteBetween.Location = new System.Drawing.Point(7, 61);
            this.downvoteBetween.Name = "downvoteBetween";
            this.downvoteBetween.Size = new System.Drawing.Size(161, 17);
            this.downvoteBetween.TabIndex = 35;
            this.downvoteBetween.Text = "Downvote rating between";
            this.downvoteBetween.UseVisualStyleBackColor = true;
            // 
            // setDelay
            // 
            this.setDelay.AutoSize = true;
            this.setDelay.Location = new System.Drawing.Point(401, 31);
            this.setDelay.Name = "setDelay";
            this.setDelay.Size = new System.Drawing.Size(93, 17);
            this.setDelay.TabIndex = 30;
            this.setDelay.Text = "Delay voting:";
            this.setDelay.UseVisualStyleBackColor = true;
            // 
            // delay
            // 
            this.delay.Location = new System.Drawing.Point(495, 29);
            this.delay.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.delay.Name = "delay";
            this.delay.Size = new System.Drawing.Size(58, 22);
            this.delay.TabIndex = 21;
            this.delay.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
            // 
            // setMaxVote
            // 
            this.setMaxVote.AutoSize = true;
            this.setMaxVote.Location = new System.Drawing.Point(7, 31);
            this.setMaxVote.Name = "setMaxVote";
            this.setMaxVote.Size = new System.Drawing.Size(86, 17);
            this.setMaxVote.TabIndex = 29;
            this.setMaxVote.Text = "Max voting:";
            this.setMaxVote.UseVisualStyleBackColor = true;
            // 
            // maxVote
            // 
            this.maxVote.Location = new System.Drawing.Point(94, 29);
            this.maxVote.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.maxVote.Name = "maxVote";
            this.maxVote.Size = new System.Drawing.Size(58, 22);
            this.maxVote.TabIndex = 27;
            this.maxVote.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // rtUrl
            // 
            this.rtUrl.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.rtUrl.Location = new System.Drawing.Point(158, 1);
            this.rtUrl.Name = "rtUrl";
            this.rtUrl.Size = new System.Drawing.Size(462, 22);
            this.rtUrl.TabIndex = 19;
            // 
            // exportToCsv
            // 
            this.exportToCsv.Location = new System.Drawing.Point(101, 87);
            this.exportToCsv.Name = "exportToCsv";
            this.exportToCsv.Size = new System.Drawing.Size(127, 23);
            this.exportToCsv.TabIndex = 43;
            this.exportToCsv.Text = "Export to CSV";
            this.exportToCsv.UseVisualStyleBackColor = true;
            this.exportToCsv.Click += new System.EventHandler(this.exportToCsv_Click);
            // 
            // loadHistory
            // 
            this.loadHistory.Location = new System.Drawing.Point(233, 87);
            this.loadHistory.Name = "loadHistory";
            this.loadHistory.Size = new System.Drawing.Size(127, 23);
            this.loadHistory.TabIndex = 44;
            this.loadHistory.Text = "Load history";
            this.loadHistory.UseVisualStyleBackColor = true;
            this.loadHistory.Click += new System.EventHandler(this.loadHistory_Click);
            // 
            // CommentListUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.loadHistory);
            this.Controls.Add(this.exportToCsv);
            this.Controls.Add(this.parallelUpDown);
            this.Controls.Add(this.parallelVotingChkBox);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.plusRatingMax);
            this.Controls.Add(this.minusRatingMax);
            this.Controls.Add(this.plusRatingMin);
            this.Controls.Add(this.minusRatingMin);
            this.Controls.Add(this.upvoteBetween);
            this.Controls.Add(this.downvoteBetween);
            this.Controls.Add(this.PasteBtn);
            this.Controls.Add(this.setDelay);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.delay);
            this.Controls.Add(this.setMaxVote);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.goBackBtn);
            this.Controls.Add(this.DownVote);
            this.Controls.Add(this.maxVote);
            this.Controls.Add(this.UpVote);
            this.Controls.Add(this.loadComms);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.rtUrl);
            this.Controls.Add(this.commListView);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.Name = "CommentListUserControl";
            this.Size = new System.Drawing.Size(977, 432);
            ((System.ComponentModel.ISupportInitialize)(this.profilePic)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.parallelUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.plusRatingMax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.minusRatingMax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.plusRatingMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.minusRatingMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.delay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxVote)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Button PasteBtn;
        internal System.Windows.Forms.CheckBox setDelay;
        internal System.Windows.Forms.RichTextBox commentViewer;
        internal System.Windows.Forms.Label label4;
        internal System.Windows.Forms.NumericUpDown delay;
        internal System.Windows.Forms.CheckBox setMaxVote;
        internal System.Windows.Forms.Label label7;
        internal System.Windows.Forms.Button goBackBtn;
        internal System.Windows.Forms.Button DownVote;
        internal System.Windows.Forms.NumericUpDown maxVote;
        internal System.Windows.Forms.Button UpVote;
        internal System.Windows.Forms.Button loadComms;
        internal System.Windows.Forms.Button cancelBtn;
        internal System.Windows.Forms.TextBox rtUrl;
        internal System.Windows.Forms.ListView commListView;
        internal System.Windows.Forms.ColumnHeader dateTimeCol;
        internal System.Windows.Forms.ColumnHeader userIdCol;
        internal System.Windows.Forms.ColumnHeader userCol;
        internal System.Windows.Forms.ColumnHeader votesCol;
        internal System.Windows.Forms.ColumnHeader commsCol;
        internal System.Windows.Forms.ColumnHeader commIdCol;
        internal System.Windows.Forms.Label total;
        internal System.Windows.Forms.RichTextBox ratingUsers;
        internal System.Windows.Forms.PictureBox profilePic;
        internal System.Windows.Forms.CheckBox downvoteBetween;
        internal System.Windows.Forms.NumericUpDown minusRatingMin;
        internal System.Windows.Forms.Label label3;
        internal System.Windows.Forms.NumericUpDown minusRatingMax;
        internal System.Windows.Forms.CheckBox upvoteBetween;
        internal System.Windows.Forms.NumericUpDown plusRatingMin;
        internal System.Windows.Forms.NumericUpDown plusRatingMax;
        internal System.Windows.Forms.Label label1;
        internal System.Windows.Forms.TabControl tabControl1;
        internal System.Windows.Forms.TabPage tabPage1;
        internal System.Windows.Forms.Label label8;
        internal System.Windows.Forms.Label lastVisitLbl;
        internal System.Windows.Forms.Label nameLbl;
        internal System.Windows.Forms.TabPage tabPage2;
        internal System.Windows.Forms.Label totalRatingLbl;
        internal System.Windows.Forms.Label ratingLbl;
        internal System.Windows.Forms.CheckBox parallelVotingChkBox;
        internal System.Windows.Forms.Label idLbl;
        internal System.Windows.Forms.Label userNameLbl;
        internal System.Windows.Forms.NumericUpDown parallelUpDown;
        internal System.Windows.Forms.Button exportToCsv;
        private System.Windows.Forms.Button loadHistory;
    }
}
