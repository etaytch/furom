using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Protocol {
    class EandETokenizer {
        private string _str;
        private string _delimiter;

        public EandETokenizer(string str, string del) {
            _str = str;
            _delimiter = del;
        }

        public string getNextToken() {
            int ind = _str.IndexOf(_delimiter);
            if (ind < 0) {
                return "\0";
            }
            else {
                string ans = _str.Substring(0, ind);                
                _str = _str.Substring(ind + 1);
                return ans;
            }
        }
    }
}
