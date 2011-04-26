using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace MessagePack
{

    // I KNOW.. STUPID NAME FOR A CLASS.. (ETAY)

    public class GetSystemMessage : Message {
       
        public GetSystemMessage(string uName)
            : base(uName) {            
        }
        public override  string ToString()
        {
            return "GETSYSTEM/$" + _uName + "/$";
        }

        public override string getMessageType() {
            return "GETSYSTEM";
        }
    }
}
