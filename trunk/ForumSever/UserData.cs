using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MessagePack;

namespace ForumSever {
    public class UserData {
        public Quartet curForum;
        public Quartet curThread;
        public Quartet curPost;
        public string username;
        public HashSet<string> addedUsers;
        public Queue<string> notifications;

        public UserData() {
            this.curForum = null;
            this.curThread = null;
            this.curPost = null;
            this.addedUsers = new HashSet<string>();
            this.username = "";
            this.notifications = new Queue<string>();
        }

        public UserData(string username) {
            this.curForum = null;
            this.curThread = null;
            this.curPost = null;
            this.addedUsers = new HashSet<string>();
            this.username = username;
            this.notifications = new Queue<string>();
            notifications.Enqueue("Hello " + username);
        }

        
        public Quartet CurForum
        {
            get { return this.curForum; }
            set { this.curForum =  value; }
        }

        public Quartet CurThread {
            get { return this.curThread; }
            set { this.curThread = value; }
        }

        public Quartet CurPost {
            get { return this.curPost; }
            set { this.curPost = value; }
        }

        public string Username {
            get { return this.username; }
            set { this.username = value; }
        }

        public HashSet<string> AddedUsers {
            get { return this.addedUsers; }
            set { this.addedUsers = value; }
        }

    }
}