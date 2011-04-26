using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MessagePack
{
    public class FriendsContentMessage : Message {

        public List<string> _friends;
        public FriendsContentMessage(string uName, List<string> friends)
            : base(uName) {
                _friends = friends;
        }
        public override string ToString() {
            string ans = "FRIENDSCONTENT/$" + _uName;
            for (int i = 0; i < _friends.Count; i++) {
                ans += _friends.ElementAt(i) + "/$";
            }

            return ans;

        }

        public override string getMessageType() {
            return "FRIENDSCONTENT";
        }
    }
}
