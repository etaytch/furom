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
        string ip = "10.100.101.124";
        int port = 10116;
        Database db;
        string userName;
        EandEProtocol protocol;
        GuiForumListener forum;
        bool loggedIn;
        Thread t1;

        public void connect()
        {
            protocol.connect();
        }

        public void disconnect()
        {
            protocol.disconnect();
        }

        public GuiClient(string p_uName, Database p_db)
        {
            this.userName = p_uName;
            this.db = p_db;
            this.loggedIn = false;
            //protocol = new EandEProtocol(/*port, ip*/);
            protocol = new EandEProtocol(port, ip);

            forum = new GuiForumListener(protocol, db);
            t1 = new Thread(new ThreadStart(forum.run));
            t1.Start();
            
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
            LogoutMessage msg = new LogoutMessage(userName);
            protocol.sendMessage(msg);
        }

        public void getForums()
        {
            GetSystemMessage msg1 = new GetSystemMessage(userName);
            protocol.sendMessage(msg1);
            GetUsersMessage msg3 = new GetUsersMessage(userName);
            protocol.sendMessage(msg3);
            GetFriendsMessage msg2 = new GetFriendsMessage(userName);
            protocol.sendMessage(msg2);
            



        }

        public void getThreads()
        {
            int fIdInt = this.db.CurrentForumId.Id;
            GetForumMessage msg = new GetForumMessage(fIdInt, userName);
            protocol.sendMessage(msg);
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
             GetPostMessage msg = new GetPostMessage(t_fId,t_tId,p_pid, userName);
            protocol.sendMessage(msg);
        }
        public void addPost(string subject,string content)
        {
            int fIdInt = this.db.CurrentForumId.Id;
            int tIdInt = this.db.CurrentThreadId.Id;
            int pIdInt = this.db.CurrentPost.Id;
            AddPostMessage msg = new AddPostMessage(fIdInt, tIdInt, 0, 0, userName, subject, content);
            protocol.sendMessage(msg);
        }
        
        public void addThread(string subject,string content)
        {
            int fIdInt = this.db.CurrentForumId.Id;
            AddThreadMessage msg = new AddThreadMessage(fIdInt, userName, subject, content);
            protocol.sendMessage(msg);
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
            this.logout();
            GuiForumListener.exit_flag = false;
            t1.Abort();
            protocol.disconnect();
           
        }

        public bool isLogged()
        {
            return this.loggedIn;
        }

        public void removeThread(int p_fid,int p_tid)
        {
            DeleteThreadMessage msg = new DeleteThreadMessage(p_fid, p_tid,this.userName);
            protocol.sendMessage(msg);
        }

        internal void removePost(int p_fid, int p_tid, int p_index)
        {
            DeletePostMessage msg = new DeletePostMessage(p_fid, p_tid,p_index, this.userName);
            protocol.sendMessage(msg);
        }

        public void getUsers()
        {
        }

        public void getFriends()
        {
        }


        internal void addReply(string p_subject, string p_content)
        {
            int fIdInt = this.db.CurrentForumId.Id;
            int tIdInt = this.db.CurrentThreadId.Id;
            int pIdInt = this.db.CurrentPost.Id;
            AddPostMessage msg = new AddPostMessage(fIdInt, tIdInt, pIdInt, pIdInt, userName, p_subject, p_content);
            protocol.sendMessage(msg);
        }
    }
}
