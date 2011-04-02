using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MessagePack
{
    public class ForumContentMessage : Message {

        public int _fId;
        public int _tId;               
        List<string> _topics;
        public ForumContentMessage(int fId, int tId, string uName, List<string> topics)
            : base(uName) {
            _fId = fId;
            _tId = tId;            
            _topics = topics;        
        }
        public override string ToString()
        {
            string ans =  "FORUMCONTENT\n" + _fId + "\n" + _tId + "\n" + _uName + "\n";
            for (int i = 0; i < _topics.Count;i++ ) {
                ans += _topics.ElementAt(i) + "\n";
            }

            return ans;

        }

        public override string getMessageType() {
            return "THREADCONTENT";
        }
    }
}
