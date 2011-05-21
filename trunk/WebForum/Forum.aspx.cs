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
        protected void Page_Load(object sender, EventArgs e) {
            General.enable();
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

            //buttonColumn.DataNavigateUrlFields = new string[] { "Forum" };
            //buttonColumn.DataNavigateUrlFormatString = "{0}";
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
            DataTable dt = new DataTable();
            DataRow dr;
            dt.Columns.Add(new DataColumn("Index", typeof(Int32)));
            dt.Columns.Add(new DataColumn("Forum", typeof(string)));
            for (int i = 0; i < forums.Count; i++)
            {

                dr = dt.NewRow();
                dr[0] = i;
                dr[1] = forums.ElementAt(i)._subject;
                dt.Rows.Add(dr);
            }

            DataView dv = new DataView(dt);
            return dv;
        }

        protected void ForumTable_RowCommsnd(Object sender, GridViewCommandEventArgs e)
        {
                int index = Convert.ToInt32(e.CommandArgument);
                this.Label1.Text = e.CommandArgument.ToString();
        }

        protected void backToForums_Click(object sender, EventArgs e)
        {

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