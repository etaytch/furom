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
            MemberInfo tMem;
            
            switch (t_msg.getMessageType())
            {
                case "LOGIN":   // Done with DB
                    returnValue = _lm.login(((LoginMessage)t_msg)._uName, ((LoginMessage)t_msg)._password);
                    if (returnValue < 0) {
                        sendError(returnValue, ((LoginMessage)t_msg)._uName);
                    }
                    else {
                        _ee.sendMessage(new Acknowledgment(((LoginMessage)t_msg)._uName, "Login Succssfuly"));
                    }
                    //_outputMassage.Enqueue(new Acknowledgment(((LoginMessage)t_msg)._uName,"Login Succssfuly"));
                    break;
                case "LOGOUT":  // Done with DB
                    returnValue = _lm.logout(((LogoutMessage)t_msg)._uName);
                    if (returnValue < 0)
                    {
                        sendError(returnValue, ((LogoutMessage)t_msg)._uName);
                    }
                    else {
                        _ee.sendMessage(new Acknowledgment(((LogoutMessage)t_msg)._uName, "Logout Succssfuly"));
                    }

                    //_outputMassage.Enqueue(new Acknowledgment(((LogoutMessage)t_msg)._uName, "Logout Succssfuly"));
                    break;
                case "ADDPOST":     // Done with DB
                    //t_userID = _lm.FindMemberByUser(((AddPostMessage)t_msg)._uName).getID();
                    t_uname = ((AddPostMessage)t_msg)._uName;
                    t_tid = ((AddPostMessage)t_msg)._tId;
                    t_fid = ((AddPostMessage)t_msg)._fId;
                    // t_postIndex = parentId???
                    t_postIndex = ((AddPostMessage)t_msg)._pIndex;
                    t_topic = ((AddPostMessage)t_msg)._subject;
                    t_content = ((AddPostMessage)t_msg)._post;
                    returnValue = _lm.addPost(t_tid, t_fid, t_postIndex, t_topic, t_content, t_uname);
                    if (returnValue < 0){
                        sendError(returnValue, ((AddPostMessage)t_msg)._uName);
                    }
                    else {
                        _ee.sendMessage(new Acknowledgment(((AddPostMessage)t_msg)._uName, "AddPost Succssfuly"));
                    }
                    //_outputMassage.Enqueue(new Acknowledgment(((LoginMessage)t_msg)._uName,"Login Succssfuly"));
                    break;
                case "ADDTHREAD":       // Done with DB
                    t_uname = ((AddThreadMessage)t_msg)._uName;
                    t_fid = ((AddThreadMessage)t_msg)._fId;                    
                    t_topic = ((AddThreadMessage)t_msg)._subject;
                    t_content = ((AddThreadMessage)t_msg)._post;
                    returnValue = _lm.addTread(t_uname, t_fid, t_topic, t_content);
                    if (returnValue < 0)
                    {
                        sendError(returnValue, ((AddThreadMessage)t_msg)._uName);
                    }
                    else {
                        _ee.sendMessage(new Acknowledgment(((AddThreadMessage)t_msg)._uName, "AddThread Succssfuly"));
                    }
                    //_outputMassage.Enqueue(new Message(""));
                    break;

                // NOT YET IMPLEMENTED AT CLIENT SIDE!!!!!!!!!!!!!!!!! (Etay)
                case "ADDFORUM":
                    tMem = _lm.FindMemberByUser(((AddForumMessage)t_msg)._uName);
                    t_userID = _lm.FindMemberByUser(((AddForumMessage)t_msg)._uName).getID();
                    
                    t_topic = ((AddForumMessage)t_msg)._topic;
                    returnValue = _lm.addForum(t_userID, t_topic);
                    if (returnValue < 0) {
                        sendError(returnValue, ((AddForumMessage)t_msg)._uName);
                    }
                    else {
                        _ee.sendMessage(new Acknowledgment(((AddForumMessage)t_msg)._uName, "AddForum Succssfuly"));
                    }
                    //_outputMassage.Enqueue(new Message(""));
                    break;
                case "GETUSERS":
                    t_uname = ((GetUsersMessage)t_msg)._uName;                    
                    if (_lm.isMember(t_uname)) {
                        if (_lm.isMember(t_uname)) {
                            List<string> users = _lm.getUsers(t_uname);
                            _ee.sendMessage(new UsersContentMessage(t_uname,users));
                        }
                        else {
                            sendError(-19, t_uname);
                        }
                        sendError(-3, t_uname);
                    }
                    break;
                case "GETFRIENDS":
                    t_uname = ((GetFriendsMessage)t_msg)._uName;
                    if (_lm.isMember(t_uname)) {
                        if (_lm.isMember(t_uname)) {
                            List<string> friends = _lm.getFriends(t_uname);
                            _ee.sendMessage(new FriendsContentMessage(t_uname, friends));
                        }
                        else {
                            sendError(-19, t_uname);
                        }
                        sendError(-3, t_uname);
                    }
                    break;


                case "ADDFRIEND":
                    t_uname = ((AddFriendMessage)t_msg)._uName;
                    t_friendUname = ((AddFriendMessage)t_msg)._friend;
                    returnValue = _lm.addMeAsFriend(t_uname,t_friendUname);
                    if (returnValue < 0)
                    {
                        sendError(returnValue, t_uname);
                    }
                    else {
                        _ee.sendMessage(new Acknowledgment(((AddFriendMessage)t_msg)._uName, "AddFriend Succssfuly"));
                    }                    
                    break;
                case "REGISTER":        // Done with DB
                    //((RegisterMessage)t_msg).
                    MemberInfo memb = new MemberInfo(((RegisterMessage)t_msg)._uName, ((RegisterMessage)t_msg)._fName, ((RegisterMessage)t_msg)._lName, ((RegisterMessage)t_msg)._password, ((RegisterMessage)t_msg)._sex, ((RegisterMessage)t_msg)._country, ((RegisterMessage)t_msg)._city, ((RegisterMessage)t_msg)._email, ((RegisterMessage)t_msg)._birthday,"0");
                    //MemberInfo memb = new MemberInfo(((RegisterMessage)t_msg)._uName, ((RegisterMessage)t_msg)._fName, ((RegisterMessage)t_msg)._lName, ((RegisterMessage)t_msg)._password, ((RegisterMessage)t_msg)._email);
                    returnValue = _lm.register(memb);
                    if (returnValue < 0)
                    {
                        sendError(returnValue, ((RegisterMessage)t_msg)._uName);
                    }
                    else {
                        _ee.sendMessage(new Acknowledgment(((RegisterMessage)t_msg)._uName, "REGISTER Succssfuly"));
                    }
                    //_outputMassage.Enqueue(new Message(""));
                    break;
                case "REMOVEFRIEND":
                    t_uname = ((RemoveFriendMessage)t_msg)._uName;
                    t_friendUname = ((RemoveFriendMessage)t_msg)._friend;
                    returnValue = _lm.removeMeAsFriend(t_uname, t_friendUname);
                    if (returnValue < 0)
                    {
                        sendError(returnValue, ((RemoveFriendMessage)t_msg)._uName);
                    }
                    else {
                        _ee.sendMessage(new Acknowledgment(((RemoveFriendMessage)t_msg)._uName, "RemoveFriend Succssfuly"));
                    }
                    //_outputMassage.Enqueue(new Message(""));
                    break;
                case "DELETEPOST":      // DB is ready for tests.. Client does not support it!!                    
                    
                    t_uname = ((DeletePostMessage)t_msg)._uName;
                    t_tid = ((DeletePostMessage)t_msg)._tId;
                    t_fid = ((DeletePostMessage)t_msg)._fId;
                    t_postIndex = ((DeletePostMessage)t_msg)._pIndex;
                    returnValue = _lm.removePost(t_fid, t_tid, t_postIndex,t_uname);
                    if (returnValue < 0)
                    {
                        sendError(returnValue, ((DeletePostMessage)t_msg)._uName);
                    }
                    else {
                        _ee.sendMessage(new Acknowledgment(((DeletePostMessage)t_msg)._uName, "DeletePost Succssfuly"));
                    }
                    //_outputMassage.Enqueue(new Message(""));
                    break;
                    
                case "DELETETHREAD":
                    //t_userID = _lm.FindMemberByUser(((DeleteThreadMessage)t_msg)._uName).getID();
                    t_uname = ((DeleteThreadMessage)t_msg)._uName;
                    t_tid = ((DeleteThreadMessage)t_msg)._tId;
                    t_fid = ((DeleteThreadMessage)t_msg)._fId;
                    /*
                    returnValue = _lm.removeTread(t_fid,t_tid,t_uname);
                    if (returnValue < 0)
                    {
                        sendError(returnValue, ((DeleteThreadMessage)t_msg)._uName);
                    }
                    else {
                        _ee.sendMessage(new Acknowledgment(((DeleteThreadMessage)t_msg)._uName, "DeleteThread Succssfuly"));
                    }
                    */
                    //_outputMassage.Enqueue(new Message(""));
                    break;

                case "GETPOST": // DB is ready for tests.. Client does not support it!!                    
                    //t_userID = _lm.FindMemberByUser(((GetPostMessage)t_msg)._uName).getID();
                    t_uname = ((GetPostMessage)t_msg)._uName;
                    t_tid = ((GetPostMessage)t_msg)._tId;
                    t_fid = ((GetPostMessage)t_msg)._fId;
                    t_postIndex = ((GetPostMessage)t_msg)._pIndex;
                    
                    ForumPost returnPost = _lm.getPost(t_fid, t_tid, t_postIndex,t_uname);
                    if (returnPost == null)
                    {
                        sendError(-8, ((GetPostMessage)t_msg)._uName);
                    }
                    else {
                        _ee.sendMessage(new PostContentMessage(t_fid, t_tid, t_postIndex, returnPost._parentId,((GetPostMessage)t_msg)._uName, returnPost._autor, returnPost._topic, returnPost._content));
                    }
                    //_outputMassage.Enqueue(new Message(""));
                    break;

                case "GETTHREAD":
                    t_tid = ((GetThreadMessage)t_msg)._tId;
                    t_fid = ((GetThreadMessage)t_msg)._fId;

                    ForumThread returnThread = _lm.getThread(t_fid, t_tid);
                    if (returnThread == null)
                    {
                        sendError(-6, ((GetThreadMessage)t_msg)._uName);
                    }
                    else {
                        List<Quartet> posts = _lm.getThreadPosts(t_fid, t_tid);
                        if (posts==null) {
                            Console.WriteLine("posts==null!!");
                        }
                        _ee.sendMessage(new ThreadContentMessage(t_fid, t_tid, ((GetThreadMessage)t_msg)._uName, returnThread._autor,returnThread._topic, returnThread._content, posts));
                    }                    
                    break;

                    // Display All forums in the system
                case "GETSYSTEM":
                    //t_userID = _lm.FindMemberByUser(((GetSystemMessage)t_msg)._uName).getID();
                    List<Quartet> t_forums = _lm.getForums();
                    
                    _ee.sendMessage(new SystemContentMessage(((GetSystemMessage)t_msg)._uName, t_forums));
                    break;

                case "GETFORUM":
                    t_fid = ((GetForumMessage)t_msg)._fId;
                    
                    if (!_lm.isForum(t_fid))
                    {
                        sendError(-9, ((GetForumMessage)t_msg)._uName);
                    }
                    else {
                        List<Quartet> forumTopics = _lm.getForum(t_fid);
                        _ee.sendMessage(new ForumContentMessage(t_fid, ((GetForumMessage)t_msg)._uName, forumTopics));
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
                default :
                    err = new Error(uname, "an unexpected error append. please try again");
                    break;
            }
            _ee.sendMessage(err);
        }

    }
}
