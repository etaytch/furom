using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ForumSever;

namespace WebForum
{
    public partial class Users : System.Web.UI.Page, PageLoader {

        string clientIP = HttpContext.Current.Request.UserHostAddress;
        List<string> users = new List<string>();
        HashSet<string> addedUsers = new HashSet<string>();

        protected void Page_Load(object sender, EventArgs e)
        {
            General.enable();
            Label2.Text = "";
            string uname = General.lm.getUserFromIP(clientIP);
            string tmpUser;
            if ((uname == null) || (uname.Equals(""))) {
                Label1.Text = "Please login in order to view users list";
                Label1.Visible = true;
            }
            else {
                Label1.Text = "Availible Users:";
                Label2.Text = "Hi " + uname+"!";
                users = General.lm.getUsers(uname);
                //List<string> users = proxyUsers();
                getSelectedUsers();
                userList.Items.Clear();

                for (int i = 0; i < users.Count; i++) {
                    tmpUser = users.ElementAt(i);
                    userList.Items.Add(new ListItem(tmpUser));
                    addedUsers.Add(tmpUser);
                }
                Label1.Visible = true;
                userList.Visible = true;
                AddFriendButton.Visible = true;
            }
            if (General.lm.isAdmin(uname))
            {
                AdminPanel.Visible = true;
            }
            General.setPage(clientIP, this);
            if (General.lm.isAdmin(uname)) {
                AdminPanel.Visible = true;
            }
             
        }

        protected void Timer1_Tick(object sender, EventArgs e) {

        }

        public void update(string ip) {
            //string clientIP = /*HttpContext.Current.*/Request.UserHostAddress;
            
            


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
            List<String> usersToAdd = new List<String>();
            UserData ud = General.lm.getUserDataFromIP(clientIP);
            usersToAdd = ud.UsersToUpdate;
            int result;
            for (int i = 0; i < usersToAdd.Count; i++) {
                result = General.lm.addMeAsFriend(uname, usersToAdd[i]);
                if (result>=0) {
                    counter++;
                }
            }


            /*
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
            
            */

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

        private void getSelectedUsers() {
            List<String> usersToDelete = new List<String>();
            UserData ud = General.lm.getUserDataFromIP(clientIP);
            string uname = General.lm.getUserFromIP(clientIP);
            for (int i = 0; i < userList.Items.Count; i++) {
                if (userList.Items[i].Selected) {
                    if (!uname.Equals(userList.Items[i].Text)) {
                        usersToDelete.Add(userList.Items[i].Text);
                    }
                }
            }
            ud.UsersToUpdate = usersToDelete;       // mark 
            
        }

        protected void removeButton_Click(object sender, EventArgs e)
        {
            string clientIP = HttpContext.Current.Request.UserHostAddress;
            string uname = General.lm.getUserFromIP(clientIP);
            string errMsg = "";
            List<String> usersToDelete = new List<String>();
            int counter = 0;
            /*
            for (int i = 0; i < userList.Items.Count; i++) {
                if (userList.Items[i].Selected) {
                    if (!uname.Equals(userList.Items[i].Text)) {
                        
                        usersToDelete.Add(userList.Items[i].Text);
                    }
                    else {
                        errMsg = "Dear " + uname + ", You cannot remove yourself! :|" + Environment.NewLine;
                    }
                }
            }
             */ 
            //int result = General.lm.removeUsers(uname, usersToDelete);
            string usersAsParameter = "";
            UserData ud = General.lm.getUserDataFromIP(clientIP);
            usersToDelete = ud.UsersToUpdate;
            for (int i = 0; i < usersToDelete.Count - 1; i++) {
                usersAsParameter += usersToDelete[i] + ",";
            }
            if (usersToDelete.Count>=1) {
                usersAsParameter += usersToDelete[usersToDelete.Count-1];
                int result = General.lm.removeUsers(uname, usersToDelete);
                
                Response.Redirect("Users.aspx");
            }
            
            

            /*
            if (result < 0) {
                sendError(result, uname);
            }
            else {
                counter=result; // Oh Lord, forgive me for this stupid assignment.. (Etay)
            }

            this.friendAdded.Text = counter + " users were removed!";
            if (!errMsg.Equals(""))
            {
                this.friendAdded.Text = errMsg + this.friendAdded.Text;
            }
            this.userList.Visible = false;
            this.AddFriendButton.Visible = false;
            this.Label1.Visible = false;
            this.friendAdded.Visible = true;
            */
        }
    }
}