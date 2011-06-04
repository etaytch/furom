using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ForumSever;

namespace WebForum {
    public partial class notify : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            General.enable();

            UserData ud = General.lm.getUserDataFromIP(Request.UserHostAddress);
            if (ud != null) {
                if (ud.notifications.Count > 0)
                    Response.Write(ud.notifications.Dequeue());
                else
                    Response.Write("");
            }
            else {
                Response.Write("");
            }            
        }
    }
}