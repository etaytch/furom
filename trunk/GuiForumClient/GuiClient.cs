using System;
using Protocol;
using MessagePack;
using DataManagment;
using System.Threading;

namespace GuiForumClient
{
    public class GuiClient 
    {
        //string ip = "10.100.101.105";
        //string ip = "10.100.101.124";
        //string ip = "132.72.193.200";
        string ip = "132.72.198.197";
        int port = 10116;
        Database db;
        string userName;
        EandEProtocol protocol;
        GuiForumListener forum;
        bool loggedIn;
        Thread t1;




        
        public GuiClient(string p_uName, Database p_db)
        {
            this.userName = p_uName;
            this.db = p_db;
            this.loggedIn = false;
            protocol = new EandEProtocol(port, ip);
            //protocol = new EandEProtocol(/*port, ip*/);
            this.startListner();
            
        }

        private void startListner()
        {
            forum = new GuiForumListener(protocol, db);
            t1 = new Thread(new ThreadStart(forum.run));
            t1.Start();
        }
        
        public string IP
        {
            get { return ip; }
        }

        internal void setNewIp()
        {
            this.t1.Abort();
            protocol = new EandEProtocol();
            this.startListner();
            this.db.Massege = "changed server IP to: localhost";
        }
        public void setNewIp(string p_ip)
        {
            ip = p_ip;
            this.t1.Abort();
            protocol = new EandEProtocol(port, ip);
            this.startListner();
            this.db.Massege = "changed server IP to: " + p_ip;
        }

        public void connect()
        {
            protocol.connect();
        }

        public void disconnect()
        {
            protocol.disconnect();
        }


        public void register(string p_fName,string p_lName,string p_uName,string p_password,string p_cpassword,string p_sex,string p_email,string p_birthday,string p_country,string p_city)
        {
            protocol.connect();
            RegisterMessage msg = new RegisterMessage(p_fName, p_lName, p_uName, p_password, p_cpassword, p_sex, p_email, p_birthday, p_country, p_city);
            protocol.sendMessage(msg);
            login(p_uName, p_password);
        }

        public void login(string p_uName, string p_pass)
        {
            protocol.connect();
            this.userName = p_uName;
            LoginMessage msg = new LoginMessage(p_uName, p_pass);
            protocol.sendMessage(msg);
            loggedIn = true;
        }
        public void logout()
        {
            if (loggedIn)
            {
                LogoutMessage msg = new LogoutMessage(userName);
                protocol.sendMessage(msg);
            }
            else
            {
                db.Massege = "you are not logged in!";
            }
       }

        public void getForums()
        {
            if (loggedIn)
            {
                GetSystemMessage msg1 = new GetSystemMessage(userName);
                protocol.sendMessage(msg1);
                GetUsersMessage msg3 = new GetUsersMessage(userName);
                protocol.sendMessage(msg3);
                GetFriendsMessage msg2 = new GetFriendsMessage(userName);
                protocol.sendMessage(msg2);
            }
            else
            {
                db.Massege = "you are not logged in!";
            }


        }


        public void getThreads()
        {
            if (loggedIn)
            {
                int fIdInt = this.db.CurrentForumId.Id;
                GetForumMessage msg = new GetForumMessage(fIdInt, userName);
                protocol.sendMessage(msg);
            }
            else
            {
                db.Massege = "you are not logged in!";
            }
        }

        public void getReplies()
        {
            int fIdInt = this.db.CurrentForumId.Id;
            int tIdInt = this.db.CurrentThreadId.Id;
            GetThreadMessage msg = new GetThreadMessage(fIdInt, tIdInt, userName);
            protocol.sendMessage(msg);
        }

        public void getPost(int p_pid)
        {
            
                int t_fId = this.db.CurrentForumId.Id;
                int t_tId = this.db.CurrentThreadId.Id;
                GetPostMessage msg = new GetPostMessage(t_fId, t_tId, p_pid, userName);
                protocol.sendMessage(msg);
            
        }
        public void addPost(string subject,string content)
        {
            if ((db.CurrentForumId.Id !=-1) &&  (db.CurrentThreadId.Id !=-1))
            {
                int fIdInt = this.db.CurrentForumId.Id;
                int tIdInt = this.db.CurrentThreadId.Id;
                int pIdInt = this.db.CurrentPost.Id;
                AddPostMessage msg = new AddPostMessage(fIdInt, tIdInt, 0, 0, userName, subject, content);
                protocol.sendMessage(msg);
            }
            else
            {
                db.Massege = "you need to select Thread first...!";
            }
        }
        
        public void addThread(string subject,string content)
        {
            if ((db.CurrentForumId.Id !=-1))
            {
                int fIdInt = this.db.CurrentForumId.Id;
                AddThreadMessage msg = new AddThreadMessage(fIdInt, userName, subject, content);
                protocol.sendMessage(msg);
            }
            else
            {
                db.Massege = "you need to select Forum first...!";
            }
        }


        public void addFriend(string friend)
        {
            AddFriendMessage msg = new AddFriendMessage(userName, friend);
            protocol.sendMessage(msg);
        }

        public void removeFriend(string friend)
        {
            RemoveFriendMessage msg = new RemoveFriendMessage(userName, friend);
            protocol.sendMessage(msg);
        }

        public void exit()
        {
            
            GuiForumListener.exit_flag = false;
            t1.Abort();
            if (loggedIn)
            {
                this.logout();
            }
            protocol.disconnect();
           
        }

        public bool isLogged()
        {
            return this.loggedIn;
        }

        public void removeThread(int p_fid,int p_tid)
        {
            if (loggedIn)
            {
                DeleteThreadMessage msg = new DeleteThreadMessage(p_fid, p_tid,this.userName);
                protocol.sendMessage(msg);
            }  
            else
            {
                db.Massege = "you are not logged in!";
            }
        }

        internal void removePost(int p_fid, int p_tid, int p_index)
        {
            if (loggedIn)
            {
                DeletePostMessage msg = new DeletePostMessage(p_fid, p_tid,p_index, this.userName);
                protocol.sendMessage(msg);
            }
            else
            {
                db.Massege = "you are not logged in!";
            }
        }

        public void getUsers()
        {
            GetSystemMessage msg1 = new GetSystemMessage(userName);
            protocol.sendMessage(msg1);
        }

        public void getFriends()
        {
            GetFriendsMessage msg2 = new GetFriendsMessage(userName);
            protocol.sendMessage(msg2);
        }


        internal void addReply(string p_subject, string p_content)
        {
            int fIdInt = this.db.CurrentForumId.Id;
            int tIdInt = this.db.CurrentThreadId.Id;
            int pIdInt = this.db.CurrentPost.Id;
            if ((db.CurrentForumId.Id != -1) && (db.CurrentThreadId.Id != -1))
            {
                AddPostMessage msg = new AddPostMessage(fIdInt, tIdInt, pIdInt, pIdInt, userName, p_subject, p_content);
                protocol.sendMessage(msg);
            }
            else
            {
                db.Massege = "you need to select Thread first...!";
            }
        }

    }
}
