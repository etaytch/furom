﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MessagePack
{
    public class SystemContentMessage : Message {

           
        public List<string> _forums;


        public SystemContentMessage(string uName, List<string> forums)
            : base(uName) 
        {            
            _forums = forums;
        }
        public override string ToString()
        {
            string ans =  "SYSTEMCONTENT\n" + _uName + "\n";
            for (int i = 0; i < _forums.Count;i++ ) {
                ans += _forums.ElementAt(i) + "\n";
            }

            return ans;

        }

        public override string getMessageType() {
            return "SYSTEMCONTENT";
        }
    }
}