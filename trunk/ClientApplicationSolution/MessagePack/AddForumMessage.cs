using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MessagePack
{
    public class AddForumMessage : Message {
        
        public string _topic;

        public AddForumMessage(string uName, string topic) : base(uName) {            
        }

        public override string ToString()
        {
            return "ADDFORUM/$" + _uName + "/$" + _topic + "/$";
        }

        public override string getMessageType() {
            return "ADDFORUM";
        }
    }
}
