using System;
using System.Collections;
using System.Linq;
using System.Text;
using MessagePack;
using System.Collections.Generic;
using VS.Logger;


namespace ForumSever
{

    public class LogicManager
    {
        public Database _db; //should be private
        private static Object _logicLock;
        private static Object _IPLock;
        private Hashtable _usersIp;

        //private Logger _logger; 

        public LogicManager(/*Logger logger*/)
        {
            _db = new Database(/*logger*/);
            //_logger = logger;
            _db.addForum(new Forum("SEX DRUGS & ROCKn'ROLL"));   //vadi: temp
            _usersIp = new Hashtable();
        }

        public LogicManager(Database p_db)
        {
            this._db = p_db;
            _usersIp = new Hashtable();
        }


        public void addUserIP(string userName,string IP){
            lock(_IPLock){
                this._usersIp.Add(IP, userName);
            }
        }
        public string getUserFromIP(string IP){
            string result = ""; 
            lock(_IPLock){
                try
                {
                    result = _usersIp[IP] as string;
                }
                catch(Exception e){
                    result="";
                }

            }
            return result;
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
            lock (_logicLock)
            {
                string uname = memb.getUName();
                // cant add Member without all fields
                if (memb.getLName() == "" | memb.getFName() == "" | memb.getUName() == "" | memb.getPass() == "" | memb.getEmail() == "")
                {
                    //massage = "missing fields";
                    //return false;

                    ////_logger.log(2, "init", "ERROR: "+uname + "did not type all neseccery details for registering");
                    return -17;
                }
                // user name already in use
                if (_db.isMember(memb.getUName()))
                {
                    //massage = "user name in use. choose  a diffrent one";
                    //return false;
                    ////_logger.log(2, "init", "ERROR: " + uname + " alread exist.");
                    return -15;
                }

                if (_db.isMember(memb.getEmail()))
                {
                    //massage = "mail in use. choose  a diffrent one";
                    //return false;
                    ////_logger.log(2, "init", "ERROR: " + uname + " tried to register with an existing email: "+memb.getEmail());
                    return -16;
                }

                _db.addMember(memb);
                return 0;
            }
        }

        public int login(string p_user, string p_pass)
        {   int result=0;
            lock (_logicLock)
            {
                if (_db.isMember(p_user))
                {
                    if (!_db.isLogin(p_user))
                    {
                        if (_db.login(p_user, p_pass))
                        {
                            _db.markUserAsLogged(p_user, 1);
                            result = 0;       // no error
                        }
                        else
                        {
                            // incorrect pass
                            ////_logger.log(2, "init", "ERROR LOGIN: " + p_user + " tried to login with wrong password");
                            result = -23;
                        }

                    }
                    else
                    {
                        ////_logger.log(2, "init", "ERROR LOGIN: " + p_user + " tried to login while already logged in");
                        result = -18;     // user already logged in
                    }
                }

                else
                {
                    ////_logger.log(2, "init", "ERROR LOGIN: " + p_user + " does not exsist");    
                    result= - 3;         // username not exist
                }
            }
            return result;
        }

        public int logout(string p_user)
        {
            lock (_logicLock)
            {
                if (_db.isMember(p_user))
                {
                    if (_db.isLogin(p_user))
                    {
                        _db.markUserAsLogged(p_user, 0);
                        return 0;       // no error
                    }
                    else
                    {
                        ////_logger.log(2, "init", "ERROR LOGOUT: " + p_user + " tried to logout while already logged out");
                        return -19;     // user is not logged in
                    }
                }
                ////_logger.log(2, "init", "ERROR LOGOUT: " + p_user + " does not exsist");    
                return -3;         // username not exist
            }

        }
        public bool isMember(string p_user) {
            lock (_logicLock)
            {
                return _db.isMember(p_user);
            }
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
               // //_logger.log(2, "init", "ERROR ADDFRIEND: " + p_uname + " does not exsist");    
                //return "incurrect user name";
                return -3;
            }
            if (!_db.isMember(p_friendUname)) {
                ////_logger.log(2, "init", "ERROR ADDFRIEND: " + p_friendUname + " does not exsist");    
                //return "the user you are trying to befriend dosn't exist";
                return -2;
            }
            if (p_uname.Equals(p_friendUname)) {

                //_logger.log(2, "init", "ERROR ADDFRIEND: User "+p_uname+" tried to add himself as friend");    
                return -24;
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
                ////_logger.log(2, "init", "ERROR REMOVEFRIEND: " + p_uname + " does not exist");    
                //return "incurrect user name";
                return -3;

            }
            if (!_db.isMember(p_friendUname)) {
                ////_logger.log(2, "init", "ERROR REMOVEFRIEND: " + p_friendUname + " does not exist");    
                //"the user you are trying to unfriend dosn't exist"                        
                return -20;
            }

            if (!_db.isFriend(p_uname, p_friendUname))
            {
                ////_logger.log(2, "init", "ERROR REMOVEFRIEND: " + p_friendUname + " is not a friend of "+p_uname);    
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
                ////_logger.log(2, "init", "ERROR ADDFORUM: incorrect username: " + t_user.getUName());
                return -3;
                //return "incurrect user name";
            }
            if (_db.findTopicInForums(p_topic))
            {
                //_logger.log(2, "init", "ERROR ADDFORUM: forum alread exist");
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
                //_logger.log(2, "init", "ERROR ADDTHREAD: incorrect username: "+p_uname);
                return -3;
                //return "incurrect user name";

            }
            if (_db.isThread(p_fid ,p_topic))
            {
                //_logger.log(2, "init", "ERROR ADDTHREAD: Thread alread exist");
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
                //_logger.log(2, "init", "ERROR REMOVETHREAD: incorrect username: " + p_uname);
                //return "incurrect user name";
                return -3;
            }
            if (!_db.getThreadAuthor(p_fid, p_tid).Equals(p_uname)) {
                //_logger.log(2, "init", "ERROR REMOVETHREAD: the thread was not written by " + p_uname);         
                //p_uname didn't write this thread..
                return -7;
            }
            if (_db.removeThread(p_fid, p_tid)) {
                return 0;
            }
            else {
                //_logger.log(2, "init", "ERROR REMOVETHREAD: SQL ERROR");
                return -22;
            }            
        }
        

        /*Posts founctions  */        
        public int addPost(int p_tid, int p_fid, int parentId, string p_topic, string p_content,string p_uname)
        {
            //MemberInfo t_user = FindMemberByUser(p_uname);            
            if (!_db.isMember(p_uname)){
                //_logger.log(2, "init", "ERROR ADDPOST: incourrect username: " + p_uname);
                return -3;
                //return "incurrect user name";

            }
            //ForumThread t_thr = _db.getTread(p_fid, p_tid);

            if (!_db.isThread(p_fid,p_tid)){
                //_logger.log(2, "init", "ERROR ADDPOST: Thread not found");
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
                //_logger.log(2, "init", "ERROR GETPOST: incourrect username: " + p_uname);
                return null;
                //return "incurrect user name";
            }
            //ForumThread t_thr = _db.getTread(p_fid, p_tid);
            if (!_db.isThread(p_fid,p_tid))
            {
                //_logger.log(2, "init", "ERROR GETPOST: Thread not found");
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
                //_logger.log(2, "init", "ERROR REMOVEPOST: " + p_uname + " does not exist");    
                return -3;
                //return "incurrect user name";
            }
                
            //ForumThread t_thr = _db.getTread(p_fId, p_tId);
            if (!_db.isThread(p_fid,p_tid))
            {
                //_logger.log(2, "init", "ERROR REMOVEPOST: the thread couldn't be found");         
                return -6;
                //return "the topic could not been found";
            }
            if ((p_index < 0) | (p_index > _db.getCurrentPostID(p_fid,p_tid)))
            {
                //_logger.log(2, "init", "ERROR REMOVEPOST: the topic couldn't be found");         
                return -8;
                //return "the post topic is out of bounds";
            }
            
            //ForumPost t_post = _db.getPost(p_fid, p_tid, p_index);
            Boolean postExist = _db.isPost("(pid = " + p_index + ") and (fid = " + p_fid + ") and (tid = "+p_tid+")");
            string postAuthor = _db.getPostAuthor(p_index, p_fid, p_tid).ToLower();
            //Console.Out.WriteLine("p_uname: " + p_uname + ", postAuthor: " + postAuthor);
            if (postExist && (postAuthor.Equals(p_uname.ToLower())))
            {
                _db.removePost(p_fid, p_tid, p_index);
                return 0;
                //return "your thread was removed from the forum";
            }
            else
            {
                //_logger.log(2, "init", "ERROR REMOVEPOST: the post was not written by "+p_uname);         
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
