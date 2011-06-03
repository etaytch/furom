using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using ForumSever;

namespace WebForum
{
    public partial class ThreadBody : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            /*
            HtmlGenericControl acc = new HtmlGenericControl("div");
            acc.Attributes["name"] = "accordion1";
            addAccordions(acc);
            Panel1.Controls.Add(acc);
             */
            UserData ud = General.lm.getUserDataFromIP(Request.UserHostAddress);
            PostsTree posts = General.lm.getThreadPostsAndContent(ud.CurForum._pIndex, ud.CurThread._pIndex, ud.Username);
            string threadContent = General.lm.getThread(ud.CurForum._pIndex, ud.CurThread._pIndex).getContent();
            HtmlGenericControl mainThread = setAccordions(posts, threadContent);
            HtmlGenericControl wrapper = new HtmlGenericControl("div");
            wrapper.Attributes["name"] = "accordion";
            wrapper.Controls.Add(addHeader(ud.CurThread._subject));
            wrapper.Controls.Add(mainThread);
            Panel1.Controls.Add(wrapper);
        }


        private HtmlGenericControl setAccordions(PostsTree p_posts,string p_content)
        {
            HtmlGenericControl acc = new HtmlGenericControl("div");
            acc.Attributes["name"] = "accordion";
            acc.Controls.Add(addContent(p_content));
            if (p_posts.Children != null)
            {
                foreach (PostsTree pt in p_posts.Children)
                {
                    acc.Controls.Add(addHeader(pt.Post._subject));
                    acc.Controls.Add(setAccordions(pt, pt.Content));
                }
            }
            return acc;
        }

        private HtmlGenericControl addContent(string p_content)
        {
            HtmlGenericControl div = new HtmlGenericControl("div");
            HtmlGenericControl p = new HtmlGenericControl("p");
            p.InnerText = p_content;
            div.Controls.Add(p);
            this.Controls.Add(div);
            return div;
        }

        private void addAccordions(HtmlGenericControl acc)
        {
            for (int i = 0; i < 10; i++)
            {
                HtmlGenericControl tmpdiv = new HtmlGenericControl("div");
                tmpdiv.Attributes["name"] = "accordion" + i;
                addDivs(tmpdiv);
                acc.Controls.Add(addHeader("section" + i));
                acc.Controls.Add(tmpdiv);
            }
        }
        private void addDivs(HtmlGenericControl acc)
        {
            for (int i = 0; i < 10; i++)
            {
                HtmlGenericControl tmpdiv = createDiv(i);
                acc.Controls.Add(addHeader("section" + i));
                acc.Controls.Add(tmpdiv);
            }
        }
        private HtmlGenericControl createDiv(int i)
        {
            HtmlGenericControl div = new HtmlGenericControl("div");
            HtmlGenericControl p = new HtmlGenericControl("p");
            p.InnerText = "I am div " + i + "!!!\n";
            div.Controls.Add(p);
            this.Controls.Add(div);
            return div;
        }

        HtmlGenericControl addHeader(string topic)
        {
            HtmlGenericControl div = new HtmlGenericControl("h3");
            HtmlGenericControl a = new HtmlGenericControl("a");
            a.Attributes["href"] = "#";
            a.InnerText = topic;
            div.Controls.Add(a);

            return div;
        }
    }
}