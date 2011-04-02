using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MessagePack;
namespace ForumSever
{
    public class Error : Message
    {
        private string p;

        public Error(string p): base("server")
        {
            // TODO: Complete member initialization
            this.p = p;
        }
    }
}
