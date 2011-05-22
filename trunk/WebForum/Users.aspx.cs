using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForum
{
    public partial class Users : System.Web.UI.Page, PageLoader {
        private object _sender;
        private EventArgs _e;
        private string _ip;
        private int first = 0;
        private HttpContext _con;

        protected void Page_Load(object sender, EventArgs e)
        {
            _sender = sender;
            _e = e;
            General.enable();
            if (_ip == null) {
                _con = HttpContext.Current;
                string clientIP = HttpContext.Current.Request.UserHostAddress;
                _ip = clientIP;
            }
            else {
                HttpContext.Current = _con;
            }
            

            Label2.Text = _ip;
            string uname = General.lm.getUserFromIP(_ip);
            if ((uname == null) || (uname.Equals(""))) {
                Label1.Text = "Please login in order to view users list";
                Label1.Visible = true;
            }
            else {
                Label1.Text = "Availible Users:";
                Label2.Text += ", " + uname;
                List<string> users = General.lm.getUsers(uname);
                //List<string> users = proxyUsers();
                this.userList.Items.Clear();
                for (int i = 0; i < users.Count; i++) {
                    this.userList.Items.Add(new ListItem(users.ElementAt(i)));
                }
                Label1.Visible = true;
                userList.Visible = true;
                AddFriendButton.Visible = true;
            }
            if (first == 0) {
                General.setPage(_ip, this);
                first++;
            }
        }

        public void update(string ip) {
            //string clientIP = /*HttpContext.Current.*/Request.UserHostAddress;
            _ip = ip;
            Page_Load(_sender, _e);
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
            string errMsg="";
            int counter = 0;

            for (int i=0; i<userList.Items.Count; i++)
                if (userList.Items[i].Selected) {
                    if (!uname.Equals(userList.Items[i].Text)) {
                        int result = General.lm.addMeAsFriend(uname, userList.Items[i].Text);
                        if (result < 0) {
                            sendError(result, uname);
                        }
                        else {
                            counter++;
                        }
                    }
                    else{
                        errMsg = "Dear " + uname + ", You cannot add yourself as friend! :|"+Environment.NewLine;
                    }                    
                }
            this.friendAdded.Text = counter + " new friend(s) were added!";
            if(!errMsg.Equals("")){
                this.friendAdded.Text = errMsg + this.friendAdded.Text;
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