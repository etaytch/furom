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
        public string _author;
        public string _body;
        public List<Quartet> _posts;
        public ThreadContentMessage(int fId, int tId, string uName, string author,string subject,string body,List<Quartet> posts)
            : base(uName) {
            _fId = fId;
            _tId = tId;
            _author = author;
            _subject = subject;
            _body = body;
            _posts = posts;        

        }
        public override string ToString() {
            string ans = "THREADCONTENT/$" + _fId + "/$" + _tId + "/$" + _uName + "/$" + _author + "/$" + _subject + "/$" + _body + "/$";
            for (int i = 0; i < _posts.Count;i++ ) {
                ans += _posts.ElementAt(i) + "/$";
            }

            return ans;

        }

        public override string getMessageType() {
            return "THREADCONTENT";
        }
    }
}
