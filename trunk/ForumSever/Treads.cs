using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ForumSever
{
    public class ForumThread : ForumPost
    {
        public static int THREAD_ID_COUNTER = 0;
        /*
        private int _ID;
        private string _topic;
        private string _content;
        private MemberInfo _autor;
         */
        private List<ForumPost> _posts;        
        int _post_counter;
        public ForumThread(int fid,string p_topic, string p_content, MemberInfo p_memb)
            : base(fid,0,p_topic, p_content, p_memb)
        {            
            _ID = THREAD_ID_COUNTER;
            /*
            _topic = p_topic;
            _content = p_content;
            _autor = p_memb;
             */
            THREAD_ID_COUNTER++;
            _posts = new List<ForumPost>();
            _post_counter = 0;
        }

        /*
        internal string getTopic()
        {
            return _topic;
        }

        public int getID()
        {
            return _ID;
        }

        internal MemberInfo getAuthor()
        {
            return _autor;
        }
         */

        public int getForumID() {
            return _fid;
        }

        internal int addPost(int p_fid,int p_tid,string p_topic, string p_content,MemberInfo p_user)
        {
            ForumPost t_post= new ForumPost(p_fid,p_tid,p_topic,p_content,p_user);
            t_post.setPostID(_post_counter);
            _posts.Add(t_post);
            _post_counter++;
            return _post_counter - 1;
        }

        public List<String> getTheardsTopics() {
            List<String> ans = new List<String>();
            for (int i = 0; i < this._posts.Count;i++ ) {
                ans.Add(this._posts.ElementAt(i)._topic);
            }
            return ans;
        }

        internal int getPostCount()
        {
            return _post_counter;
        }

        internal ForumPost getPostAt(int p_index)
        {
            if ((p_index >= 0) & (p_index < _post_counter))
            {
                return _posts.ElementAt(p_index);
            }
            return null;
        }

        internal void RemovePostAt(int p_index)
        {
            if ((p_index >= 0) & (p_index < _post_counter))
            {
                _posts.RemoveAt(p_index);
            }
            
        }

        public void setID(int p_tid) {
            _tid = p_tid;
        }
    }
}
