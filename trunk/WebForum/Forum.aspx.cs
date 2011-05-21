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
                if (this._currentForum != null) {
                    this.CreateThreadSource();
                }
            }
        }

        private void setCurrents() { 
            string clientIP = HttpContext.Current.Request.UserHostAddress;
            UserData ud = General.lm.getUserDataFromIP(clientIP);
            if (ud != null) {
                this._currentForum = ud.curForum;
                this._currentthread = ud.curThread;
                this._userName = ud.username;
            }
            else {
                this._currentForum = null;
                this._currentthread = null;
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
            this.setPosts();
            this.ForumListPanel.Visible = false;
            this.ForumWithThreadsPanel.Visible = false;
            this.ThreadWithPostsPanel.Visible = true;
            removeThreadError.Visible = false;
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

        private void setPosts()
        {
            PostTable.Columns.Clear();
            BoundField IDColumn = new BoundField();
            IDColumn.DataField = "Index";
            IDColumn.DataFormatString = "{0}";
            IDColumn.HeaderText = "Index";

            ButtonField postColumn = new ButtonField();
            postColumn.DataTextField = "post";
            postColumn.DataTextFormatString = "{0}";
            postColumn.HeaderText = "post";

            ButtonField autorColumn = new ButtonField();
            autorColumn.DataTextField = "autor";
            autorColumn.DataTextFormatString = "{0}";
            autorColumn.HeaderText = "autor";

            threadTable.Columns.Add(IDColumn);
            threadTable.Columns.Add(postColumn);
            threadTable.Columns.Add(autorColumn);
            threadTable.AutoGenerateColumns = false;

            ICollection dv = CreatePostdSource();
            PostTable.DataSource = dv;
            PostTable.DataBind();
        }

        ICollection CreatePostdSource()
        {

            List<Quartet> posts = General.lm.getThreadPosts(_currentForum._pIndex,_currentthread._pIndex);
            threadsData = new DataTable();
            DataRow dr;
            threadsData.Columns.Add(new DataColumn("Index", typeof(Int32)));
            threadsData.Columns.Add(new DataColumn("thread", typeof(string)));
            threadsData.Columns.Add(new DataColumn("autor", typeof(string)));
            for (int i = 0; i < posts.Count; i++)
            {
                dr = threadsData.NewRow();
                dr[0] = i;
                dr[1] = posts.ElementAt(i)._subject;
                dr[2] = posts.ElementAt(i)._author;
                threadsData.Rows.Add(dr);
            }
            DataView dv = new DataView(threadsData);
            return dv;
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


       

  

    }
}