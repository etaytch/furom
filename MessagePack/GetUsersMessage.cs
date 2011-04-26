using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MessagePack
{
    public class GetUsersMessage : Message{        
        public GetUsersMessage(string uName) : base(uName) {                        
        }

        public override string ToString()
        {
            return "GETUSERS\n" + _uName + "\n";
        }

        public override string getMessageType() {
            return "GETUSERS";
        }
    }
}
