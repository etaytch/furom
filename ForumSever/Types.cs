using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ForumSever
{
    public class MemberInfo
    {
        int _ID;
        string _uName;
        string _fName;
        string _lName;
        string _pass;
        string _eMail;
        bool _logged;
        IList<MemberInfo> _friends;

        public MemberInfo(int p_id,string p_uName,string p_fName,string p_lName,string p_pass,string p_eMail){
            this._ID = p_id;
            this._fName = p_fName;
            this._lName = p_lName;
            this._uName = p_uName;
            this._pass = p_pass;
            this._eMail = p_eMail;
            this._logged = false;
            this._friends = new List<MemberInfo>();
        }

        public MemberInfo(string p_uName, string p_fName, string p_lName, string p_pass, string p_eMail)
        {
            this._ID = -1;
            this._fName = p_fName;
            this._lName = p_lName;
            this._uName = p_uName;
            this._pass = p_pass;
            this._eMail = p_eMail;
            this._friends = new List<MemberInfo>();
        }

        public MemberInfo(MemberInfo p_other)
        {
            this._ID = p_other._ID;
            this._fName = p_other._fName;
            this._lName = p_other._lName;
            this._uName = p_other._uName;
            this._pass = p_other._pass;
            this._eMail = p_other._eMail;
            this._friends = p_other.getFriends();
        }

        public MemberInfo()
        {
        }

        public string getLName()
        {
            return _lName;
        }

        public int getID()
        {
            return _ID;
        }

        public void setID(int p_ID)
        {
            _ID=p_ID;
        }

        public string getFName()
        {
            return _fName;
        }

        public string getUName()
        {
            return _uName;
        }

        public string getPass()
        {
            return _pass;
        }

        public string getEmail()
        {
            return _eMail;
        }

        public IList<MemberInfo> getFriends()
        {
            IList<MemberInfo> t_tmp= new List<MemberInfo>();
            foreach (MemberInfo memb in _friends)
            {
                t_tmp.Add(new MemberInfo(memb));
            }
            return t_tmp;
        }

        public bool isLogged()
        {
            return _logged;
        }

        public void login()
        {
            _logged= true;
        }

        public void logout()
        {
            _logged = false;
        }



        public bool hasFriend(int p_toBeAdded)
        {
            foreach (MemberInfo memb in _friends)
            {
                if (memb.getID() == p_toBeAdded)
                {
                    return true;
                }
            }
            return false;
        }

        internal void addFriend(MemberInfo t_toBeAdded)
        {
            _friends.Add(t_toBeAdded);
        }

        internal void removeFriend(MemberInfo t_toBeRemoved)
        {
            _friends.Remove(t_toBeRemoved);
        }
    }
}
