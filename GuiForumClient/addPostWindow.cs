﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GuiForumClient
{
    public partial class addPostWindow : addWindow
    {
        public addPostWindow(): base()
        {
            this.Text = "add new Post";
           // InitializeComponent();
           
        }

        public addPostWindow(DataManagment.Database db, GuiClient client) : base (db,client)
        {
            this.Text = "add new Post";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.client.reply(this.textBox1.Text, this.textBox2.Text);
        }
    }


}
