using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForum
{
    public partial class Users : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            General.enable();
            string clientIP = HttpContext.Current.Request.UserHostAddress;
            Label2.Text = clientIP;
            string uname = General.lm.getUserFromIP(clientIP);
            Label2.Text += ", " + uname;
            List<string> users = General.lm.getUsers(uname);
            //List<string> users = proxyUsers();
            for (int i = 0; i < users.Count; i++)
                this.userList.Items.Add(new ListItem(users.ElementAt(i)));
        }

        /*private List<string> proxyUsers()
        {
            List<string> users = new List<string>();

            for (int i = 0; i < 10; i++)
            {
                users.Add("user" + i);   
            }
            return users;
        }*/

        protected void AddFriendButton_Click(object sender, EventArgs e)
        {
            string clientIP = HttpContext.Current.Request.UserHostAddress;
            string uname = General.lm.getUserFromIP(clientIP);

            for (int i=0; i<userList.Items.Count; i++)
                if (userList.Items[i].Selected) {
                    int result = General.lm.addMeAsFriend(uname, userList.Items[i].Text);
                    if (result < 0)
                        sendError(result, uname);
                }
            this.userList.Visible = false;
            this.AddFriendButton.Visible = false;
            this.Label1.Visible = false;
            this.friendAdded.Visible = true;
        }

        private void sendError(int returnValue, string uname) {
            // TODO case with all posible errors and the creation of the proper message, with pushing the error to the queue 
            switch (returnValue) {
                case -1:
                    this.friendAdded.Text = uname + " is already a friend with this user";
                    break;
                case -2:
                    this.friendAdded.Text = uname + " user you are trying to befriend dosn't exist";
                    break;
                case -3:
                    this.friendAdded.Text = "incorrect user name";
                    break;
                case -4:
                    this.friendAdded.Text = "the user you are trying to unfriend is not a friend of yours";
                    break;                                
                case -17:
                    this.friendAdded.Text = "some field are empty!";
                    break;               
                case -20:
                    this.friendAdded.Text = "the user you are trying to unfriend dosn't exist";
                    break;
                case -22:
                    this.friendAdded.Text = "sql error occured";
                    break;
                case -24:
                    this.friendAdded.Text = "cannot be a friend of yourself";
                    break;

                default:
                    this.friendAdded.Text = "an unexpected error append. please try again";
                    break;
            }
            this.userList.Visible = false;
            this.AddFriendButton.Visible = false;
            this.Label1.Visible = false;
            this.friendAdded.Visible = true;
        }
    }
}