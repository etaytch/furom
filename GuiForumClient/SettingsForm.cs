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
    public partial class SettingsForm : Form
    {
        private GuiClient client;

        public SettingsForm()
        {
            InitializeComponent();
       
        }

        public SettingsForm(GuiClient client)
        {
            // TODO: Complete member initialization
            this.client = client;
            InitializeComponent();
            setIP();
        }

        private void setIP()
        {
            string tmp = client.IP;
            int ind;
            string[] ips = new string[4];
            for (int i = 0; i < 3; i++)
            {
                ind = tmp.IndexOf(".");
                if (ind == -1)
                {
                    ips[i] = tmp;
                }
                else
                {
                    ips[i] = tmp.Substring(0, ind);
                    tmp = tmp.Substring(ind + 1);
                }
            }
            this.textBox1.Text = ips[0];
            this.textBox2.Text = ips[1];
            this.textBox3.Text = ips[2];
            //this.textBox4.Text = ips[3];
            this.textBox4.Text = tmp;


        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (this.checkBox1.Checked)
            {
                client.setNewIp();
            }
            else
            {
                if (correctText(this.textBox1) && correctText(this.textBox2) && correctText(this.textBox3) && correctText(this.textBox4))
                {
                    string new_ip = this.textBox1.Text + "." + this.textBox2.Text + "." + this.textBox3.Text + "." + this.textBox4.Text;
                    client.setNewIp(new_ip);
                }
            }
        }

        private bool correctText(TextBox p_textBox)
        {
            if (p_textBox.Text.IsNormalized())
            {
                return true;
            }
            return false; 
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

                this.textBox1.Enabled = !checkBox1.Checked;
                this.textBox2.Enabled = !checkBox1.Checked;
                this.textBox3.Enabled = !checkBox1.Checked;
                this.textBox4.Enabled = !checkBox1.Checked;
                if (checkBox1.Checked)
                {
                    this.label2.ForeColor = System.Drawing.Color.Gray;
                }
                else
                {
                    this.label2.ForeColor = System.Drawing.Color.Black;
                }

        }

    }
}
