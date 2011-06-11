using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ForumSever;

namespace WebForum
{
    public partial class removePost : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int post_id = Int32.Parse(Request.QueryString["postID"]);
            int thread_id = Int32.Parse(Request.QueryString["threadID"]);

            UserData ud = General.lm.getUserDataFromIP(Request.UserHostAddress);
            if (thread_id < 0)
            {
                General.lm.removeForum(ud.CurForum._pIndex, ud.username);
                Response.Redirect("Forum.aspx");
            }
            else if (post_id > 0)
            {
                General.lm.removePost(ud.CurForum._pIndex, thread_id, post_id, ud.username);
                Response.Redirect("ForumPage.aspx");
            }
            else if (post_id == 0)
            {
                General.lm.removeThread(ud.CurForum._pIndex, thread_id, ud.username);
                Response.Redirect("ForumPage.aspx");
            }
          
        }
    }
}