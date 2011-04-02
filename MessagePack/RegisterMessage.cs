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
            return "REGISTER\n" + _fName + "\n" + _lName + "\n" + _uName + "\n" + _password + "\n" + _confirmedPassword + "\n"
                + _sex + "\n" + _email + "\n" + _birthday + "\n" + _country + "\n" + _city + "\n";
        }

        public override string getMessageType() {
            return "REGISTER";
        }
    }
}
