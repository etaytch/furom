using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Protocol;
using MessagePack;

namespace ForumClient
{
    public class Client 
    {
        string userName;
        EandEProtocol protocol;
        ForumListener forum;

        public Client(string uName, EandEProtocol p, ForumListener f)
        {
            protocol = p;
            userName = uName;
        }

        public void logout()
        {
            LogoutMessage msg = new LogoutMessage(userName);
            protocol.sendMessage(msg);
            forum.Incoming();
        }

        public void getForums()
        {
            GetSystemMessage msg = new GetSystemMessage(userName);
            protocol.sendMessage(msg);
            forum.Incoming();
        }

        public void getThreads(string fId)
        {
            int fIdInt = Int32.Parse(fId);
            GetForumMessage msg = new GetForumMessage(fIdInt, userName);
            protocol.sendMessage(msg);
            forum.Incoming();
        }

        public void getReplies(string fId, string tId)
        {
            int fIdInt = Int32.Parse(fId);
            int tIdInt = Int32.Parse(tId);
            GetThreadMessage msg = new GetThreadMessage(fIdInt, tIdInt, userName);
            protocol.sendMessage(msg);
            forum.Incoming();
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
            forum.Incoming();
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
            forum.Incoming();
        }

        public void addFriend(string friend)
        {
            AddFriendMessage msg = new AddFriendMessage(userName, friend);
            protocol.sendMessage(msg);
            forum.Incoming();
        }

        public void removeFriend(string friend)
        {
            RemoveFriendMessage msg = new RemoveFriendMessage(userName, friend);
            protocol.sendMessage(msg);
            forum.Incoming();
        }
    }
}
