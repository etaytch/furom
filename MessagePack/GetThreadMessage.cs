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
            return "GETTHREAD\n" + _fId + "\n" + _tId + "\n" + _uName + "\n";
        }

        public override string getMessageType() {
            return "GETTHREAD";
        }
    }
}
