using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
                return -1;
            }
            // user name already in use
            if (_db.FindMemberByUser(memb.getUName()) != null)
            {
                //massage = "user name in use. choose  a diffrent one";
                //return false;
                return -2;
            }
            if (_db.FindMemberByEmail(memb.getEmail()) != null)
            {
                //massage = "mail in use. choose  a diffrent one";
                //return false;
                return -3;
            }
            _db.addMember(memb);
            return 0;
        }

        public int login(string p_user, string p_pass)
        {
            //Console.WriteLine("in login!");
            MemberInfo t_user = _db.FindMemberByUser(p_user);
            if ((t_user != null) && (t_user.getPass() == p_pass))
            {
                t_user.login();
                return 0;
            }

            return -3;
        }

        public int logout(string user)
        {
            MemberInfo memb = _db.FindMemberByUser(user);
            if ((memb != null) && (memb.isLogged()))
            {
                memb.logout();
                return 0;
            }

            return -1;
        }

        public bool isLogged(string user)
        {
            MemberInfo memb = _db.FindMemberByUser(user);
            return ((memb != null) && (memb.isLogged()));
        }

        public MemberInfo FindMemberByUser(string username)
        {
            return _db.FindMemberByUser(username);
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

        public int addTread(int p_fid, int p_userID, string p_topic, string p_content)
        {
            MemberInfo t_user = _db.FindMemberByID(p_userID);
            if (t_user == null)
            {
                return -3;
                //return "incurrect user name";

            }
            if (_db.findTopicInThreads(p_fid, p_topic))
            {
                //return "topic already exists, choose new topic";
                return -5;
            }

            ForumThread t_thr = new ForumThread(p_fid, p_topic, p_content, t_user);
            int t_ID = _db.addTread(t_thr);

            t_thr.setID(t_ID); // threadID wasn't initialized until now!! (Etay)

            //return "your thread was added to the forum";
            return t_ID;
        }

        public ForumThread getTread(int p_fid, int p_tid)
        {
            /*             
              OLD NIV' CODE:                                        
             
              for (int i = 0; i < _db.MemberCount(); i++){
                  if (_db.getTreadFrom(i).getID() == p_tid){
                      return _db.getTreadFrom(i);
                  }

              }
              return null;
           */

            //Etay:
            return _db.getTread(p_fid, p_tid);
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
                    if (tThread.getAuthor() == t_user)
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

        public int addPost(int p_fid, int p_userID, int p_tid, string p_topic, string p_content)
        {
            MemberInfo t_user = _db.FindMemberByID(p_userID);
            if (t_user == null)
            {
                return -3;
                //return "incurrect user name";

            }
            ForumThread t_thr = _db.getTread(p_fid, p_tid);
            if (t_thr == null)
            {
                return -6;
                //return "the topic could not been found";
            }

            int index = t_thr.addPost(p_fid, p_tid, p_topic, p_content, t_user);
            return index;
        }

        public ForumPost getPost(int p_fid, int p_userID, int p_tid, int p_index)
        {
            MemberInfo t_user = _db.FindMemberByID(p_userID);
            if (t_user == null)
            {
                return null;
                //return "incurrect user name";

            }
            ForumThread t_thr = _db.getTread(p_fid, p_tid);
            if (t_thr == null)
            {
                return null;
                //return "the topic could not been found";
            }
            return t_thr.getPostAt(p_index);

        }

        public int removePost(int p_fId, int p_userID, int p_tId, int p_index)
        {
            MemberInfo t_user = _db.FindMemberByID(p_userID);
            if (t_user == null)
            {
                return -3;
                //return "incurrect user name";
            }
            ForumThread t_thr = _db.getTread(p_fId, p_tId);
            if (t_thr == null)
            {
                return -6;
                //return "the topic could not been found";
            }
            if ((p_index < 0) | (p_index > t_thr.getPostCount()))
            {
                return -8;
                //return "the post topic is out of bounds";
            }
            ForumPost t_post = t_thr.getPostAt(p_index);
            if (t_post.getAuthor() == t_user)
            {
                t_thr.RemovePostAt(p_index);
                for (int j = p_index; j < t_thr.getPosts().Count(); j++) {
                    t_thr.getPosts().ElementAt(j).setPostID(j);
                }
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
}
