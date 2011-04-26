using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MessagePack
{
    public class LogoutMessage : Message {
        public LogoutMessage(string uName) : base(uName) {                        
        }
        public override string ToString()
        {
            return "LOGOUT/$" + _uName + "/$";
        }

        public override string getMessageType() {
            return "LOGOUT";
        }
    }
}
