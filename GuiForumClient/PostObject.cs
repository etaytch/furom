using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataManagment
{
    public class PostObject
    {
        string topic;
        string author;
        string content;

        
        public PostObject()
        {
            this.topic = "undefined" ;
            this.author = "undefined" ;
            this.content = "undefined" ;
        }

        public PostObject(string p_topic,string p_author,string p_content)
        {
            this.topic = p_topic;
            this.author = p_author;
            this.content = p_content;
        }


        /************************
        * 
        *  getter and setter 
        *
        ************************/
        public string Topic
        {
            get { return topic; }
            set { topic = value; }
        }

        public string Author
        {
            get { return author; }
            set { author = value; }
        }

        public string Content
        {
            get { return content; }
            set { content = value; }
        }


    }
}
