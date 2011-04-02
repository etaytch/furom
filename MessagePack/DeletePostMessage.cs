using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MessagePack
{
    public class DeletePostMessage: Message {
        public int _fId;
        public int _tId;
        public int _pIndex;

        public DeletePostMessage(int fId, int tId, int pIndex, string uName) : base(uName) {
            _fId = fId;
            _tId = tId;
            _pIndex = pIndex;           
        }
        public override string ToString()
        {
            return "DELETEPOST\n" + _fId + "\n" + _tId + "\n" + _pIndex + "\n" + _uName + "\n";
        }

        public override string getMessageType() {
            return "DELETEPOST";
        }
    }
}
