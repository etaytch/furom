using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MessagePack;

namespace MessagePack
{
    public class Error : Message
    {
        private string _msg;

        public Error(string uname,string msg): base(uname)
        {
            // TODO: Complete member initialization
            _msg = msg;
        }

        public override string ToString()
        {
            return "ERROR\n" + _uName + "\n" + _msg + "\n";
        }

        public override string getMessageType() {
            return "Error";
        }

        public string getMsg() {
            return this._msg;
        }
    }
}
