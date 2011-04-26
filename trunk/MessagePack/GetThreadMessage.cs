using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MessagePack
{
    public class GetThreadMessage : Message {

        public int _tId;
        public int _fId;

        public GetThreadMessage(int fId, int tId,string uName)
            : base(uName) {       
            _fId = fId;
            _tId = tId;
        }

        public override string ToString()
        {
            return "GETTHREAD/$" + _fId + "/$" + _tId + "/$" + _uName + "/$";
        }

        public override string getMessageType() {
            return "GETTHREAD";
        }
    }
}
