﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForum {
    public partial class logout : System.Web.UI.Page, PageLoader {
        protected void Page_Load(object sender, EventArgs e) {

        }


        public void update(string ip) {
            // do nothing..
        }

        protected void Button1_Click(object sender, EventArgs e) {
            General.enable();
            string clientIP = HttpContext.Current.Request.UserHostAddress;
            string uname = General.lm.getUserFromIP(clientIP);            

            int result = General.lm.logout(uname);
            if (result < 0) {
                sendError(result, uname);
            }            
            General.lm.removeUserIP(clientIP);
            this.error.Text = "logged out";
            this.logoutButton.Visible = false;
            this.error.Visible = true;
            
        }

        private void sendError(int returnValue, string uname) {
            // TODO case with all posible errors and the creation of the proper message, with pushing the error to the queue 
            switch (returnValue) {
                
                case -3:
                    this.error.Text = "incorrect user name";
                    break;             
                case -15:
                    this.error.Text = "the username is already exist";
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
                case -21:
                    this.error.Text = "could not retrieve users list";
                    break;
                case -22:
                    this.error.Text = "sql error occured";
                    break;
                default:
                    this.error.Text = "an unexpected error append. please try again";
                    break;
            }
            this.logoutButton.Visible = false;
            this.error.Visible = true;

        }
    }
}