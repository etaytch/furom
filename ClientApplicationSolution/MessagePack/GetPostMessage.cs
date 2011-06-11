using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MessagePack
{
    public class GetPostMessage : Message {

        public int _tId;
        public int _fId;
        public int _pIndex;

        public GetPostMessage(int fId, int tId, int pIndex, string uName) : base(uName) {
            _fId = fId;
            _tId = tId;
            _pIndex = pIndex;
            
        }
        public override string ToString()
        {
            return "GETPOST/$" + _fId + "/$" + _tId + "/$" + _pIndex + "/$" + _uName + "/$";
        }

        public override string getMessageType() {
            return "GETPOST";
        }
    }
}
