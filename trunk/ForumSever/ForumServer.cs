using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ForumSever
{
    class ForumServer
    {
        static void Main(string[] args)
        {
            MassageHandler mh = new MassageHandler();
            mh.startForum();
            while (true)
            {
                Console.WriteLine("Welcome to the Forum Server!");
                mh.readMassage();
            }
 
        }
    }
}
