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
        int thread_id;
        protected void Page_Load(object sender, EventArgs e)
        {            
            post_id = Int32.Parse(Request.QueryString["postID"]);
            thread_id = Int32.Parse(Request.QueryString["threadID"]);

            ud = General.lm.getUserDataFromIP(Request.UserHostAddress);

            if (thread_id >= 0)
            {
                Panel1.Visible = true;
                Panel2.Visible = false;
                if (post_id > 0)
                {
                    ForumPost myPost = General.lm.getPost(ud.CurForum._pIndex, thread_id, post_id, ud.username);
                    this.OriginalContent.Text = myPost.getContent();
                    this.OriginalTopic.Text = myPost.getTopic();
                }
                else
                {
                    ForumThread myThread = General.lm.getThread(ud.CurForum._pIndex, thread_id);
                    this.OriginalContent.Text = myThread.getContent();
                    this.OriginalTopic.Text = myThread.getTopic();
                }
            }
            else
            {

                Panel1.Visible = false;
                Panel2.Visible = true;
               //  ForumThread myThread = General.lm.getThread(ud.CurForum._pIndex, thread_id);
                 ForumName.Text = ud.CurForum._subject;
                 //this.newThreadtopic.Text = myThread.getTopic();
                 //this.NewThreadContent.Text = myThread.getContent();
            }
          }

        protected void Button1_Click(object sender, EventArgs e)
        {
            General.lm.addPost(thread_id, ud.CurForum._pIndex,post_id, this.topic.Text, this.content.Text, ud.Username);
            Response.Redirect("/ForumPage.aspx");
        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("/ForumPage.aspx");
        }

        protected void newThreadButtenOk_Click(object sender, EventArgs e)
        {
            General.lm.addTread(ud.Username,ud.curForum._pIndex,newThreadtopic.Text,NewThreadContent.Text);
            Response.Redirect("/ForumPage.aspx");
        }

        protected void NewThreadCancelButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("/ForumPage.aspx");
        }
    }
}