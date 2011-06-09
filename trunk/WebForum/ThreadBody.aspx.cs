using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using ForumSever;
using MessagePack;

namespace WebForum
{
    public partial class ThreadBody : System.Web.UI.Page
    {
        protected int accCounter;
        protected void Page_Load(object sender, EventArgs e)
        {
            accCounter = 0;

            // running OF server
            UserData ud = General.lm.getUserDataFromIP(Request.UserHostAddress);
            PostsTree posts = General.lm.getThreadPostsAndContent(ud.CurForum._pIndex, ud.CurThread._pIndex, ud.Username);
            string threadContent = General.lm.getThread(ud.CurForum._pIndex, ud.CurThread._pIndex).getContent();
            

            /*
            // running localy
            UserData ud = createUd();
            PostsTree posts = createPosts(ud.Username,3,2);
            string threadContent = "the thread content";
            */
            HtmlGenericControl mainThread = setAccordions(posts, threadContent);
            HtmlGenericControl wrapper = new HtmlGenericControl("div");
            wrapper.Attributes["name"] = "accordion0";
            wrapper.Controls.Add(addHeader(ud.CurThread._subject,0));
            wrapper.Controls.Add(mainThread);

            Panel1.Controls.Add(wrapper);
            Panel1.Controls.Add(currentPost());


        }

        private Control currentPost()
        {
            HtmlGenericControl result = new HtmlGenericControl("div");
            result.Attributes["ID"] = "myPost";
            result.Attributes["name"] = "myPost";
            result.Attributes["postID"] = "0";
            result.Attributes["perantID"] = "0";
            result.Attributes["subject"] = "0";
            result.Attributes["userName"] = "0";

            return result;
        }

        private PostsTree createPosts(string p_userName,int level,int num)
        {
            PostsTree result = new PostsTree();
            result.Post = new Quartet((num+1)*(level+1), 1, "I am post #" +num+ " in level "+level, p_userName);
            result.Content = "bla bla bla";
            level--;
            if (level>=0){
                 for (int  i = 0;  i < 2;  i++)
                 {
                    result.Children.Add(createPosts(p_userName,level,i));
                }
            }
            return result;
        }


        private UserData createUd()
        {
            UserData result = new UserData("testUser");
            result.CurForum = new Quartet(1, 0, "forum subject", "testUser");
            result.CurThread = new Quartet(2, 1, "thread subject", "testUser");
            result.CurPost = null;
            return result;
        }


        private HtmlGenericControl setAccordions(PostsTree p_posts,string p_content)
        {
            HtmlGenericControl acc = new HtmlGenericControl("div");
            acc.Attributes["name"] = "accordion" + accCounter;
            acc.Attributes["postID"] = "" + p_posts.Post._pIndex;
//            acc.Attributes["perantID"] = "" + p_posts.Post._parent;
            acc.Attributes["subject"] = p_posts.Post._subject;
            acc.Attributes["content"] = p_content;
//            acc.Attributes["userName"] = p_posts.Post._author;
            accCounter++;
            acc.Controls.Add(addHeader("content",p_posts.Post._pIndex));
            acc.Controls.Add(addContent(p_content, p_posts.Post._pIndex));
            if (p_posts.Children != null)
            {
                foreach (PostsTree pt in p_posts.Children)
                {
                    acc.Controls.Add(addHeader(pt.Post._author + " says: " + pt.Post._subject,pt.Post._pIndex));
                    acc.Controls.Add(setAccordions(pt, pt.Content));
                }
            }
            return acc;
        }

        private HtmlGenericControl addContent(string p_content,int p_index)
        {
            HtmlGenericControl div = new HtmlGenericControl("div");
            HtmlGenericControl wrapper = new HtmlGenericControl("div");
            wrapper.Attributes["name"] = "wrapper";
            wrapper.Controls.Add(addHeader("",p_index));
            HtmlGenericControl p = new HtmlGenericControl("p");
            div.Attributes["name"] = "postContent"+accCounter;
            p.InnerText = p_content;
            div.Controls.Add(p);
            wrapper.Controls.Add(div);

            return wrapper;
        }

        HtmlGenericControl addHeader(string topic,int p_id)
        {
            HtmlGenericControl div = new HtmlGenericControl("h3");
            HtmlGenericControl a = new HtmlGenericControl("a");
            a.Attributes["href"] = "#";
            a.Attributes["postID"] = ""+p_id;
            a.InnerText = topic;
            div.Controls.Add(a);

            return div;
        }

        protected void removeThreadButton_Click(object sender, ImageClickEventArgs e)
        {    
            UserData ud = General.lm.getUserDataFromIP(Request.UserHostAddress);
            General.lm.removeThread(ud.CurForum._pIndex,ud.CurThread._pIndex,ud.Username);
            Response.Redirect("/Forum.aspx");
        }

        protected void deletePostFunction(object sender, ImageClickEventArgs e)
        {
            UserData ud = General.lm.getUserDataFromIP(Request.UserHostAddress);
            int post_id = -1;
            General.lm.removePost(ud.CurForum._pIndex, ud.CurThread._pIndex,post_id, ud.Username);

        }

    }
}