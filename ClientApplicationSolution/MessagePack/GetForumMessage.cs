using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MessagePack
{
    public class GetForumMessage : Message {

        public int _fId;

        public GetForumMessage(int fId,string uName)  : base(uName) {
            _fId = fId;
        }
        public override string ToString()
        {
            return "GETFORUM/$" + _fId + "/$" + _uName + "/$";
        }

        public override string getMessageType() {
            return "GETFORUM";
        }
    }
}
