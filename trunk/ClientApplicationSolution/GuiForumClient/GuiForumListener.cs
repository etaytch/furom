using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Protocol;
using MessagePack;
using DataManagment;

namespace GuiForumClient
{
    public class GuiForumListener
    {
        EandEProtocol protocol;
        Message msg;
        Database db;
        public static bool exit_flag;

        public GuiForumListener(EandEProtocol p, Database p_db)
        {
            protocol = p;
            db = p_db;
            exit_flag = true;
        }

        public void run()
        {
            while (exit_flag)
            {
              Incoming();
            }

        }
        public void Incoming()
        {
            msg = protocol.getMessage();   
            switch (msg.getMessageType()) 
            {
                case "Acknowledgment":
                    handle_Acknowledgment((Acknowledgment)msg);
                    break;
                case "Error":
                    handle_Error((Error)msg);
                    break;
                case "SYSTEMCONTENT":
                    handle_SystemContentMessage((SystemContentMessage)msg);
                    break;
                case "FORUMCONTENT":
                    handle_ForumContentMessage((ForumContentMessage)msg);
                    break;
                case "THREADCONTENT":
                    handle_ThreadContentMessage((ThreadContentMessage)msg);
                    break;
                case "USERSCONTENT":
                    handle_UsersContentMessage((UsersContentMessage)msg);
                    break;
                case "FRIENDSCONTENT":
                    handle_FriendsContentMessage((FriendsContentMessage)msg);
                    break;
                case "POSTCONTENT":
                    handle_PostContentMessage((PostContentMessage)msg);
                    break;
                case "POPUPCONTENT":
                    handle_PopupContentMessage((PopUpContent)msg);
                    break;
                default:
                    break;
            }
        }

        private void handle_FriendsContentMessage(FriendsContentMessage friendsContentMessage)
        {
            db.Friends = friendsContentMessage._friends;
        }

        private void handle_UsersContentMessage(UsersContentMessage usersContentMessage)
        {
            db.Users = usersContentMessage._users;
        }

        private void handle_PostContentMessage(PostContentMessage postContentMessage)
        {
            db.setCurrent(postContentMessage._subject, postContentMessage._author, postContentMessage._body, postContentMessage._pIndex);
        }
        private void handle_PopupContentMessage(PopUpContent PopupContentMessage)
        {
            db.PopupMassege = PopupContentMessage.getMsg();
        }
        private void handle_ThreadContentMessage(ThreadContentMessage threadContentMessage)
        {
            int t_tId = threadContentMessage._tId;
           //List<string> t_topics = getTopics( threadContentMessage._posts);
            db.cleanPosts();
            db.cleanCurrentPost();
            db.CurrentPost = new PostObject(threadContentMessage._subject, threadContentMessage._author, threadContentMessage._body, threadContentMessage._fId);
            foreach (Quartet post in threadContentMessage._posts)
            {
                
                db.addPost(post);
            }
        }

        private void handle_ForumContentMessage(ForumContentMessage forumContentMessage)
        {
            int t_fId = forumContentMessage._fId;
            //List<string> t_topics = getTopics(forumContentMessage._topics);
            db.cleanThreads();
            foreach (Quartet thread in forumContentMessage._topics)
            {     
                db.addThread(thread._subject, thread._pIndex);
            }

        }

        private List<string> getTopics(List<Quartet> list)
        {
            List<string> topics= new List<string>();
            foreach (Quartet post in list)
            {
                topics.Add(post._subject);
            }
            return topics;
        }

        private void handle_SystemContentMessage(SystemContentMessage p_sysContentMessage)
        {
            List<Quartet> t_topics = p_sysContentMessage._forums;
            db.cleanForums();
            if (t_topics != null)
            {
                foreach (Quartet forum in t_topics)
                {
                    db.addForum(forum);
                }
            }
        }

        private void handle_Error(Error msg)
        {
            db.Massege = msg.getMsg();
        }

        private void handle_Acknowledgment(Acknowledgment msg)
        {
            db.Massege = msg.getMsg();
        }
    }
}
