using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MessagePack;
using Client;

namespace Protocol {
    public class EandEProtocol : ProtocolInterface {

        ForumTcpClient _client;


        public EandEProtocol()
        {
            _client = new ForumTcpClient();

        }

        public EandEProtocol(int p_port_num, string p_ip_num)
        {
            _client = new ForumTcpClient(p_port_num,p_ip_num);
            
        }

        public void connect() {
            _client.connect();            
        }

        public void disconnect() {
            _client.disconnect();
        }

        public Message getMessage()
        {
            Message message;            
            string msg = _client.receive();
            EandETokenizer tok = new EandETokenizer(msg, "\n");
            string next = tok.getNextToken();            
            switch (next) {
                
                /**
                * POSTCONTENT\n
                * <fourumID>\n
                * <threadID>\n
                * <postIndex>\n
                * <username>\n
                * <author>\n                 
                * <subject>\n
                * <body>\n
                **/
                case "POSTCONTENT":
                    message = new PostContentMessage(Convert.ToInt32(tok.getNextToken()), Convert.ToInt32(tok.getNextToken()),
                                    Convert.ToInt32(tok.getNextToken()), Convert.ToInt32(tok.getNextToken()), tok.getNextToken(), tok.getNextToken(), tok.getNextToken(), tok.getNextToken());
                    return message;


                /**
                * THREADCONTENT\n
                * <forumID>\n
                * <threadID>\n                
                * <username>\n
                * <Topic #1>\n                 
                * ...........
                * <Topic #n>\n                 

                **/
                case "THREADCONTENT":
                    int forumID = Convert.ToInt32(tok.getNextToken());
                    int threadID = Convert.ToInt32(tok.getNextToken());
                    string uName = tok.getNextToken();
                    List<Quartet> topics = new List<Quartet>();
                    String tStr;
                    Quartet quad;
                    int pind, par;
                    string sub, auth;
                    while(!(tStr = tok.getNextToken()).Equals("\0")){
                        pind = Convert.ToInt32(tStr);
                        par = Convert.ToInt32(tok.getNextToken());
                        sub = tok.getNextToken();
                        auth = tok.getNextToken();
                        quad = new Quartet(pind, par, sub, auth);
                        topics.Add(quad);
                    }
                    message = new ThreadContentMessage(forumID, threadID, uName, topics);
                    return message;

                /**
            * FORUMCONTENT\n
            * <forumID>\n                
            * <username>\n
            * <Topic #1>\n                 
            * ...........
            * <Topic #n>\n                 

            **/
                case "FORUMCONTENT":
                    int t_forumID = Convert.ToInt32(tok.getNextToken());
                    string t_uName = tok.getNextToken();
                    List<string> t_topics = new List<string>();
                    String t_tStr;
                    while (!(t_tStr = tok.getNextToken()).Equals("\0")) {
                        t_topics.Add(t_tStr);
                    }
                    message = new ForumContentMessage(t_forumID, t_uName, t_topics);
                    return message;
                    
                /**
                * SYSTEMCONTENT\n
                * <username>\n
                * <forums>\n
                **/
                case "SYSTEMCONTENT":
                    string t_uName2 = tok.getNextToken();
                    string t_temp;
                    List<string> t_forums = new List<string>();
                    while((t_temp = tok.getNextToken()) != "\0")
                        t_forums.Add(t_temp);
                    message = new SystemContentMessage(t_uName2, t_forums);
                    return message;

                /**
                * ERROR\n
                * <errormsg>\n
                **/
                case "ERROR":
                    message = new Error(tok.getNextToken(), tok.getNextToken());
                    return message;

                /**
                 * Acknowledgment\n
                 * <errormsg>\n
                 **/
                case "Acknowledgment":
                    message = new Acknowledgment(tok.getNextToken(), tok.getNextToken());
                    return message;
            }
            Console.WriteLine("(this is the e&e protocol talking) unknown massage! plese check it..... probably will crash now....");
            return null;
        }

        public void sendMessage(Message msg) {
            _client.send(msg.ToString());
        }
    }
}
