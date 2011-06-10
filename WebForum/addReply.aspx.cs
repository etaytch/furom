using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MessagePack;
using ForumSever;

namespace WebForum
{
    public partial class addReplay : System.Web.UI.Page
    {
        UserData ud;
        int post_id;
        protected void Page_Load(object sender, EventArgs e)
        {            
            post_id = Int32.Parse(Request.QueryString["postID"]);
           
            ud = General.lm.getUserDataFromIP(Request.UserHostAddress);
            
            if (post_id > 0)
            {
                ForumPost myPost = General.lm.getPost(ud.CurForum._pIndex, ud.CurThread._pIndex, post_id, ud.username);
                this.OriginalContent.Text = myPost.getContent();
                this.OriginalTopic.Text = myPost.getTopic();
            }
            else
            {
                ForumThread myThread= General.lm.getThread(ud.CurForum._pIndex, ud.CurThread._pIndex);
                this.OriginalContent.Text = myThread.getContent();
                this.OriginalTopic.Text = myThread.getTopic();
            }
          }

        protected void Button1_Click(object sender, EventArgs e)
        {
            General.lm.addPost(ud.CurThread._pIndex, ud.CurForum._pIndex,post_id, this.topic.Text, this.content.Text, ud.Username);
            Response.Redirect("/ThreadBody.aspx");
        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("/ThreadBody.aspx");
        }
    }
}