using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MessagePack;

namespace ForumSever
{

    public class LogicManager
    {
        public Database _db; //should be private

        public LogicManager()
        {
            _db = new Database();
            _db.addForum(new Forum("SEX DRUGS & ROCKn'ROLL"));   //vadi: temp
        }

        public LogicManager(Database p_db)
        {
            this._db = p_db;
        }


        /*
        public int register(MemberInfo memb);
        public int login(string p_user, string p_pass);
        public int logout(string p_user);
        public bool isMember(string p_user);
        public bool isLogged(string p_user);
        public List<string> getUsers(string p_uname);
        public List<string> getFriends(string p_uname);
        public MemberInfo FindMemberByUser(string p_user);
        public int addMeAsFriend(string p_uname, string p_friendUname);
        public int removeMeAsFriend(string p_uname, string p_friendUname);
        public List<Quartet> getForums();
        public bool isForum(int p_fid);
        public int addForum(int p_userID, string p_topic);
        public int addTread(string p_uname, int p_fid, string p_topic, string p_content);
        public List<Quartet> getThreadPosts(int p_fid, int p_tid);
        public ForumThread getThread(int p_fid, int p_tid);
        public List<Quartet> getForum(int p_fid);
        public int removeTread(int p_fid, int p_tid, string p_uname);
        public int addPost(int p_tid, int p_fid, int parentId, string p_topic, string p_content, string p_uname);
        public ForumPost getPost(int p_fid, int p_tid, int p_index, string p_uname);
        public int removePost(int p_fid, int p_tid, int p_index, string p_uname);
        */


        /*Member functions*/        

        public int register(MemberInfo memb)
        {
            // cant add Member without all fields
            if (memb.getLName() == "" | memb.getFName() == "" | memb.getUName() == "" | memb.getPass() == "" | memb.getEmail() == "")
            {
                //massage = "missing fields";
                //return false;
                return -17;
            }
            // user name already in use
            if (_db.isMember(memb.getUName())){
                //massage = "user name in use. choose  a diffrent one";
                //return false;
                return -15;
            }

            if (_db.isMember(memb.getEmail()))
            {
                //massage = "mail in use. choose  a diffrent one";
                //return false;
                return -16;
            }
  
            _db.addMember(memb);
            return 0;
        }

        public int login(string p_user, string p_pass)
        {

            if (_db.isMember(p_user)) {
                if (!_db.isLogin(p_user)) {
                    if (_db.login(p_user, p_pass)) {
                        _db.markUserAsLogged(p_user, 1);
                        return 0;       // no error
                    }
                    else {
                        // incorrect pass
                        return -23;
                    }
                    
                }
                else {
                    return -18;     // user already logged in
                }
            }
            else return -3;         // username not exist
        }

        public int logout(string p_user)
        {
            if (_db.isMember(p_user)) {
                if (_db.isLogin(p_user)) {
                    _db.markUserAsLogged(p_user, 0);
                    return 0;       // no error
                }
                else {
                    return -19;     // user is not logged in
                }
            }
            return -3;         // username not exist
        }

        public bool isMember(string p_user) {
            return _db.isMember(p_user);
        }

        public bool isLogged(string p_user)
        {
            MemberInfo memb = _db.FindMember("username = '" + p_user + "'");
            return ((memb != null) && (memb.isLogged()));
        }

        public List<string> getUsers(string p_uname){
            return _db.getUsers(p_uname);
        }

        public List<string> getFriends(string p_uname) {
            return _db.getFriends(p_uname);
        }
        
        public MemberInfo FindMemberByUser(string p_user)
        {
            return _db.FindMember("username = '" + p_user + "'");
        }

        public int addMeAsFriend(string p_uname,string p_friendUname)
        {
            if (!_db.isMember(p_uname)) {
                //return "incurrect user name";
                return -3;
            }
            if (!_db.isMember(p_friendUname)) {
                //return "the user you are trying to befriend dosn't exist";
                return -2;
            }

            if (_db.isFriend(p_uname,p_friendUname))
            {
                //return "user is already a friend with this user";
                return -1;
            }
            _db.addFriend(p_uname, p_friendUname);            
            //return "you have a new friend";
            return 0;
        }

        public int removeMeAsFriend(string p_uname, string p_friendUname)
        {
            if (!_db.isMember(p_uname)) {
                //return "incurrect user name";
                return -3;

            }
            if (!_db.isMember(p_friendUname)) {
                //"the user you are trying to unfriend dosn't exist"                        
                return -20;
            }



            if (!_db.isFriend(p_uname, p_friendUname))
            {
                return -4;
                //return "the user you are trying to unfriend is not a friend of yours";
            }
            _db.removeFriend(p_uname, p_friendUname);
            //return "you have removed yourself as friend";
            return 0;
        }

        public List<Quartet> getForums() {
            return _db.getForums();
        
        }

        public bool isForum(int p_fid) {
            return _db.isForum(p_fid);
        }

        //addForum
        public int addForum(int p_userID, string p_topic)
        {
            MemberInfo t_user = _db.FindMemberByID(p_userID);
            if (t_user == null)
            {
                return -3;
                //return "incurrect user name";
            }
            if (_db.findTopicInForums(p_topic))
            {
                //return "topic already exists, choose new topic";
                return -5;
            }
            Forum t_forum = new Forum(p_topic);
            int t_ID = _db.addForum(t_forum);

            t_forum.setId(t_ID);

            //return "your forum was added to the system";
            return t_ID;

        }

        public int addTread(string p_uname, int p_fid, string p_topic, string p_content)
        {
            MemberInfo memb = FindMemberByUser(p_uname);
            if (memb == null) {
                return -3;
                //return "incurrect user name";

            }
            if (_db.isThread(p_fid ,p_topic))
            {
                //return "topic already exists, choose new topic";
                return -5;
            }

            ForumThread t_thr = new ForumThread(p_fid, p_topic, p_content, p_uname);
            int t_ID = _db.addTread(t_thr);            

            //return "your thread was added to the forum";
            return t_ID;
        }

        public List<Quartet> getThreadPosts(int p_fid, int p_tid) {
            return _db.getThreadPosts(p_fid, p_tid);
        }

        public ForumThread getThread(int p_fid, int p_tid)
        {            
            return _db.getThread(p_fid, p_tid);
        }

        public List<Quartet> getForum(int p_fid)
        {
            return _db.getForum(p_fid);
        }
        
        public int removeThread(int p_fid, int p_tid,string p_uname)
        {
            if (!_db.isMember(p_uname)) {
                //return "incurrect user name";
                return -3;
            }
            if (!_db.getThreadAuthor(p_fid, p_tid).Equals(p_uname)) {
                //p_uname didn't write this thread..
                return -7;
            }
            if (_db.removeThread(p_fid, p_tid)) {
                return 0;
            }
            else {
                return -22;
            }            
        }
        

        /*Posts founctions  */        
        public int addPost(int p_tid, int p_fid, int parentId, string p_topic, string p_content,string p_uname)
        {
            //MemberInfo t_user = FindMemberByUser(p_uname);            
            if (!_db.isMember(p_uname)){
                return -3;
                //return "incurrect user name";

            }
            //ForumThread t_thr = _db.getTread(p_fid, p_tid);

            if (!_db.isThread(p_fid,p_tid)){
                return -6;
                //return "the topic could not been found";
            }
            _db.addPost(p_tid, p_fid, parentId, p_topic, p_content, p_uname);
            //int index = t_thr.addPost(p_fid, p_tid, p_topic, p_content, t_user);
            return 0;
        }
        

        public ForumPost getPost(int p_fid, int p_tid, int p_index, string p_uname)
        {           
            if (!_db.isMember(p_uname))
            {
                return null;
                //return "incurrect user name";
            }
            //ForumThread t_thr = _db.getTread(p_fid, p_tid);
            if (!_db.isThread(p_fid,p_tid))
            {
                return null;
                //return "the topic could not been found";
            }
            return _db.getPost(p_fid, p_tid, p_index, p_uname);

        }

        public int removePost(int p_fid, int p_tid, int p_index, string p_uname)
        {
            //MemberInfo t_user = _db.FindMemberByID(p_userID);
            if (!_db.isMember(p_uname))
            {
                return -3;
                //return "incurrect user name";
            }
                
            //ForumThread t_thr = _db.getTread(p_fId, p_tId);
            if (!_db.isThread(p_fid,p_tid))
            {                        
                return -6;
                //return "the topic could not been found";
            }
            if ((p_index < 0) | (p_index > _db.getCurrentPostID(p_fid,p_tid)))
            {
                return -8;
                //return "the post topic is out of bounds";
            }
            
            //ForumPost t_post = _db.getPost(p_fid, p_tid, p_index);
            Boolean postExist = _db.isPost("(pid = '" + p_index + "') and (fid = '" + p_fid + "') and (tid = '"+p_tid+"')");
            if (postExist && (_db.getPostAuthor(p_fid, p_tid, p_index).Equals(p_uname)))
            {
                _db.removePost(p_fid, p_tid, p_index);
                return 0;
                //return "your thread was removed from the forum";
            }
            else
            {
                return -7;
                //return "the topic you where trying to remove was submited by a diffrent user";
            }
            return -1;
        }


        public void updateCurrentThread(int t_fid, int t_tid, string t_uname) {
            _db.updateCurrentThread(t_fid, t_tid, t_uname);
        }

        public List<string> getFriendsToUpdate(string t_uname) {
            return _db.getFriendsToUpdate(t_uname);
        }

        public string getForumName(int t_fid) {
            return _db.getForumName(t_fid);
        }

        public List<string> getForumViewers(int t_fid) {
            return _db.getForumViewers(t_fid);
        }

        public List<string> getThreadViewersToUpdate(string t_uname, int t_fid, int t_tid) {
            return _db.getThreadViewersToUpdate(t_uname, t_fid, t_tid);            
        }

        public string getThreadName(int t_fid, int t_tid) {
            return _db.getThreadName(t_fid, t_tid);
        }
    }
}
