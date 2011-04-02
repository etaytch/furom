/*
  using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ForumSever;
using MessagePack;

namespace ForumServer
{
    class LogicManager
    {
        Database _db;

        public LogicManager()
        {
            _db = new Database();
        }


        public int addMember(MemberInfo memb)
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
        
        public MemberInfo FindMemberByUser(string username)
        {
           return _db.FindMemberByUser(username);
        }
        

        public int login(string p_user, string p_pass)
        {
            MemberInfo t_user = _db.FindMemberByUser(p_user);
            if ((t_user != null) && (t_user.getPass() == p_pass))
            {
                t_user.login();         
                   return 0;
            }

            return -1;
        }

        public bool isLogged(string user)
        {
            MemberInfo memb= _db.FindMemberByUser(user);
            return ((memb!=null)&& (memb.isLogged()));
        }

        public int logout(string user)
        {
            MemberInfo memb = _db.FindMemberByUser(user);
            if ((memb != null)&&(memb.isLogged()))
            {
                memb.logout();
                return 0;
            }

            return -1;
        }

        public int addMeAsFriend(string p_toBeAdded, string p_AddedTo)
        {
            MemberInfo t_AddedTo = _db.FindMemberByUser(p_AddedTo);
            MemberInfo t_toBeAdded = _db.FindMemberByUser(p_toBeAdded);
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
 
            if (t_AddedTo.hasFriend(p_toBeAdded))
            {
                //return "user is already a friend with this user";
                return -1;
            }
            t_AddedTo.addFriend(t_toBeAdded);
            //return "you have a new friend";
            return t_toBeAdded.getID();
        }

        public object removeMeAsFriend(string p_toBeRemoved, string p_removedFrom)
        {
            MemberInfo t_removedFrom = _db.FindMemberByUser(p_removedFrom);
            MemberInfo t_toBeRemoved = _db.FindMemberByUser(p_toBeRemoved);
            if (t_toBeRemoved == null)
            {
                return "incurrect user name";
            }

            if (t_removedFrom == null)
            {
                return "the user you are trying to befriend dosn't exist";
            }



            if (!t_removedFrom.hasFriend(p_toBeRemoved))
            {
                return "the user you are trying to unfriend is not a friend of yours";
            }
            t_removedFrom.removeFriend(t_toBeRemoved);
            return "you have removed yourself as friend";
        }

        //Threads founctions  


        public string addTread(string p_user, string p_topic, string p_content)
        {
            MemberInfo t_user = _db.FindMemberByUser(p_user);
            if (t_user == null)
            {
                return "incurrect user name";
                
            }
            if (_db.findTopicInThreads(p_topic))
            {
                return "topic already exists, choose new topic";
            }

            ForumThread t_thr = new ForumThread(p_topic, p_content, t_user);
            //TODO!!
            _db.addTread(p_topic, p_content, p_user);
            return "your thread was added to the forum";
        }
        
        private bool findTopicInThreads(string p_topic)
        {
            bool b=false;
            int i = 0;
            while (!b& i<_db._Treads.Count())
            {
                b=(p_topic==_Treads.ElementAt(i).getTopic());
                i++;
            }
            return b;
        }
        


        public ForumThread getTread(string p_topic)
        {
            for (int i = 0; i < _db.MemberCount(); i++)
            {
                if (_db.getTreadFrom(i).getTopic() == p_topic)
                {
                    return _db.getTreadFrom(i);
                }
               
            }
            return null;
        }

        public string removeTread(string p_user, string p_topic)
        {
            MemberInfo t_user = _db.FindMemberByUser(p_user);
            if (t_user == null)
            {
                return "incurrect user name";
            }
            for (int i = 0; i < _db.MemberCount(); i++)
            {
                if (_db.getTreadFrom(i).getTopic() == p_topic)
                {
                    if (_db.getTreadFrom(i).getAuthor() == t_user)
                    {
                        _db.RemoveThreadAt(i);
                        return "your thread was removed from the forum";
                    }
                    else{
                        return "the topic you where trying to remove was submited by a diffrent user";
                    }
                }

            }
            return "the topic could not been found"; ;
        }
    }
}

*/