using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForum {
    public partial class davar : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
           General.enable();
           if (Request.HttpMethod == "GET") {
               string userName = Request.QueryString["userName"];
               if(userName == "")
                   Response.Write("empty");
               else if (!General.db.isMember(userName))
                   Response.Write("true");
               else
                   Response.Write("false");
            }
        }
    }
}