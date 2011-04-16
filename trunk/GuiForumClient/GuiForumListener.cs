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
                case "Acknowledgement":
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
                case "POSTCONTENT":
                    handle_PostContentMessage((PostContentMessage)msg);
                    break;
                default:
                    break;
            }
        }

        private void handle_PostContentMessage(PostContentMessage postContentMessage)
        {
            db.setCurrent(postContentMessage._subject, postContentMessage._author, postContentMessage._body);
        }

        private void handle_ThreadContentMessage(ThreadContentMessage threadContentMessage)
        {
            int t_tId = threadContentMessage._tId;
            List<string> t_topics = threadContentMessage._topics;
            foreach (string topic in t_topics)
            {
                db.cleanPost();
                db.addPost(topic);
            }
        }

        private void handle_ForumContentMessage(ForumContentMessage forumContentMessage)
        {
            int t_fId = forumContentMessage._fId;
            List<string> t_topics = forumContentMessage._topics;
            foreach (string topic in t_topics)
            {
                db.cleanThreads();
                db.addThread(topic);
            }

        }

        private void handle_SystemContentMessage(SystemContentMessage systemContentMessage)
        {
            List<string> t_topics = systemContentMessage._forums;
            foreach (string topic in t_topics)
            {
                db.cleanForum();
                db.addForum(topic);
            }
        }

        private void handle_Error(Error msg)
        {
            throw new NotImplementedException();
        }

        private void handle_Acknowledgment(Acknowledgment msg)
        {
            throw new NotImplementedException();
        }
    }
}
