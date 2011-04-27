using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MessagePack;

namespace MessagePack
{
    public class PopUpContent : Message
    {
        private string _msg;

        public PopUpContent(string uname, string forumName, string topic)
            : base(uname) {            
            _msg = "User "+uname+" added new topic \""+topic+"\" to "+forumName;
        }

        public override string ToString()
        {
            return "PopUpContent/$" + _uName + "/$" + _msg + "/$";
        }

        public override string getMessageType() {
            return "POPUPCONTENT";
        }

        public string getMsg() {
            return this._msg;
        }
    }
}
