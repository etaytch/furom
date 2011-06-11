using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using ForumSever;
using MessagePack;

namespace WebForum {
    public partial class ForumPage : System.Web.UI.Page {
        bool isAdmin;
        int counter = 0;
        UserData ud;
        protected void Page_Load(object sender, EventArgs e) {

            // running OF server
            ud = General.lm.getUserDataFromIP(Request.UserHostAddress);
            isAdmin = General.lm.isAdmin(ud.Username);

            HtmlGenericControl centerContainer = new HtmlGenericControl("div");
            centerContainer.Attributes["id"] = "center-container";
            HtmlGenericControl navbar = new HtmlGenericControl("div");
            navbar.Attributes["id"] = "navigate-bar";
            navbar.InnerText = ud.CurForum._subject;
            HtmlGenericControl actions = new HtmlGenericControl("div");
            actions.Attributes["class"] = "actions";
            HtmlGenericControl actionsa = new HtmlGenericControl("a");
            actionsa.Attributes["onclick"] = "addthread()";
            actionsa.InnerText = "Add Thread";
            actions.Controls.Add(actionsa);
            navbar.Controls.Add(actions);
            centerContainer.Controls.Add(navbar);
            HtmlGenericControl main = new HtmlGenericControl("div");
            main.Attributes["id"] = "main1";
            List<Quartet> forum = General.lm.getForum(ud.CurForum._pIndex);

            foreach (Quartet thread in forum) {
                PostsTree post = General.lm.getThreadPostsAndContent(ud.CurForum._pIndex, thread._pIndex, ud.Username);
                ForumThread ft = General.lm.getThread(ud.CurForum._pIndex, thread._pIndex);
                ft.setID(thread._pIndex);
                setMsg(main, post, ft);
            }

            centerContainer.Controls.Add(main);
            ForumPanel.Controls.Add(centerContainer);
        }

        private void setMsg(HtmlGenericControl container, PostsTree p_posts, ForumThread thread) {
            counter++;
            HtmlGenericControl msg = new HtmlGenericControl("div");
            msg.Attributes["class"] = "forum-message";
            msg.Attributes["id"] = "thread" + counter;
            HtmlGenericControl subject = new HtmlGenericControl("div");
            subject.Attributes["class"] = "subject";
            subject.InnerText = thread.getTopic();
            if (p_posts.Children != null) {
                HtmlGenericControl subjecta = new HtmlGenericControl("a");
                subjecta.Attributes["onclick"] = "toggle_thread('" + counter + "')";
                subjecta.Attributes["title"] = "toggle children";
                HtmlGenericControl subjectaimg = new HtmlGenericControl("img");
                subjectaimg.Attributes["src"] = "image/button-minus.gif";
                subjectaimg.Attributes["id"] = "img_toggle_" + counter;
                subjectaimg.Attributes["alt"] = "[-]";
                subjecta.Controls.Add(subjectaimg);
                subject.Controls.Add(subjecta);
            }
            msg.Controls.Add(subject);
            HtmlGenericControl date = new HtmlGenericControl("div");
            date.Attributes["class"] = "date";
            date.InnerText = "by " + thread.getAuthor();
            msg.Controls.Add(date);
            HtmlGenericControl body = new HtmlGenericControl("div");
            body.Attributes["class"] = "body";
            body.Attributes["id"] = "body_" + counter;
            body.InnerText = thread.getContent();
            msg.Controls.Add(body);
            HtmlGenericControl actions = new HtmlGenericControl("div");
            actions.Attributes["class"] = "actions";
            HtmlGenericControl actionsa = new HtmlGenericControl("a");
            actionsa.Attributes["href"] = "addReply.aspx?threadID=" + thread.getThreadID()+"&postID=0";
            actionsa.InnerText = "Reply";
            actions.Controls.Add(actionsa);
            if (isAdmin || ud.username.Equals(thread.getAuthor())) {
                HtmlGenericControl nbsp = new HtmlGenericControl();
                nbsp.InnerHtml = "&nbsp&nbsp";
                actions.Controls.Add(nbsp);
                HtmlGenericControl actionsb = new HtmlGenericControl("a");
                actionsb.Attributes["href"] = "removePost.aspx?threadID=" + thread.getThreadID() + "&postID=0";
                actionsb.InnerText = "Delete";
                actions.Controls.Add(actionsb);
            }
            msg.Controls.Add(actions);
            container.Controls.Add(msg);
            if (p_posts.Children != null) {
                HtmlGenericControl indent = new HtmlGenericControl("div");
                indent.Attributes["class"] = "indent";
                indent.Attributes["id"] = "level_" + counter;
                foreach (PostsTree pt in p_posts.Children) {
                    setchildMsg(indent, pt, pt.Content, thread.getThreadID(), thread.getThreadID());
                }
                container.Controls.Add(indent);
            }
        }

        private void setchildMsg(HtmlGenericControl container, PostsTree p_posts, string p_content, int p_father, int threadID) {
            counter++;
            HtmlGenericControl msg = new HtmlGenericControl("div");
            msg.Attributes["class"] = "forum-message";
            msg.Attributes["id"] = "thread" + counter;
            HtmlGenericControl subject = new HtmlGenericControl("div");
            subject.Attributes["class"] = "subject";
            subject.InnerText = p_posts.Post._subject;
            if (p_posts.Children != null) {
                HtmlGenericControl subjecta = new HtmlGenericControl("a");
                subjecta.Attributes["onclick"] = "toggle_thread('" + counter + "')";
                subjecta.Attributes["title"] = "toggle children";
                HtmlGenericControl subjectaimg = new HtmlGenericControl("img");
                subjectaimg.Attributes["src"] = "image/button-minus.gif";
                subjectaimg.Attributes["id"] = "img_toggle_" + counter;
                subjectaimg.Attributes["alt"] = "[-]";
                subjecta.Controls.Add(subjectaimg);
                subject.Controls.Add(subjecta);
            }
            msg.Controls.Add(subject);
            HtmlGenericControl date = new HtmlGenericControl("div");
            date.Attributes["class"] = "date";
            date.InnerText = "by " + p_posts.Post._author;
            msg.Controls.Add(date);
            HtmlGenericControl body = new HtmlGenericControl("div");
            body.Attributes["class"] = "body";
            body.Attributes["id"] = "body_" + counter;
            body.InnerText = p_content;
            msg.Controls.Add(body);
            HtmlGenericControl actions = new HtmlGenericControl("div");
            actions.Attributes["class"] = "actions";
            HtmlGenericControl actionsa = new HtmlGenericControl("a");
            actionsa.Attributes["href"] = "addReply.aspx?threadID=" + threadID + "&postID=" + p_posts.Post._pIndex;
            actionsa.InnerText = "Reply";
            actions.Controls.Add(actionsa);
            if (isAdmin || ud.username.Equals(p_posts.Post._author)) {
                HtmlGenericControl nbsp = new HtmlGenericControl();
                nbsp.InnerHtml = "&nbsp&nbsp";
                actions.Controls.Add(nbsp);
                HtmlGenericControl actionsb = new HtmlGenericControl("a");
                actionsb.Attributes["href"] = "removePost.aspx?threadID=" + threadID + "&postID=" + p_posts.Post._pIndex;
                actionsb.InnerText = "Delete";
                actions.Controls.Add(actionsb);
            }


            msg.Controls.Add(actions);
            container.Controls.Add(msg);

            if (p_posts.Children != null) {
                HtmlGenericControl indent = new HtmlGenericControl("div");
                indent.Attributes["class"] = "indent";
                indent.Attributes["id"] = "level_" + counter;
                foreach (PostsTree pt in p_posts.Children) {
                    setchildMsg(indent, pt, pt.Content, p_posts.Post._pIndex, threadID);
                }
                container.Controls.Add(indent);
            }
        }

        protected void okThreadButton_Click(object sender, EventArgs e) {
            string t_topic = this.threadTopicBox.Text;
            string t_content = this.threadContentBox.Text;
            int result = General.lm.addTread(ud.username, ud.curForum._pIndex, t_topic, t_content);
            if (result >= 0) {
                this.addThreadPanel.Visible = false;
                this.ForumPanel.Visible = true;
            }
            else {
                this.addThreadPanel.Visible = true;
                this.addThreadError.Visible = true;
                this.addThreadError.Text = "Error adding new thread";
                this.ForumPanel.Visible = false;
            }
        }

        protected void threadcancelButton_Click(object sender, EventArgs e) {
            this.addThreadPanel.Visible = false;
            this.ForumPanel.Visible = true;
        }
    }
}