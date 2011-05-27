using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using ForumSever;

namespace WebForum {
    public partial class Register : System.Web.UI.Page, PageLoader {

        protected void Page_Load(object sender, EventArgs e) {
 
        }

        public void update(string ip) {
            // do nothing..
        }

        protected void registerButton_Click(object sender, EventArgs e)
        {
            string t_fname = this.First.Text;
            string t_lname = this.Last.Text;
            string t_uname = this.UserName.Text;
            string t_pass = this.Password.Text;
            string t_repass = this.RePassword.Text;
            string t_mail = this.Email.Text;
            string t_birthday = this.Birthday.Text;
            string t_city = this.City.Text;
            this.error.Text = "";
            this.errorPanel.Visible = true;
          
            this.errorPanel.Visible = false;
            try
            {
                string t_country = this.Country.SelectedItem.Text;
                string t_sex = this.Sex.SelectedItem.Text;
            }
            catch (Exception ejggj)
            {
            }


            //CODE OF REGISTER

            General.enable();
            //General.uName = this.UserName.Text;
            int result = General.lm.register(new MemberInfo(t_uname, t_fname, t_lname, t_pass, this.Sex.Text, 
                this.Country.Text, t_city, t_mail, t_birthday, "0"));
            if (result < 0)
                sendError(result, this.UserName.Text);
            else {
                //this.Panel1.Visible = false;
                this.Panel2.Visible = false;
                this.error.Visible = false;
                this.Panel3.Visible = true;
                //this.success0.Visible = false;
            }     
            
        }

        private void sendError(int returnValue, string uname) {
            // TODO case with all posible errors and the creation of the proper message, with pushing the error to the queue 
            switch (returnValue) {
                case -15:
                    this.error.Text = "the username is already exist";
                    
                    break;
                case -16:
                    this.error.Text = "the email is already exist";
                    break;
                case -17:
                    this.error.Text = "some field are empty!";
                    break;
                case -18:
                    this.error.Text = "is already logged in.";
                    break;
                case -22:
                    this.error.Text = "sql error occured";
                    break;
                default:
                    this.error.Text = "an unexpected error append. please try again";
                    break;
            }
            //this.Panel1.Visible = false;
            //this.error.Visible = true;
            this.errorPanel.Visible = true;
        }

    }
}

