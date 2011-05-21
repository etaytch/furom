using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MessagePack;
using System.Collections;
using System.Data;
namespace WebForum {
    public partial class Forum : System.Web.UI.Page {
        private Quartet _currentForum;
        private Quartet _currentthread;
        private string _userName;
        DataTable forumData;
        DataTable threadsData;
        DataTable postData;
        protected void Page_Load(object sender, EventArgs e) {
           // General.enable();
            this._currentForum = new Quartet(0,0,"","");
            this._currentthread = new Quartet(0, 0, "", "");
   
            string userName= General.lm.getUserFromIP(Request.UserHostAddress);

           if (General.lm.isLogged(userName))
            {
                _userName = userName;
                welcomePanel.Visible = false;
                ForumListPanel.Visible = true;
                backToForums.Visible = true;
            }
        }

       
        private void setForums()
        {
            this.forumName.Text = this._currentForum._subject;
            this.AutorName.Text = this._currentForum._author;
        }

        private void FakeProxy()
        {
            this._currentForum._author = "Niv";
        }


        private void setForumTable()
        {
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
             BoundField IDColumn = new BoundField();
             IDColumn.DataField = "Index";
             IDColumn.DataFormatString = "{0}";
             IDColumn.HeaderText = "Index";

             ButtonField threadColumn = new ButtonField();
             threadColumn.DataTextField = "thread";
             threadColumn.DataTextFormatString = "{0}";
             threadColumn.HeaderText = "thread";

             ButtonField autorColumn = new ButtonField();
             autorColumn.DataTextField = "autor";
             autorColumn.DataTextFormatString = "{0}";
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
                this.setThreads();
                this.ForumListPanel.Visible = false;
                this.ForumWithThreadsPanel.Visible = true;
        }

        private Quartet FindCurrentForum(int p_index)
        { 	     
            string forumName= (string)(this.forumData.Rows[p_index][1]);
            List<Quartet> t_forums = Global.lm.getForums();
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
            /*
            this._currentForum = FindCurrentForum(Convert.ToInt32(e.CommandArgument));
            this.setThreads();
            this.ForumListPanel.Visible = false;
            this.ForumWithThreadsPanel.Visible = true;
             */ 
        }

        protected void backToForums_Click(object sender, EventArgs e)
        {
            ForumWithThreadsPanel.Visible= false;
            ThreadWithPostsPanel.Visible = false;
            ForumListPanel.Visible = true;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            activateForums();
           
        }

        private void activateForums()
        {
            this.welcomePanel.Visible = false;
            this.ForumListPanel.Visible = true;
            this.backToForums.Visible = true;
            setForumTable();
        }


       

  

    }
}