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
                * LOGIN\n
                * <username>\n
                * <password>\n
                **/ 
                case "LOGIN":
                    message = new LoginMessage(tok.getNextToken(), tok.getNextToken());///check thet                    
                    return message;

                /**
                * LOGOUT\n
                * <username>\n
                **/ 
                case "LOGOUT":
                    message = new LogoutMessage(tok.getNextToken());
                    return message;
                /**
                * ADDPOST\n
                * <fourumID>\n
                * <threadID>\n
                * <postIndex>\n
                * <username>\n
                * <postSubjec>\n
                * <postBody>\n
                **/
                case "ADDPOST":
                    message = new AddPostMessage(Convert.ToInt32(tok.getNextToken()), Convert.ToInt32(tok.getNextToken()), 
                                    Convert.ToInt32(tok.getNextToken()), tok.getNextToken(), tok.getNextToken(), tok.getNextToken());
                    return message;
                
                /**
                * ADDFORUM\n
                * <username>\n
                * <topic>\n
                **/
                case "ADDFORUM":
                    message = new AddForumMessage(tok.getNextToken(), tok.getNextToken());
                    return message;


                /**
                * ADDTHREAD\n
                * <fourumID>\n                
                * <username>\n
                * <threadSubjec>\n
                * <threadBody>\n
                **/
                case "ADDTHREAD":
                    message = new AddThreadMessage(Convert.ToInt32(tok.getNextToken()), 
                                    tok.getNextToken(), tok.getNextToken(), tok.getNextToken());
                    return message;
                /**
                * ADDFRIEND\n
                * <username>\n
                * <friendUsername>\n
                **/
                case "ADDFRIEND":
                    message = new AddFriendMessage(tok.getNextToken(), tok.getNextToken());///check thet
                    return message;
                /**
                * REGISTER\n
                * <firstName>\n
                * <lastName>\n
                * <password>\n
                * <confirmedPassword>\n
                * <sex>\n
                * <country>\n
                * <city>\n
                * <email>\n
                * <birthday>\n
                **/
                case "REGISTER":
                    message = new RegisterMessage(tok.getNextToken(), tok.getNextToken(), tok.getNextToken(), tok.getNextToken(), 
                                    tok.getNextToken(), tok.getNextToken(), tok.getNextToken(), tok.getNextToken(), tok.getNextToken(), tok.getNextToken());///check thet
                    return message;
                /**
                * REMOVEFRIEND\n
                * <userame>\n
                * <friendUsername>\n
                **/
                case "REMOVEFRIEND":
                    message = new RemoveFriendMessage(tok.getNextToken(), tok.getNextToken());///check thet
                    return message;

                /**
                * DELETEPOST\n
                * <fourumID>\n
                * <threadID>\n
                * <postIndex>\n
                * <username>\n
                **/
                case "DELETEPOST":
                    message = new DeletePostMessage(Convert.ToInt32(tok.getNextToken()), Convert.ToInt32(tok.getNextToken()),
                                    Convert.ToInt32(tok.getNextToken()), tok.getNextToken());
                    return message;

                /**
                * DELETETHREAD\n
                * <fourumID>\n
                * <threadID>\n
                * <postIndex>\n
                **/
                case "DELETETHREAD":
                    message = new DeleteThreadMessage(Convert.ToInt32(tok.getNextToken()), Convert.ToInt32(tok.getNextToken()), tok.getNextToken());
                    return message;

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
                                    Convert.ToInt32(tok.getNextToken()), tok.getNextToken(), tok.getNextToken(), tok.getNextToken(), tok.getNextToken());
                    return message;


                /**
                * GETPOST\n
                * <fourumID>\n
                * <threadID>\n
                * <postIndex>\n
                * <username>\n
                **/
                case "GETPOST":
                    message = new GetPostMessage(Convert.ToInt32(tok.getNextToken()), Convert.ToInt32(tok.getNextToken()),
                                    Convert.ToInt32(tok.getNextToken()), tok.getNextToken());
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
                    List<string> topics = new List<string>();
                    String tStr;
                    while(!(tStr = tok.getNextToken()).Equals("")){
                        topics.Add(tStr);
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
                    while (!(t_tStr = tok.getNextToken()).Equals("")) {
                        t_topics.Add(t_tStr);
                    }
                    message = new ForumContentMessage(t_forumID, t_uName, t_topics);
                    return message;
                                                
                /**
                * GETTHREAD\n
                * <fourumID>\n
                * <threadID>\n
                * <username>\n
                **/
                case "GETTHREAD":
                    message = new DeleteThreadMessage(Convert.ToInt32(tok.getNextToken()),
                                    Convert.ToInt32(tok.getNextToken()), tok.getNextToken());
                    return message;

                /**
                * GETSYSTEM\n
                * <username>\n
                **/
                case "GETSYSTEM":
                    message = new GetSystemMessage(tok.getNextToken());
                    return message;

                /**
                * GETFORUM\n
                * <fourumID>\n
                * <username>\n
                **/
                case "GETFORUM":
                    message = new GetForumMessage(Convert.ToInt32(tok.getNextToken()),tok.getNextToken());
                    return message;

                /**
                * ERROR\n
                * <errormsg>\n
                **/
                case "ERROR":
                    message = new Error(tok.getNextToken(), tok.getNextToken());
                    return message;

                /**
                 * ERROR\n
                 * <errormsg>\n
                 **/
                case "Acknowledgment":
                    message = new Acknowledgment(tok.getNextToken(), tok.getNextToken());
                    return message;
            }
            return null;
        }

        public void sendMessage(Message msg) {
            _client.send(msg.ToString());
        }
    }
}
