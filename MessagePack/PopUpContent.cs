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
        private string t_uname;
        private string forumName;
        private string threadName;
        private string t_topic;

        public PopUpContent(string p_uname, string friend,string p_forumName, string p_topic)
            : base(friend) {
                _msg = "User " + p_uname + " added new topic \"" + p_topic + "\" to forum: \"" + p_forumName+"\"";
        }

        public PopUpContent(string p_uname, string friend,string p_forumName, string p_threadName, string p_topic)
            : base(friend) {
                _msg = "User " + p_uname + " added new post \"" + p_topic + "\" to thread: \"" + p_topic + "\" in forum: \"" + p_forumName + "\"";
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
