using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MessagePack
{
    public class SystemContentMessage : Message {


        public List<Quartet> _forums;


        public SystemContentMessage(string uName, List<Quartet> forums)
            : base(uName) 
        {            
            _forums = forums;
        }
        public override string ToString()
        {
            string ans =  "SYSTEMCONTENT/$" + _uName + "/$";
            for (int i = 0; i < _forums.Count;i++ ) {
                ans += _forums.ElementAt(i) + "/$";
            }

            return ans;

        }

        public override string getMessageType() {
            return "SYSTEMCONTENT";
        }
    }
}
