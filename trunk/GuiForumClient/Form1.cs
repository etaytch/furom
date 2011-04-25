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

        private StatusStrip statusStrip1;
        private ToolStripProgressBar toolStripProgressBar1;
        private ToolStripContainer toolStripContainer1;
        private SplitContainer splitContainer1;
        private MenuStrip menuStrip1;
        private SplitContainer splitContainer2;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private ToolStrip toolStrip1;
        private ToolStripContainer toolStripContainer2;
        private ToolStrip toolStrip2;
        private ToolStripLabel removeFriend;
        private RichTextBox richTextBox1;
        private TabPage tabPage3;
        private ToolStrip toolStrip3;
        private ToolStripLabel addFriend;
        private ListView listView1;
        private ListView listView2;
        private TreeView treeView1;
        private IContainer components;
        private BindingSource viewDataBindingSource;
        private BindingSource viewDataBindingSource1;
        private Panel panel1;
        private Database db;
        private ToolStripButton toolStripButton2;
        private ToolStripButton removethread;
        private ToolStripButton toolStripButton1;
        private GuiClient client;

        private Database.ForumsChangedHandler forums_delegate;
        private Database.ThreadsChangedHandler threads_delegate;
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
        private RichTextBox postPreview;
        private Database.PostsChangedHandler posts_delegate;

        public Form1()
        {
           
            InitializeComponent();
            forums_delegate = new Database.ForumsChangedHandler(this.ForumsChanged);
            threads_delegate = new Database.ThreadsChangedHandler(this.ThreadsChanged);
            posts_delegate = new Database.PostsChangedHandler(this.PostsChanged);
            db = new Database();
            db.ForumsChangedEvent += forums_delegate;
            db.ThreadsChangedEvent += threads_delegate;
            db.PostsChangedEvent += posts_delegate;
            this.client = new GuiClient("tmp_user", db);

        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("vadi");
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("eliav");
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem("niv");
            System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem("etay");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.panel1 = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.viewDataBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.listView1 = new System.Windows.Forms.ListView();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.removeFriend = new System.Windows.Forms.ToolStripLabel();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.listView2 = new System.Windows.Forms.ListView();
            this.toolStrip3 = new System.Windows.Forms.ToolStrip();
            this.addFriend = new System.Windows.Forms.ToolStripLabel();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.removethread = new System.Windows.Forms.ToolStripButton();
            this.removePost = new System.Windows.Forms.ToolStripButton();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.loginToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logoutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.registerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.threadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.postToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.postToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.thredToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.toolStripContainer2 = new System.Windows.Forms.ToolStripContainer();
            this.viewDataBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.postPreview = new System.Windows.Forms.RichTextBox();
            this.statusStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.viewDataBindingSource1)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.toolStrip3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.toolStripContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.viewDataBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 437);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(700, 22);
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
            this.panel1.Size = new System.Drawing.Size(700, 437);
            this.panel1.TabIndex = 1;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tabControl1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(700, 413);
            this.splitContainer1.SplitterDistance = 203;
            this.splitContainer1.TabIndex = 1;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(203, 413);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.treeView1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(195, 387);
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
            this.treeView1.Size = new System.Drawing.Size(189, 381);
            this.treeView1.TabIndex = 0;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // viewDataBindingSource1
            // 
            this.viewDataBindingSource1.DataSource = typeof(DataManagment.ViewData);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.listView1);
            this.tabPage2.Controls.Add(this.toolStrip2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(195, 387);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "friends";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // listView1
            // 
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2});
            this.listView1.Location = new System.Drawing.Point(3, 3);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(189, 381);
            this.listView1.TabIndex = 1;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.List;
            // 
            // toolStrip2
            // 
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.removeFriend});
            this.toolStrip2.Location = new System.Drawing.Point(3, 3);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(189, 25);
            this.toolStrip2.TabIndex = 0;
            this.toolStrip2.Text = "toolStrip2";
            this.toolStrip2.Visible = false;
            // 
            // removeFriend
            // 
            this.removeFriend.Name = "removeFriend";
            this.removeFriend.Size = new System.Drawing.Size(79, 22);
            this.removeFriend.Text = "Remove Friend";
            this.removeFriend.Click += new System.EventHandler(this.removeFriend_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.listView2);
            this.tabPage3.Controls.Add(this.toolStrip3);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(195, 387);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "users";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // listView2
            // 
            this.listView2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView2.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem3,
            listViewItem4});
            this.listView2.Location = new System.Drawing.Point(3, 28);
            this.listView2.Name = "listView2";
            this.listView2.Size = new System.Drawing.Size(189, 356);
            this.listView2.TabIndex = 1;
            this.listView2.UseCompatibleStateImageBehavior = false;
            this.listView2.View = System.Windows.Forms.View.List;
            // 
            // toolStrip3
            // 
            this.toolStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addFriend});
            this.toolStrip3.Location = new System.Drawing.Point(3, 3);
            this.toolStrip3.Name = "toolStrip3";
            this.toolStrip3.Size = new System.Drawing.Size(189, 25);
            this.toolStrip3.TabIndex = 0;
            this.toolStrip3.Text = "toolStrip3";
            // 
            // addFriend
            // 
            this.addFriend.Name = "addFriend";
            this.addFriend.Size = new System.Drawing.Size(79, 22);
            this.addFriend.Text = "Add To Friends";
            this.addFriend.Click += new System.EventHandler(this.addFriend_Click);
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
            this.splitContainer2.Size = new System.Drawing.Size(493, 413);
            this.splitContainer2.SplitterDistance = 196;
            this.splitContainer2.TabIndex = 0;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.Location = new System.Drawing.Point(0, 25);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(493, 188);
            this.richTextBox1.TabIndex = 1;
            this.richTextBox1.Text = "this is where the user will enter his response...";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripButton2,
            this.removethread,
            this.removePost});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(493, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(77, 22);
            this.toolStripButton1.Text = "addthread";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(69, 22);
            this.toolStripButton2.Text = "add post";
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // removethread
            // 
            this.removethread.Image = ((System.Drawing.Image)(resources.GetObject("removethread.Image")));
            this.removethread.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.removethread.Name = "removethread";
            this.removethread.Size = new System.Drawing.Size(98, 22);
            this.removethread.Text = "remove thread";
            this.removethread.Click += new System.EventHandler(this.removethread_Click);
            // 
            // removePost
            // 
            this.removePost.Image = global::GuiForumClient.Properties.Resources.Delete_icon;
            this.removePost.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.removePost.Name = "removePost";
            this.removePost.Size = new System.Drawing.Size(87, 22);
            this.removePost.Text = "remove post";
            this.removePost.Click += new System.EventHandler(this.removePost_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.addToolStripMenuItem,
            this.removeToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(700, 24);
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
            this.toolStripMenuItem1.Size = new System.Drawing.Size(45, 20);
            this.toolStripMenuItem1.Text = "Menu";
            // 
            // loginToolStripMenuItem
            // 
            this.loginToolStripMenuItem.Name = "loginToolStripMenuItem";
            this.loginToolStripMenuItem.Size = new System.Drawing.Size(111, 22);
            this.loginToolStripMenuItem.Text = "login";
            this.loginToolStripMenuItem.Click += new System.EventHandler(this.loginToolStripMenuItem_Click);
            // 
            // logoutToolStripMenuItem
            // 
            this.logoutToolStripMenuItem.Name = "logoutToolStripMenuItem";
            this.logoutToolStripMenuItem.Size = new System.Drawing.Size(111, 22);
            this.logoutToolStripMenuItem.Text = "logout";
            this.logoutToolStripMenuItem.Click += new System.EventHandler(this.logoutToolStripMenuItem_Click);
            // 
            // registerToolStripMenuItem
            // 
            this.registerToolStripMenuItem.Name = "registerToolStripMenuItem";
            this.registerToolStripMenuItem.Size = new System.Drawing.Size(111, 22);
            this.registerToolStripMenuItem.Text = "register";
            this.registerToolStripMenuItem.Click += new System.EventHandler(this.registerToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(111, 22);
            this.exitToolStripMenuItem.Text = "exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // addToolStripMenuItem
            // 
            this.addToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.threadToolStripMenuItem,
            this.postToolStripMenuItem});
            this.addToolStripMenuItem.Name = "addToolStripMenuItem";
            this.addToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.addToolStripMenuItem.Text = "add";
            // 
            // threadToolStripMenuItem
            // 
            this.threadToolStripMenuItem.Name = "threadToolStripMenuItem";
            this.threadToolStripMenuItem.Size = new System.Drawing.Size(108, 22);
            this.threadToolStripMenuItem.Text = "Thread";
            this.threadToolStripMenuItem.Click += new System.EventHandler(this.threadToolStripMenuItem_Click);
            // 
            // postToolStripMenuItem
            // 
            this.postToolStripMenuItem.Name = "postToolStripMenuItem";
            this.postToolStripMenuItem.Size = new System.Drawing.Size(108, 22);
            this.postToolStripMenuItem.Text = "Post";
            this.postToolStripMenuItem.Click += new System.EventHandler(this.postToolStripMenuItem_Click);
            // 
            // removeToolStripMenuItem
            // 
            this.removeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.postToolStripMenuItem1,
            this.thredToolStripMenuItem});
            this.removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            this.removeToolStripMenuItem.Size = new System.Drawing.Size(55, 20);
            this.removeToolStripMenuItem.Text = "remove";
            // 
            // postToolStripMenuItem1
            // 
            this.postToolStripMenuItem1.Name = "postToolStripMenuItem1";
            this.postToolStripMenuItem1.Size = new System.Drawing.Size(100, 22);
            this.postToolStripMenuItem1.Text = "post";
            // 
            // thredToolStripMenuItem
            // 
            this.thredToolStripMenuItem.Name = "thredToolStripMenuItem";
            this.thredToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.thredToolStripMenuItem.Text = "thred";
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(700, 434);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(700, 459);
            this.toolStripContainer1.TabIndex = 0;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer2
            // 
            // 
            // toolStripContainer2.ContentPanel
            // 
            this.toolStripContainer2.ContentPanel.Size = new System.Drawing.Size(700, 434);
            this.toolStripContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer2.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer2.Name = "toolStripContainer2";
            this.toolStripContainer2.Size = new System.Drawing.Size(700, 459);
            this.toolStripContainer2.TabIndex = 0;
            this.toolStripContainer2.Text = "toolStripContainer2";
            // 
            // viewDataBindingSource
            // 
            this.viewDataBindingSource.DataSource = typeof(DataManagment.ViewData);
            // 
            // postPreview
            // 
            this.postPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.postPreview.Location = new System.Drawing.Point(0, 0);
            this.postPreview.Name = "postPreview";
            this.postPreview.Size = new System.Drawing.Size(493, 196);
            this.postPreview.TabIndex = 0;
            this.postPreview.Text = "";
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(700, 459);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStripContainer1);
            this.Controls.Add(this.toolStripContainer2);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
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
            ((System.ComponentModel.ISupportInitialize)(this.viewDataBindingSource1)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.toolStrip3.ResumeLayout(false);
            this.toolStrip3.PerformLayout();
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



        /******************** MVC *****************/
 
        
        public void ForumsChanged(object sender, ForumsChangedEventArgs e)
        {
            object[] args = { sender, e };
            this.Invoke(new ForumsChangedInvoker(ForumsChangedDelegate),args);
        }
        
        public void ThreadsChanged(object sender, ThreadsChangedEventArgs e)
        {
            object[] args = { sender, e };
            this.Invoke(new ThreadsChangedInvoker(ThreadsChangedDelegate), args);
        }

        public void PostsChanged(object sender, PostsChangedEventArgs e)
        {
            object[] args = { sender, e };
            this.Invoke(new PostsChangedInvoker(PostsChangedDelegate), args);

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
                }
                else
                {
                    treeView1.Nodes.Add(forum.Name);
                }
                lines[i] = forum.Name;
                i++;
            }
            System.IO.File.WriteAllLines("..\\..\\..\\log.txt", lines);
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
            if (!post_name_exists)
            {
                this.postPreview.Clear();
            }
            currentThread.Nodes.Clear();
            getChildren(currentThread, 0, posts);
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
            ListView.SelectedListViewItemCollection newFriends = this.listView2.SelectedItems;
            for (int i = 0; i < newFriends.Count; i++)
            {
                this.client.addFriend(newFriends[i].Text);
            }
        }

        private void removeFriend_Click(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection selectedFriends = this.listView2.SelectedItems;
            for (int i = 0; i < selectedFriends.Count; i++)
            {
                this.client.removeFriend(selectedFriends[i].Text);
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode node = this.treeView1.SelectedNode;
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

    }
}
