﻿using System;
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
                    Console.WriteLine("system content");
                    break;
                case "FORUMCONTENT":
                    Console.WriteLine("forum content");
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