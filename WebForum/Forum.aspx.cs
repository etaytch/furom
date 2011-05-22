using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MessagePack;
using System.Collections;
using System.Data;
using ForumSever;
namespace WebForum {
    public partial class Forum : System.Web.UI.Page {
        private Quartet _currentForum;
        private Quartet _currentthread;
        private Quartet _currentPost;
        private string _userName;
        DataTable forumData;
        DataTable threadsData;
        DataTable postData;
        
        
        protected void Page_Load(object sender, EventArgs e) {
            if (IsCallback) { return; }
            General.enable();
            //this._currentForum = new Quartet(0,0,"","");
            //this._currentthread = new Quartet(0, 0, "", "");
   
            string userName= General.lm.getUserFromIP(Request.UserHostAddress);
            if (General.lm.isLogged(userName))
            {
                setCurrents();
                _userName = userName;
                
                activateForums();
                if (this._currentForum != null)
                {
                    this.CreateThreadSource();
                    if (this._currentthread != null)
                    {

                        this.CreatePostdSource(0); //vadi 
                    }
                }
            }
        }

        private void setCurrents() { 
            string clientIP = HttpContext.Current.Request.UserHostAddress;
            UserData ud = General.lm.getUserDataFromIP(clientIP);
            if (ud != null) {
                this._currentForum = ud.curForum;
                this._currentthread = ud.curThread;
                this._currentPost = ud.CurPost;
                this._userName = ud.username;
            }
            else {
                this._currentForum = null;
                this._currentthread = null;
                this._currentPost = null;
                this._userName = "";
            }
        }

        /*
         private void setForums()
         {
             this.forumName.Text = this._currentForum._subject;
             this.AutorName.Text = this._currentForum._author;
         }
        
         private void FakeProxy()
         {
             this._currentForum._author = "Niv";
         }
         */

        private void setForumTable()
        {
            if (IsCallback) { return; }
            ForumTable.Columns.Clear();
            BoundField IDColumn = new BoundField();
            IDColumn.DataField = "Index";
            IDColumn.DataFormatString = "{0}";
            IDColumn.HeaderText = "Index";

            ButtonField buttonColumn = new ButtonField();
            buttonColumn.DataTextField = "Forum";
            buttonColumn.DataTextFormatString = "{0}";
            buttonColumn.HeaderText = "Forum";
            ForumTable.Columns.Add(IDColumn);
            ForumTable.Columns.Add(buttonColumn);
            ForumTable.AutoGenerateColumns = false;

            ICollection dv = CreateForumSource();
            
            ForumTable.DataSource = dv;
            ForumTable.DataBind(); 
        }

         ICollection CreateForumSource()
        {
            List<Quartet> forums = General.lm.getForums();
            forumData = new DataTable();
            DataRow dr;
            forumData.Columns.Add(new DataColumn("Index", typeof(Int32)));
            forumData.Columns.Add(new DataColumn("Forum", typeof(string)));
            for (int i = 0; i < forums.Count; i++)
            {

                dr = forumData.NewRow();
                dr[0] = i;
                dr[1] = forums.ElementAt(i)._subject;
                forumData.Rows.Add(dr);
            }

            DataView dv = new DataView(forumData);
            return dv;
        }

         private void setThreads()
         {
             threadTable.Columns.Clear();
             BoundField IDColumn = new BoundField();
             IDColumn.DataField = "Index";
             IDColumn.DataFormatString = "{0}";
             IDColumn.HeaderText = "Index";

             ButtonField threadColumn = new ButtonField();
             threadColumn.DataTextField = "thread";
             threadColumn.DataTextFormatString = "{0}";
             threadColumn.HeaderText = "thread";

             BoundField autorColumn = new BoundField();
             autorColumn.DataField = "autor";
             autorColumn.DataFormatString = "{0}";
             autorColumn.HeaderText = "autor";

             threadTable.Columns.Add(IDColumn);
             threadTable.Columns.Add(threadColumn);
             threadTable.Columns.Add(autorColumn);
             threadTable.AutoGenerateColumns = false;

             ICollection dv = CreateThreadSource();
             threadTable.DataSource = dv;
             threadTable.DataBind();
         }

         ICollection CreateThreadSource()
         {
             List<Quartet> threads = General.lm.getForum(_currentForum._pIndex);
             threadsData = new DataTable();
             DataRow dr;
             threadsData.Columns.Add(new DataColumn("Index", typeof(Int32)));
             threadsData.Columns.Add(new DataColumn("thread", typeof(string)));
             threadsData.Columns.Add(new DataColumn("autor", typeof(string)));
             for (int i = 0; i < threads.Count; i++)
             {
                 dr = threadsData.NewRow();
                 dr[0] = i;
                 dr[1] = threads.ElementAt(i)._subject;
                 dr[2] = threads.ElementAt(i)._author;
                 threadsData.Rows.Add(dr);
             }
             DataView dv = new DataView(threadsData);
             return dv;
         }

        protected void ForumTable_RowCommsnd(Object sender, GridViewCommandEventArgs e)
        {
                this._currentForum = FindCurrentForum(Convert.ToInt32(e.CommandArgument));
                string clientIP = HttpContext.Current.Request.UserHostAddress;
                UserData ud = General.lm.getUserDataFromIP(clientIP);
                ud.CurForum = this._currentForum;
                this.setThreads();
                this.ForumListPanel.Visible = false;
                this.ForumWithThreadsPanel.Visible = true;
                this.forumName.Text = _currentForum._subject;
                this.AutorName.Text = _currentForum._author;                
        }

        private Quartet FindCurrentForum(int p_index)
        {
            string forumName= (string)(this.forumData.Rows[p_index][1]);
            List<Quartet> t_forums = General.lm.getForums();
            foreach (Quartet t_forum in t_forums)
            {
                if (t_forum._subject.Equals(forumName))
                {
                    return t_forum;
                }
            }
            return null;
        }

        protected void ThreadTable_RowCommsnd(Object sender, GridViewCommandEventArgs e)
        {
            
            this._currentthread = FindCurrentThread(Convert.ToInt32(e.CommandArgument));
            string clientIP = HttpContext.Current.Request.UserHostAddress;
            UserData ud = General.lm.getUserDataFromIP(clientIP);
            ud.CurThread = this._currentthread;
            this.setPosts(0);

            //this.postText.Text = fp.getContent2();
            
            this.ForumListPanel.Visible = false;
            this.ForumWithThreadsPanel.Visible = false;
            this.ThreadWithPostsPanel.Visible = true;
            removeThreadError.Visible = false;
            this.PostPanel.Visible = true;
            this.forumNameInThread.Text = this._currentForum._subject;
            this.ThreadName.Text = this._currentthread._subject;
            this.ThreadAutorName.Text = this._currentthread._author;
             
        }

        private Quartet FindCurrentThread(int p_index)
        {
            string threadName = (string)(this.threadsData.Rows[p_index][1]);
            List<Quartet> t_threads = General.lm.getForum(_currentForum._pIndex);
            foreach (Quartet t_thread in t_threads)
            {
                if (t_thread._subject.Equals(threadName))
                {
                    return t_thread;
                }
            }
            return null;
        }

        private Quartet FindCurrentPost(int p_index)
        {
            string postName = (string)(this.postData.Rows[p_index][2]);
            List<Quartet> t_posts = General.lm.getThreadPosts(_currentForum._pIndex, _currentthread._pIndex);
            foreach (Quartet t_post in t_posts)
            {
                if (t_post._subject.Equals(postName))
                {
                    return t_post;
                }
            }
            return null;
        }


        private void setPosts(int par)
        {
            PostTable.Columns.Clear();

            ButtonField plusColumn = new ButtonField();
            plusColumn.DataTextField = "plus";
            plusColumn.DataTextFormatString = "{0}";
            plusColumn.HeaderText = "";

            BoundField IDColumn = new BoundField();
            IDColumn.DataField = "Index";
            IDColumn.DataFormatString = "{0}";
            IDColumn.HeaderText = "Index";

            BoundField postColumn = new BoundField();
            postColumn.DataField = "post";
            postColumn.DataFormatString = "{0}";
            postColumn.HeaderText = "post";

            BoundField autorColumn = new BoundField();
            autorColumn.DataField = "autor";
            autorColumn.DataFormatString = "{0}";
            autorColumn.HeaderText = "autor";

            PostTable.Columns.Add(plusColumn);
            PostTable.Columns.Add(IDColumn);
            PostTable.Columns.Add(postColumn);
            PostTable.Columns.Add(autorColumn);
            PostTable.AutoGenerateColumns = false;

            ICollection dv = CreatePostdSource(par);
            PostTable.DataSource = dv;
            PostTable.DataBind();
        }

        ICollection CreatePostdSource(int par)
        {

            List<Quartet> posts = General.lm.getThreadPosts(_currentForum._pIndex,_currentthread._pIndex);
            postData = new DataTable();
            DataRow dr;

            postData.Columns.Add(new DataColumn("plus", typeof(string)));
            postData.Columns.Add(new DataColumn("Index", typeof(Int32)));
            postData.Columns.Add(new DataColumn("post", typeof(string)));
            postData.Columns.Add(new DataColumn("autor", typeof(string)));
           // postData.Columns.Add(new DataColumn("grid", typeof(GridView)));

            posts.Sort(delegate(Quartet q1, Quartet q2){return q1._parent.CompareTo(q2._parent);});
            
            for (int i = 0; i < posts.Count; i++)
            {
                if ((posts.ElementAt(i)._parent == par) || (par==-1))
                {
                    dr = postData.NewRow();
                    dr[0] = "[+]";
                    dr[1] = i;
                    dr[2] = posts.ElementAt(i)._subject;
                    dr[3] = posts.ElementAt(i)._author;
                    postData.Rows.Add(dr);
                }
            }
            DataView dv = new DataView(postData);
            return dv;
        }



        protected void PostsTable_RowCommsnd(Object sender, GridViewCommandEventArgs e)
        {

            //this._currentthread = FindCurrentThread(Convert.ToInt32(e.CommandArgument));
            this._currentPost = FindCurrentPost(Convert.ToInt32(e.CommandArgument)); 
            string clientIP = HttpContext.Current.Request.UserHostAddress;
            UserData ud = General.lm.getUserDataFromIP(clientIP);
            ud.CurThread = this._currentthread;
            ud.CurPost = this._currentPost;

            this.setPosts(ud.CurPost._pIndex);
            ForumPost fp  = General.lm.getPost(ud.CurForum._pIndex, ud.CurThread._pIndex, ud.curPost._pIndex, ud.Username);
            this.postText.Text = fp.getContent2();
            this.postName.Text = ud.CurPost._subject;
            this.PostAutorName.Text = ud.CurPost._author;
            this.ForumListPanel.Visible = false;
            this.ForumWithThreadsPanel.Visible = false;
            this.ThreadWithPostsPanel.Visible = true;
            this.ForumListPanel.Visible = false;
            this.ForumWithThreadsPanel.Visible = false;
            this.ThreadWithPostsPanel.Visible = true;
            this.PostPanel.Visible = true;
            this.forumNameInThread.Text = this._currentForum._subject;
            this.ThreadName.Text = this._currentthread._subject;
            this.ThreadAutorName.Text = this._currentthread._author;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            
            activateForums();
           
        }

        private void activateForums()
        {
            this.welcomePanel.Visible = false;
            this.ForumListPanel.Visible = true;
            //this.backToForums.Visible = true;
            setForumTable();
        }

        protected void addthreadButton_Click(object sender, EventArgs e)
        {
            this.addThreadPanel.Visible = true;
            this.ForumWithThreadsPanel.Visible = false;
            this.ForumListPanel.Visible = false;
            this.addThreadError.Visible = false;

        }

        protected void okThreadButton_Click(object sender, EventArgs e)
        {
            string t_topic = this.threadTopicBox.Text;
            string t_content = this.threadContentBox.Text;
            int result= General.lm.addTread(_userName, _currentForum._pIndex, t_topic, t_content);
            if (result >= 0)
            {
                this.setThreads();
                this.addThreadPanel.Visible = false;
                this.ForumWithThreadsPanel.Visible = true;
                this.ForumListPanel.Visible = false;
            }
            else
            {
                this.addThreadPanel.Visible = true;
                this.addThreadError.Visible = true;
                this.addThreadError.Text = "Add Error here!";
                this.ForumListPanel.Visible = false;

            }
        }

        protected void threadcancelButton_Click(object sender, EventArgs e)
        {
            this.addThreadPanel.Visible = false;
            this.ForumWithThreadsPanel.Visible = true;
            this.ForumListPanel.Visible = false;
        }

        protected void removeThreadButton_Click(object sender, EventArgs e)
        {
            int result= General.lm.removeThread(_currentForum._pIndex, _currentthread._pIndex, _userName);
            if (result >= 0)
            {
            }
            else
            {
                removeThreadError.Visible = true;
                removeThreadError.Text= "ADD error here";

            }

        }

        protected void addPostButton_Click(object sender, EventArgs e)
        {
            this.addPostPanel.Visible = true;
            this.ThreadWithPostsPanel.Visible = false;
            this.ForumListPanel.Visible = false;
            this.addPostError.Visible = false;

        }

        protected void OkPostButton_Click(object sender, EventArgs e)
        {
            string t_topic = this.postTopicBox.Text;
            string t_content = this.PostContextBox.Text;
            int result = General.lm.addPost( _currentForum._pIndex,_currentthread._pIndex,_currentPost._pIndex, t_topic, t_content,_userName);
            if (result >= 0)
            {
                this.setPosts(_currentPost._pIndex);
                this.addPostPanel.Visible = false;
                this.ThreadWithPostsPanel.Visible = true;
                this.ForumListPanel.Visible = false;
            }
            else
            {
                this.addPostPanel.Visible = true;
                this.addPostError.Visible = true;
                this.addPostError.Text = "Add Error here!";
                this.ForumListPanel.Visible = false;

            }

        }

        protected void CancelPost_Click(object sender, EventArgs e)
        {
            this.addPostPanel.Visible = false;
            this.ThreadWithPostsPanel.Visible = true;
            this.ForumListPanel.Visible = false;
        }

        protected void addPostButton_Click1(object sender, EventArgs e)
        {
            this.addPostPanel.Visible = true;
            this.ThreadWithPostsPanel.Visible = false;
            this.ForumListPanel.Visible = false;
            this.addPostError.Visible = false;
        }


       

  

    }
}