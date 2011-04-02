
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ForumSever
{
    public class Forum
    {
        //public static int THREAD_ID_COUNTER = 0;        
        private List<ForumThread> _threads;
        private int _fid;
        private string _topic;
        int _thread_counter;

        public Forum(string p_topic) {
            _fid = 0;
            _topic = p_topic;
            _threads = new List<ForumThread>();
            
            
            //_ID = THREAD_ID_COUNTER;            
            //THREAD_ID_COUNTER++;
            
            _thread_counter = 0;
        }


        public int getId() {
            return this._fid;
        }

        public string getTopic() {
            return this._topic;
        }

        public void setId(int p_fid) {
            this._fid = p_fid;
        }


        public int addTread(ForumThread p_tread) {
            _thread_counter++;
            _threads.Add(p_tread);            
            return _threads.IndexOf(p_tread);
        }

        public List<ForumThread> getThreads() {
            return this._threads;
        }

        public List<String> getTheardsTopics() {
            List<String> ans = new List<String>();
            for (int i = 0; i < this._threads.Count;i++ ) {
                ans.Add(this._threads.ElementAt(i)._topic);
            }
            return ans;
        }

        internal int getThreadCount()
        {
            return _thread_counter;
        }

    }
}
