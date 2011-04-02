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
            return "DELETETHREAD\n" + _fId + "\n" + _tId + "\n" + _pIndex + "\n" + _uName + "\n";
        }

        public override string getMessageType() {
            return "DELETETHREAD";
        }
    }
}
