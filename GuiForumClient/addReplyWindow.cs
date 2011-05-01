using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GuiForumClient
{
    public partial class addReplyWindow : addWindow
    {
        public addReplyWindow(): base()
        {
            this.Text = "Reply";           
        }

        public addReplyWindow(DataManagment.Database db, GuiClient client) : base (db,client)
        {
            this.Text = "Reply";
        }

        public override void button1_Click(object sender, EventArgs e)
        {
            this.client.addReply(this.textBox2.Text, this.textBox1.Text);
            this.client.getReplies();
            this.textBox2.Text = "";
            this.textBox1.Text = "";

        }
    }


}
