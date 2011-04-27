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
    public partial class RegisterForm : Form
    {
        private GuiClient client;



        public RegisterForm()
        {
            InitializeComponent();
        }

        public RegisterForm(GuiClient client)
        {
            InitializeComponent();
            this.client = client;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string t_fname = this.first.Text ;
            string t_lname = this.last.Text;
            string t_uname = this.user.Text;
            string t_pass = this.password.Text;
            string t_repass = this.repassword.Text;
            string t_sex = this.sex.Text;
            string t_mail = this.mail.Text;
            string t_birthday = this.date.Text;
            string t_country= this.country.Text;
            string t_city = this.city.Text;

            if ((t_uname == "") || (t_uname == null))
            {
                this.error.Text = "missing user name";
            }
            else if ((t_pass == "") || (t_pass == null))
            {
                this.error.Text = "missing password";
            }
            else if (t_pass != t_repass)
            {
                this.error.Text = "password mismatch, retype password";
            }
            else if ((t_mail == "") || (t_mail == null))
            {
                this.error.Text = "missing mail";
            }
            else if ((t_fname == "")||(t_fname == null) )  
            {
                this.error.Text = "missing first name";
            }
            else if ((t_lname == "") || (t_lname == null))
            {
                this.error.Text = "missing last name";
            }
            else if (!(this.checkBox1.Checked))
            {
                this.error.Text = "You must read the License agreement";
            }
            else
            {
              this.client.register(t_fname, t_lname, t_uname, t_pass, t_repass, t_sex, t_mail, t_birthday, t_country, t_city);
              this.client.getForums();
              Close();
            }
        }
    }
}
