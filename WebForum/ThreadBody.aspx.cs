using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace WebForum
{
    public partial class ThreadBody : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HtmlGenericControl acc = new HtmlGenericControl("div");
            acc.Attributes["name"] = "accordion1";
            addAccordions(acc);
            Panel1.Controls.Add(acc);
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