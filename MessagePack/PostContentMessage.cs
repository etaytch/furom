using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MessagePack
{
    public class PostContentMessage : Message {

        public int _fId;
        public int _tId;        
        public int _pIndex;
        public int _parentInd;
        public string _author;
        public string _subject;
        public string _body;               
        public PostContentMessage(int fId, int tId, int pIndex, int parentInd, string uName, string author, string subject, string body)
            : base(uName) {
            _fId = fId;
            _tId = tId;
            _pIndex = pIndex;
            _parentInd = parentInd;
            _author = author;
            _subject = subject;
            _body = body;            
        }
        public override string ToString()
        {
            return "POSTCONTENT\n" + _fId + "\n" + _tId + "\n" + _pIndex + "\n" + _parentInd + "\n" + _uName + "\n" + _author + "\n" + _subject + "\n" + _body + "\n";
        }

        public override string getMessageType()
        {
            return "POSTCONTENT";
        }
    }
}
