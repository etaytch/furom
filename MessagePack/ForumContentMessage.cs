using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MessagePack
{
    public class ForumContentMessage : Message {

        public int _fId;
        public List<Quartet> _topics=new List<Quartet>();
        public ForumContentMessage(int fId, string uName, List<Quartet> topics)
            : base(uName) {
            _fId = fId;        
            _topics = topics;        
        }
        public override string ToString()
        {
            string ans =  "FORUMCONTENT\n" + _fId + "\n"  + _uName + "\n";
            for (int i = 0; i < _topics.Count;i++ ) {
                ans += _topics.ElementAt(i) + "\n";
            }

            return ans;

        }

        public override string getMessageType() {
            return "FORUMCONTENT";
        }
    }
}
