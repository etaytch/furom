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
            
            ClientManager manager = new ClientManager();
            manager.connect();
            Console.WriteLine("Welcome!");
            while (!manager.exit)
            {
                while (!manager.loggedIn && !manager.exit) // While user isn't logged in yet he can only register, login or exit
                    manager.regLogin();

                if (manager.loggedIn) // once he's logged a new Client is created for him
                    manager.newClient();
  
                while (manager.loggedIn && !manager.exit) // While user is logged in he can communicate with the forum
                    manager.forumCom();
            }
            manager.disconnect();
            Console.WriteLine("Goodbye!");
        }
    }
}
