using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForum {
    public partial class Login : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
         //   RegisterHyperLink.NavigateUrl = "Register.aspx?ReturnUrl=" + HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]);
        }

        protected void LoginButton_Click(object sender, EventArgs e)
        {
            General.enable();
            string clientIP = HttpContext.Current.Request.UserHostAddress;
            string uname = this.UserName.Text;
            //General.uName = this.UserName.Text;
            int result = General.lm.login(uname, this.Password.Text);
            if (result < 0) 
                sendError(result, this.UserName.Text);            
            else {
                this.error.Text = " login succeed";
                General.lm.addUserIP(uname, clientIP);
                this.LoginButton.Visible = false;
                this.Panel1.Visible = false;
                this.error.Visible = true;
            }
        }

        private void sendError(int returnValue, string uname) {
            // TODO case with all posible errors and the creation of the proper message, with pushing the error to the queue 
            switch (returnValue) {
                case -1:
                    this.error.Text = uname + " is already a friend with this user";
                    break;
                case -2:
                    this.error.Text = uname + " user you are trying to befriend dosn't exist";
                    break;
                case -3:
                   this.error.Text = "incorrect user name";
                    break;
                case -4:
                    this.error.Text = "the user you are trying to unfriend is not a friend of yours";
                    break;
                case -5:
                    this.error.Text = "topic already exists, choose new topic";
                    break;
                case -6:
                    this.error.Text = " the topic could not been found";
                    break;
                case -7:
                    this.error.Text = "the topic you where trying to remove was submited by a diffrent user";
                    break;
                case -8:
                    this.error.Text = "the post topic is out of bounds";
                    break;
                case -9:
                    this.error.Text = "the forum doesn't exist";
                    break;
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
                case -19:
                    this.error.Text = "is not logged in.";
                    break;
                case -20:
                    this.error.Text = "the user you are trying to unfriend dosn't exist";
                    break;
                case -21:
                    this.error.Text = "could not retrieve users list";
                    break;
                case -22:
                    this.error.Text = "sql error occured";
                    break;
                case -23:
                    this.error.Text = "incorrect password";
                    break;
                case -24:
                    this.error.Text = "cannot be a friend of yourself";
                    break;

                default:
                    this.error.Text = "an unexpected error append. please try again";
                    break;
            }
            this.error.Visible = true;

        }
    }
}
