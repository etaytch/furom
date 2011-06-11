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
        string _sex;
        string _country;
        string _city;
        string _eMail;
        string _birthday;
        bool _logged;
        IList<MemberInfo> _friends;

        public MemberInfo(/*int p_id, */string p_uName, string p_fName, string p_lName, string p_pass, string p_sex, string p_country, string p_city, string p_eMail, string p_birthday,string logged) {
            //this._ID = p_id;
            this._ID =0;
            this._fName = p_fName;
            this._lName = p_lName;
            this._uName = p_uName;
            this._pass  = p_pass;
            this._sex = p_sex;
            this._country = p_country;
            this._city = p_city;
            this._eMail = p_eMail;
            this._birthday = p_birthday;
            if(logged.Equals("0")){
                this._logged = false;
            }
            else {
                this._logged = true;
            }
            
            this._friends = new List<MemberInfo>();
        }
          
        public MemberInfo(string p_uName, string p_fName, string p_lName, string p_pass, string p_eMail)
        {
            //this._ID = -1;
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
            this._sex = p_other._sex;
            this._country = p_other._country;
            this._city = p_other._city;
            this._eMail = p_other._eMail;
            this._birthday = p_other._birthday;
            this._friends = p_other.getFriends();
        }

        public void setpass(string pass){
            _pass = pass;
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


        public string getSex() {
            return _sex;
        }

        public string getCountry() {
            return _country;
        }

        public string getCity() {
            return _city;
        }

        public string getBirthday() {
            return _birthday;
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

        public override string ToString() {
            return "----------------------\n"
                +"Username: "+_uName+ "\n"
                +"First Name: "+_fName+ "\n"
                +"Last Name: "+_lName+ "\n"
                +"Pass: "+_pass+ "\n"
                +"Sex: "+_sex+ "\n"
                +"Country: "+_country+ "\n"
                +"City: "+_city+ "\n"
                +"Email: "+_eMail+ "\n"
                +"Birthday: "+_birthday+ "\n"
                + "Logged: " + _logged + "\n"
                + "----------------------";        
        }
    }
}
