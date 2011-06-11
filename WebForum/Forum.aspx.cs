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
        private string _userName;
        DataTable forumData;
        
        protected void Page_Load(object sender, EventArgs e) {
            if (IsCallback) { return; }
            General.enable();
            string userName= General.lm.getUserFromIP(Request.UserHostAddress);
            if (General.lm.isLogged(userName)) {
                _userName = userName;
                activateForums();
                if (General.lm.isAdmin(userName)) {
                    adminPanel.Visible = true;
                }
            }
        }

        private void setForumTable() {
            if (IsCallback) { return; }
            ForumTable.Columns.Clear();
            BoundField IDColumn = new BoundField();
            IDColumn.DataField = "Index";
            IDColumn.DataFormatString = "{0}";
            IDColumn.HeaderText = "Index";
            IDColumn.ItemStyle.Width = 5;
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

         ICollection CreateForumSource() {
            List<Quartet> forums = General.lm.getForums();
            forumData = new DataTable();
            DataRow dr;
            forumData.Columns.Add(new DataColumn("Index", typeof(Int32)));
            forumData.Columns.Add(new DataColumn("Forum", typeof(string)));
            for (int i = 0; i < forums.Count; i++) {

                dr = forumData.NewRow();
                dr[0] = i;
                dr[1] = forums.ElementAt(i)._subject;
                forumData.Rows.Add(dr);
            }

            DataView dv = new DataView(forumData);
            return dv;
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

        protected void Button1_Click(object sender, EventArgs e) {
            activateForums();
        }

        protected void ForumTable_RowCommsnd(Object sender, GridViewCommandEventArgs e) {
            this._currentForum = FindCurrentForum(Convert.ToInt32(e.CommandArgument));
            string clientIP = HttpContext.Current.Request.UserHostAddress;
            UserData ud = General.lm.getUserDataFromIP(clientIP);
            ud.CurForum = this._currentForum;
            Response.Redirect("ForumPage.aspx");
        }



        private void activateForums()
        {
            this.welcomePanel.Visible = false;
            this.ForumListPanel.Visible = true;
            setForumTable();
        }

        protected void removeForumButton_Click(object sender, EventArgs e)
        {
            this.ForumListPanel.Visible = true;
        }

        protected void AddForumButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddForum.aspx");
        }
    }
}