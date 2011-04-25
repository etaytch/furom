using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GuiForumClient;

namespace GuiForumClient
{
    public partial class addThreadWindow : addWindow
    {
        public addThreadWindow() : base()
        {
            this.Text = "add new Thread";
        }

        public addThreadWindow(DataManagment.Database db, GuiClient client) : base(db,client)
        {
            this.Text = "add new Thread";
        }

        public override void button1_Click(object sender, EventArgs e)
        {
            this.client.post(this.textBox1.Text, this.textBox2.Text);

        }
    }
}
