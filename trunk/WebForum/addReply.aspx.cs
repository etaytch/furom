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
 
            //updating the current post localy
            
            /*
            this.currentPost.Attributes["postID"] = Request.QueryString["postID"];
            this.currentPost.Attributes["perantID"] = Request.QueryString["perantID"];
            
            this.currentPost.Attributes["subject"] = Request.QueryString["subject"];
            this.currentPost.Attributes["userName"] = Request.QueryString["userName"];
            */
            

            
            //updating the current post in server. can go???
            
            post_id = Int32.Parse(Request.QueryString["postID"]);
            /*
            int parent_id = Int32.Parse(Request.QueryString["perantID"]);
            string subject = Request.QueryString["subject"];
            string userName = Request.QueryString["userName"];
            /*
            UserData ud = General.lm.getUserDataFromIP(Request.UserHostAddress);
            ud.CurPost = new Quartet(post_id, parent_id, subject, userName);
            */


            // running On Server
            
            ud = General.lm.getUserDataFromIP(Request.UserHostAddress);


            if (post_id > 0)
            {
                ForumPost myPost = General.lm.getPost(ud.CurForum._pIndex, ud.CurThread._pIndex, post_id, ud.username);


                // running localy
                /*
                ForumPost myPost = new ForumPost(0, 0, 0, "my topic", "bla bla blah", "Nigi");
                 */
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
            
            //UserData ud = General.lm.getUserDataFromIP(Request.UserHostAddress);
            General.lm.addPost(ud.CurThread._pIndex, ud.CurForum._pIndex,post_id, this.topic.Text, this.content.Text, ud.Username);
            Response.Redirect("/ThreadBody.aspx");
        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("/ThreadBody.aspx");
        }
    }
}