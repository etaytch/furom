using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForum {
    public partial class notify : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            General.enable();
            string user = General.lm.getUserFromIP(Request.UserHostAddress);
            if (General.lm.isLogged(user))
                Response.Write(user + " logged in");
            else
                Response.Write("not logged in");
            
        }
    }
}