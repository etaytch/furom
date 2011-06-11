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

        public PopUpContent(string friend, string msg)
            : base(friend)
        {
            _msg = msg;
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
