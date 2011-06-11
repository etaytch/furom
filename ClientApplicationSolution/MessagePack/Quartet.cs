using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MessagePack {
     public class Quartet {
        public int _pIndex;
        public int _parent;
        public string _subject;
        public string _author;

        public Quartet(int pIndex, int parent, string subject, string author) {
            _pIndex = pIndex;
            _parent = parent;
            _subject = subject;
            _author = author;
        }

        public override string ToString() {
            return _pIndex + "/$" + _parent + "/$" + _subject + "/$" + _author;
        }

    }
}
