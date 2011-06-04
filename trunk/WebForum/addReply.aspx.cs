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
        protected void Page_Load(object sender, EventArgs e)
        {

            //updating the current post localy
            this.currentPost.Attributes["postID"] = Request.QueryString["postID"];
            /*
            this.currentPost.Attributes["perantID"] = Request.QueryString["perantID"];
            this.currentPost.Attributes["subject"] = Request.QueryString["subject"];
            this.currentPost.Attributes["userName"] = Request.QueryString["userName"];
            */
            

            /*
            //updating the current post in server. can go???
            int post_id = Int32.Parse(Request.QueryString["postID"]);
            int parent_id = Int32.Parse(Request.QueryString["perantID"]);
            string subject = Request.QueryString["subject"];
            string userName = Request.QueryString["userName"];
            UserData ud = General.lm.getUserDataFromIP(Request.UserHostAddress);
            ud.CurPost = new Quartet(post_id, parent_id, subject, userName);
            */

            /*
            UserData ud = General.lm.getUserDataFromIP(Request.UserHostAddress);
            ForumPost myPost= General.lm.getPost(ud.CurForum._pIndex, ud.CurThread._pIndex, Int32.Parse(this.currentPost.Attributes["postID"]), ud.username);
             */
            ForumPost myPost = new ForumPost(0, 0, 0, "my topic", "bla bla blah", "Nigi");
            this.OriginalContent.Text = myPost.getContent();
            this.OriginalTopic.Text = myPost.getTopic();
            /*
            if (userName == "")
                Response.Write("empty");
            else if (!General.db.isMember(userName))
                Response.Write("true");
            else
                Response.Write("false");
             */ 
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            
            UserData ud = General.lm.getUserDataFromIP(Request.UserHostAddress);
            General.lm.addPost(ud.CurThread._pIndex, ud.CurForum._pIndex,Int32.Parse( this.currentPost.Attributes["postID"]), this.topic.Text, this.content.Text, ud.Username);
            Response.Redirect("/ThreadBody.aspx");
        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("/ThreadBody.aspx");
        }
    }
}