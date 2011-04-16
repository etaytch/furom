using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MessagePack
{
    public class AddPostMessage : Message {

        public int _fId;
        public int _tId;        
        public int _pIndex;    
        public string _subject;
        public string _post;
        public int _parentInd;


        public AddPostMessage(int fId, int tId, int pIndex, int parentInd, string uName, string subject, string post): base(uName) {
            _fId = fId;
            _tId = tId;
            _pIndex = pIndex;            
            _subject = subject;
            _post = post;
            _parentInd = parentInd;
        }
        public override string ToString()
        {
            return "ADDPOST\n" + _fId + "\n" + _tId + "\n" + _pIndex + "\n" + _parentInd + "\n" + _uName + "\n" + _subject + "\n" + _post + "\n";
        }

        public override string getMessageType() {
            return "ADDPOST";
        }
    }
}
