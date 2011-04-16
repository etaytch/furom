﻿ using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace ForumSever
{
    public class Database
    {
        private List<MemberInfo> _Members;
        public  List<Forum> _forums;         //private
        private int _counter;
        private SqlConnection _conn;

        public Database()
        {
            _Members = new List<MemberInfo>();
            _forums = new List<Forum>();
            _counter = 0;
            _conn = new SqlConnection(/*user id=username;" +
                                       "password=pass;*/"server=ETAY-PC\\SQLEXPRESS;" +
                                       "Trusted_Connection=yes;" +
                                       "database=Furom; " +
                                       "connection timeout=30");            
            try {
                _conn.Open();
                //SqlDataReader ans = null;
                //ans = runSelect("INSERT INTO Users Values ('etaytch1','etay','tchechanovski','1234','m','Israel','Beer Sheva','etaytch@gmail.com','06.04.1985')");
                /*
                while (myReader.Read()) {
                    Console.WriteLine(myReader["fid"].ToString());
                    Console.WriteLine(myReader["fname"].ToString());
                }
                */
            }
            catch (Exception e) {
                Console.WriteLine(e.ToString());
            }                                                
        }

        /*Members founctions  */
 
        public int MemberCount()
        {
            return _Members.Count();
        }

        public bool addMember(MemberInfo memb)
        {
            memb.setID( _counter);
            _counter++;
            _Members.Add(memb);
            string str = "('" + memb.getUName() + "'";
            str += ",'" + memb.getFName() + "'";
            str += ",'" + memb.getLName() + "'";
            str += ",'" + memb.getPass() + "'";
            str += ",'" + memb.getSex() + "'";
            str += ",'" + memb.getCountry() + "'";
            str += ",'" + memb.getCity() + "'";
            str += ",'" + memb.getEmail() + "'";
            str += ",'" + memb.getBirthday() + "')";
            try {             
                runSelect("INSERT INTO Users Values "+str);
            }
            catch (Exception e) {
                Console.WriteLine("Error while adding member. Maybe the username '" + memb.getUName()+"' already exsist?");
            }     
            return true;
        }

        private SqlDataReader runSelect(String command){
            SqlDataReader ans = null;
            SqlCommand myCommand = new SqlCommand(command, _conn);            
            return myCommand.ExecuteReader();
        }

        public object FindMemberByEmail(string mail){
            SqlDataReader reader = runSelect("SELECT * FROM Users WHERE email = '"+mail+"'");
            if (reader != null) {
                while (reader.Read()) {
                    Console.WriteLine(reader["username"].ToString());
                    Console.WriteLine(reader["fname"].ToString());
                    Console.WriteLine(reader["lname"].ToString());
                    Console.WriteLine(reader["password"].ToString());
                    Console.WriteLine(reader["sex"].ToString());
                    Console.WriteLine(reader["country"].ToString());
                    Console.WriteLine(reader["city"].ToString());
                    Console.WriteLine(reader["email"].ToString());
                    Console.WriteLine(reader["birthday"].ToString());
                    break;
                }
            
            }

            foreach (MemberInfo memb in _Members)
            {

                if (memb.getEmail() == mail)
                {
                    return memb;
                }

            }
            return null;
        }

        public MemberInfo FindMemberByUser(string username)
        {
            SqlDataReader reader = runSelect("SELECT * FROM Users WHERE username = '" + username + "'");
            if (!reader.HasRows) {
                return null;
            }
            if (reader.Read()) {
                Console.WriteLine("***: " + reader["username"].ToString());
                Console.WriteLine("***: " + reader["fname"].ToString());
                Console.WriteLine("***: " + reader["lname"].ToString());
                Console.WriteLine("***: " + reader["password"].ToString());
                Console.WriteLine("***: " + reader["sex"].ToString());
                Console.WriteLine("***: " + reader["country"].ToString());
                Console.WriteLine("***: " + reader["city"].ToString());
                Console.WriteLine("***: " + reader["email"].ToString());
                Console.WriteLine("***: " + reader["birthday"].ToString());                
            }
                
   
            foreach (MemberInfo memb in _Members)
            {

                if (memb.getUName() == username)
                {
                    return memb;
                }

            }
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
        

        public Forum findForum(int fid){
            for (int j = 0; j < _forums.Count(); j++) {
                if (fid == _forums.ElementAt(j).getId()) {
                    return _forums.ElementAt(j);                    
                }
            }
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

        
        /*Threads founctions  */

        public int addTread(ForumThread p_tread)
        {
            Forum tForum = findForum(p_tread.getForumID());
            if (tForum!=null) {
                return tForum.addTread(p_tread);                                
            }

            // Etay
            return -1;
        }

        public bool findTopicInForums(string topic) {            
            for (int i = 0; i < _forums.Count();i++ ) {
                if (_forums.ElementAt(i).getTopic().Equals(topic)) {
                    return true;
                }
            }
            return false;

        }

        public bool findTopicInThreads(int fid,string topic){
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

        public Forum getForum(int fid) {
            return this._forums.ElementAt(fid);
        }

        //getForum(p_fid)

        public ForumThread getTread(int fid, int p_tId) {
            Forum tForum = findForum(fid);
            if (tForum != null) {
                for (int i = 0; i < tForum.getThreads().Count; i++) {
                    Console.WriteLine(tForum.getThreads().ElementAt(i).getID());
                    if (tForum.getThreads().ElementAt(i).getID() == p_tId) {
                        return tForum.getThreads().ElementAt(i);
                    }
                }
            }
            return null;
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

    }
}