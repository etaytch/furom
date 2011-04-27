using System;
using System.Net.Sockets;
using System.Text;
using common;
using System.Threading;
using System.Collections;

namespace Client
{
    public class ForumTcpClient : ForumTcpClientInterface
    {

        private int port_num = 10116;
        private string ip_num = "localhost";
        private bool connected = false;
        private TcpClient tcpClient;
        private NetworkStream networkStream;
        private BlockingQueue<string> massages_from_server;
        //private Queue massages_from_server = Queue.Synchronized(new Queue());
        private Thread listner_thread;


        public ForumTcpClient()
        {
            tcpClient = new TcpClient();
            massages_from_server = new BlockingQueue<string>();
        }

        public ForumTcpClient(int p_port_num, string p_ip_num)
        {
            this.port_num = p_port_num;
            this.ip_num = p_ip_num;
            tcpClient = new TcpClient();
            massages_from_server = new BlockingQueue<string>();
        }




        /************************
       * 
       *  is connected ?
       *
       ************************/
        public bool isConnected
        { get { return this.connected; } }



        /************************
        * 
        *  connect
        *
        ************************/
        public void connect()
        {

            if (!connected)
            {
                try
                {
                    tcpClient.Connect(ip_num, port_num);
                    networkStream = tcpClient.GetStream();
                    listner_thread = new Thread(new ThreadStart(listen));
                    this.connected = true;
                    listner_thread.Start();
                }
                catch (SocketException)
                {
                    Console.WriteLine("Server not available!");
                }
                catch (System.IO.IOException)
                {
                    Console.WriteLine("Server not available!");
                }
                catch (Exception e)
                {
                    Console.WriteLine("strange exception");

                    //Console.WriteLine(e.ToString());
                }
            }
        }


        /************************
        * 
        *  send
        *
        ************************/
        public void send(string massege)
        {

            try
            {
                if (networkStream.CanWrite)
                {
                    Byte[] sendBytes = Encoding.ASCII.GetBytes(massege);
                    networkStream.Write(sendBytes, 0, sendBytes.Length);
                }
            }
            catch (NullReferenceException e)
            {
                this.disconnect();
            }

        }

        /************************
        * 
        *  recieve
        *
        ************************/
        public string receive()
        {
            return (string)massages_from_server.Dequeue();//.Dequeue();
        }


        /************************
        * 
        *  listen
        *
        ************************/
        private void listen()
        {
            try
            {
                if (networkStream.CanRead)
                {
                    while (connected)
                    {
                        // Console.WriteLine("listening...");
                        byte[] bytes = new byte[tcpClient.ReceiveBufferSize];
                        int t_bytes_read = networkStream.Read(bytes, 0, (int)tcpClient.ReceiveBufferSize);
                        string t_returndata = Encoding.ASCII.GetString(bytes, 0, t_bytes_read);
                        //Console.WriteLine("recieved by the client listener before Enqueue: {0}", t_returndata);
                        massages_from_server.Enqueue(t_returndata);
                        //Console.WriteLine("recieved by the client listener: {0}", t_returndata);
                    }
                }
                else Console.WriteLine("listener cannot read from stream!!!");
            }
            catch (SocketException)
            {
                Console.WriteLine("Server not available!");
            }
            catch (System.IO.IOException)
            {
                Console.WriteLine("Server not available!");
            }
            catch (Exception e)
            {
                Console.WriteLine("strange exception");

                //Console.WriteLine(e.ToString());
            }

        }

        /************************
        * 
        *  disconnect
        *
        ************************/
        public void disconnect()
        {
            connected = false;
            if (listner_thread != null)
            {
                listner_thread.Abort();
                networkStream.Close();
                tcpClient.Close();
            }
        }



        /************************
        * 
        *  test
        *
        ************************/
        /*
        static public void Main()
        {
            Console.WriteLine("*** C L I E N T ***");
            ForumTcpClient client = new ForumTcpClient();
            client.connect();
            if (client.isConnected)
            {

                client.send("hello");
                
                Console.WriteLine("massage recieved to client:{0}", client.receive());
                Console.WriteLine("massage recieved to client:{0}", client.receive());
                Thread.Sleep(8000);
                client.disconnect();
            }
        }
        */ 
    }//class TcpClient

} //namespace LowLevelClient