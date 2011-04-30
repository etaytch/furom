using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using common;
using MessagePack;
using Server;
using System.Threading;
using Protocol;
namespace ForumSever
{
    public class MassageHandler
    {
        private LogicManager _lm;
        private EandEProtocol _ee;

        public MassageHandler()
        {
            _lm = new LogicManager();
            //TODO: this should change to the real constractor, or to a parametor
            _ee = new EandEProtocol();
           // _outputMassage = new BlockingQueue<Message>();
        }


        public void startForum()
        {
            _ee.startServer();
        }

        public void stopForum()
        {
            _ee.stopServer();
        }


        public void readMassage(){
            Message t_msg = _ee.getMessage();
            //Console.WriteLine("int readMessage. t_msg="+t_msg);
            //this should be given to a thread
            Handle(t_msg);
        }

        private void Handle(Message t_msg)
        {
            int returnValue;
            int t_userID;
            string t_friendUname;
            int t_tid;
            int t_fid;            
            int t_postIndex;
            string t_topic;
            string t_content;
            string t_uname;
            string t_fname;
            string t_lname;
            string t_pass;
            string t_sex;
            string t_email;
            string t_birthday;
            string t_city;
            string t_country;
            MemberInfo tMem;
            ForumThread returnThread;

            switch (t_msg.getMessageType())
            {
                case "LOGIN":   // Done with DB                    
                    LoginMessage t_loginMsg = ((LoginMessage)t_msg);

                    t_uname     =   t_loginMsg._uName;
                    t_pass      =   t_loginMsg._password;
                    
                    returnValue = _lm.login(t_uname, t_pass);
                    if (returnValue < 0) {
                        sendError(returnValue, t_uname);
                    }
                    else {
                        _ee.sendMessage(new Acknowledgment(t_uname, "Login Succssfuly"));
                    }
                    //_outputMassage.Enqueue(new Acknowledgment(((LoginMessage)t_msg)._uName,"Login Succssfuly"));
                    break;
                case "LOGOUT":  // Done with DB
                    LogoutMessage t_logoutMsg = ((LogoutMessage)t_msg);

                    t_uname     =    t_logoutMsg._uName;
                    
                    returnValue = _lm.logout(t_uname);
                    if (returnValue < 0)
                    {
                        sendError(returnValue, t_uname);
                    }
                    else {
                        _ee.sendMessage(new Acknowledgment(t_uname, "Logout Succssfuly"));
                    }

                    //_outputMassage.Enqueue(new Acknowledgment(((LogoutMessage)t_msg)._uName, "Logout Succssfuly"));
                    break;
                case "ADDPOST":     // Done with DB
                    AddPostMessage t_addPostMsg = ((AddPostMessage)t_msg);

                    t_uname         =   t_addPostMsg._uName;
                    t_tid           =   t_addPostMsg._tId;
                    t_fid           =   t_addPostMsg._fId;
                    // t_postIndex = parentId???
                    t_postIndex     =   t_addPostMsg._pIndex;
                    t_topic         =   t_addPostMsg._subject;
                    t_content       =   t_addPostMsg._post;

                    returnValue = _lm.addPost(t_tid, t_fid, t_postIndex, t_topic, t_content, t_uname);
                    if (returnValue < 0){
                        sendError(returnValue, t_uname);
                    }
                    else {
                        _ee.sendMessage(new Acknowledgment(t_uname, "AddPost Succssfuly"));
                        List<string> usersToUpdate = _lm.getFriendsToUpdate(t_uname);   // friends
                        //Console.WriteLine(usersToUpdate.ToString());
                        List<string> viewersToUpdate = _lm.getThreadViewersToUpdate(t_uname,t_fid,t_tid);
                        usersToUpdate.Union<string>(viewersToUpdate);
                        string forumName = _lm.getForumName(t_fid);
                        returnThread = _lm.getThread(t_fid, t_tid);
                        string threadName = returnThread._topic;//_lm.getThreadName(t_fid,t_tid);
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
                    break;
                case "ADDTHREAD":       // Done with DB
                    AddThreadMessage t_addThreadMsg = ((AddThreadMessage)t_msg);

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
                        List<string> usersToUpdate = _lm.getFriendsToUpdate(t_uname);
                        string forumName = _lm.getForumName(t_fid);
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
                    break;

                // NOT YET IMPLEMENTED AT CLIENT SIDE!!!!!!!!!!!!!!!!! (Etay)
                case "ADDFORUM":
                    AddForumMessage t_addForumMsg = ((AddForumMessage)t_msg);

                    t_uname     =   t_addForumMsg._uName;
                    tMem        =   _lm.FindMemberByUser(t_uname);
                    t_userID    =   _lm.FindMemberByUser(t_uname).getID();
                    t_topic     =   t_addForumMsg._topic;

                    returnValue = _lm.addForum(t_userID, t_topic);
                    if (returnValue < 0) {
                        sendError(returnValue, t_uname);
                    }
                    else {
                        _ee.sendMessage(new Acknowledgment(t_uname, "AddForum Succssfuly"));
                    }
                    //_outputMassage.Enqueue(new Message(""));
                    break;
                case "GETUSERS":
                    GetUsersMessage t_getUsersMsg = ((GetUsersMessage)t_msg);

                    t_uname     =    t_getUsersMsg._uName;
                    
                    if (_lm.isMember(t_uname)) {
                        if (_lm.isLogged(t_uname)) {
                            List<string> users = _lm.getUsers(t_uname);
                            _ee.sendMessage(new UsersContentMessage(t_uname, users));
                        }
                        else {
                            sendError(-19, t_uname);
                        }
                    }
                    else {
                        sendError(-3, t_uname);
                    }
                    break;
                case "GETFRIENDS":
                    GetFriendsMessage t_getFriendsMsg = ((GetFriendsMessage)t_msg);

                    t_uname     =   t_getFriendsMsg._uName;

                    if (_lm.isMember(t_uname)) {
                        if (_lm.isLogged(t_uname)) {
                            List<string> friends = _lm.getFriends(t_uname);
                            _ee.sendMessage(new FriendsContentMessage(t_uname, friends));
                        }
                        else {
                            sendError(-19, t_uname);
                        }
                    }
                    else {
                        sendError(-3, t_uname);
                    }
                    break;


                case "ADDFRIEND":
                    AddFriendMessage t_addFriendMsg = ((AddFriendMessage)t_msg);
                    
                    t_uname         =       t_addFriendMsg._uName;
                    t_friendUname   =       t_addFriendMsg._friend;
                    
                    returnValue = _lm.addMeAsFriend(t_uname,t_friendUname);
                    if (returnValue < 0)
                    {
                        sendError(returnValue, t_uname);
                    }
                    else {
                        _ee.sendMessage(new Acknowledgment(t_uname, "AddFriend Succssfuly"));
                    }                    
                    break;
                case "REGISTER":        // Done with DB                    
                    RegisterMessage t_registerMsg = ((RegisterMessage)t_msg);
                    
                    t_uname         =       t_registerMsg._uName;
                    t_fname         =       t_registerMsg._fName;
                    t_lname         =       t_registerMsg._lName;
                    t_pass          =       t_registerMsg._password;
                    t_sex           =       t_registerMsg._sex;
                    t_country       =       t_registerMsg._country;
                    t_city          =       t_registerMsg._city;
                    t_email         =       t_registerMsg._email;
                    t_birthday      =       t_registerMsg._birthday;

                    MemberInfo memb = new MemberInfo(t_uname, t_fname, t_lname, t_pass, t_sex, t_country, t_city, t_city, t_birthday, "0");                    
                    returnValue = _lm.register(memb);
                    if (returnValue < 0)
                    {
                        sendError(returnValue, t_uname);
                    }
                    else {
                        _ee.sendMessage(new Acknowledgment(t_uname, "REGISTER Succssfuly."));
                        List<string> users = _lm.getUsers(t_uname);
                        foreach (string str in users) {
                            if(!t_uname.Equals(str)){
                                _ee.sendMessage(new UsersContentMessage(str, users));
                            }                            
                        }                                                
                    }                    
                    break;
                case "REMOVEFRIEND":
                    RemoveFriendMessage t_removeFriendMsg = ((RemoveFriendMessage)t_msg);

                    t_uname         =       t_removeFriendMsg._uName;
                    t_friendUname   =       t_removeFriendMsg._friend;

                    returnValue = _lm.removeMeAsFriend(t_uname, t_friendUname);
                    if (returnValue < 0)
                    {
                        sendError(returnValue, t_uname);
                    }
                    else {
                        _ee.sendMessage(new Acknowledgment(t_uname, "RemoveFriend Succssfuly"));
                    }                    
                    break;
                case "DELETEPOST":      // DB is ready for tests.. Client does not support it!!                    
                    DeletePostMessage t_deletePostMsg = ((DeletePostMessage)t_msg);

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
                    //_outputMassage.Enqueue(new Message(""));
                    break;
                    
                case "DELETETHREAD":
                    DeleteThreadMessage t_deleteThreadMsg = ((DeleteThreadMessage)t_msg);

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
                                        
                    break;

                case "GETPOST": // DB is ready for tests.. Client does not support it!!                    
                    GetPostMessage t_getPostMsg = ((GetPostMessage)t_msg);

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
                    }
                    
                    break;

                case "GETTHREAD":
                    GetThreadMessage t_getThreadMsg = ((GetThreadMessage)t_msg);

                    t_uname     =       t_getThreadMsg._uName;
                    t_tid       =       t_getThreadMsg._tId;
                    t_fid       =       t_getThreadMsg._fId;

                    returnThread = _lm.getThread(t_fid, t_tid);
                    if (returnThread == null)
                    {
                        sendError(-6, t_uname);
                    }
                    else {
                        List<Quartet> posts = _lm.getThreadPosts(t_fid, t_tid);
                        _ee.sendMessage(new ThreadContentMessage(t_fid, t_tid, t_uname, returnThread._autor, returnThread._topic, returnThread._content, posts));
                        _lm.updateCurrentThread(t_fid, t_tid,t_uname);                        
                    }                    
                    break;

                    // Display All forums in the system
                case "GETSYSTEM":
                    GetSystemMessage t_getSystemMsg = ((GetSystemMessage)t_msg);
                    t_uname = t_getSystemMsg._uName;
                    if (_lm.isMember(t_uname)) {
                        if (_lm.isLogged(t_uname)) {
                            List<Quartet> t_forums = _lm.getForums();
                            _ee.sendMessage(new SystemContentMessage(t_uname, t_forums));
                        }
                        else {
                            sendError(-19, t_uname);
                        }
                    }
                    else {
                        sendError(-3, t_uname);
                    }                                        
                    break;

                case "GETFORUM":
                    GetForumMessage t_getForumMsg = ((GetForumMessage)t_msg);

                    t_uname     =    t_getForumMsg._uName;
                    
                    if (_lm.isMember(t_uname)) {
                        if (_lm.isLogged(t_uname)) {
                            t_fid = t_getForumMsg._fId;

                            if (!_lm.isForum(t_fid)) {
                                sendError(-9, t_uname);
                            }
                            else {
                                List<Quartet> forumTopics = _lm.getForum(t_fid);
                                _ee.sendMessage(new ForumContentMessage(t_fid, t_uname, forumTopics));
                                _lm.updateCurrentThread(t_fid, -1, t_uname);     
                            }
                        }
                        else {
                            sendError(-19, t_uname);
                        }
                    }
                    else {
                        sendError(-3, t_uname);
                    }
                    
                    break;
                default:
                    break;
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
