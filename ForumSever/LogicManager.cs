﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MessagePack;

namespace ForumSever
{
    /**
     * 
     * 
     * 
     * 
     * 
     */
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
            if (_db.isMember("username = '"+memb.getUName()+"'")){
                //massage = "user name in use. choose  a diffrent one";
                //return false;
                return -15;
            }

            if (_db.isMember("username = '" + memb.getEmail() + "'"))
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
/*
            //Console.WriteLine("in login!");
            MemberInfo t_user = _db.FindMemberByUser(p_user);
            if ((t_user != null) && (t_user.getPass() == p_pass))
            {
                t_user.login();
                return 0;
            }

            return -3;
 */

            if (_db.isMember("username = '" + p_user + "'")) {
                _db.markUserAsLogged(p_user,1);
                return 0;
            }
            else return -3;
        }

        public int logout(string p_user)
        {
/*
            MemberInfo memb = _db.FindMemberByUser(user);
            if ((memb != null) && (memb.isLogged()))
            {
                memb.logout();
                return 0;
            }
*/
            if (_db.isMember("username = '" + p_user + "'")) {
                _db.markUserAsLogged(p_user, 0);
                return 0;
            }
            return -3;
        }

        public bool isLogged(string p_user)
        {
            MemberInfo memb = _db.FindMember("username = '" + p_user + "'");
            return ((memb != null) && (memb.isLogged()));
        }

        public MemberInfo FindMemberByUser(string p_user)
        {
            return _db.FindMember("username = '" + p_user + "'");
        }

        public int addMeAsFriend(int p_toBeAddedID, int p_AddedToID)
        {
            MemberInfo t_AddedTo = _db.FindMemberByID(p_AddedToID);
            MemberInfo t_toBeAdded = _db.FindMemberByID(p_toBeAddedID);            
            if (t_toBeAdded == null)
            {
                //return "incurrect user name";
                return -3;
            }

            if (t_AddedTo == null)
            {
                //return "the user you are trying to befriend dosn't exist";
                return -2;
            }

            if (t_AddedTo.hasFriend(p_toBeAddedID))
            {
                //return "user is already a friend with this user";
                return -1;
            }
            t_AddedTo.addFriend(t_toBeAdded);
            //return "you have a new friend";
            return t_toBeAdded.getID();
        }

        public int removeMeAsFriend(int p_toBeRemoved, int p_removedFrom)
        {
            MemberInfo t_removedFrom = _db.FindMemberByID(p_removedFrom);
            MemberInfo t_toBeRemoved = _db.FindMemberByID(p_toBeRemoved);
            if (t_toBeRemoved == null)
            {
                return -3;
                //return "incurrect user name";
            }

            if (t_removedFrom == null)
            {
                return -2;
                //return "the user you are trying to befriend dosn't exist";
            }



            if (!t_removedFrom.hasFriend(p_toBeRemoved))
            {
                return -4;
                //return "the user you are trying to unfriend is not a friend of yours";
            }
            t_removedFrom.removeFriend(t_toBeRemoved);
            //return "you have removed yourself as friend";
            return 0;
        }


        /*Threads founctions  */


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
            if (_db.isThread("(fid = '" + p_fid + "') and (" + "subject = '" + p_topic + "')"))
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

        public Forum getForum(int p_fid)
        {
            return _db.getForum(p_fid);
        }

        public int removeTread(int p_fid, int p_userID, int p_tID)
        {
            MemberInfo t_user = _db.FindMemberByID(p_userID);
            if (t_user == null)
            {
                return -3;
                //return "incurrect user name";
            }
            ForumThread tThread = null;
            for (int i = 0; i < _db.MemberCount(); i++)
            {
                tThread = _db.getTreadFrom(p_fid, i);
                if (tThread.getID() == p_tID)
                {
                    if (tThread.getAuthor() == t_user.getUName())
                    {
                        _db.RemoveThreadAt(p_fid, i);
                        return 0;
                        //return "your thread was removed from the forum";
                    }
                    else
                    {
                        return -7;
                        //return "the topic you where trying to remove was submited by a diffrent user";
                    }
                }

            }
            //return "the topic could not been found"; ;
            return -6;
        }


        /*Posts founctions  */        
        public int addPost(int p_tid, int p_fid, int parentId, string p_topic, string p_content,string p_uname)
        {
            //MemberInfo t_user = FindMemberByUser(p_uname);            
            if (!_db.isMember("username = '" + p_uname + "'")){
                return -3;
                //return "incurrect user name";

            }
            //ForumThread t_thr = _db.getTread(p_fid, p_tid);

            if (!_db.isThread("(fid = '" + p_fid + "') and (tid = '"+p_tid+"')")){
                return -6;
                //return "the topic could not been found";
            }
            Console.WriteLine("AddPost: "+_db.addPost(p_tid, p_fid, parentId, p_topic, p_content, p_uname));
            //int index = t_thr.addPost(p_fid, p_tid, p_topic, p_content, t_user);
            return 0;
        }
        //(t_fid, t_tid, t_postIndex,t_uname);
        public ForumPost getPost(int p_fid, int p_tid, int p_index, string p_uname)
        {           
            if (!_db.isMember("username = '" + p_uname + "'"))
            {
                return null;
                //return "incurrect user name";
            }
            //ForumThread t_thr = _db.getTread(p_fid, p_tid);
            if (!_db.isThread("(fid = '" + p_fid + "') and (tid = '"+p_tid+"')"))
            {
                return null;
                //return "the topic could not been found";
            }
            return _db.getPost(p_fid, p_tid, p_index, p_uname);

        }
        //t_fid, t_tid, t_postIndex,t_uname
        public int removePost(int p_fid, int p_tid, int p_index, string p_uname)
        {
            //MemberInfo t_user = _db.FindMemberByID(p_userID);
            if (!_db.isMember("username = '" + p_uname + "'"))
            {
                return -3;
                //return "incurrect user name";
            }
                
            //ForumThread t_thr = _db.getTread(p_fId, p_tId);
            if (!_db.isThread("(fid = '" + p_fid + "') and (tid = '"+p_tid+"')"))
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
    }
}
