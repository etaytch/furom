using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MessagePack
{
    public class RegisterMessage : Message {
        public string _fName;
        public string _lName;
        public string _password;
        public string _confirmedPassword;
        public string _sex;
        public string _country;
        public string _city;
        public string _email;
        public string _birthday;        
        public RegisterMessage(string fName, string lName, string uName, string password, string confirmedPassword, 
                            string sex, string email, string birthday, string country, string city) : base(uName) {
            _fName = fName;
            _lName = lName;            
            _password = password;
            _confirmedPassword = confirmedPassword;
            _sex = sex;
            _email = email;
            _birthday = birthday;
            _country = country;
            _city = city;            
        }

        public override string ToString() {
            return "REGISTER/$" + _fName + "/$" + _lName + "/$" + _uName + "/$" + _password + "/$" + _confirmedPassword + "/$"
                + _sex + "/$" + _email + "/$" + _birthday + "/$" + _country + "/$" + _city + "/$";
        }

        public override string getMessageType() {
            return "REGISTER";
        }
    }
}
