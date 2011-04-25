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
        private List<Quartet> posts;
        private PostObject currentPost;
        private ViewData currentForumId;
        private ViewData currentThreadId;


        public Database()
        {
            forums = new List<ViewData>();
            threads = new List<ViewData>();
            posts = new List<Quartet>();
            currentPost = new PostObject("Welcom to the \"ALUFIM \" furom!","HaAlufim","Have Fun!",-1);	   
		    currentForumId = new ViewData("Sheker",-1);
            currentThreadId = new ViewData("Sheker", -1);
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

        public List<Quartet> Posts
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
            CurrentThreadId = new ViewData("Sheker", -1);
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
				
        public ViewData CurrentForumId
        {
            get { return currentForumId; }
            set { currentForumId = value; }
        }

        public ViewData CurrentThreadId
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

        internal void addPost(Quartet p_post)
        {
            //ViewHirarchiData t_vd = new ViewHirarchiData(topic, 0);
            this.posts.Add(p_post);
            this.Posts = posts;
        }

        internal void addForum(Quartet forum)
        {
            ViewData t_vd = new ViewData(forum._subject, forum._pIndex);
            this.forums.Add(t_vd);
            this.Forums = forums;
        }


        /************************MVC*********************/
        //declerations
        public delegate void ForumsChangedHandler(object sender, ForumsChangedEventArgs e);
        public delegate void ThreadsChangedHandler(object sender, ThreadsChangedEventArgs e);
        public delegate void PostsChangedHandler(object sender, PostsChangedEventArgs e);
        public delegate void CurrentPostChangedHandler(object sender, CurrentPostChangedEventArgs e);

        //variables
        public event ForumsChangedHandler ForumsChangedEvent;
        public event ThreadsChangedHandler ThreadsChangedEvent;
        public event PostsChangedHandler PostsChangedEvent;
        public event CurrentPostChangedHandler CurrentPostChangedEvent;


        protected void ForumsChanged()
        {
            ForumsChangedEvent(this, new ForumsChangedEventArgs(forums,CurrentForumId));
        }
        protected void ThreadsChanged()
        {
            ThreadsChangedEvent(this, new ThreadsChangedEventArgs(threads, CurrentThreadId,currentForumId));
        }
        protected void PostsChanged()
        {
            PostsChangedEvent(this, new PostsChangedEventArgs(posts,CurrentThreadId, CurrentForumId,CurrentPost));
        }

        protected void CurrentPostChanged()
        {
            CurrentPostChangedEvent(this, new CurrentPostChangedEventArgs(CurrentPost));
        }
        /************************MVC END*********************/


        internal int findForumIndex(string p_forumName)
        {
            
            foreach (ViewData forum in this.Forums)
            {
                if (forum.Name == p_forumName)
                {
                    return forum.Id;

                }
            }
            return -1;
        }

        internal int findthreadIndex(string p_threadName)
        {
            foreach (ViewData thread in this.Threads)
            {
                if (thread.Name == p_threadName)
                {
                    return thread.Id;

                }
            }
            return -1;
        }

        internal int findPostIndex(string p_name)
        {
            foreach (Quartet post in this.posts)
            {
                if (post._subject.Equals(p_name))
                {
                    return post._pIndex;

                }
            }
            return -1;
        }
    }




     public class ForumsChangedEventArgs  : EventArgs
    {
        private object _forums;
        private ViewData _currentForumID; 
        public ForumsChangedEventArgs(object p_forums,ViewData p_curr_int)
        {
            _forums  = p_forums;
            _currentForumID = p_curr_int;

        }
        
         public object Forums
        {get {return _forums; } }

         public ViewData CurrentForumID
         { get { return _currentForumID; } }
    }


     public class ThreadsChangedEventArgs : EventArgs
     {
         private object _threads;
         private ViewData _currentThreadID;
         private ViewData _currentForumID; 

         public ThreadsChangedEventArgs(object p_threads,ViewData p_currentThreadID,ViewData p_currentForumID)
         {
             _threads = p_threads;
             _currentThreadID = p_currentThreadID;
             _currentForumID = p_currentForumID;
         }

         public object Threads
         {get{return _threads;}}


         public ViewData CurrentThreadID
         { get { return _currentThreadID; } }

         public ViewData CurrentForumID
         { get { return _currentForumID; } }
     }

     public class PostsChangedEventArgs : EventArgs
     {
         private List<Quartet> _posts;
         private ViewData _currentThreadID;
         private ViewData _currentForumID;
         PostObject _currentPost;
         public PostsChangedEventArgs(List<Quartet> p_posts, ViewData p_currentThreadID, ViewData p_currentForumID, PostObject p_currentPost)
         {
             _posts = p_posts;
             _currentPost= p_currentPost;
             _currentThreadID = p_currentThreadID;
             _currentForumID = p_currentForumID;
         }

         public List<Quartet> Posts
         {
             get
             {
                 return _posts;
             }
         }

         public ViewData CurrentThreadID
         { get { return _currentThreadID; } }

         public ViewData CurrentForumID
         { get { return _currentForumID; } }

         public PostObject CurrentPost
         { get { return _currentPost; } }
     }


     public class CurrentPostChangedEventArgs : EventArgs
     {
         PostObject _currentPost;
         public CurrentPostChangedEventArgs(PostObject p_currentPost)
         {
             _currentPost = p_currentPost;
         }
         public PostObject CurrentPost
         { get { return _currentPost; } }
     }

}
