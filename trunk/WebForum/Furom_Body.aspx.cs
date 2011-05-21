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
            General.enable();
             this.GridView1.DataSource = this.CreateDataSource();
             // Make the grid databoud.
             GridView1.DataBind();
        }

        protected void BulletedList1_Click(object sender, BulletedListEventArgs e)
        {

        }

        DataTable CreateDataSource()
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
            return dt;
        }

        
    }

}