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
        //private BlockingQueue<Message> _inputMassage;
       // private BlockingQueue<Message> _outputMassage; 


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
            int t_friendID;
            int t_tid;
            int t_fid;
            int t_postIndex;
            string t_topic;
            string t_content;
            
            switch (t_msg.getMessageType())
            {
                case "LOGIN":
                    returnValue = _lm.login(((LoginMessage)t_msg)._uName, ((LoginMessage)t_msg)._password);
                    if (returnValue < 0) {
                        sendError(returnValue, ((LoginMessage)t_msg)._uName);
                    }
                    else {
                        _ee.sendMessage(new Acknowledgment(((LoginMessage)t_msg)._uName, "Login Succssfuly"));
                    }
                    //_outputMassage.Enqueue(new Acknowledgment(((LoginMessage)t_msg)._uName,"Login Succssfuly"));
                    break;
                case "LOGOUT":
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
                case "ADDPOST":
                    t_userID = _lm.FindMemberByUser(((AddPostMessage)t_msg)._uName).getID();
                    t_tid = ((AddPostMessage)t_msg)._tId;
                    t_fid = ((AddPostMessage)t_msg)._fId;
                    t_topic = ((AddPostMessage)t_msg)._subject;
                    t_content = ((AddPostMessage)t_msg)._post;
                    returnValue = _lm.addPost(t_fid, t_userID, t_tid, t_topic, t_content);
                    if (returnValue < 0)
                    {
                        sendError(returnValue, ((AddPostMessage)t_msg)._uName);
                    }
                    else {
                        _ee.sendMessage(new Acknowledgment(((AddPostMessage)t_msg)._uName, "AddPost Succssfuly"));
                    }
                    //_outputMassage.Enqueue(new Acknowledgment(((LoginMessage)t_msg)._uName,"Login Succssfuly"));
                    break;
                case "ADDTHREAD":
                    t_userID = _lm.FindMemberByUser(((AddThreadMessage)t_msg)._uName).getID();
                    t_fid = ((AddThreadMessage)t_msg)._fId;                    
                    t_topic = ((AddThreadMessage)t_msg)._subject;
                    t_content = ((AddThreadMessage)t_msg)._post;
                    returnValue = _lm.addTread(t_fid,t_userID, t_topic, t_content);
                    if (returnValue < 0)
                    {
                        sendError(returnValue, ((AddThreadMessage)t_msg)._uName);
                    }
                    else {
                        _ee.sendMessage(new Acknowledgment(((AddThreadMessage)t_msg)._uName, "AddThread Succssfuly"));
                    }
                    //_outputMassage.Enqueue(new Message(""));
                    break;

                case "ADDFORUM":
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


                case "ADDFRIEND":
                    t_userID = _lm.FindMemberByUser(((AddFriendMessage)t_msg)._uName).getID();
                    t_friendID = _lm.FindMemberByUser(((AddFriendMessage)t_msg)._friend).getID();
                    returnValue = _lm.addMeAsFriend(t_userID, t_friendID);
                    if (returnValue < 0)
                    {
                        sendError(returnValue, ((AddFriendMessage)t_msg)._uName);
                    }
                    else {
                        _ee.sendMessage(new Acknowledgment(((AddFriendMessage)t_msg)._uName, "AddFriend Succssfuly"));
                    }
                    //_outputMassage.Enqueue(new Message(""));
                    break;
                case "REGISTER":
                    MemberInfo memb = new MemberInfo(((RegisterMessage)t_msg)._uName, ((RegisterMessage)t_msg)._fName, ((RegisterMessage)t_msg)._lName, ((RegisterMessage)t_msg)._password, ((RegisterMessage)t_msg)._email);
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
                    t_userID = _lm.FindMemberByUser(((RemoveFriendMessage)t_msg)._uName).getID();
                    t_friendID = _lm.FindMemberByUser(((RemoveFriendMessage)t_msg)._friend).getID();
                    returnValue = _lm.removeMeAsFriend(t_userID, t_friendID);
                    if (returnValue < 0)
                    {
                        sendError(returnValue, ((RemoveFriendMessage)t_msg)._uName);
                    }
                    else {
                        _ee.sendMessage(new Acknowledgment(((RemoveFriendMessage)t_msg)._uName, "RemoveFriend Succssfuly"));
                    }
                    //_outputMassage.Enqueue(new Message(""));
                    break;
                case "DELETEPOST":
                    t_userID = _lm.FindMemberByUser(((DeletePostMessage)t_msg)._uName).getID();
                    t_tid = ((DeletePostMessage)t_msg)._tId;
                    t_fid = ((DeletePostMessage)t_msg)._fId;
                    t_postIndex = ((DeletePostMessage)t_msg)._pIndex;
                    returnValue = _lm.removePost(t_fid,t_userID, t_tid, t_postIndex);
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
                    t_userID = _lm.FindMemberByUser(((DeleteThreadMessage)t_msg)._uName).getID();
                    t_tid = ((DeleteThreadMessage)t_msg)._tId;
                    t_fid = ((DeleteThreadMessage)t_msg)._fId;
                    returnValue = _lm.removeTread(t_fid,t_userID, t_tid);
                    if (returnValue < 0)
                    {
                        sendError(returnValue, ((DeleteThreadMessage)t_msg)._uName);
                    }
                    else {
                        _ee.sendMessage(new Acknowledgment(((DeleteThreadMessage)t_msg)._uName, "DeleteThread Succssfuly"));
                    }
                    //_outputMassage.Enqueue(new Message(""));
                    break;
                case "GETPOST":
                    t_userID = _lm.FindMemberByUser(((GetPostMessage)t_msg)._uName).getID();
                    t_tid = ((GetPostMessage)t_msg)._tId;
                    t_fid = ((GetPostMessage)t_msg)._fId;
                    t_postIndex = ((GetPostMessage)t_msg)._pIndex;
                    
                    ForumPost returnPost = _lm.getPost(t_fid,t_userID, t_tid, t_postIndex);                    
                    if (returnPost == null)
                    {
                        sendError(-8, ((GetPostMessage)t_msg)._uName);
                    }
                    else {                        
                        _ee.sendMessage(new PostContentMessage(t_fid, t_tid, t_postIndex, ((GetPostMessage)t_msg)._uName,(returnPost._autor).getUName(),returnPost._topic,returnPost._content));
                    }
                    //_outputMassage.Enqueue(new Message(""));
                    break;

                case "GETTHREAD":
                    t_tid = ((GetThreadMessage)t_msg)._tId;
                    t_fid = ((GetThreadMessage)t_msg)._fId;
                    ForumThread returnThread = _lm.getTread(t_fid, t_tid);
                    if (returnThread == null)
                    {
                        sendError(-6, ((GetThreadMessage)t_msg)._uName);
                    }
                    else {
                        _ee.sendMessage(new ThreadContentMessage(returnThread.getID(), t_tid, ((GetThreadMessage)t_msg)._uName, returnThread.getTheardsTopics()));
                    }

                    //this should change to real return message
                    //_outputMassage.Enqueue(new Message(""));
                    break;

                    // Display All forums in the system
                case "GETSYSTEM":
                    t_userID = _lm.FindMemberByUser(((GetSystemMessage)t_msg)._uName).getID();
                    List<Forum> t_forum = _lm._db._forums;
                    List<string> t_forum_topics = new List<string>();
                    foreach (Forum tt_forum in t_forum){
                        t_forum_topics.Add(tt_forum.getTopic());
                    }
                    _ee.sendMessage(new SystemContentMessage(((GetSystemMessage)t_msg)._uName,t_forum_topics));
                    break;

                case "GETFORUM":
                    t_fid = ((GetForumMessage)t_msg)._fId;
                    Forum returnForum = _lm.getForum(t_fid);
                    if (returnForum == null)
                    {
                        sendError(-6, ((GetThreadMessage)t_msg)._uName);
                    }
                    else {
                        //_ee.sendMessage(new ThreadContentMessage(returnThread.getID(), t_tid, ((GetThreadMessage)t_msg)._uName, returnThread.getTheardsTopics()));
                    }

                    //this should change to real return message
                    //_outputMassage.Enqueue(new Message(""));
                    break;




                // |======================|
                // | WHERE IS GETFORUM??? |
                // |======================|

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
                    err = new Error(uname, "incurrect user name");
                    break;
                case -4:
                    err = new Error(uname, "nthe user you are trying to unfriend is not a friend of yours");
                    break;
                case -5:
                    err = new Error(uname, "ntopic already exists, choose new topic");
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
                default :
                    err = new Error(uname, "an unexpected error append. please try again");
                    break;
            }
            _ee.sendMessage(err);
        }

    }
}
