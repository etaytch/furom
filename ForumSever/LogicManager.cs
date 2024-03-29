﻿using System;
using System.Collections;
using System.Linq;
using System.Text;
using MessagePack;
using System.Collections.Generic;
using Log;

namespace ForumSever
{

    public class LogicManager
    {
        public Database _db; //should be private
        private static Object _logicLock= new Object();
        private static Object _IPLock= new Object();
        private Hashtable _usersIp;
        private Hashtable _usersData;
        private string _type = "";

        public LogicManager(String p_type)
        {
            _type = p_type;
            _db = new Database();
            _usersIp = new Hashtable();
            _usersData = new Hashtable();
        }

        public LogicManager(Database p_db,String p_type)
        {
            this._db = p_db;
            this._type = p_type;
            _usersIp = new Hashtable();
            _usersData = new Hashtable();
        }

        public void addUserIP(string userName,string IP){
            lock(_IPLock){
                if (_usersIp[IP] == null) {
                    this._usersIp.Add(IP, userName);
                    this._usersData.Add(IP, new UserData(userName));
                }
                else {
                    _usersIp[IP] = userName;
                    _usersData[IP] = new UserData(userName);
                }                                
            }
        }

        public void removeUserIP(string IP)
        {
            lock (_IPLock)
            {
                if(this._usersIp[IP]!=null){
                    this._usersIp.Remove(IP);                    
                }
                if (this._usersData[IP] != null) {
                    this._usersData.Remove(IP);
                }
                
            }
        }

        public string getUserFromIP(string IP) {
            string result = "";
            lock (_IPLock) {
                try {
                    result = _usersIp[IP] as string;
                }
                catch (Exception) {
                    result = "";
                }
            }
            return result;
        }

        public List<UserData> getFriendsUserData(string username) {
            List<UserData> result = new List<UserData>();            
            lock (_IPLock) {
                try {                    
                    List<string> friendsNames = getFriends(username);
                    List<string> friendsIPs = getFriendsIPS(friendsNames);
                    foreach (string frIP in friendsIPs) {
                        result.Add(getUserDataFromIP(frIP));
                    }                                        
                }
                catch (Exception) {
                    result = null;
                }
            }
            return result;
        }

        private List<string> getFriendsIPS(List<string> friendsNames) {
            List<string> result = new List<string>();
            Hashtable reverseHash = getReverseHashTable();            

            foreach (string fr in friendsNames) {
                string ip = (string)reverseHash[fr];
                if(ip!=null){
                    result.Add(ip);
                }                
            }
            return result;
        }

        public Hashtable getReverseHashTable() {
            Hashtable reverseHash = new Hashtable();
            foreach (string key in _usersIp.Keys) {
                string val = (string)_usersIp[key];
                reverseHash[val] = key;
            }

            return reverseHash;
        }

        public List<UserData> getWatchersUserData(string username,int p_fid) {
            List<UserData> result = new List<UserData>();
            //UserData posterUser;
            lock (_IPLock) {
                try {
                    //posterUser = getUserDataFromIP(IP);                    
                    foreach (string ip in _usersData.Keys) {
                        UserData tmp = (UserData)_usersData[ip];
                        if(!tmp.Username.Equals(username)){
                            if (tmp.curForum._pIndex == p_fid) {
                                result.Add(tmp);
                            }                        
                        }                        
                    }
                }
                catch (Exception) {
                    result = null;
                }
            }
            return result;
        }

        public string findIPbyUsername(string username) { 
            foreach(string key in _usersIp.Keys){
                string val = (string)_usersIp[key];
                if(val.Equals(username)){
                    return key;
                }
            }
            return "";
        }

        private void printHash() {
            Logger.append("Start of Hash content:", Logger.ERROR);
            foreach (string key in _usersIp.Keys) {
                string val = (string)_usersIp[key];
                Logger.append("key: "+key+", val: "+val, Logger.ERROR);
            }
            Logger.append("End of Hash content:", Logger.ERROR);
        }

        public List<UserData> getFriendsAndWatchersUserData(string username,int p_fid) {
            List<UserData> result = new List<UserData>();            
            List<UserData> friends;
            List<UserData> watchers;
            //string IP = findIPbyUsername(username);
            lock (_IPLock) {
                try {
                    friends = getFriendsUserData(username);
                    watchers = getWatchersUserData(username, p_fid);
                    result = (List<UserData>)friends.Union(watchers);
                }
                catch (Exception) {
                    result = null;
                }
            }
            return result;
        }

        public UserData getUserDataFromIP(string IP) {
            UserData result;
            lock (_IPLock) {
                try {
                    //printHash();
                    result = _usersData[IP] as UserData;
                }
                catch (Exception) {
                    result = null;
                }
            }
            return result;
        }
      
        /*Member functions*/        

        public int register(MemberInfo memb)
        {
            lock (_logicLock)
            {
                string uname = memb.getUName();
                // cant add Member without all fields
                if (memb.getLName() == "" | memb.getFName() == "" | memb.getUName() == "" | memb.getPass() == "" | memb.getEmail() == "")
                {
                    Logger.append(" ERROR: "+uname + "did not type all neseccery details for registering", Logger.ERROR);
                    return -17;
                }
                // user name already in use
                if (_db.isMember(memb.getUName()))
                {
                    Logger.append(" ERROR: " + uname + " alread exist.", Logger.ERROR);

                    return -15;
                }

                if (_db.isMember(memb.getEmail()))
                {
                    Logger.append("ERROR: " + uname + " tried to register with an existing email: " + memb.getEmail(), Logger.ERROR);
                    return -16;
                }
                Logger.append("The user " + uname + " registered succesfully ", Logger.INFO);
                _db.addMember(memb);
                return 0;
            }
        }

        public int login(string p_user, string p_pass)
        {   
            int result=0;
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
                            Logger.append("ERROR LOGIN: " + p_user + " tried to login with wrong password", Logger.ERROR);
                            result = -23;
                        }

                    }
                    else
                    {
                        Logger.append("ERROR LOGIN: " + p_user + " tried to login while already logged in", Logger.ERROR);
                        result = -18;     // user already logged in
                    }
                }

                else
                {
                    Logger.append("ERROR LOGIN: " + p_user + " does not exsist", Logger.ERROR);
                    result= - 3;         // username not exist
                }
            }
            Logger.append("The user " + p_user + " logged in succesfully ", Logger.INFO);
            return result;
        }

        public int logout(string p_user)
        {
            int result = 0;
            lock (_logicLock)
            {
                if (_db.isMember(p_user))
                {
                    if (_db.isLogin(p_user))
                    {
                        _db.markUserAsLogged(p_user, 0);
                        result= 0;       // no error
                    }
                    else
                    {
                        Logger.append("ERROR LOGOUT: " + p_user + " tried to logout while already logged out", Logger.ERROR);
                        result= - 19;     // user is not logged in
                    }
                }
                Logger.append("ERROR LOGOUT: " + p_user + " does not exsist", Logger.ERROR);

                result= - 3;         // username not exist
            }
            Logger.append("The user " + p_user + " logged out succesfully ", Logger.INFO);
            return result;

        }
        public bool isMember(string p_user) {
            bool result = false;
            lock (_logicLock)
            {
                result= _db.isMember(p_user);
            }
            return result;
        }

        public bool isLogged(string p_user)
        {
            MemberInfo memb=null ;
            lock (_logicLock)
            {
                memb = _db.FindMember("username = '" + p_user + "'");
            }           
            return ((memb != null) && (memb.isLogged()));
        }

        public bool isAdmin(string p_user) {
            return _db.isAdmin(p_user);
        }

        public List<string> getUsers(string p_uname){
            List<string> result = null;
            lock (_logicLock)
            {
                result= _db.getUsers(p_uname);
            }
            return result;
        }

        public List<string> getFriends(string p_uname) {
            List<string> result = null;
            lock (_logicLock)
            {
                result= _db.getFriends(p_uname);
            }
            return result;
        }
        
        public MemberInfo FindMemberByUser(string p_user)
        {
            MemberInfo result = null;
            lock (_logicLock)
            {
                result= _db.FindMember("username = '" + p_user + "'");
            }
            return result;
        }

        public int addMeAsFriend(string p_uname,string p_friendUname)
        {
            lock (_logicLock)
            {
                if (!_db.isMember(p_uname))
                {  
                    Logger.append("ERROR ADDFRIEND: " + p_uname + " does not exsist", Logger.ERROR);
                    return -3;
                }
                if (!_db.isMember(p_friendUname))
                {
                    Logger.append("ERROR ADDFRIEND: " + p_friendUname + " does not exsist", Logger.ERROR);
                    return -2;
                }
                if (p_uname.Equals(p_friendUname))
                {
                    Logger.append("ERROR ADDFRIEND: User " + p_uname + " tried to add himself as friend", Logger.ERROR);
                    return -24;
                }

                if (_db.isFriend(p_uname, p_friendUname))
                {
                    Logger.append("ERROR ADDFRIEND: User " + p_uname + " tried to add an existing friend again", Logger.ERROR);
                    return -1;
                }
                Logger.append("The user " + p_uname + " added " + p_friendUname + " to his friend list succesfully ", Logger.INFO);
                _db.addFriend(p_uname, p_friendUname);
                return 0;
            }
        }

        public int removeMeAsFriend(string p_uname, string p_friendUname)
        {
            int result = 0;
            lock (_logicLock)
            {
                if (!_db.isMember(p_uname))
                {
                    Logger.append("ERROR REMOVEFRIEND: " + p_uname + " does not exist", Logger.ERROR);
                    //return "incurrect user name";
                    result = -3;

                }
                if (!_db.isMember(p_friendUname))
                {
                    Logger.append("ERROR REMOVEFRIEND: " + p_friendUname + " does not exist", Logger.ERROR);
                    //"the user you are trying to unfriend dosn't exist"                        
                    result = -20;
                }

                if (!_db.isFriend(p_uname, p_friendUname))
                {
                    Logger.append("ERROR REMOVEFRIEND: " + p_friendUname + " is not a friend of " + p_uname, Logger.ERROR);
                    //return "the user you are trying to unfriend is not a friend of yours";
                    result = -4;
                }
                _db.removeFriend(p_uname, p_friendUname);

                Logger.append("The user " + p_uname + " removed " + p_friendUname + " from his friends list ", Logger.INFO);
                result = 0;
            }
            return result;
        }

        public List<Quartet> getForums() 
        {
            List<Quartet> result = null;
            lock(_logicLock)
            {
            result = _db.getForums();
            }
            return result;        
        }

        public bool isForum(int p_fid) {
            bool result = false;
            lock (_logicLock)
            {
                result= _db.isForum(p_fid);
            }
            return result;
        }

        //addForum
        public int addForum(string p_uname, string p_topic)
        {
            if (!_db.isMember(p_uname)) {
                Logger.append("ERROR ADDFORUM: incorrect username: " + p_uname, Logger.ERROR);
                return -3;
            }
            if (!_db.isAdmin(p_uname)) {
                Logger.append("ERROR ADDFORUM: the user: " + p_uname + " is not admin! ", Logger.ERROR);
                return -7;
            }
            if (_db.recordExsist("Select * from forums where fname = '"+p_topic+"'")) {
                Logger.append("ERROR ADDFORUM: the forum: " + p_topic + " is already exsist! ", Logger.ERROR);
                return -8;
            }
            if (_db.addForum(p_topic)) {
                Logger.append("The Forum " + p_topic + " has been created successfully by the admin " + p_uname, Logger.INFO);
                return 0;
            }
            return -1;  // unexpected error

        }

        public int addTread(string p_uname, int p_fid, string p_topic, string p_content)
        {
            int result = 0;
            lock (_logicLock)
            {
                MemberInfo memb = FindMemberByUser(p_uname);
                if (memb == null)
                {
                    Logger.append("ERROR ADDTHREAD: incorrect username: " + p_uname, Logger.ERROR);
                    result= -3;
                }
                else if (_db.isThread(p_fid, p_topic))
                {
                    Logger.append("ERROR ADDTHREAD: Thread "+p_topic+" alread exist", Logger.ERROR);
                    result= -5;
                }

                else
                {
                    ForumThread t_thr = new ForumThread(p_fid, p_topic, p_content, p_uname);
                    result = _db.addTread(t_thr);
                    string forumName = _db.getForumName(p_fid);
                    if(_type.Equals("web")){
                        List<UserData> webFriends = getFriendsUserData(p_uname);
                        foreach (UserData viewer in webFriends) {                                                        
                            viewer.notifications.Enqueue("User " + p_uname + " added new thread in " + forumName);
                            Logger.append("Notifing " + viewer.Username + " that the user " + p_uname + " added new thread in " + forumName, Logger.INFO);
                        }
                    }
                }

            }
            Logger.append("The Thread " + p_topic + " added to the forum " + getForumName(p_fid), Logger.INFO);
            return result;
        }

        public List<Quartet> getThreadPosts(int p_fid, int p_tid) {
            List<Quartet> result = null;
            lock (_logicLock)
            {
                result= _db.getThreadPosts(p_fid, p_tid);
            }
            return result;
        }

        public PostsTree getThreadPostsAndContent(int p_fid, int p_tid, string uname)
        {
            PostsTree pt = new PostsTree();
            List<Quartet> result = null;
            lock (_logicLock)
            {
                result = _db.getThreadPosts(p_fid, p_tid);
            }

            pt.Children = fillPostTree(p_fid, p_tid, pt, result, uname);
            return pt;
        }

        private List<PostsTree> fillPostTree(int p_fid, int p_tid,PostsTree pt, List<Quartet> result, string uname)
        {
            List<PostsTree> pt_children = new List<PostsTree>();
            foreach (Quartet post in result)
            {
                if (post._parent == pt.Post._pIndex)
                {
                    PostsTree child = new PostsTree();
                    child.Content = getPost(p_fid, p_tid, post._pIndex, uname).getContent(); //get the post content
                    child.Post = post;  //post metadata
                    child.Children = fillPostTree(p_fid, p_tid, child, result, uname); //now look for posts children
                    pt_children.Add(child);
                }
            }
            if (pt_children.Count == 0) return null;
            return pt_children;
        }

        public ForumThread getThread(int p_fid, int p_tid)
        {
            ForumThread result = null;
            lock (_logicLock)
            {
                result= _db.getThread(p_fid, p_tid);
            }
            return result;
        }

        public List<Quartet> getForum(int p_fid)
        {
            List<Quartet> result = null;
            lock (_logicLock)
            {
                result = _db.getForum(p_fid);
            }
            return result;
        }        

        public int removeForum(int p_fid,string p_uname) {
            if (!_db.isMember(p_uname)) {
                Logger.append("ERROR REMOVEFORUM: incorrect username: " + p_uname, Logger.ERROR);
                return -3;
            }            
            if (!_db.isAdmin(p_uname)) {
                Logger.append("ERROR REMOVEFORUM: the user: " + p_uname + " is not admin! ", Logger.ERROR);
                return -7;
            }
            if (_db.removeForum(p_fid)) {
                Logger.append("The Forum " + this.getForumName(p_fid) + " was removed by the admin " + p_uname, Logger.INFO);
                return 0;
            }
            else {
                Logger.append("ERROR REMOVEFORUM: SQL ERROR", Logger.ERROR);
                return -22;
            }
        }


        public int removeThread(int p_fid, int p_tid,string p_uname)
        {
            string userType = "user";
            if (!_db.isMember(p_uname))
            {
                Logger.append("ERROR REMOVETHREAD: incorrect username: " + p_uname, Logger.ERROR);
                return -3;
            }
            if (_db.isAdmin(p_uname)) {
                userType = "admin";
            }
            else if (!_db.getThreadAuthor(p_fid, p_tid).Equals(p_uname))
            { 
                Logger.append("ERROR REMOVETHREAD: the thread was not written by " + p_uname, Logger.ERROR);
                return -7;
            }
            if (_db.removeThread(p_fid, p_tid))
            {
                Logger.append("The Thread " + this.getThreadName(p_fid, p_tid) + " removed from the forum" + getForumName(p_fid) + " by the "+userType+": " + p_uname, Logger.INFO);
                return 0;
            }
            else
            {
                Logger.append("ERROR REMOVETHREAD: SQL ERROR", Logger.ERROR);
                return -22;
            }            
        }
        

        /*Posts founctions  */        
        public int addPost(int p_tid, int p_fid, int parentId, string p_topic, string p_content,string p_uname)
        {
            int result = 0;
            lock (_logicLock)
            {
                if (!_db.isMember(p_uname))
                {
                    Logger.append("ERROR ADDPOST: incourrect username: " + p_uname, Logger.ERROR);
                    result = -3;

                }
                //ForumThread t_thr = _db.getTread(p_fid, p_tid);

                else if (!_db.isThread(p_fid, p_tid)) {
                    Logger.append("ERROR ADDPOST: Thread " + p_tid + " was not found in forum " + getForumName(p_fid), Logger.ERROR);
                    result = -6;
                }
                else {
                    _db.addPost(p_tid, p_fid, parentId, p_topic, p_content, p_uname);

                    string threadName = _db.getThreadName(p_fid, p_tid);
                    string forumName = _db.getForumName(p_fid);
                    string author = _db.getThreadAuthor(p_fid,p_tid);

                    List<UserData> webUsersDataToUpdate = getFriendsAndWatchersUserData(p_uname, p_fid);
                    if (webUsersDataToUpdate == null) {
                        webUsersDataToUpdate = new List<UserData>();
                    }

                    Hashtable reversedHash = getReverseHashTable();
                    string authorIP = (string)reversedHash[author];
                    if(authorIP!=null){
                        UserData ud = getUserDataFromIP(authorIP);                        
                        string name = ud.Username;
                        if(!webUsersDataToUpdate.Contains(ud)){
                            webUsersDataToUpdate.Add(ud);
                        }
                    }                    

                    if (webUsersDataToUpdate != null) {
                        foreach (UserData viewer in webUsersDataToUpdate) {
                            viewer.notifications.Enqueue("User " + p_uname + " added new post to thread " + threadName + " in " + forumName);
                            Logger.append("Notifing " + viewer.Username + " that the user " + p_uname + " added new post to thread " + threadName + " in " + forumName, Logger.INFO);
                        }
                    }

                    result = 0;
                }
            }
            Logger.append("The Post " + p_topic + " added to the forum " + getForumName(p_fid) +" by the user "+p_uname, Logger.INFO);
            return result;
        }
        

        public ForumPost getPost(int p_fid, int p_tid, int p_index, string p_uname)
        {          
            ForumPost result = null;
            lock (_logicLock)
            {
                if (!_db.isMember(p_uname))
                {

                    Logger.append("ERROR GETPOST: incourrect username: "+ p_uname, Logger.ERROR);
                    result= null;
                }
                else if (!_db.isThread(p_fid, p_tid))
                {
                    Logger.append("ERROR GETPOST: Thread not found "+ p_uname, Logger.ERROR);
                    result = null;
                }
                else
                {
                    result = _db.getPost(p_fid, p_tid, p_index, p_uname);
                }
            }
                return result;

        }

        public int removeUsers(string adminUser, List<string> usersToDelete) {
            List<string> existUsersToDelete = new List<string>();
            if (!_db.isMember(adminUser)) {
                Logger.append("ERROR REMOVEUSERS: " + adminUser + " does not exist", Logger.ERROR);
                return -3;
                //return "incurrect user name";
            }
            if (!_db.isAdmin(adminUser)) {
                Logger.append("ERROR REMOVEUSERS: the user: " + adminUser + " is not admin! ", Logger.ERROR);
                return -7;
            }

            foreach(string user in usersToDelete){
                if (_db.isMember(user)) {
                    existUsersToDelete.Add(user);
                }
            }

            _db.removeFriendsFromTables(adminUser,existUsersToDelete);      // remove users from every user' friends table
            _db.removeUsers(existUsersToDelete);                            // remove users from the database
            return existUsersToDelete.Count;
        }


        public int removePost(int p_fid, int p_tid, int p_index, string p_uname)
        {
            string userType = "user";
            
            if (!_db.isMember(p_uname))
            {
                Logger.append("ERROR REMOVEPOST: " + p_uname + " does not exist", Logger.ERROR);    
                return -3;
                //return "incurrect user name";
            }
            if (!_db.isThread(p_fid, p_tid))
            {
                Logger.append("ERROR REMOVEPOST: the thread couldn't be found", Logger.ERROR);
                return -6;
            }
            if ((p_index < 0) | (p_index > _db.getCurrentPostID(p_fid, p_tid)))
            {
                Logger.append("ERROR REMOVEPOST: the topic couldn't be found", Logger.ERROR);
                return -8;
            }
            
            Boolean postExist = _db.isPost("(pid = " + p_index + ") and (fid = " + p_fid + ") and (tid = " + p_tid + ")");
            string postAuthor = _db.getPostAuthor(p_index, p_fid, p_tid).ToLower();
            if (!postExist) {
                Logger.append("ERROR REMOVEPOST: the topic couldn't be found", Logger.ERROR);
                return -8;
            }

            if (_db.isAdmin(p_uname)) {
                userType = "admin";
            }
            else if (!postAuthor.Equals(p_uname.ToLower()))
            {
                Logger.append("ERROR REMOVEPOST: the post was not written by " + p_uname, Logger.ERROR);
                return -7;                        
            }            
            if(_db.removePost(p_fid, p_tid, p_index)){
                    Logger.append("post " + p_index + " removed from the thread " + getThreadName(p_fid, p_tid) + " in forum " + getForumName(p_fid) + " by the "+userType+": " + p_uname, Logger.INFO);
                return 0;
            }
            else {
                Logger.append("ERROR REMOVEPOST: SQL ERROR", Logger.ERROR);
                return -22;
            }            
            
            
               
        }

        public void updateCurrentThread(int t_fid, int t_tid, string t_uname) {
            lock (_logicLock)
            {
                _db.updateCurrentThread(t_fid, t_tid, t_uname);
            }
        }

        public List<string> getFriendsToUpdate(string t_uname) {
            List<string> result = null;
            lock (_logicLock)
            {
                result= _db.getFriendsToUpdate(t_uname);
            }
            return result;
        }

        public string getForumName(int t_fid) {
            string result = "";
            lock (_logicLock)
            {
                result= _db.getForumName(t_fid);
            }
            return result;
        }

        public List<string> getForumViewers(int t_fid) {
            List<string> result = null;
            lock (_logicLock)
            {
                result= _db.getForumViewers(t_fid);
            }
            return result;
        }



        public List<string> getThreadViewersToUpdate(string t_uname, int t_fid, int t_tid) {
            List<string> result = null;
            lock (_logicLock)
            {
                result= _db.getThreadViewersToUpdate(t_uname, t_fid, t_tid);         
            }
            return result;
               
        }

        public string getThreadName(int t_fid, int t_tid) 
        {
            string result = "";
            lock (_logicLock)
            {
                result = _db.getThreadName(t_fid, t_tid);
            }
            return result;
        }

        public Database db {
            get { return this._db; }
        }

        public Hashtable usersIp {
            get { return _usersIp; }
        }

        public Hashtable usersData {
            get { return _usersData; }
        }

       
    }
}
