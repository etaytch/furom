using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Protocol;
using MessagePack;

namespace ForumClient
{
   public class ClientManager
   {

        string ip = "10.100.101.196";
        int port = 10116;

        EandEProtocol protocol;
        ForumListener forum;
        Client client;
        string username;
        public Boolean loggedIn;
        public Boolean exit;

        public ClientManager()
        {
            protocol = new EandEProtocol(port, ip);
            forum = new ForumListener(protocol);
            loggedIn = false;
            exit = false;
        }

        public void connect() 
        {
            protocol.connect();
        }

        public void disconnect()
        {
            protocol.disconnect();
        }

        public void newClient()
        {
            client = new Client(username, protocol, forum);
            Console.WriteLine("Hello " + username + ", you are now logged in.");
        }

        public void regLogin()  // command prompt for the client to register or login
        {
            Console.WriteLine("Enter command:");
            string command = Console.ReadLine();
            string[] commandSplit = command.Split(' ');
            switch (commandSplit[0])
            {
                case "exit":
                    exit = true;
                    break;
                case "help":
                    Console.WriteLine("'register' - register a new user");
                    Console.WriteLine("'login <username> <password>' - login the forum");
                    Console.WriteLine("'exit' - close program");
                    break;
                case "register":
                    if (commandSplit.Length == 1)
                    {
                        Console.Write("Enter first name: ");
                        string fName = Console.ReadLine();
                        Console.Write("Enter last name: ");
                        string lName = Console.ReadLine();
                        Console.Write("Enter username: ");
                        string uName = Console.ReadLine();
                        Console.Write("Enter password: ");
                        string password = Console.ReadLine();
                        Console.Write("Confirm password: ");
                        string cpassword = Console.ReadLine();
                        Console.Write("Enter sex: ");
                        string sex = Console.ReadLine();
                        Console.Write("Enter e-mail: ");
                        string email = Console.ReadLine();
                        Console.Write("Enter birthday: ");
                        string birthday = Console.ReadLine();
                        Console.Write("Enter country: ");
                        string country = Console.ReadLine();
                        Console.Write("Enter city: ");
                        string city = Console.ReadLine();
                        RegisterMessage msg = new RegisterMessage(fName, lName, uName, password, cpassword, sex, email, birthday, country, city);
                        protocol.sendMessage(msg);
                        forum.Incoming();
                    }
                    else
                        goto default;
                    break;
                case "login":
                    if (commandSplit.Length == 3)
                    {
                        username = commandSplit[1];
                        LoginMessage msg = new LoginMessage(commandSplit[1], commandSplit[2]);
                        protocol.sendMessage(msg);
                        forum.Incoming();
                        loggedIn = true;
                        Console.WriteLine("logged in: " + loggedIn.ToString());
                    }
                    else
                        goto default;
                    break;
                default:
                    Console.WriteLine("Error. type 'help' for command list.");
                    break;
            }
        }

        public void forumCom()  // command prompt for the user to communicate with the forum
        {
            Console.WriteLine("Enter command:");
            string command = Console.ReadLine();
            string[] commandSplit = command.Split(' ');
            switch (commandSplit[0])
            {
                case "exit":
                    exit = true;
                    goto case "logout";
                case "help":
                    Console.WriteLine("'logout' - logout the forum");
                    Console.WriteLine("'forums' - get the forums list");
                    Console.WriteLine("'threads <forumID>' - get the threads list of <forumID>");
                    Console.WriteLine("'replies <forumID> <threadID>' - get the replies of <threadID> in <forumID>");
                    Console.WriteLine("'post <forumID>' - post a new thread in <forumID>");
                    Console.WriteLine("'reply <forumID> <threadID>' - reply to <threadID> in <forumID>");
                    Console.WriteLine("'addfriend <friendName>' - add <friendName> as friend");
                    Console.WriteLine("'removefriend <friendName>' - remove <friendName> as friend");
                    Console.WriteLine("'exit' - close program");
                    break;
                case "logout":
                    if (commandSplit.Length == 1)
                    {
                        client.logout();
                        loggedIn = false;
                    }
                    else
                        goto default;
                    break;
                case "forums":
                    if (commandSplit.Length == 1)
                        client.getForums();
                    else
                        goto default;
                    break;
                case "threads":
                    if (commandSplit.Length == 2)
                        client.getThreads(commandSplit[1]);
                    else
                        goto default;
                    break;
                case "replies":
                    if (commandSplit.Length == 3)
                        client.getReplies(commandSplit[1], commandSplit[2]);
                    else
                        goto default;
                    break;
                case "reply":
                    if (commandSplit.Length == 3)
                        client.reply(commandSplit[1], commandSplit[2]);
                    else
                        goto default;
                    break;
                case "post":
                    if (commandSplit.Length == 2)
                        client.post(commandSplit[1]);
                    else
                        goto default;
                    break;
                case "addfriend":
                    if (commandSplit.Length == 2)
                        client.addFriend(commandSplit[1]);
                    else
                        goto default;
                    break;
                case "removefriend":
                    if (commandSplit.Length == 2)
                        client.removeFriend(commandSplit[1]);
                    else
                        goto default;
                    break;
                default:
                    Console.WriteLine("Error. type 'help' for command list.");
                    break;
            }
        }
    }
}
