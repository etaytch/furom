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
        int id;
        
        public PostObject()
        {
            this.topic = "undefined" ;
            this.author = "undefined" ;
            this.content = "undefined" ;
            this.id = -1;
        }

        public PostObject(string p_topic,string p_author,string p_content,int p_id)
        {
            this.topic = p_topic;
            this.author = p_author;
            this.content = p_content;
            this.id = p_id;
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

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

    }
}
