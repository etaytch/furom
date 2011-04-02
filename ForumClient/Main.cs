using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Protocol;
using MessagePack;
using common;

namespace ForumClient
{
    public class ForumClient
    {
        public static void Main(string[] args)
        {
            
            Boolean loggedIn = false;
            Boolean exit = false;
            ClientManager manager = new ClientManager(exit, loggedIn);
 
            manager.connect();
            Console.WriteLine("Welcome!");
            while (!exit)
            {
                while (!loggedIn && !exit) // While user isn't logged in yet he can only register, login or exit
                    manager.regLogin();

                if (loggedIn) // once he's logged a new Client is created for him
                    manager.newClient();
  
                while (loggedIn && !exit) // While user is logged in he can communicate with the forum
                    manager.forumCom();
            }
            manager.disconnect();
            Console.WriteLine("Goodbye!");
        }
    }
}
