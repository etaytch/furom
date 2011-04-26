using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MessagePack
{
    public class GetFriendsMessage : Message {        

        public GetFriendsMessage(string uName) : base(uName) {                        
        }

        public override string ToString()
        {
            return "GETFRIENDS\n" + _uName + "\n";
        }

        public override string getMessageType() {
            return "GETFRIENDS";
        }
    }
}
