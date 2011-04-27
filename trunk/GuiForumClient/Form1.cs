using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DataManagment;
using MessagePack;

namespace GuiForumClient
{
    public partial class Form1 : Form
    {

        public delegate void ForumsChangedInvoker(object sender, ForumsChangedEventArgs e);
        public delegate void ThreadsChangedInvoker(object sender, ThreadsChangedEventArgs e);
        public delegate void PostsChangedInvoker(object sender, PostsChangedEventArgs e);
        public delegate void CurrentPostChangedInvoker(object sender, CurrentPostChangedEventArgs e);
        public delegate void MassegeChangedInvoker(object sender, MassegeChangedEventArgs e);
        public delegate void PopupMassegeChangedInvoker(object sender, PopupMassegeChangedEventArgs e);
        public delegate void FriendsChangedInvoker(object sender, FriendsChangedEventArgs e);
        public delegate void UsersChangedInvoker(object sender, UsersChangedEventArgs e);


        private StatusStrip statusStrip1;
        private ToolStripProgressBar toolStripProgressBar1;
        private ToolStripContainer toolStripContainer1;
        private SplitContainer splitContainer1;
        private MenuStrip menuStrip1;
        private SplitContainer splitContainer2;
        private ToolStrip toolStrip1;
        private ToolStripContainer toolStripContainer2;
        private RichTextBox richTextBox1;
        private IContainer components;
        private BindingSource viewDataBindingSource;
        private BindingSource viewDataBindingSource1;
        private Panel panel1;
        private Database db;
        private ToolStripButton toolStripButton2;
        private ToolStripButton removethread;
        private ToolStripButton toolStripButton1;
        private GuiClient client;

        private Database.ChangeEventHandler change_delegate;
        /*
        private Database.ForumsChangedHandler forums_delegate;
        private Database.ThreadsChangedHandler threads_delegate;
        private Database.PostsChangedHandler posts_delegate;
        private Database.CurrentPostChangedHandler CurrentPost_delegate;
        private Database.MassegeChangedHandler Massege_delegate;
        private Database.FriendsChangedHandler Friends_delegate;
        private Database.UsersChangedHandler Users_delegate;
        */

        private ToolStripMenuItem toolStripMenuItem1;
        private ToolStripMenuItem loginToolStripMenuItem;
        private ToolStripMenuItem logoutToolStripMenuItem;
        private ToolStripMenuItem registerToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem addToolStripMenuItem;
        private ToolStripMenuItem threadToolStripMenuItem;
        private ToolStripMenuItem postToolStripMenuItem;
        private ToolStripButton removePost;
        private ToolStripMenuItem removeToolStripMenuItem;
        private ToolStripMenuItem postToolStripMenuItem1;
        private ToolStripMenuItem thredToolStripMenuItem;
        private ToolStripButton replyButton;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TreeView treeView1;
        private TabPage tabPage4;
        private ToolStrip toolStrip5;
        private ListView users_list;
        private ToolStripMenuItem replayToolStripMenuItem;
        private ToolStripMenuItem helpToolStripMenuItem;
        private ToolStripMenuItem settingsToolStripMenuItem;
        private ToolStripMenuItem aboutToolStripMenuItem;
        private TabPage tabPage3;
        private ListView friends_list;
        private ToolStrip toolStrip2;
        private ToolStrip toolStrip3;
        private ToolStripLabel removeFriends;
        private Button removeFriendButton;
        private Button addFriends;
        private RichTextBox postPreview;
        

        public Form1()
        {
           
            InitializeComponent();
            change_delegate = new Database.ChangeEventHandler(this.ChangeEvent);
            db = new Database();
            db.change += change_delegate;

            this.client = new GuiClient("tmp_user", db);
            this.disableButtens();

        }

        private void disableButtens()
        {
            this.toolStripButton1.Enabled = false;
            this.toolStripButton2.Enabled = false;
            this.removePost.Enabled = false;
            this.removethread.Enabled = false;
            this.replyButton.Enabled = false;
        }

        private void enableButtens()
        {
            this.toolStripButton1.Enabled = true;
            this.toolStripButton2.Enabled = true;
            this.removePost.Enabled = true;
            this.removethread.Enabled = true;
            this.replyButton.Enabled = true;
        }
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.panel1 = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.removeFriendButton = new System.Windows.Forms.Button();
            this.friends_list = new System.Windows.Forms.ListView();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStrip3 = new System.Windows.Forms.ToolStrip();
            this.removeFriends = new System.Windows.Forms.ToolStripLabel();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.addFriends = new System.Windows.Forms.Button();
            this.users_list = new System.Windows.Forms.ListView();
            this.toolStrip5 = new System.Windows.Forms.ToolStrip();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.postPreview = new System.Windows.Forms.RichTextBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.removethread = new System.Windows.Forms.ToolStripButton();
            this.removePost = new System.Windows.Forms.ToolStripButton();
            this.replyButton = new System.Windows.Forms.ToolStripButton();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.loginToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logoutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.registerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.threadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.postToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.replayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.postToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.thredToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.toolStripContainer2 = new System.Windows.Forms.ToolStripContainer();
            this.viewDataBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.viewDataBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.statusStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.toolStrip3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.toolStripContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.viewDataBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewDataBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 437);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(960, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(100, 16);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.splitContainer1);
            this.panel1.Controls.Add(this.menuStrip1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(960, 437);
            this.panel1.TabIndex = 1;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 27);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tabControl1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(960, 410);
            this.splitContainer1.SplitterDistance = 276;
            this.splitContainer1.TabIndex = 1;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(276, 410);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.treeView1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(268, 384);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Forums";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // treeView1
            // 
            this.treeView1.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.viewDataBindingSource1, "Name", true));
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Location = new System.Drawing.Point(3, 3);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(262, 378);
            this.treeView1.TabIndex = 0;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.removeFriendButton);
            this.tabPage3.Controls.Add(this.friends_list);
            this.tabPage3.Controls.Add(this.toolStrip2);
            this.tabPage3.Controls.Add(this.toolStrip3);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(268, 384);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "friends";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // removeFriendButton
            // 
            this.removeFriendButton.Location = new System.Drawing.Point(8, 3);
            this.removeFriendButton.Name = "removeFriendButton";
            this.removeFriendButton.Size = new System.Drawing.Size(207, 25);
            this.removeFriendButton.TabIndex = 4;
            this.removeFriendButton.Text = "remove Friends";
            this.removeFriendButton.UseVisualStyleBackColor = true;
            this.removeFriendButton.Click += new System.EventHandler(this.removeFriendButton_Click);
            // 
            // friends_list
            // 
            this.friends_list.Dock = System.Windows.Forms.DockStyle.Fill;
            this.friends_list.Location = new System.Drawing.Point(3, 28);
            this.friends_list.Name = "friends_list";
            this.friends_list.Size = new System.Drawing.Size(262, 353);
            this.friends_list.TabIndex = 3;
            this.friends_list.UseCompatibleStateImageBehavior = false;
            this.friends_list.View = System.Windows.Forms.View.List;
            // 
            // toolStrip2
            // 
            this.toolStrip2.Location = new System.Drawing.Point(3, 3);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(262, 25);
            this.toolStrip2.TabIndex = 2;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // toolStrip3
            // 
            this.toolStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.removeFriends});
            this.toolStrip3.Location = new System.Drawing.Point(3, 3);
            this.toolStrip3.Name = "toolStrip3";
            this.toolStrip3.Size = new System.Drawing.Size(215, 25);
            this.toolStrip3.TabIndex = 0;
            this.toolStrip3.Text = "toolStrip3";
            this.toolStrip3.Visible = false;
            // 
            // removeFriends
            // 
            this.removeFriends.Name = "removeFriends";
            this.removeFriends.Size = new System.Drawing.Size(91, 22);
            this.removeFriends.Text = "Remove Friends";
            this.removeFriends.Click += new System.EventHandler(this.addFriend_Click);
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.addFriends);
            this.tabPage4.Controls.Add(this.users_list);
            this.tabPage4.Controls.Add(this.toolStrip5);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(268, 384);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "users";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // addFriends
            // 
            this.addFriends.Location = new System.Drawing.Point(9, 1);
            this.addFriends.Name = "addFriends";
            this.addFriends.Size = new System.Drawing.Size(207, 25);
            this.addFriends.TabIndex = 5;
            this.addFriends.Text = "Add to Friends";
            this.addFriends.UseVisualStyleBackColor = true;
            this.addFriends.Click += new System.EventHandler(this.button1_Click);
            // 
            // users_list
            // 
            this.users_list.Dock = System.Windows.Forms.DockStyle.Fill;
            this.users_list.Location = new System.Drawing.Point(3, 28);
            this.users_list.Name = "users_list";
            this.users_list.Size = new System.Drawing.Size(262, 353);
            this.users_list.TabIndex = 1;
            this.users_list.UseCompatibleStateImageBehavior = false;
            this.users_list.View = System.Windows.Forms.View.List;
            // 
            // toolStrip5
            // 
            this.toolStrip5.Location = new System.Drawing.Point(3, 3);
            this.toolStrip5.Name = "toolStrip5";
            this.toolStrip5.Size = new System.Drawing.Size(262, 25);
            this.toolStrip5.TabIndex = 0;
            this.toolStrip5.Text = "toolStrip5";
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.postPreview);
            this.splitContainer2.Panel1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.richTextBox1);
            this.splitContainer2.Panel2.Controls.Add(this.toolStrip1);
            this.splitContainer2.Panel2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.splitContainer2.Size = new System.Drawing.Size(680, 410);
            this.splitContainer2.SplitterDistance = 194;
            this.splitContainer2.TabIndex = 0;
            // 
            // postPreview
            // 
            this.postPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.postPreview.Location = new System.Drawing.Point(0, 0);
            this.postPreview.Name = "postPreview";
            this.postPreview.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.postPreview.Size = new System.Drawing.Size(680, 194);
            this.postPreview.TabIndex = 0;
            this.postPreview.Text = "";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.richTextBox1.Location = new System.Drawing.Point(0, 47);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.richTextBox1.Size = new System.Drawing.Size(680, 165);
            this.richTextBox1.TabIndex = 1;
            this.richTextBox1.Text = "";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(40, 40);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.removethread,
            this.toolStripButton2,
            this.removePost,
            this.replyButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(680, 47);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(132, 44);
            this.toolStripButton1.Text = "Add Thread";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(114, 44);
            this.toolStripButton2.Text = "Add Post";
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // removethread
            // 
            this.removethread.Image = ((System.Drawing.Image)(resources.GetObject("removethread.Image")));
            this.removethread.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.removethread.Name = "removethread";
            this.removethread.Size = new System.Drawing.Size(159, 44);
            this.removethread.Text = "Remove Thread";
            this.removethread.Click += new System.EventHandler(this.removethread_Click);
            // 
            // removePost
            // 
            this.removePost.Image = ((System.Drawing.Image)(resources.GetObject("removePost.Image")));
            this.removePost.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.removePost.Name = "removePost";
            this.removePost.Size = new System.Drawing.Size(141, 44);
            this.removePost.Text = "Remove Post";
            this.removePost.Click += new System.EventHandler(this.removePost_Click);
            // 
            // replyButton
            // 
            this.replyButton.Image = ((System.Drawing.Image)(resources.GetObject("replyButton.Image")));
            this.replyButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.replyButton.Name = "replyButton";
            this.replyButton.Size = new System.Drawing.Size(95, 44);
            this.replyButton.Text = "Reply ";
            this.replyButton.Click += new System.EventHandler(this.replyButton_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.addToolStripMenuItem,
            this.removeToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(960, 27);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loginToolStripMenuItem,
            this.logoutToolStripMenuItem,
            this.registerToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(57, 23);
            this.toolStripMenuItem1.Text = "Menu";
            // 
            // loginToolStripMenuItem
            // 
            this.loginToolStripMenuItem.Name = "loginToolStripMenuItem";
            this.loginToolStripMenuItem.Size = new System.Drawing.Size(152, 24);
            this.loginToolStripMenuItem.Text = "Login";
            this.loginToolStripMenuItem.Click += new System.EventHandler(this.loginToolStripMenuItem_Click);
            // 
            // logoutToolStripMenuItem
            // 
            this.logoutToolStripMenuItem.Name = "logoutToolStripMenuItem";
            this.logoutToolStripMenuItem.Size = new System.Drawing.Size(152, 24);
            this.logoutToolStripMenuItem.Text = "Logout";
            this.logoutToolStripMenuItem.Click += new System.EventHandler(this.logoutToolStripMenuItem_Click);
            // 
            // registerToolStripMenuItem
            // 
            this.registerToolStripMenuItem.Name = "registerToolStripMenuItem";
            this.registerToolStripMenuItem.Size = new System.Drawing.Size(152, 24);
            this.registerToolStripMenuItem.Text = "Register";
            this.registerToolStripMenuItem.Click += new System.EventHandler(this.registerToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 24);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // addToolStripMenuItem
            // 
            this.addToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.threadToolStripMenuItem,
            this.postToolStripMenuItem,
            this.replayToolStripMenuItem});
            this.addToolStripMenuItem.Name = "addToolStripMenuItem";
            this.addToolStripMenuItem.Size = new System.Drawing.Size(46, 23);
            this.addToolStripMenuItem.Text = "Add";
            // 
            // threadToolStripMenuItem
            // 
            this.threadToolStripMenuItem.Name = "threadToolStripMenuItem";
            this.threadToolStripMenuItem.Size = new System.Drawing.Size(152, 24);
            this.threadToolStripMenuItem.Text = "Thread";
            this.threadToolStripMenuItem.Click += new System.EventHandler(this.threadToolStripMenuItem_Click);
            // 
            // postToolStripMenuItem
            // 
            this.postToolStripMenuItem.Name = "postToolStripMenuItem";
            this.postToolStripMenuItem.Size = new System.Drawing.Size(152, 24);
            this.postToolStripMenuItem.Text = "Post";
            this.postToolStripMenuItem.Click += new System.EventHandler(this.postToolStripMenuItem_Click);
            // 
            // replayToolStripMenuItem
            // 
            this.replayToolStripMenuItem.Name = "replayToolStripMenuItem";
            this.replayToolStripMenuItem.Size = new System.Drawing.Size(152, 24);
            this.replayToolStripMenuItem.Text = "replay";
            this.replayToolStripMenuItem.Click += new System.EventHandler(this.replayToolStripMenuItem_Click);
            // 
            // removeToolStripMenuItem
            // 
            this.removeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.postToolStripMenuItem1,
            this.thredToolStripMenuItem});
            this.removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            this.removeToolStripMenuItem.Size = new System.Drawing.Size(70, 23);
            this.removeToolStripMenuItem.Text = "Remove";
            // 
            // postToolStripMenuItem1
            // 
            this.postToolStripMenuItem1.Name = "postToolStripMenuItem1";
            this.postToolStripMenuItem1.Size = new System.Drawing.Size(152, 24);
            this.postToolStripMenuItem1.Text = "Post";
            // 
            // thredToolStripMenuItem
            // 
            this.thredToolStripMenuItem.Name = "thredToolStripMenuItem";
            this.thredToolStripMenuItem.Size = new System.Drawing.Size(152, 24);
            this.thredToolStripMenuItem.Text = "Thread";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(49, 23);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(213, 24);
            this.settingsToolStripMenuItem.Text = "Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(213, 24);
            this.aboutToolStripMenuItem.Text = "About Furom\'s Forum";
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(960, 434);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(960, 459);
            this.toolStripContainer1.TabIndex = 0;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer2
            // 
            // 
            // toolStripContainer2.ContentPanel
            // 
            this.toolStripContainer2.ContentPanel.Size = new System.Drawing.Size(960, 434);
            this.toolStripContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer2.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer2.Name = "toolStripContainer2";
            this.toolStripContainer2.Size = new System.Drawing.Size(960, 459);
            this.toolStripContainer2.TabIndex = 0;
            this.toolStripContainer2.Text = "toolStripContainer2";
            // 
            // viewDataBindingSource1
            // 
            this.viewDataBindingSource1.DataSource = typeof(DataManagment.ViewData);
            // 
            // viewDataBindingSource
            // 
            this.viewDataBindingSource.DataSource = typeof(DataManagment.ViewData);
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(960, 459);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStripContainer1);
            this.Controls.Add(this.toolStripContainer2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.toolStrip3.ResumeLayout(false);
            this.toolStrip3.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.toolStripContainer2.ResumeLayout(false);
            this.toolStripContainer2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.viewDataBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewDataBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void Form1_Load(object sender, System.EventArgs e)
        {
            TreeNode nodeParent;

            foreach (ViewData forum in this.db.Forums)
            {
                nodeParent = treeView1.Nodes.Add(forum.Name);
            }
        }


        protected override void OnFormClosing(FormClosingEventArgs e) {

            base.OnFormClosing(e);

            if (e.CloseReason == CloseReason.WindowsShutDown) return;

            // Confirm user wants to close
            switch (MessageBox.Show(this, "Are you sure you want to close?", "Closing", MessageBoxButtons.YesNo)) {
                case DialogResult.No:
                    e.Cancel = true;
                    break;                   
                default:
                    client.exit();
                    break;
            }
        }

        /******************** MVC *****************/
 
        public void ChangeEvent(object sender, EventArgs e)
        {
            object[] args = { sender, e };
            switch (e.ToString()) {
                case "DataManagment.ForumsChangedEventArgs":
                    this.Invoke(new ForumsChangedInvoker(ForumsChangedDelegate), args);
                    break;
                case "DataManagment.FriendsChangedEventArgs":
                    this.Invoke(new FriendsChangedInvoker(FriendsChangedDelegate), args);
                    break;
                case "DataManagment.UsersChangedEventArgs":
                    this.Invoke(new UsersChangedInvoker(UsersChangedDelegate), args);
                    break;
                case "DataManagment.ThreadsChangedEventArgs":
                    this.Invoke(new ThreadsChangedInvoker(ThreadsChangedDelegate), args);
                    break;
                case "DataManagment.PostsChangedEventArgs":
                    this.Invoke(new PostsChangedInvoker(PostsChangedDelegate), args);
                    break;
                case "DataManagment.CurrentPostChangedEventArgs":
                    this.Invoke(new CurrentPostChangedInvoker(CurrentPostChangedDelegate), args);
                    break;
                case "DataManagment.MassegeChangedEventArgs":
                    this.Invoke(new MassegeChangedInvoker(MassegeChangedDelegate), args);
                    break;
                case "DataManagment.PopupMassegeChangedEventArgs":
                    this.Invoke(new PopupMassegeChangedInvoker(PopupMassegeChangedDelegate), args);
                    break;
                default:
                    break;
            }
        }

        public void MassegeChangedDelegate(object sender, MassegeChangedEventArgs e)
        {
            this.richTextBox1.Text += "\n\n";
            this.richTextBox1.Text+=e.Massege;
            this.richTextBox1.SelectionStart = richTextBox1.Text.Length;
            this.richTextBox1.ScrollToCaret();
            this.richTextBox1.Refresh();
        }

        public void PopupMassegeChangedDelegate(object sender, PopupMassegeChangedEventArgs e)
        {
            new NotifyWindow("", e.Massege);
            this.richTextBox1.Text += "\n\n";
            this.richTextBox1.Text += e.Massege;
            this.richTextBox1.SelectionStart = richTextBox1.Text.Length;
            this.richTextBox1.ScrollToCaret();
            this.richTextBox1.Refresh();
        }

        public void FriendsChangedDelegate(object sender, FriendsChangedEventArgs e)
        {
            friends_list.Clear();
            foreach (string t_username in e.Friends)
            {
                friends_list.Items.Add(t_username);
            }
        }

        public void UsersChangedDelegate(object sender, UsersChangedEventArgs e)
        {
            users_list.Clear();
            ListViewItem tmp_item;
            foreach (string t_username in e.Users)
            {
                users_list.Items.Add(t_username);
            }
        }

        public void CurrentPostChangedDelegate(object sender, CurrentPostChangedEventArgs e)
        {
            this.postPreview.Text = "Author: ";
            this.postPreview.Text += e.CurrentPost.Author;
            this.postPreview.Text += "\n\n\n";
            this.postPreview.Text += e.CurrentPost.Content;
        }
        public void ForumsChangedDelegate(object sender, ForumsChangedEventArgs e)
        {
            // update GUI
            List<ViewData> forums = (List<ViewData>)e.Forums;


            TreeNode selected = findNode(treeView1.Nodes,"f",e.CurrentForumID.Name);
            //TreeNode selected = null;
            bool forum_name_exists = false;
            string forum_name = "defult";
            if (selected != null)
            {
                forum_name = selected.Text;
                foreach (ViewData forum in forums)
                {
                    if (forum.Name == forum_name)
                    {
                        forum_name_exists = true;
                        break;
                    }
                }
            }

            treeView1.Nodes.Clear();
            string[] lines = new string[db.Forums.Count];
            int i = 0;
            foreach (ViewData forum in this.db.Forums)
            {
                if ((forum_name_exists) && (forum.Name == forum_name))
                {
                    treeView1.Nodes.Add(selected);
                    selected.ExpandAll();
                }
                else
                {
                    treeView1.Nodes.Add(forum.Name);
                }
                lines[i] = forum.Name;
                i++;
            }
            System.IO.File.WriteAllLines("..\\..\\..\\log.txt", lines);
            this.enableButtens();
        }

        private TreeNode findNode(TreeNodeCollection p_treeView, string p_type, string p_name)    //type   =  f , t , p
        {
            TreeNode ans= null;
            foreach (TreeNode t_node in p_treeView)
            {
                ans = findNodeHelper(t_node, p_type, p_name);
                if (ans != null) return ans;
            }
            return null;
        }

        private TreeNode findNodeHelper(TreeNode p_treeNode, string p_type, string p_name)    //type   =  f , t , p
        {
            TreeNode ans= null;
            if (p_treeNode.Text.Equals(p_name))
            {
                if ((p_type.Equals("f")) && (p_treeNode.Parent == null))
                {
                    return p_treeNode;
                }
                if ((p_type.Equals("t")) && (p_treeNode.Parent!= null) && (p_treeNode.Parent.Parent == null))
                {
                    return p_treeNode;
                }
                if ((p_type.Equals("p")) && (p_treeNode.Parent != null) && (p_treeNode.Parent.Parent != null))
                {
                    return p_treeNode;
                }
            }
            ans = findNode(p_treeNode.Nodes, p_type, p_name);
            return ans;
        }

        public void ThreadsChangedDelegate(object sender, ThreadsChangedEventArgs e)
        {
            // update GUI
            List<ViewData> threads = (List<ViewData>)e.Threads;
            TreeNode selected = findNode(treeView1.Nodes, "t", e.CurrentThreadID.Name);
            TreeNode currentForum = findNode(treeView1.Nodes, "f", e.CurrentForumID.Name);
            //TreeNode selected = null;
            bool thread_name_exists = false;
            string thread_name = "defult";
            if ((selected != null) && (selected.Parent != null))
            {
                while (selected.Parent.Parent != null)
                {
                    selected = selected.Parent;
                }
                thread_name = selected.Text;
                foreach (ViewData thread in threads)
                {
                    if (thread.Name == thread_name)
                    {
                        thread_name_exists = true;
                        break;
                    }
                }
            }
            currentForum.Nodes.Clear();
            string[] lines = new string[db.Threads.Count];
            int i = 0;
            foreach (ViewData thread in this.db.Threads)
            {
                if ((thread_name_exists) && (thread.Name.Equals(thread_name)))
                {
                    currentForum.Nodes.Add(selected);   
                }
                else
                {
                    currentForum.Nodes.Add(thread.Name);
                }
                lines[i] = thread.Name;
                i++;
            }
            currentForum.Expand();
            if (thread_name_exists)
            {
                selected.ExpandAll();
            }
            System.IO.File.WriteAllLines("..\\..\\..\\Threadlog.txt", lines);
        }

        public void PostsChangedDelegate(object sender, PostsChangedEventArgs e)
        {
            List<Quartet> posts = e.Posts;
            TreeNode selected = findNode(treeView1.Nodes, "p", e.CurrentPost.Topic);
            TreeNode currentThread = findNode(treeView1.Nodes, "t", e.CurrentThreadID.Name);
            bool post_name_exists = false;
            string post_name = "defult";
            if ((selected != null) && (selected.Parent != null))
            {
                post_name = selected.Text;
                foreach (Quartet post in posts)
                {
                    if (post._subject == post_name)
                    {
                        post_name_exists = true;
                        break;
                    }
                }
            }
            currentThread.Nodes.Clear();
            getChildren(currentThread, 0, posts);
            currentThread.Expand();
        }


        private void getChildren(TreeNode p_node,int p_nodeID, List<Quartet> p_posts)
        {
            TreeNode Node = null;
            foreach (Quartet post in p_posts) // locate all children of this category
            {
                if (post._parent == p_nodeID) // found a child
                {
                    Node = p_node.Nodes.Add(post._subject); // add the child
                    getChildren(Node,post._pIndex,p_posts); // find this child's children
                }
            }
        }
        /******************** MVC END *****************/




        private void bindingSource2_CurrentChanged(object sender, EventArgs e)
        {
            Form addNewThread = new addPostWindow(db, client);
            addNewThread.ShowDialog();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Form addNewThread = new addThreadWindow(db,client);
           addNewThread.ShowDialog();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Form addNewThread = new addPostWindow(db, client);
            addNewThread.ShowDialog();
        }

        private void threadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form addNewThread = new addThreadWindow(db, client);
            addNewThread.ShowDialog();
        }

        private void postToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form addNewThread = new addPostWindow(db, client);
            addNewThread.ShowDialog();
        }

        private void loginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoginForm login = new LoginForm(client);
            login.ShowDialog();
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            client.logout();
            disableButtens();
            //db.cleanPosts();
            //db.cleanThreads();
        }

        private void registerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RegisterForm regForm = new RegisterForm(client);
            regForm.ShowDialog();

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            client.exit();
            Close();
        }

        private void OnClose(object sender, EventArgs e)
        {
            client.exit();
            Close();
        }

        private void removethread_Click(object sender, EventArgs e)
        {
            this.client.removeThread(db.CurrentForumId.Id, db.CurrentThreadId.Id);
        }

        private void removePost_Click(object sender, EventArgs e)
        {
            this.client.removePost(db.CurrentForumId.Id, db.CurrentThreadId.Id,db.CurrentPost.Id);
        }

        private void addFriend_Click(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection selectedFriends = this.friends_list.SelectedItems;
            for (int i = 0; i < selectedFriends.Count; i++)
            {
                this.client.removeFriend(selectedFriends[i].Text);
            }
        }


        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode node = this.treeView1.SelectedNode;
            node.Expand();
            if (node.Parent == null)
            {
                
                db.CurrentForumId = new ViewData (node.Text,db.findForumIndex(node.Text));
                client.getThreads();
            }
            else if (node.Parent.Parent == null)
            {
                db.CurrentThreadId = new ViewData(node.Text,db.findthreadIndex(node.Text));
                client.getReplies();
            }
            else
            {
                int postId= db.findPostIndex(node.Text);
                client.getPost(postId);
            }
        }

        private void replyButton_Click(object sender, EventArgs e)
        {
           Form addNewReply = new addReplyWindow(db, client);
            addNewReply.ShowDialog();            

        }

        private void replayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.replyButton_Click(sender, e);
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form settingsForm = new SettingsForm(client);
            settingsForm.ShowDialog();
        }

        private void removeFriendButton_Click(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection removedFriends = this.friends_list.SelectedItems;
            for (int i = 0; i < removedFriends.Count; i++)
            {
                this.client.removeFriend(removedFriends[i].Text);
            }
            this.client.getFriends();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            ListView.SelectedListViewItemCollection newFriends = this.users_list.SelectedItems;
            for (int i = 0; i < newFriends.Count; i++)
            {
                if (db.Friends.Contains(newFriends[i].Text))
                {
                    this.db.Massege = newFriends[i].Text + " is already a friend of yours!!!";
                }
                else
                {
                    this.client.addFriend(newFriends[i].Text);
                }
            }
            this.client.getFriends();
        }

    }
}
