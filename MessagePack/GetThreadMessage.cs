using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MessagePack
{
    public class GetThreadMessage : GetPostMessage {

        public GetThreadMessage(int fId, int tId,string uName)
            : base(fId, tId, 0, uName) {       
        }

        public override string ToString()
        {
            return "GETTHREAD\n" + _fId + "\n" + _tId + "\n" + _pIndex + "\n" + _uName + "\n";
        }

        public override string getMessageType() {
            return "GETTHREAD";
        }
    }
}
