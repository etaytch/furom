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
    public partial class LoginForm : Form
    {
        private GuiClient client;

        public LoginForm()
        {
            InitializeComponent();
        }

        public LoginForm(GuiClient client)
        {
            InitializeComponent();
            this.client = client;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void maskedTextBox2_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.client.login(this.user.Text, this.password.Text);
            this.client.getForums();
        }
    }
}
