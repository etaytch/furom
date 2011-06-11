using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MessagePack
{
    public class UsersContentMessage : Message {

        public List<string> _users;
        public UsersContentMessage(string uName, List<string> users)
            : base(uName) {
                _users = users;
        }
        public override string ToString() {
            string ans = "USERSCONTENT/$" + _uName + "/$";
            for (int i = 0; i < _users.Count;i++ ) {
                ans += _users.ElementAt(i) + "/$";
            }

            return ans;

        }

        public override string getMessageType() {
            return "USERSCONTENT";
        }
    }
}
