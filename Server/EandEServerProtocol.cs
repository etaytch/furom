﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MessagePack;
using Server;
using common;

namespace Protocol {
    public class EandEProtocol : ProtocolInterface {

        System.Collections.Hashtable _table;
        ForumTcpServer _vadi;
        Queue<string> msgs;
        BasicMassage msg = null;

        public void startServer()
        {
            _vadi.startServer();
        }

        public void stopServer()
        {
            _vadi.stopServer();
        }

        public EandEProtocol() 
        {
            _table = new System.Collections.Hashtable(1000);
            _vadi = new ForumTcpServer();
            msgs = new Queue<string>();
        }        

        public void divideMsgs(string msg) {
            
            char c = (char)4;
            string restMsg = msg;
            string tmpStr;
            int ind = restMsg.IndexOf(c);
            while(ind>=0){
                tmpStr = restMsg.Substring(0, ind);
                Console.WriteLine("current MSG = "+tmpStr);
                this.msgs.Enqueue(tmpStr);
                restMsg = restMsg.Substring(ind + 1);
                ind = restMsg.IndexOf(c);
            }            
        }

        public Message getMessage()
        {
            Message message;
            if (msgs.Count == 0) {
                msg = _vadi.receive();
                divideMsgs(msg.Massage);
            }

            string str = msgs.Dequeue(); 
            
            string uName;
            EandETokenizer tok = new EandETokenizer(str, "/$");
            string next = tok.getNextToken();            
            switch (next) {
                /**
                * LOGIN\n
                * <username>\n
                * <password>\n
                **/ 
                case "LOGIN":
                    message = new LoginMessage(uName = tok.getNextToken(), tok.getNextToken());
                    if (!_table.ContainsKey(uName))
                        _table.Add(uName, msg.Uid);
                    else
                        _table[uName] = msg.Uid;
                    return message;

                /**
                * LOGOUT\n
                * <username>\n
                **/ 
                case "LOGOUT":
                    message = new LogoutMessage(uName = tok.getNextToken());
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
                                        Convert.ToInt32(tok.getNextToken()), Convert.ToInt32(tok.getNextToken()), uName = tok.getNextToken(), tok.getNextToken(), tok.getNextToken());
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
                                        uName = tok.getNextToken(), tok.getNextToken(), tok.getNextToken());
                        return message;
                    /**
                    * GETUSERS\n
                    * <username>\n                
                    **/
                    case "GETUSERS":
                        message = new GetUsersMessage(uName = tok.getNextToken());
                        return message;

                    /**
                    * GETFRIENDS\n
                    * <username>\n                
                    **/
                    case "GETFRIENDS":
                        message = new GetFriendsMessage(uName = tok.getNextToken());
                        return message;

                    /**
                    * ADDFRIEND\n
                    * <username>\n
                    * <friendUsername>\n
                    **/
                    case "ADDFRIEND":
                        message = new AddFriendMessage(uName = tok.getNextToken(), tok.getNextToken());
                        return message;
                    /**
                    * REGISTER\n
                    * <firstName>\n
                    * <lastName>\n
                    * <username>\n
                    * <password>\n
                    * <confirmedPassword>\n
                    * <sex>\n
                    * <country>\n
                    * <city>\n
                    * <email>\n
                    * <birthday>\n
                    **/

                        //RegisterMessage msg = new RegisterMessage(p_fName, p_lName, p_uName, p_password, p_cpassword, p_sex, p_email, p_birthday, p_country, p_city);
                    case "REGISTER":
                        message = new RegisterMessage(tok.getNextToken(), tok.getNextToken(), uName = tok.getNextToken(), tok.getNextToken(),
                                        tok.getNextToken(), tok.getNextToken(), tok.getNextToken(), tok.getNextToken(), tok.getNextToken(), tok.getNextToken());
                        if (!_table.ContainsKey(uName))
                            _table.Add(uName, msg.Uid);
                        return message;
                    /**
                    * REMOVEFRIEND\n
                    * <userame>\n
                    * <friendUsername>\n
                    **/
                    case "REMOVEFRIEND":
                        message = new RemoveFriendMessage(uName = tok.getNextToken(), tok.getNextToken());
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
                                    Convert.ToInt32(tok.getNextToken()), uName = tok.getNextToken());
                    return message;

                /**
                * DELETETHREAD\n
                * <fourumID>\n
                * <threadID>\n
                * <userame>\n
                **/
                case "DELETETHREAD":
                    int fid = Convert.ToInt32(tok.getNextToken());
                    int tid = Convert.ToInt32(tok.getNextToken());
                    int postIndex = Convert.ToInt32(tok.getNextToken());
                    string uname = tok.getNextToken();
                    //message = new DeleteThreadMessage(Convert.ToInt32(tok.getNextToken()), Convert.ToInt32(tok.getNextToken()), uName = tok.getNextToken());
                    message = new DeleteThreadMessage(fid, tid, uname);
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
                                    Convert.ToInt32(tok.getNextToken()), uName = tok.getNextToken());
                    return message;
                /**
                * GETTHREAD\n
                * <fourumID>\n
                * <threadID>\n
                * <username>\n
                **/
                case "GETTHREAD":
                    message = new GetThreadMessage(Convert.ToInt32(tok.getNextToken()),
                                    Convert.ToInt32(tok.getNextToken()), uName = tok.getNextToken());
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
                    message = new GetForumMessage(Convert.ToInt32(tok.getNextToken()), uName = tok.getNextToken());
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
                default:
                    return new Error("System","Unrecognized Message for the protocol to send!");
            }                        
        }

        public void sendMessage(Message msg) {
            Object t_temp = _table[msg._uName];
            if (t_temp!=null)
            {
                _vadi.send(msg.ToString()+(char)4, (int)t_temp);
            }
        }


    }
}
