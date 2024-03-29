﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ForumSever
{
    public class ForumThread : ForumPost
    {

        private List<ForumPost> _posts;        
        int _post_counter;
        public ForumThread(int fid,string p_topic, string p_content, string p_author)
            : base(fid, 0,0, p_topic, p_content, p_author)
        {            
        
            _posts = new List<ForumPost>();
            _post_counter = 0;
        }

        public override int  getForumID() {
            return _fid;
        }
 
        public List<ForumPost> getPosts() {
            return this._posts;
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
