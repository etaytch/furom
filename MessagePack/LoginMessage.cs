using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MessagePack
{
     public class LoginMessage : Message{
        public string _password;

        public LoginMessage(string uName, string password) : base(uName){                        
            _password = password;
        }

        public override string ToString()
        {
            string ans = "LOGIN/$"+_uName+"/$"+_password+"/$";
            return ans;
        }

        public override string getMessageType() {
            return "LOGIN";
        }
    }
}
