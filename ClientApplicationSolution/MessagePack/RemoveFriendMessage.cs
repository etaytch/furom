﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MessagePack
{
    public class RemoveFriendMessage : Message{
        public string _friend;

        public RemoveFriendMessage(string uName, string friend) : base(uName) {            
            _friend = friend;
        }

        public override string ToString() {
            return "REMOVEFRIEND/$"+_uName+"/$"+_friend+"/$";
        }

        public override string getMessageType() {
            return "REMOVEFRIEND";
        }
    }
}
