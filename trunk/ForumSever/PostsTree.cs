using System;
using System.Collections.Generic;
using System.Linq;
using MessagePack;


namespace ForumSever
{
    public class PostsTree
    {
        public PostsTree()
        {
            Content = "master";
            Post = new Quartet(0,0,"","");
            Children = new List<PostsTree>(); 
        }

        public Quartet Post{ get; set; }
        public string Content { get; set; }
        public List<PostsTree> Children { get; set; }
    }
}