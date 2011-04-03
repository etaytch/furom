


using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Collections;
using System.Threading;
using common;


namespace Server
{
    public class ForumTcpServer : ForumTcpServerInterface         //SynchronousSocketListener
    {


        private const int MAX_NUM_OF_CLIENTS = 10000;
        private int port_num = 10116;
        private string ip_num = "localhost";
        private ArrayList clientSockets;
        private bool continueReclaim = true;
        private Thread threadReclaim;
        private Thread mainListeningThread;
        private bool is_running = false;
        private TcpListener listener = null;
        private BlockingQueue<BasicMassage> massages_from_clients;



        public ForumTcpServer()
        {
            //TcpListener listener = new TcpListener(System.Net.IPAddress.Any, port_num);
            massages_from_clients = new BlockingQueue<BasicMassage>();
        }

        public ForumTcpServer(int p_port_num, string p_ip_num)
        {
            this.port_num = p_port_num;
            this.ip_num = p_ip_num;
            //TcpListener listener = new TcpListener(System.Net.IPAddress.Any, port_num);
            massages_from_clients = new BlockingQueue<BasicMassage>();
        }





        /************************
        * 
        *  start the server
        *
        ************************/
        public void startServer()
        {
            mainListeningThread = new Thread(new ThreadStart(startListening));
            mainListeningThread.Start();
        }

        /************************
        * 
        *  stop the server
        *
        ************************/
        public void stopServer()
        {
           // mainListeningThread = new Thread(new ThreadStart(startListening));
            Console.WriteLine("ending connection...");
            listener.Stop();
            is_running = false;
            //mainListeningThread.Join();
        }



        /************************
        * 
        *  send
        *
        ************************/
        public void send(string p_massege, int p_uid)
        {
            foreach (Object t_client in clientSockets)
            {
                if (p_uid == ((ClientHandler)t_client).Uid)
                {
                    ((ClientHandler)t_client).send(p_massege);
                    return;
                }
            }
            Console.WriteLine("user <{0}> doesn't exist in the system!", p_uid);
        }



        public void send(BasicMassage massage)
        {
            send(massage.Massage, massage.Uid);
        }

        /************************
        * 
        *  recieve
        *
        ************************/
        public BasicMassage receive()
        {

            return massages_from_clients.Dequeue();
        }



        /************************
        * 
        *  is running?
        *
        ************************/
        public bool isRunning
        { get { return this.is_running; } }


        /************************
        * 
        *  Listening Thread
        *
        ************************/
        private void startListening()
        {

            is_running = true;
            clientSockets = new ArrayList();
            //remove dead clients from the arrayList (thread)
            threadReclaim = new Thread(new ThreadStart(reclaim));
            threadReclaim.Start();
            listener = new TcpListener(System.Net.IPAddress.Any, port_num);
            try
            {
                //vadi

                listener.Start();
                int Cycle = MAX_NUM_OF_CLIENTS;    //after MAX_NUM_OF_CLIENTS the server will shotdown  
                int ClientNbr = 0;

                // Start listening for connections.
                Console.WriteLine("Waiting for a connection...");
                while ((Cycle > 0) && is_running)
                {

                    TcpClient handler = listener.AcceptTcpClient();
                    if (handler != null)
                    {
                        Console.WriteLine("Client#{0} accepted!", ++ClientNbr);
                        // An incoming connection needs to be processed.
                        lock (clientSockets.SyncRoot)
                        {
                            int i = clientSockets.Add(new ClientHandler(ClientNbr, handler, massages_from_clients));
                            ((ClientHandler)clientSockets[i]).Start();

                        }
                        --Cycle;
                    }
                    else
                        break;
                }
                listener.Stop();
                continueReclaim = false;
                threadReclaim.Join();

                foreach (Object Client in clientSockets)
                {
                    ((ClientHandler)Client).Stop();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            Console.WriteLine("\nHit enter to continue...");
            Console.Read();

        }

        /************************
        * 
        *  reclaim
        *
        ************************/
        //cyclic dead client cleaner
        private void reclaim()
        {
            while (continueReclaim)
            {
                lock (clientSockets.SyncRoot)
                {
                    for (int x = clientSockets.Count - 1; x >= 0; x--)
                    {
                        Object Client = clientSockets[x];
                        if (!((ClientHandler)Client).Alive)
                        {
                            clientSockets.Remove(Client);
                            Console.WriteLine("A client left");
                        }
                    }
                }
                Thread.Sleep(300);
            }
        }


        /************************
        * 
        *  Test
        *
        ************************/ 
        /*
        public static int Main(String[] args)
        {

            ForumTcpServer server = new ForumTcpServer();
            server.startServer();
            Thread.Sleep(5000);
            server.send("this is to client 1", 1);
            server.send("this is to client 2", 2);
            return 0;
        }

        */
    } //class TcpServer



    class ClientHandler
    {
        int uid;
        TcpClient ClientSocket;
        private BlockingQueue<BasicMassage> massages_from_client;
        bool ContinueProcess = false;
        Thread ClientThread;
        NetworkStream networkStream;

        public ClientHandler(int p_uid, TcpClient p_clientSocket, BlockingQueue<BasicMassage> p_massages_from_clients)
        {
            this.uid = p_uid;
            this.ClientSocket = p_clientSocket;
            this.massages_from_client = p_massages_from_clients;
        }




        /************************
        * 
        *  start client thread
        *
        ************************/
        public void Start()
        {
            ContinueProcess = true;
            ClientThread = new Thread(new ThreadStart(Process));
            ClientThread.Start();
        }



        /************************
        * 
        *  getter and setter fo the usesrname
        *
        ************************/
        public int Uid
        {
            get { return uid; }
            set { uid = value; }
        }


        /************************
        * 
        *  is Alive ?
        *
        ************************/
        public bool Alive
        {
            get { return (ClientThread != null && ClientThread.IsAlive); }
        }



        /************************
        * 
        *  send
        *
        ************************/
        public void send(string massege)
        {
            if (networkStream.CanWrite)
            {
                Console.WriteLine("Sending: \n---------------------\n{0}\n---------------------\n", massege);
                Byte[] sendBytes = Encoding.ASCII.GetBytes(massege);
                networkStream.Write(sendBytes, 0, sendBytes.Length);
            }
        }


        /************************
        * 
        *  process 
        *
        ************************/
        private void Process()
        {

            // Incoming data from the client.
            string data = null;

            // Data buffer for incoming data.
            byte[] bytes;

            if (ClientSocket != null)
            {
                networkStream = ClientSocket.GetStream();
                ClientSocket.ReceiveTimeout = 1000; // 1000 miliseconds

                while (ContinueProcess)
                {
                    bytes = new byte[ClientSocket.ReceiveBufferSize];
                    try
                    {
                        int BytesRead = networkStream.Read(bytes, 0, (int)ClientSocket.ReceiveBufferSize);
                        if (BytesRead > 0)
                        {
                            data = Encoding.ASCII.GetString(bytes, 0, BytesRead);

                            // Show the data on the console.
                            Console.WriteLine("Text received : {0}\n\n", data);
                            massages_from_client.Enqueue(new BasicMassage(uid, data));

                            /*
                            //Echo the data back to the client.
                            Console.WriteLine("sending replay to client");
                            byte[] sendBytes = Encoding.ASCII.GetBytes(data);
                            networkStream.Write(sendBytes, 0, sendBytes.Length);
                            Console.WriteLine("replay sent");
                            // if (data == "quit") break;
                            */
                        }
                    }
                    catch (IOException) { } // Timeout
                    catch (SocketException)
                    {
                        Console.WriteLine("Conection is broken!");
                        break;
                    }
                    Thread.Sleep(200);
                } // while ( ContinueProcess )
                networkStream.Close();
                ClientSocket.Close();
            }
        }  // Process()


        /************************
        * 
        *  stop client handler
        *
        ************************/
        public void Stop()
        {
            ContinueProcess = false;
            if (ClientThread != null && ClientThread.IsAlive)
                ClientThread.Join();
        }


    } // class ClientHandler 


}

