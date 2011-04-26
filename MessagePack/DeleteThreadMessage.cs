using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MessagePack
{
    public class DeleteThreadMessage : DeletePostMessage {

        public DeleteThreadMessage(int fId, int tId, string uName)
            : base(fId, tId, 0, uName) {            
        }
        public override string ToString()
        {
            return "DELETETHREAD/$" + _fId + "/$" + _tId + "/$" + _pIndex + "/$" + _uName + "/$";
        }

        public override string getMessageType() {
            return "DELETETHREAD";
        }
    }
}
