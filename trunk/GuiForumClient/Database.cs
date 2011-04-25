using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using GuiForumClient;
using System.ComponentModel;
using MessagePack;
namespace DataManagment
{
    public class Database
    {


        private List<ViewData> forums;
        private List<ViewData> threads;
        private List<ViewHirarchiData> posts;
        private PostObject currentPost;
        private int currentForumId;
        private int currentThreadId;


        public Database()
        {
            forums = new List<ViewData>();
            threads = new List<ViewData>();
            posts = new List<ViewHirarchiData>();
            currentPost = new PostObject("Welcom to the \"ALUFIM \" furom!","HaAlufim","Have Fun!",-1);	   
		    currentForumId = -1;
            currentThreadId = -1;
            initData();
        }

        private void initData()
        {
            ViewData t_vd;

            for (int i = 0; i < 10; i++)
            {
                t_vd = new ViewData("Forum" + i, i);
                forums.Add(t_vd);
            }
        }

        public List<ViewData> Forums
        {
            get { return forums; }
            set { forums = value; ForumsChanged(); }
        }


        public List<ViewData> Threads
        {
            get { return threads; }
            set { threads = value; ThreadsChanged(); }
        }

        public List<ViewHirarchiData> Posts
        {
            get { return posts; }
            set { posts = value; PostsChanged(); }
        }

        public PostObject CurrentPost
        {
            get { return currentPost; }
            set { currentPost = value; }
        }

 		public void setCurrent(string p_topic,string p_author,string p_content,int p_id)
        {
            this.CurrentPost = (new PostObject(p_topic, p_author, p_content,p_id));
        }
		
       internal void cleanThreads()
        {
            threads.Clear();
			this.Threads = threads;
            CurrentThreadId = -1;
        }
		
		
        internal void cleanPosts()
        {
            posts.Clear();
            this.Posts = posts; 
        }
        internal void cleanForums()
        {
            forums.Clear();
            this.Forums = forums;
        }
				
        public int CurrentForumId
        {
            get { return currentForumId; }
            set { currentForumId = value; }
        }

        public int CurrentThreadId
        {
            get { return currentThreadId; }
            set { currentThreadId = value; }
        }



 

        internal void addThread(string topic,int tid)
        {
            ViewData t_vd = new ViewData(topic, tid);
            this.threads.Add(t_vd);
            this.Threads = threads;
        }

        internal void addPost(string topic)
        {
            ViewHirarchiData t_vd = new ViewHirarchiData(topic, 0);
            this.posts.Add(t_vd);
            this.Posts = posts;
        }

        internal void addForum(Quartet topic)
        {
            ViewData t_vd = new ViewData(topic._subject, topic._pIndex);
            this.forums.Add(t_vd);
            this.Forums = forums;
        }


        /************************MVC*********************/
        //declerations
        public delegate void ForumsChangedHandler(object sender, ForumsChangedEventArgs e);
        public delegate void ThreadsChangedHandler(object sender, ThreadsChangedEventArgs e);
        public delegate void PostsChangedHandler(object sender, PostsChangedEventArgs e);

        //variables
        public event ForumsChangedHandler ForumsChangedEvent;
        public event ThreadsChangedHandler ThreadsChangedEvent;
        public event PostsChangedHandler PostsChangedEvent;


        protected void ForumsChanged()
        {
            ForumsChangedEvent(this, new ForumsChangedEventArgs(forums));
        }
        protected void ThreadsChanged()
        {
            ThreadsChangedEvent(this, new ThreadsChangedEventArgs(threads));
        }
        protected void PostsChanged()
        {
            PostsChangedEvent(this, new PostsChangedEventArgs(posts));
        }



        /************************MVC*********************/

    }




     public class ForumsChangedEventArgs  : EventArgs
    {
        private object _forums;


        public ForumsChangedEventArgs(object p_forums)
        {
            _forums  = p_forums;
        }
        
         public object Forums
        {
            get
            {
                return _forums;
            }
        }
    }


     public class ThreadsChangedEventArgs : EventArgs
     {
         private object _threads;

         public ThreadsChangedEventArgs(object p_threads)
         {
             _threads = p_threads;
         }

         public object Threads
         {
             get
             {
                 return _threads;
             }
         }
     }

     public class PostsChangedEventArgs : EventArgs
     {
         private object _posts;

         public PostsChangedEventArgs(object p_posts)
         {
             _posts = p_posts;
         }

         public object Posts
         {
             get
             {
                 return _posts;
             }
         }
     }

}
