using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ForumSever
{
    public class ForumPost 
    {
        public static int POST_ID_COUNTER = 0;
        internal int _fid;
        internal int _tid;
        internal int _pindex;
        internal int _ID;
        internal string _topic;
        internal string _content;
        internal MemberInfo _autor;
        public ForumPost(int fid, int tid, string p_topic, string p_content, MemberInfo p_memb)
        {
            _fid = fid;
            _tid = tid;
            _ID = POST_ID_COUNTER;
            _topic = p_topic;
            _content = p_content;
            _autor = p_memb;
            POST_ID_COUNTER++;
        }


        public void setPostID(int p_pindex) {
            this._pindex = p_pindex;
        }

        internal string getTopic()
        {
            return _topic;
        }

        public int getForumID() {
            return _fid;
        }

        public int getThreadID() {
            return _tid;
        }

        public int getID()
        {
            return _ID;
        }

        internal MemberInfo getAuthor()
        {
            return _autor;
        }
    }
}
