using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MessagePack;
using System.Data;
using System.Collections;


namespace WebForum
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            General.enable();
            
            BoundField IDColumn = new BoundField();
            IDColumn.DataField = "Index";
            IDColumn.DataFormatString = "{0}";
            IDColumn.HeaderText = "Index";

            ButtonField buttonColumn = new ButtonField();
            buttonColumn.DataTextField = "Forum";
            buttonColumn.DataTextFormatString = "{0}";

            //buttonColumn.DataNavigateUrlFields = new string[] { "Forum" };
            //buttonColumn.DataNavigateUrlFormatString = "{0}";
            buttonColumn.HeaderText = "Forum";
            
            GridView1.Columns.Add(IDColumn);
            GridView1.Columns.Add(buttonColumn);
            GridView1.AutoGenerateColumns = false;
            
            ICollection dv = CreateDataSource(); 
            GridView1.DataSource = dv;
            GridView1.DataBind(); 

        }

        protected void BulletedList1_Click(object sender, BulletedListEventArgs e)
        {

        }

        ICollection CreateDataSource()
        {

            List<Quartet> forums = General.lm.getForums();
            DataTable dt = new DataTable();
            DataRow dr;
            dt.Columns.Add(new DataColumn("Index", typeof(Int32)));
            dt.Columns.Add(new DataColumn("Forum", typeof(string)));
            for (int i = 0; i < forums.Count; i++)
            {

                dr = dt.NewRow();
                dr[0] = i;
                dr[1] = forums.ElementAt(i)._subject;
                dt.Rows.Add(dr);
            }

            DataView dv = new DataView(dt);
            return dv;
        }

        protected void GridView1_RowCommand(Object sender, GridViewCommandEventArgs e)
        {
                int index = Convert.ToInt32(e.CommandArgument);
                this.Label1.Text = e.CommandArgument.ToString();
            }
        }
}

