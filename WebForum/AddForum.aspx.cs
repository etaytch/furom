using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForum
{
    public partial class AddForum : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void createButton_Click(object sender, EventArgs e)
        {
            string userName = General.lm.getUserFromIP(Request.UserHostAddress);
            General.lm.addForum(userName, ForumName.Text);
            Response.Redirect("Forum.aspx");
        }
    }
}