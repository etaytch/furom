using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MessagePack;

namespace MessagePack
{
    public class Acknowledgment : Message
    {
        private string _msg;

        public Acknowledgment(string uname, string msg) : base(uname){            
            _msg = msg;
        }

        public override string ToString()
        {
            return "Acknowledgment/$" + _uName + "/$" + _msg+ "/$";
        }

        public override string getMessageType() {
            return "Acknowledgment";
        }

        public string getMsg() {
            return this._msg;
        }
    }
}
