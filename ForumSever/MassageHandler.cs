﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using common;
using MessagePack;
using Server;
using System.Threading;
using Protocol;
using Log;
namespace ForumSever
{
    public class MassageHandler
    {
        private LogicManager _lm;
        private EandEProtocol _ee;

        public MassageHandler()
        {            
            _lm = new LogicManager("app");
            _ee = new EandEProtocol();
        }

        public MassageHandler(LogicManager lm)
        {    
            _lm = lm;
            _ee = new EandEProtocol();
        }

        public LogicManager lm{
            get {return _lm;}
        }

        public EandEProtocol ee {
            get { return _ee; }
        }


        public void startForum()
        {
            _ee.startServer();
            //Logger.append("StartingTcpServer", Logger.INFO);

        }

        public void stopForum()
        {
            _ee.stopServer();
            //logger.log(2, "deinit", "stoping server");
        }


        public void readMassage(){
            Message t_msg = _ee.getMessage();
            switch (t_msg.getMessageType()) {
                case "LOGIN": 
                    Handle((LoginMessage)t_msg); 
                    break;
                case "LOGOUT": 
                    Handle((LogoutMessage)t_msg);
                    break;
                case "ADDPOST": 
                    Handle((AddPostMessage)t_msg);
                    break;
                case "ADDTHREAD": 
                    Handle((AddThreadMessage)t_msg);      
                    break;
                case "GETUSERS": 
                    Handle((GetUsersMessage)t_msg); 
                    break;
                case "GETFRIENDS": 
                    Handle((GetFriendsMessage)t_msg);
                    break;
                case "ADDFRIEND":
                    Handle((AddFriendMessage)t_msg);
                    break;
                case "REGISTER": 
                    Handle((RegisterMessage)t_msg);
                    break;                    
                case "REMOVEFRIEND":
                    Handle((RemoveFriendMessage)t_msg);
                    break;
                case "DELETEPOST":
                    Handle((DeletePostMessage)t_msg);
                    break;
                case "DELETETHREAD":
                    Handle((DeleteThreadMessage)t_msg);
                    break;
                case "GETPOST": 
                    Handle((GetPostMessage)t_msg);
                    break;                   
                case "GETTHREAD":
                    Handle((GetThreadMessage)t_msg);
                    break;                    
                case "GETSYSTEM":
                    Handle((GetSystemMessage)t_msg);
                    break;                    
                case "GETFORUM":
                    Handle((GetForumMessage)t_msg);
                    break;                    
                default:
                    break;
            }
        }


        private void Handle(LoginMessage t_loginMsg){
            int returnValue;
            string t_uname;
            string t_pass;
            t_uname     =   t_loginMsg._uName;
            t_pass      =   t_loginMsg._password;
                    
            returnValue = _lm.login(t_uname, t_pass);
            if (returnValue < 0) {
                sendError(returnValue, t_uname);
            }
            else {
                _ee.sendMessage(new Acknowledgment(t_uname, "Login Succssfuly"));
                //logger.log(2, "", t_uname + " logged succssfuly");
            }
        }


        private void Handle(LogoutMessage t_logoutMsg) {
            int returnValue;
            string t_uname;
            t_uname = t_logoutMsg._uName;
            returnValue = _lm.logout(t_uname);
            if (returnValue < 0) {
                sendError(returnValue, t_uname);
            }
            else {
                _ee.sendMessage(new Acknowledgment(t_uname, "Logout Succssfuly"));
                // logger.log(2, "", t_uname + " logged out succssfuly");
            }
        }


        private void Handle(AddPostMessage t_addPostMsg) {
            int returnValue;
            string t_uname;
            int t_tid;
            int t_fid;
            int t_postIndex;
            string t_topic;
            string t_content;
            ForumThread returnThread;

            t_uname = t_addPostMsg._uName;
            t_tid = t_addPostMsg._tId;
            t_fid = t_addPostMsg._fId;
            // t_postIndex = parentId???
            t_postIndex = t_addPostMsg._pIndex;
            t_topic = t_addPostMsg._subject;
            t_content = t_addPostMsg._post;

            returnValue = _lm.addPost(t_tid, t_fid, t_postIndex, t_topic, t_content, t_uname);
            if (returnValue < 0) {
                sendError(returnValue, t_uname);
            }
            else {
                _ee.sendMessage(new Acknowledgment(t_uname, "AddPost Succssfuly"));
                returnThread = _lm.getThread(t_fid, t_tid);
                string threadName = returnThread._topic;//_lm.getThreadName(t_fid,t_tid);
                string forumName = _lm.getForumName(t_fid);
                //logger.log(2, "", "User " + t_uname + " added new post \"" + t_topic + "\" to thread: \"" + threadName + "\" in forum: \"" + forumName + "\"");
                List<string> usersToUpdate = _lm.getFriendsToUpdate(t_uname);   // friends
                //Console.WriteLine(usersToUpdate.ToString());
                List<string> viewersToUpdate = _lm.getThreadViewersToUpdate(t_uname, t_fid, t_tid);
                usersToUpdate.Union<string>(viewersToUpdate);

                usersToUpdate.Remove(returnThread._autor);
                foreach (string friend in usersToUpdate) {
                    //Console.WriteLine("checking friend: " + friend);
                    if (_lm.isLogged(friend)) {
                        //Console.WriteLine("***Sending popup to: "+friend);

                        _ee.sendMessage(new PopUpContent(friend, "User " + t_uname + " added new post \"" + t_topic + "\" to thread: \"" + threadName + "\" in forum: \"" + forumName + "\""));

                    }
                }
                if (_lm.isLogged(returnThread._autor)) {
                    _ee.sendMessage(new PopUpContent(returnThread._autor, "User " + t_uname + " added new post \"" + t_topic + "\" to your thread: \"" + threadName + "\" in forum: \"" + forumName + "\""));
                }
                returnThread = _lm.getThread(t_fid, t_tid);
                List<Quartet> posts = _lm.getThreadPosts(t_fid, t_tid);
                foreach (string viewer in viewersToUpdate) {
                    _ee.sendMessage(new ThreadContentMessage(t_fid, t_tid, viewer, returnThread._autor, returnThread._topic, returnThread._content, posts));
                }                
            }
        }
       


        private void Handle(AddThreadMessage t_addThreadMsg) {
            int returnValue;
            string t_uname;
            string t_topic;
            string t_content;
            int t_fid;

            t_uname     =   t_addThreadMsg._uName;
            t_fid       =   t_addThreadMsg._fId;
            t_topic     =   t_addThreadMsg._subject;
            t_content   =   t_addThreadMsg._post;
                    
            returnValue = _lm.addTread(t_uname, t_fid, t_topic, t_content);
            if (returnValue < 0)
            {
                sendError(returnValue, t_uname);
            }
            else {
                _ee.sendMessage(new Acknowledgment(t_uname, "AddThread Succssfuly"));
                string forumName = _lm.getForumName(t_fid);
                // logger.log(2, "", "User " + t_uname + " added new topic \"" + t_topic + "\" to forum: \"" + forumName + "\"");
                List<string> usersToUpdate = _lm.getFriendsToUpdate(t_uname);
                        
                foreach (string friend in usersToUpdate) {
                    if (_lm.isLogged(friend)) {
                        _ee.sendMessage(new PopUpContent(friend, "User " + t_uname + " added new topic \"" + t_topic + "\" to forum: \"" + forumName + "\""));
                    }
                }

                usersToUpdate = _lm.getForumViewers(t_fid);
                List<Quartet> forumTopics = _lm.getForum(t_fid);
                foreach (string viewer in usersToUpdate) {                            
                    _ee.sendMessage(new ForumContentMessage(t_fid, viewer, forumTopics));
                }                
            } 
        } 
      

        private void Handle(AddForumMessage t_addForumMsg) {
            int returnValue;
            string t_uname;
            int t_userID;
            string t_topic;
            MemberInfo tMem;    

            t_uname     =   t_addForumMsg._uName;
            tMem        =   _lm.FindMemberByUser(t_uname);
            t_userID    =   _lm.FindMemberByUser(t_uname).getID();
            t_topic     =   t_addForumMsg._topic;

            returnValue = _lm.addForum(t_uname, t_topic);
            if (returnValue < 0) {
                sendError(returnValue, t_uname);
            }
            else {
                _ee.sendMessage(new Acknowledgment(t_uname, "AddForum Succssfuly"));
                //logger.log(2, "", "User " + t_uname + " added new forum \"" + t_topic + "\".");
            }
        }


        private void Handle(GetUsersMessage t_getUsersMsg) {
            string t_uname;        

            t_uname     =    t_getUsersMsg._uName;
                    
            if (_lm.isMember(t_uname)) {
                if (_lm.isLogged(t_uname)) {
                    List<string> users = _lm.getUsers(t_uname);
                    _ee.sendMessage(new UsersContentMessage(t_uname, users));
                    //logger.log(2, "", "list of users sent to "+t_uname);
                }
                else {
                    //logger.log(2, "init", "ERROR GETUSERS: username: " + t_uname+" is not logged in");
                    sendError(-19, t_uname);
                }
            }
            else {
                //logger.log(2, "init", "ERROR GETUSERS: incorrect username: " + t_uname);
                sendError(-3, t_uname);
            }
        }


        private void Handle(GetFriendsMessage t_getFriendsMsg) {
            string t_uname;       

            t_uname     =   t_getFriendsMsg._uName;

            if (_lm.isMember(t_uname)) {
                if (_lm.isLogged(t_uname)) {
                    List<string> friends = _lm.getFriends(t_uname);
                    _ee.sendMessage(new FriendsContentMessage(t_uname, friends));
                    //logger.log(2, "", "list of friends sent to " + t_uname);
                }
                else {
                    //logger.log(2, "init", "ERROR GETFRIENDS: username: " + t_uname+" is not logged in");
                    sendError(-19, t_uname);
                }
            }
            else {
                //logger.log(2, "init", "ERROR GETFRIENDS: incorrect username: " + t_uname);
                sendError(-3, t_uname);
            }
        }


        private void Handle(AddFriendMessage t_addFriendMsg) {
            int returnValue;
            string t_uname;
            string t_friendUname;
               
            t_uname         =       t_addFriendMsg._uName;
            t_friendUname   =       t_addFriendMsg._friend;
                    
            returnValue = _lm.addMeAsFriend(t_uname,t_friendUname);
            if (returnValue < 0)
            {
                sendError(returnValue, t_uname);
            }
            else {
                _ee.sendMessage(new Acknowledgment(t_uname, "AddFriend Succssfuly"));
                // logger.log(2, "", t_uname + " added "+t_friendUname+" to his list of friends.");
            }  
        }


        private void Handle(RegisterMessage t_registerMsg) {
            string t_uname, t_fname, t_lname, t_pass, t_sex, t_email, t_birthday, t_city, t_country;
            int returnValue;        
            t_uname         =       t_registerMsg._uName;
            t_fname         =       t_registerMsg._fName;
            t_lname         =       t_registerMsg._lName;
            t_pass          =       t_registerMsg._password;
            t_sex           =       t_registerMsg._sex;
            t_country       =       t_registerMsg._country;
            t_city          =       t_registerMsg._city;
            t_email         =       t_registerMsg._email;
            t_birthday      =       t_registerMsg._birthday;
            MemberInfo memb = new MemberInfo(t_uname, t_fname, t_lname, t_pass, t_sex, t_country, t_city, t_email, t_birthday, "0");                    
            returnValue = _lm.register(memb);
            if (returnValue < 0)
            {
                sendError(returnValue, t_uname);
            }
            else {
                _ee.sendMessage(new Acknowledgment(t_uname, "REGISTER Succssfuly."));
                // logger.log(2, "", t_uname + " registered succssfuly.");
                List<string> users = _lm.getUsers(t_uname);
                foreach (string str in users) {
                    if(!t_uname.Equals(str)){
                        _ee.sendMessage(new UsersContentMessage(str, users));
                    }                            
                }                                                
            }   
        }


        private void Handle(RemoveFriendMessage t_removeFriendMsg) {
            int returnValue;
            string t_uname;
            string t_friendUname;        

            t_uname         =       t_removeFriendMsg._uName;
            t_friendUname   =       t_removeFriendMsg._friend;

            returnValue = _lm.removeMeAsFriend(t_uname, t_friendUname);
            if (returnValue < 0)
            {
                sendError(returnValue, t_uname);
            }
            else {
                _ee.sendMessage(new Acknowledgment(t_uname, "RemoveFriend Succssfuly"));
                //logger.log(2, "", t_uname + " removed " + t_friendUname + " from his list of friends.");

            }  
        }      


        private void Handle(DeletePostMessage t_deletePostMsg) {
            int returnValue;
            int t_tid;
            int t_fid;
            int t_postIndex;
            string t_uname;
            ForumThread returnThread;

                
            t_uname        =    t_deletePostMsg._uName;
            t_tid          =    t_deletePostMsg._tId;
            t_fid          =    t_deletePostMsg._fId;
            t_postIndex    =    t_deletePostMsg._pIndex;
                        
            returnValue = _lm.removePost(t_fid, t_tid, t_postIndex,t_uname);
            if (returnValue < 0)
            {
                sendError(returnValue, t_uname);
            }
            else {
                _ee.sendMessage(new Acknowledgment(t_uname, "DeletePost Succssfuly"));
                // logger.log(2, "", t_uname + " deleted post succssfuly.");
                returnThread = _lm.getThread(t_fid, t_tid);
                List<Quartet> posts = _lm.getThreadPosts(t_fid, t_tid);
                List<string> usersToUpdate = _lm.getThreadViewersToUpdate(t_uname,t_fid,t_tid);
                if (!usersToUpdate.Contains(t_uname)) {
                    usersToUpdate.Add(t_uname);
                }
                //_ee.sendMessage(new ForumContentMessage(t_fid, t_uname, forumTopics));                        
                foreach (string viewer in usersToUpdate) {
                    _ee.sendMessage(new ThreadContentMessage(t_fid, t_tid, viewer, returnThread._autor, returnThread._topic, returnThread._content, posts));
                }

            }
        }


        private void Handle(DeleteThreadMessage t_deleteThreadMsg) {
            int returnValue;
            int t_tid;
            int t_fid;
            string t_uname;
                
            t_uname     =       t_deleteThreadMsg._uName;
            t_tid       =       t_deleteThreadMsg._tId;
            t_fid       =       t_deleteThreadMsg._fId;
                    
            returnValue = _lm.removeThread(t_fid,t_tid,t_uname);
            if (returnValue < 0)
            {
                sendError(returnValue, t_uname);
            }
            else {
                _ee.sendMessage(new Acknowledgment(t_uname, "DeleteThread Succssfuly"));
                // logger.log(2, "", t_uname + " deleted thread succssfuly.");
                List<Quartet> forumTopics = _lm.getForum(t_fid);
                List<string> usersToUpdate = _lm.getForumViewers(t_fid);
                if (!usersToUpdate.Contains(t_uname)) {
                    usersToUpdate.Add(t_uname);
                }                        
                //_ee.sendMessage(new ForumContentMessage(t_fid, t_uname, forumTopics));                        
                foreach (string viewer in usersToUpdate) {
                    _ee.sendMessage(new ForumContentMessage(t_fid, viewer, forumTopics));
                }



                // NEED TO UPDATE ALL VIEWERS AS WELL!!!!

            }
        }


        private void Handle(GetPostMessage t_getPostMsg) {
            int t_tid;
            int t_fid;
            int t_postIndex;
            string t_uname;
                
            t_uname         =   t_getPostMsg._uName;
            t_tid           =   t_getPostMsg._tId;
            t_fid           =   t_getPostMsg._fId;
            t_postIndex     =   t_getPostMsg._pIndex;
                    
            ForumPost returnPost = _lm.getPost(t_fid, t_tid, t_postIndex,t_uname);
            if (returnPost == null)
            {
                sendError(-8, t_uname);
            }
            else {
                _ee.sendMessage(new PostContentMessage(t_fid, t_tid, t_postIndex, returnPost._parentId,t_uname, returnPost._autor, returnPost._topic, returnPost._content));
                //logger.log(2, "", "the post \""+returnPost.getTopic()+"\" was sent to user "+t_uname);
            }
        }


        private void Handle(GetThreadMessage t_getThreadMsg) {
            int t_tid;
            int t_fid;
            string t_uname;
            ForumThread returnThread;
                
            t_uname     =       t_getThreadMsg._uName;
            t_tid       =       t_getThreadMsg._tId;
            t_fid       =       t_getThreadMsg._fId;

            returnThread = _lm.getThread(t_fid, t_tid);
            if (returnThread == null)
            {
                //logger.log(2, "init", "ERROR GETTHREAD: Thread not found");
                sendError(-6, t_uname);
            }
            else {
                List<Quartet> posts = _lm.getThreadPosts(t_fid, t_tid);
                _ee.sendMessage(new ThreadContentMessage(t_fid, t_tid, t_uname, returnThread._autor, returnThread._topic, returnThread._content, posts));
                //logger.log(2, "", "the thread \"" + returnThread.getTopic() + "\" was sent to user " + t_uname);
                _lm.updateCurrentThread(t_fid, t_tid,t_uname);                        
            }     
        }


        private void Handle(GetSystemMessage t_getSystemMsg) {
            string t_uname;

            t_uname = t_getSystemMsg._uName;
            if (_lm.isMember(t_uname)) {
                if (_lm.isLogged(t_uname)) {
                    List<Quartet> t_forums = _lm.getForums();
                    _ee.sendMessage(new SystemContentMessage(t_uname, t_forums));
                    //logger.log(2, "", "the fourm list was sent to user " + t_uname);
                }
                else {
                    // logger.log(2, "init", "ERROR GETSYSTEM: username: " + t_uname + " is not logged in");
                    sendError(-19, t_uname);
                }
            }
            else {
                // logger.log(2, "init", "ERROR GETSYSTEM: incorrect username: " + t_uname);
                sendError(-3, t_uname);
            }
        }


        private void Handle(GetForumMessage t_getForumMsg) {
            int t_fid;
            string t_uname;        

            t_uname     =    t_getForumMsg._uName;
                    
            if (_lm.isMember(t_uname)) {
                if (_lm.isLogged(t_uname)) {
                    t_fid = t_getForumMsg._fId;

                    if (!_lm.isForum(t_fid)) {
                        //logger.log(2, "init", "ERROR GETFORUM: Forum couldn't be found");
                        sendError(-9, t_uname);
                    }
                    else {
                        List<Quartet> forumTopics = _lm.getForum(t_fid);
                        _ee.sendMessage(new ForumContentMessage(t_fid, t_uname, forumTopics));
                        //logger.log(2, "", "the forum \"" + _lm.getForumName(t_fid) + "\" was sent to user " + t_uname);
                        _lm.updateCurrentThread(t_fid, -1, t_uname);     
                    }
                }
                else {
                    //logger.log(2, "init", "ERROR GETFORUM: username: " + t_uname + " is not logged in");
                    sendError(-19, t_uname);
                }
            }
            else {
                // logger.log(2, "init", "ERROR GETFORUM: incorrect username: " + t_uname);
                sendError(-3, t_uname);
            }
        }
        

        private void sendError(int returnValue,string uname)
        {
            Error err;
            // TODO case with all posible errors and the creation of the proper message, with pushing the error to the queue 
            switch (returnValue)
            {
                case -1 :
                    err = new Error(uname,"user is already a friend with this user");
                    break;
                case -2:
                    err = new Error(uname, "the user you are trying to befriend dosn't exist");
                    break;
                case -3:
                    err = new Error(uname, "incorrect user name");
                    break;
                case -4:
                    err = new Error(uname, "the user you are trying to unfriend is not a friend of yours");
                    break;
                case -5:
                    err = new Error(uname, "topic already exists, choose new topic");
                    break;
                case -6:
                    err = new Error(uname, "the topic could not been found");
                    break;
                case -7:
                    err = new Error(uname, "the topic you where trying to remove was submited by a diffrent user");
                    break;
                case -8:
                    err = new Error(uname, "the post topic is out of bounds");
                    break;
                case -9:
                    err = new Error(uname, "the forum doesn't exist");
                    break;
                case -15:
                    err = new Error(uname, "the username is already exist");
                    break;
                case -16:
                    err = new Error(uname, "the email is already exist");
                    break;
                case -17:                    
                    err = new Error(uname, "some field are empty!");
                    break;
                case -18:
                    err = new Error(uname, uname+" is already logged in.");
                    break;
                case -19:
                    err = new Error(uname, uname + " is not logged in.");
                    break;
                case -20:
                    err = new Error(uname, "the user you are trying to unfriend dosn't exist");
                    break;
                case -21:
                    err = new Error(uname, "could not retrieve users list");
                    break;
                case -22:
                    err = new Error(uname, "sql error occured");
                    break;
                case -23:
                    err = new Error(uname, "incorrect password");
                    break;
                case -24:
                    err = new Error(uname, "cannot be a friend of yourself;)");
                    break;

                default :
                    err = new Error(uname, "an unexpected error append. please try again");
                    break;
            }
            _ee.sendMessage(err);
        }

    }
}
