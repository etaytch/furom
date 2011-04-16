using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Protocol;
using MessagePack;
using DataManagment;
using System.Threading;

namespace GuiForumClient
{
    public class GuiClient 
    {
        string ip = "10.100.101.196";
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

        public void getThreads(string fId)
        {
            int fIdInt = Int32.Parse(fId);
            GetForumMessage msg = new GetForumMessage(fIdInt, userName);
            protocol.sendMessage(msg);
           // forum.Incoming();
        }

        public void getReplies(string fId, string tId)
        {
            int fIdInt = Int32.Parse(fId);
            int tIdInt = Int32.Parse(tId);
            GetThreadMessage msg = new GetThreadMessage(fIdInt, tIdInt, userName);
            protocol.sendMessage(msg);
            //forum.Incoming();
        }

        public void reply(string fId, string tId)
        {
            int fIdInt = Int32.Parse(fId);
            int tIdInt = Int32.Parse(tId);
            Console.Write("Enter subject: ");
            string subject = Console.ReadLine();
            Console.Write("Enter content: ");
            string content = Console.ReadLine();
            AddPostMessage msg = new AddPostMessage(fIdInt, tIdInt, 0, userName, subject, content);
            protocol.sendMessage(msg);
           // forum.Incoming();
        }

        public void post(string fId)
        {
            int fIdInt = Int32.Parse(fId);
            Console.Write("Enter subject: ");
            string subject = Console.ReadLine();
            Console.Write("Enter content: ");
            string content = Console.ReadLine();
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
        }
    }
}
