using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using VS.Logger;

namespace ForumSever
{
    public class ForumServer
    {
        static bool connected = true;

        public static void Start(LogicManager lm/*, Logger log*/)
        {

            MassageHandler mh = new MassageHandler(lm/*,log*/);
            mh.startForum();
            Thread keyboard_listener = new Thread(new ThreadStart(listen));
            keyboard_listener.Start();
            Console.WriteLine("Welcome to the Forum Server!");
            while (connected)
            {
                mh.readMassage();
            }
            mh.stopForum();
        }

        static void Main(string[] args)
        {

            MassageHandler mh = new MassageHandler();
            mh.startForum();
            Thread keyboard_listener = new Thread(new ThreadStart(listen));
            keyboard_listener.Start();
            Console.WriteLine("Welcome to the Forum Server!");            
            while (connected)
            {
                mh.readMassage();
            }
            mh.stopForum();
        }

        static void listen()
        {
            while (connected)
            {
                switch (Console.ReadLine())
                {
                    case "quit":
                        Console.WriteLine("Stopping server");
                        connected = false;
                        break;
                    default:
                        break;
                }
            }
        }

    }
}
