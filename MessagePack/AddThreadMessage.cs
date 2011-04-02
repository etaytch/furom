using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MessagePack
{
    public class AddThreadMessage : AddPostMessage {

        public AddThreadMessage(int fId, string uName, string subject, string post) : base(fId,0,0,uName,subject,post){            
        }

        public override string ToString()
        {
            return "ADDTHREAD\n" + _fId + "\n" + _pIndex + "\n" + _uName + "\n" + _subject + "\n" + _post + "\n";
        }

        public override string getMessageType() {
            return "ADDTHREAD";
        }
    }
}
