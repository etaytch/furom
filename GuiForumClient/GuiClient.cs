using System;
using Protocol;
using MessagePack;
using DataManagment;
using System.Threading;

namespace GuiForumClient
{
    public class GuiClient 
    {
        string ip = "192.168.1.105";
        int port = 10116;
        Database db;
        string userName;
        EandEProtocol protocol;
        GuiForumListener forum;
        bool loggedIn;
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
            protocol = new EandEProtocol(port, ip);
            protocol.connect();
            forum = new GuiForumListener(protocol, db);
            Thread t1 = new Thread(new ThreadStart(forum.run));
            t1.Start();
            
        }
        public void register(string p_fName,string p_lName,string p_uName,string p_password,string p_cpassword,string p_sex,string p_email,string p_birthday,string p_country,string p_city)
        {
            RegisterMessage msg = new RegisterMessage(p_fName, p_lName, p_uName, p_password, p_cpassword, p_sex, p_email, p_birthday, p_country, p_city);
            protocol.sendMessage(msg);
            login(p_uName, p_password);
        }

        public void login(string p_uName, string p_pass)
        {
            this.userName = p_uName;
            LoginMessage msg = new LoginMessage(p_uName, p_pass);
            protocol.sendMessage(msg);
            //forum.Incoming();
            loggedIn = true;
        }
        public void logout()
        {
            LogoutMessage msg = new LogoutMessage(userName);
            protocol.sendMessage(msg);
          //  forum.Incoming();
        }

        public void getForums()
        {
            GetSystemMessage msg = new GetSystemMessage(userName);
            protocol.sendMessage(msg);
         //   forum.Incoming();
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
            //forum.Incoming();
        }

        public void reply(string subject,string content)
        {
            /*
            int fIdInt = Int32.Parse(fId);
            int tIdInt = Int32.Parse(tId);
            int pIdInt= Int32.Parse(parentId);
             */
            int fIdInt = this.db.CurrentForumId.Id;
            int tIdInt = this.db.CurrentThreadId.Id;
            int pIdInt = this.db.CurrentPost.Id;
            AddPostMessage msg = new AddPostMessage(fIdInt, tIdInt, 0, pIdInt, userName, subject, content);
            protocol.sendMessage(msg);
           // forum.Incoming();
        }

        public void post(string subject,string content)
        {
            int fIdInt = this.db.CurrentForumId.Id;
            AddThreadMessage msg = new AddThreadMessage(fIdInt, userName, subject, content);
            protocol.sendMessage(msg);
         //   forum.Incoming();
        }

        public void addFriend(string friend)
        {
            AddFriendMessage msg = new AddFriendMessage(userName, friend);
            protocol.sendMessage(msg);
        //    forum.Incoming();
        }

        public void removeFriend(string friend)
        {
            RemoveFriendMessage msg = new RemoveFriendMessage(userName, friend);
            protocol.sendMessage(msg);
          //  forum.Incoming();
        }

        public void exit()
        {
            GuiForumListener.exit_flag = false;
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
    }
}
