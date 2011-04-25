using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MessagePack
{
    public class ThreadContentMessage : Message {

        public int _fId;
        public int _tId;
        public string _subject;
        public string _body;
        public List<Quartet> _posts;
        public ThreadContentMessage(int fId, int tId, string uName, string subject,string body,List<Quartet> posts)
            : base(uName) {
            _fId = fId;
            _tId = tId;
            _subject = subject;
            _body = body;
            _posts = posts;        
        }
        public override string ToString() {
            string ans = "THREADCONTENT\n" + _fId + "\n" + _tId + "\n" + _uName + "\n" + _subject + "\n" + _body + "\n";
            for (int i = 0; i < _posts.Count;i++ ) {
                ans += _posts.ElementAt(i) + "\n";
            }

            return ans;

        }

        public override string getMessageType() {
            return "THREADCONTENT";
        }
    }
}
