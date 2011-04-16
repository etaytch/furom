using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using GuiForumClient;

namespace DataManagment
{
    public class Database
    {
        private List<ViewData> forums;
        private List<ViewData> threads;
        private List<ViewHirarchiData> posts;
        private PostObject currentPost;


        public Database()
        {
            forums = new List<ViewData>();
            threads = new List<ViewData>();
            posts = new List<ViewHirarchiData>();
            currentPost = new PostObject("Welcom to the \"ALUFIM \" furom!","HaAlufim","Have Fun!");

        }
    }

}
