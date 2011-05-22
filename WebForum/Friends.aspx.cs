using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForum {
    public partial class Friends : System.Web.UI.Page, PageLoader {
        protected void Page_Load(object sender, EventArgs e) {
            General.enable();
            string clientIP = HttpContext.Current.Request.UserHostAddress;
            string uname = General.lm.getUserFromIP(clientIP);
            if ((uname == null) || (uname.Equals(""))) {
                Label1.Text = "Please login in order to view users list";
                Label1.Visible = true;
            }
            else {
                List<string> friends = General.lm.getFriends(uname);
                //List<string> users = proxyUsers();
                for (int i = 0; i < friends.Count; i++) {
                    this.friendList.Items.Add(new ListItem(friends.ElementAt(i)));
                }
            }
        }

        public void update(string ip) {     
            // do nothing..
        }

        protected void RemoveFriendButton_Click(object sender, EventArgs e) {
            string clientIP = HttpContext.Current.Request.UserHostAddress;
            string uname = General.lm.getUserFromIP(clientIP);

            for (int i = 0; i < friendList.Items.Count; i++)
                if (friendList.Items[i].Selected) {
                    int result = General.lm.removeMeAsFriend(uname, friendList.Items[i].Text);
                    if (result < 0)
                        sendError(result, uname);
                }
            this.friendList.Visible = false;
            this.removFriendButton.Visible = false;
            this.Label1.Visible = false;
            this.removSecceed.Visible = true;
        }

        private void sendError(int returnValue, string uname) {
            // TODO case with all posible errors and the creation of the proper message, with pushing the error to the queue 
            switch (returnValue) {               
                case -2:
                    this.removSecceed.Text = uname + " user you are trying to befriend dosn't exist";
                    break;
                case -3:
                    this.removSecceed.Text = "incorrect user name";
                    break;
                case -4:
                    this.removSecceed.Text = "the user you are trying to unfriend is not a friend of yours";
                    break;
                case -17:
                    this.removSecceed.Text = "some field are empty!";
                    break;
                case -19:
                    this.removSecceed.Text = "is not logged in.";
                    break;
                case -20:
                    this.removSecceed.Text = "the user you are trying to unfriend dosn't exist";
                    break;
                case -22:
                    this.removSecceed.Text = "sql error occured";
                    break;
                default:
                    this.removSecceed.Text = "an unexpected error append. please try again";
                    break;
            }
            this.friendList.Visible = false;
            this.removFriendButton.Visible = false;
            this.Label1.Visible = false;
            this.removSecceed.Visible = true;

        }
    }
}