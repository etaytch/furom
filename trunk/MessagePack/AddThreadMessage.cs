using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MessagePack
{
    public class AddThreadMessage : Message {

        public int _fId;
        public string _subject;
        public string _post;

        public AddThreadMessage(int fId, string uName, string subject, string post) : base(uName){
            _fId = fId;
            _subject = subject;
            _post = post;
        }

        public override string ToString()
        {
            return "ADDTHREAD\n" + _fId + "\n" + _uName + "\n" + _subject + "\n" + _post + "\n";
        }

        public override string getMessageType() {
            return "ADDTHREAD";
        }
    }
}
