 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using MessagePack;
using VS.Logger;
using System.Security.Cryptography;

namespace ForumSever
{
    public class Database
    {
        private List<MemberInfo> _Members;
        public  List<Forum> _forums;         //private
        private int _counter;
        private SqlConnection _conn;
        //private Logger _logger;

        public Database(/*Logger logger*/) {
            _Members = new List<MemberInfo>();
            _forums = new List<Forum>();
            _counter = 0;
            //_logger = logger;
            
            _conn = new SqlConnection("server=Vadi-PC\\SQLEXPRESS;" +
                                       "Trusted_Connection=yes;" +
                                       "database=Furom; " +
                                       "connection timeout=30");

            runOtherSQL("UPDATE Users SET logged=0, currForum=-1,currThread=-1");            
             
        }
        /*Members founctions  */       

        public int MemberCount()
        {
            return _Members.Count();
        }


        public int getCurrentPostID(int p_fid, int p_tid) {
            int ans;
            SqlDataReader reader = runSelectSQL("select pid from Posts where (fid = " + p_fid + ") and (tid = " + p_tid + ") and (pid >=all (select pid from Posts where (fid = " + p_fid + ") and (tid = " + p_tid + ")))");
            if (!reader.HasRows) {
                //Console.WriteLine("SQL=empty");
                _conn.Close();
                return 0;
            }

            while (reader.Read()) {
                ans = Convert.ToInt32(reader["pid"].ToString());
                _conn.Close();
                return ans;
            }
            return -1; // not reachable..
        }

        public bool addPost(int p_tid, int p_fid, int p_parentId, string p_topic, string p_content, string p_uname) {
            Boolean ans = false;
            int nextPostId = 0; 
            try {
                nextPostId = getCurrentPostID(p_fid, p_tid)+1;
                runSelectSQL("INSERT INTO Posts Values (" + nextPostId + "," + p_tid + "," + p_fid + "," + p_parentId + ",'" + p_topic + "','" + p_content + "','" + p_uname + "')");
                ans = true;
                _conn.Close();
            }
            catch (Exception e) {
                _conn.Close();
                //Console.WriteLine("Error while adding Post...\n" + "INSERT INTO Posts Values (" + nextPostId + "," + p_tid + "," + p_fid + "," + p_parentId + ",'" + p_topic + "','" + p_content + "','" + p_uname + "')");
                //Console.WriteLine(e.ToString());
            }     

            return ans;
        }



        public bool removeThread(int p_fid, int p_tid) {
            try {
                runSelectSQL("Delete From Threads Where " + "(fid = " + p_fid + ") and (tid = " + p_tid + ")");
                _conn.Close();
                return true;
            }
            catch (Exception e) {
                _conn.Close();
                //Console.WriteLine("Error while removing Post...\n");
                //Console.WriteLine(e.ToString());
            }

            return false;
        }



        public bool removePost(int p_fid, int p_tid, int p_index) {
            try {
                runSelectSQL("Delete From Posts Where " + "(pid = " + p_index + ") and (fid = " + p_fid + ") and (tid = " + p_tid + ")");
                _conn.Close();
                return true;
            }
            catch (Exception e) {
                _conn.Close();
                //Console.WriteLine("Error while removing Post...\n");
                //Console.WriteLine(e.ToString());
            }

            return false;
        }

        public ForumPost getPost(int p_fid, int p_tid, int p_index, string p_uname) {
            ForumPost ans=null;
            SqlDataReader reader = runSelectSQL("SELECT * FROM Posts WHERE (pid=" +p_index+") and (fid="+p_fid+") and (tid="+p_tid+")");
            if (!reader.HasRows) {
                //Console.WriteLine("SQL=empty");
                _conn.Close();
                return null;
            }


            while (reader.Read()) {
                //public ForumPost(int fid, int tid, string p_topic, string p_content, string p_author)                
                ans = new ForumPost(Convert.ToInt32(reader["fid"].ToString()), Convert.ToInt32(reader["tid"].ToString()), Convert.ToInt32(reader["parentid"].ToString()), reader["subject"].ToString(), reader["body"].ToString(), reader["author"].ToString());
                _conn.Close();
                return ans;
            }
            return ans;
        }


        public bool login(string p_user, string p_pass) {
            string tmp = getMd5Hash(p_pass);
            return recordExsist("SELECT * FROM Users WHERE (username = '" + p_user + "') and (password = '" + getMd5Hash(p_pass) + "')");
        }

        public bool addMember(MemberInfo memb)
        {
            memb.setID( _counter);
            _counter++;
            _Members.Add(memb);
            string tmp = getMd5Hash(memb.getPass());
            string str = "('" + memb.getUName() + "'";
            str += ",'" + memb.getFName() + "'";
            str += ",'" + memb.getLName() + "'";
            str += ",'" + getMd5Hash(memb.getPass()) + "'";
            str += ",'" + memb.getSex() + "'";
            str += ",'" + memb.getCountry() + "'";
            str += ",'" + memb.getCity() + "'";
            str += ",'" + memb.getEmail() + "'";
            str += ",'" + memb.getBirthday() + "'";
            str += "," + 0;
            str += "," + (-1);
            str += "," + (-1) + ")";
            try {             
                runSelectSQL("INSERT INTO Users Values "+str);
                _conn.Close();
            }
            catch (Exception e) {
                _conn.Close();
                //Console.WriteLine("Error while adding member. Maybe the username '" + memb.getUName()+"' already exsist?");
                //Console.WriteLine(e.ToString());
            }

            try {
                runSelectSQL("CREATE TABLE " + memb.getUName() + " (uname varchar(50) NOT NULL, PRIMARY KEY (uname),FOREIGN KEY (uname) REFERENCES Users(username) ON DELETE CASCADE)");
                _conn.Close();
            }
            catch (Exception e) {
                _conn.Close();
                //Console.WriteLine("Error while adding member. Maybe the username '" + memb.getUName() + "' already exsist?");
                //Console.WriteLine(e.ToString());
            }     



            return true;
        }

        private SqlDataReader runSelectSQL(String command){
            if (!_conn.State.Equals("Open")) {
                _conn.Open();
            }
            SqlDataReader ans = null;
            SqlCommand myCommand = new SqlCommand(command, _conn);         
            return myCommand.ExecuteReader();            
        }

        private void runOtherSQL(String command) {
            _conn.Open();
            SqlDataReader ans = null;
            SqlCommand myCommand1 = new SqlCommand(command, _conn);
            myCommand1.ExecuteNonQuery();
            _conn.Close();
        }

        /*
        public int MemberCount();
        public int getCurrentPostID(int p_fid, int p_tid);
        public bool addPost(int p_tid, int p_fid, int p_parentId, string p_topic, string p_content, string p_uname);
        public bool removePost(int p_tid, int p_fid, int p_index);
        public ForumPost getPost(int p_fid, int p_tid, int p_index, string p_uname);
        public bool addMember(MemberInfo memb);
        private SqlDataReader runSelectSQL(String command);
        private void runOtherSQL(String command);
        public object FindMemberByEmail(string mail);
        public void markUserAsLogged(string username, int logged);
        public bool isMember(string username);
        public bool isThread(string where);
        public bool isPost(string where);
        */

        public object FindMemberByEmail(string mail){
            SqlDataReader reader = runSelectSQL("SELECT * FROM Users WHERE email = '"+mail+"'");

            if (reader != null) {
                while (reader.Read()) {
                    //Console.WriteLine(reader["username"].ToString());
                    //Console.WriteLine(reader["fname"].ToString());
                    //Console.WriteLine(reader["lname"].ToString());
                    //Console.WriteLine(reader["password"].ToString());
                    //Console.WriteLine(reader["sex"].ToString());
                    //Console.WriteLine(reader["country"].ToString());
                    //Console.WriteLine(reader["city"].ToString());
                    //Console.WriteLine(reader["email"].ToString());
                    //Console.WriteLine(reader["birthday"].ToString());
                    break;
                }
            
            }
            _conn.Close();
            foreach (MemberInfo memb in _Members)
            {

                if (memb.getEmail() == mail)
                {
                    return memb;
                }

            }
            return null;
        }

        public void markUserAsLogged(string username,int logged) {
            //Console.WriteLine("UPDATE Users SET logged=1 WHERE username = '" + username + "'");
            runOtherSQL("UPDATE Users SET logged=" + logged + " WHERE username = '" + username + "'");
        }

        public bool isMember(string username) {
            return recordExsist("SELECT * FROM Users WHERE username = '" + username + "'");
        }

        public bool isThread(int p_fid,int p_tid) {
            return recordExsist("SELECT * FROM Threads WHERE (fid = " + p_fid + ") and (tid = "+p_tid+")");
        }

        public bool isThread(int p_fid, string p_topic) {
            return recordExsist("SELECT * FROM Threads WHERE (fid = " + p_fid + ") and (subject = '" + p_topic + "')");
        }

        public bool isPost(string where) {
            return recordExsist("SELECT * FROM Posts WHERE " + where);
        }

        public bool isForum(int p_fid) {
            return recordExsist("SELECT * FROM Forums WHERE fid=" + p_fid);
        }

        public bool isFriend(string p_uname, string p_friendUname) {
            return recordExsist("SELECT * FROM " + p_uname + " WHERE uname='" + p_friendUname+"'");
        }
        public bool isLogin(string username) {
            return recordExsist("SELECT * FROM Users WHERE (username='"+username+"') and (logged=1)");
        }

        public bool recordExsist(string query) {
            SqlDataReader reader = runSelectSQL(query);
            if (!reader.HasRows) {
                //Console.WriteLine("SQL=empty");
                _conn.Close();
                return false;
            }
            _conn.Close();
            return true;
        }

        public string getThreadAuthor(int p_fid, int p_tid) {
            string ans = "";

            SqlDataReader reader = runSelectSQL("SELECT * FROM Threads WHERE (fid = '" + p_fid + "') and (tid = '" + p_tid + "')");
            if (!reader.HasRows) {
                //Console.WriteLine("SQL=empty");
                _conn.Close();
                return ans;
            }


            while (reader.Read()) {
                ans = reader["author"].ToString();
                _conn.Close();
                return ans;
            }
            return ans;
        }


        public string getPostAuthor(int p_pid,int p_fid,int p_tid){
            string ans="";

            SqlDataReader reader = runSelectSQL("SELECT * FROM Posts WHERE " + "(pid = '" + p_pid + "') and (fid = '" + p_fid + "') and (tid = '" + p_tid + "')");
            if (!reader.HasRows) {
                //Console.WriteLine("SQL=empty");
                _conn.Close();
                return null;
            }
            
                    
            while (reader.Read()) {
                ans = reader["author"].ToString();
                _conn.Close();
                return ans;                
            }
            return ans;
        }

        public MemberInfo FindMember(string where){
            MemberInfo ans;
            SqlDataReader reader = runSelectSQL("SELECT * FROM Users WHERE " + where);
            if (!reader.HasRows) {
                //Console.WriteLine("SQL=empty");
                _conn.Close();
                return null;
            }
            
                    
            while (reader.Read()) {
                ans = new MemberInfo(reader["username"].ToString(), reader["fname"].ToString(), reader["lname"].ToString(), reader["password"].ToString(), reader["sex"].ToString(), reader["country"].ToString(), reader["city"].ToString(), reader["email"].ToString(), reader["birthday"].ToString(), reader["logged"].ToString());
                _conn.Close();
                return ans;                
            }

/*            
            foreach (MemberInfo memb in _Members){
                if (memb.getUName() == username){
                    return memb;
                }

            }
 */ 
           return null;
        }

        private int getPlace(string user)
        {
            for (int i = 0; i < _Members.Count(); i++)
            {
                if (_Members.ElementAt(i).getUName() == user)
                    return i;
            }
                return -1;
        }

        public List<string> getUsers(string p_uname) {
            List<string> ans = new List<string>(); 
            SqlDataReader reader = runSelectSQL("SELECT * FROM Users");
            if (!reader.HasRows) {
                //Console.WriteLine("SQL=empty");
                _conn.Close();
                return ans;
            }

            while (reader.Read()) {
                ans.Add(reader["username"].ToString());
            }
            _conn.Close();
            return ans;
        }

        public List<string> getFriends(string p_uname) {
            List<string> ans = new List<string>();
            SqlDataReader reader = runSelectSQL("SELECT * FROM "+p_uname);
            if (!reader.HasRows) {
                //Console.WriteLine("SQL=empty");
                _conn.Close();
                return ans;
            }

            while (reader.Read()) {
                ans.Add(reader["uname"].ToString());
            }
            _conn.Close();
            return ans;
        }


        public List<Quartet> getForums() {
            List<Quartet> ans = new List<Quartet>();

            SqlDataReader reader = runSelectSQL("SELECT * FROM Forums");
            if (!reader.HasRows) {
                //Console.WriteLine("SQL=empty");
                _conn.Close();
                return ans;
            }


            while (reader.Read()) {
                ans.Add(new Quartet(Convert.ToInt32(reader["fid"].ToString()), 0, reader["fname"].ToString(), ""));
            }
            _conn.Close();
                

            return ans;
        }


        public Forum findForum(int fid){
/*
            Forum ans;
            SqlDataReader reader = runSelectSQL("SELECT * FROM Users WHERE " + where);
            if (!reader.HasRows) {
                Console.WriteLine("SQL=empty");
                _conn.Close();
                return null;
            }


            while (reader.Read()) {
                ans = new MemberInfo(reader["username"].ToString(), reader["fname"].ToString(), reader["lname"].ToString(), reader["password"].ToString(), reader["sex"].ToString(), reader["country"].ToString(), reader["city"].ToString(), reader["email"].ToString(), reader["birthday"].ToString(), reader["logged"].ToString());
                _conn.Close();
                return ans;
            }
*/            
/*
            for (int j = 0; j < _forums.Count(); j++) {
                if (fid == _forums.ElementAt(j).getId()) {
                    return _forums.ElementAt(j);                    
                }
            }
 */ 
            return null;
        }

        public int addForum(Forum p_forum) {
            for (int i = 0; i < _forums.Count; i++) {
                if (_forums.ElementAt(i).getTopic().Equals(p_forum.getTopic())) {
                    return -1;
                }
            }
            // Etay
            _forums.Add(p_forum);
            p_forum.setId(_forums.Count-1);
            return _forums.Count-1;
        }

        public int getCurrentThreadID(){
            int ans;
            SqlDataReader reader = runSelectSQL("select tid from Threads where tid >=all (select tid from threads)");
            if (!reader.HasRows) {
                //Console.WriteLine("SQL=empty");
                _conn.Close();
                return 1;
            }
            
            while(reader.Read()){
                ans = Convert.ToInt32(reader["tid"].ToString());
                _conn.Close();
                return ans;
            }
            return -1; // not reachable..
        }

        public void addFriend(string p_uname, string p_friendUname) {
            try {
                //Console.WriteLine("INSERT INTO " + p_uname + " Values ('" + p_friendUname+"')");
                runSelectSQL("INSERT INTO " + p_uname + " Values ('" + p_friendUname + "')");
                _conn.Close();
            }
            catch (Exception e) {
                _conn.Close();
                //Console.WriteLine("Error while adding friend. uname: " + p_uname + ", friendUname: " + p_friendUname);
                //Console.WriteLine(e.ToString());
            }     
        }

        public void removeFriend(string p_uname, string p_friendUname) {
            try {
                //Console.WriteLine("Delete From " + p_uname + " where uname = '" + p_friendUname + "'");
                runSelectSQL("Delete From " + p_uname + " where uname = '" + p_friendUname + "'");
                _conn.Close();
            }
            catch (Exception e) {
                _conn.Close();
                //Console.WriteLine("Error while removing friend. uname: " + p_uname + ", friendUname: " + p_friendUname);
                //Console.WriteLine(e.ToString());
            }
        }
        /*Threads founctions  */

        public int addTread(ForumThread p_tread){
            int nextId = getCurrentThreadID()+1;
            p_tread.setID(nextId);

            string str = "('" + nextId + "'";
            str += ",'" + p_tread._fid + "'";
            str += ",'" + p_tread._topic + "'";
            str += ",'" + p_tread._content + "'";
            str += ",'" + p_tread._autor + "')";
            try {
                //Console.WriteLine("INSERT INTO Threads Values " + str);
                runSelectSQL("INSERT INTO Threads Values " + str);
                _conn.Close();
            }
            catch (Exception e) {
                _conn.Close();
                //Console.WriteLine("Error while adding thread. Maybe the topic '" + p_tread._topic + "' already exsist?");
                //Console.WriteLine(e.ToString());
            }     
            // Etay
            return p_tread.getThreadID();
        }

        public bool findTopicInForums(string topic) {            
            for (int i = 0; i < _forums.Count();i++ ) {
                if (_forums.ElementAt(i).getTopic().Equals(topic)) {
                    return true;
                }
            }
            return false;

        }

        public bool isTopic(int fid, string topic) {
            SqlDataReader reader = runSelectSQL("SELECT * FROM Threads WHERE fid="+fid+" and subject = '"+topic+"'");
            if (!reader.HasRows) {
                //Console.WriteLine("SQL=empty");
                _conn.Close();
                return false;
            }
            _conn.Close();
            return true;
        }

        public bool findTopicInThreads(int fid,string topic){
            return isTopic(fid,topic);
/*
            // Search for the forum
            Forum tForum = findForum(fid);

            // if forum found - search for topic
            if (tForum != null) {
                
                bool threadFound = false;
                int i = 0;
                while (!threadFound & i < tForum.getTheardsTopics().Count()) {
                    threadFound = (topic == tForum.getTheardsTopics().ElementAt(i));
                    i++;
                }
                return threadFound;
            }
            else return false;            
 */
        }

        public ForumThread getTreadFrom(int fid,int i){
            Forum tForum = findForum(fid);
            if (tForum != null) {
                if ((i < tForum.getThreads().Count()) & (i >= 0)) {
                    return tForum.getThreads().ElementAt(i);
                }            
            }
            
            return null;
        }

        public ForumThread getTread(int fid, string p_topic){
            Forum tForum = findForum(fid);
            if (tForum != null) {
                for (int i = 0; i < tForum.getTheardsTopics().Count(); i++) {
                    if (tForum.getTheardsTopics().ElementAt(i) == p_topic) {
                        return tForum.getThreads().ElementAt(i);
                    }
                }            
            }
            
            return null;
        }

        public List<Quartet> getForum(int p_fid) {
            List<Quartet> ans = new List<Quartet>();
            SqlDataReader reader = runSelectSQL("SELECT * FROM Threads WHERE (fid=" + p_fid + ")");
            //Console.WriteLine("SELECT * FROM Threads WHERE (fid=" + p_fid + ")");
            if (!reader.HasRows) {
                //Console.WriteLine("SQL=empty");
                _conn.Close();
                return ans;
            }

            while (reader.Read()) {
                //public ForumThread(int fid,string p_topic, string p_content, string p_author)                
                ans.Add(new Quartet(Convert.ToInt32(reader["tid"].ToString()),0, reader["subject"].ToString(), reader["author"].ToString()));
            }
            _conn.Close();
            return ans;
        }

        //getForum(p_fid)

        public ForumThread getThread(int p_fid, int p_tid) {
            ForumThread ans=null;
            SqlDataReader reader = runSelectSQL("SELECT * FROM Threads WHERE (fid=" + p_fid + ") and (tid=" + p_tid + ")");
            if (!reader.HasRows) {
                //Console.WriteLine("SQL=empty");
                _conn.Close();
                return null;
            }


            while (reader.Read()) {
                //public ForumThread(int fid,string p_topic, string p_content, string p_author)
                ans = new ForumThread(Convert.ToInt32(reader["fid"].ToString()), reader["subject"].ToString(), reader["body"].ToString(), reader["author"].ToString());
                _conn.Close();
                return ans;
            }
            
            return null;
        }

        public List<Quartet> getThreadPosts(int p_fid, int p_tid) {
            List<Quartet> ans = new List<Quartet>();
            
            SqlDataReader reader = runSelectSQL("SELECT * FROM Posts WHERE (fid=" + p_fid + ") and (tid=" + p_tid + ")");
            if (!reader.HasRows) {
                //Console.WriteLine("SQL=empty");
                _conn.Close();
                return ans;
            }


            while (reader.Read()) {                
                ans.Add(new Quartet(Convert.ToInt32(reader["pid"].ToString()),Convert.ToInt32(reader["parentid"].ToString()),reader["subject"].ToString(),reader["author"].ToString()));
            }
            _conn.Close();
            return ans;
        }

        internal void RemoveThreadAt(int fid,int i){
            Forum tForum = findForum(fid);
            if (tForum != null) {
                if ((i < tForum.getThreads().Count()) & (i >= 0)) {
                    tForum.getThreads().RemoveAt(i);
                    for (int j = i; j < tForum.getThreads().Count();j++ ) {
                        tForum.getThreads().ElementAt(j).setID(j);
                    }
                }
            }
            
        }

        internal MemberInfo FindMemberByID(int p_userID)
        {
            foreach (MemberInfo memb in _Members)
            {

                if (memb.getID() == p_userID)
                {
                    return memb;
                }

            }
            return null;
        }

        public void updateCurrentThread(int t_fid, int t_tid, string t_uname) {
            runOtherSQL("UPDATE Users SET currForum=" + t_fid + ", currThread = " + t_tid + " WHERE username = '" + t_uname + "'");
        }

        internal List<string> getFriendsToUpdate(string t_uname) {
            List<string> ans = new List<string>();
            List<string> allUsers = getUsers(t_uname);
            foreach(string user in allUsers){
                if(!t_uname.Equals(user)){
                    if (isFriend(user, t_uname)) {
                        ans.Add(user);
                    }
                }
            }
            return ans;
        }

        public string getForumName(int t_fid) {
            SqlDataReader reader = runSelectSQL("SELECT fname FROM Forums WHERE (fid=" + t_fid + ")");
            string ans = "";
            if (!reader.HasRows) {
                //Console.WriteLine("SQL=empty");
                _conn.Close();
                return ans;
            }


            while (reader.Read()) {
                //public ForumThread(int fid,string p_topic, string p_content, string p_author)
                ans = reader["fname"].ToString();
                _conn.Close();
                return ans;
            }
            return ans;                        
        }

        public List<string> getForumViewers(int t_fid) {
            List<string> ans = new List<string>();

            SqlDataReader reader = runSelectSQL("SELECT username FROM Users Where (currForum=" + t_fid + ") and (logged=1)");
            if (!reader.HasRows) {
                //Console.WriteLine("SQL=empty");
                _conn.Close();
                return ans;
            }


            while (reader.Read()) {
                ans.Add(reader["username"].ToString());
            }
            _conn.Close();

            return ans;            
        }

        internal List<string> getThreadViewersToUpdate(string t_uname, int t_fid, int t_tid) {
            List<string> ans = new List<string>();
            SqlDataReader reader = runSelectSQL("SELECT username FROM Users Where (currForum=" + t_fid+") and (currThread="+t_tid+") and (logged=1)");
            if (!reader.HasRows) {
                //Console.WriteLine("SQL=empty");
                _conn.Close();
                return ans;
            }


            while (reader.Read()) {
                ans.Add(reader["username"].ToString());
            }
            _conn.Close();


            return ans;
        }

        public string getThreadName(int t_fid, int t_tid) {
            SqlDataReader reader = runSelectSQL("SELECT subject FROM Threads WHERE (fid=" + t_fid + ") and (tid="+t_tid+")");
            string ans = "";
            if (!reader.HasRows) {
                //Console.WriteLine("SQL=empty");
                _conn.Close();
                return ans;
            }


            while (reader.Read()) {
                //public ForumThread(int fid,string p_topic, string p_content, string p_author)
                ans = reader["subject"].ToString();
                _conn.Close();
                return ans;
            }
            return ans;                        
            
        }
        // Hash an input string and return the hash as
        // a 32 character hexadecimal string.
        private string getMd5Hash(string input) {
            // Create a new instance of the MD5CryptoServiceProvider object.
            MD5 md5Hasher = MD5.Create();

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++) {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        // Verify a hash against a string.
        private bool verifyMd5Hash(string input, string hash) {
            // Hash the input.
            string hashOfInput = getMd5Hash(input);

            // Create a StringComparer an compare the hashes.
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            if (0 == comparer.Compare(hashOfInput, hash)) {
                return true;
            }
            else {
                return false;
            }
        }


        private void Main1() {
            string source = "Hello World!";

            string hash = getMd5Hash(source);

            Console.WriteLine("The MD5 hash of " + source + " is: " + hash + ".");

            Console.WriteLine("Verifying the hash...");

            if (verifyMd5Hash(source, hash)) {
                Console.WriteLine("The hashes are the same.");
            }
            else {
                Console.WriteLine("The hashes are not same.");
            }

        }


    }


}
