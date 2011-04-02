using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Protocol;
using MessagePack;

namespace ForumClient
{
    public class ForumListener
    {
        EandEProtocol protocol;
        Message msg;

        public ForumListener(EandEProtocol p)
        {
            protocol = p;
        }

        public void Incoming()
        {
            msg = protocol.getMessage();   
            switch (msg.getMessageType()) 
            {
                case "Acknowledgement":
                    Acknowledgment ack = (Acknowledgment)msg;
                    Console.WriteLine("Ack:" + ack.getMsg());
                    break;
                case "Error":
                    Error err = (Error)msg;
                    Console.WriteLine("Error: " + err.getMsg());
                    break;
                case "SYSTEMCONTENT":
                    SystemContentMessage systemContent = (SystemContentMessage)msg;
                    int fNum = 0;
                    Console.WriteLine("Forums: ");
                    foreach (string forum in systemContent._forums)
                    {
                        Console.WriteLine(fNum + ". " + forum);
                        fNum++;
                    }
                    break;
                case "FORUMCONTENT":
                    ForumContentMessage forumContent = (ForumContentMessage)msg;
                    Console.WriteLine("Forum: " + forumContent._fId);
                    foreach (string topic in forumContent._topics)
                        Console.WriteLine(topic);
                    break;
                case "THREADCONTENT":
                    ThreadContentMessage thread = (ThreadContentMessage)msg;
                    Console.WriteLine("Forum: " +  thread._fId);
                    Console.WriteLine("Thread: " + thread._tId);
                    break;
                case "POSTCONTENT":
                    PostContentMessage message = (PostContentMessage)msg;
                    Console.WriteLine("post content");
                    break;
                default:
                    break;
            }
        }
    }
}
