﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MessagePack
{
    public class AddFriendMessage : Message{
        public string _friend;

        public AddFriendMessage(string uName, string friend)  : base(uName){            
            _friend = friend;
        }

        public override string ToString()
        {
            return "ADDFRIEND/$" + _uName + "/$" + _friend + "/$";
        }

        public override string getMessageType() {
            return "ADDFRIEND";
        }
    }
}
