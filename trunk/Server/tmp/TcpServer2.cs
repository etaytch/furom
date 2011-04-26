using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Collections;
using System.Threading;

class SharedState {
        public bool ContinueProcess ;
        public int NumberOfClients ;
        public AutoResetEvent Ev ;
}


public  class SynchronousSocketListener {
        
  private  const int    portNum = 10116 ;
  private  static       SharedState SharedStateObj  ;
  
  public  static  void StartListening() {

    SharedStateObj = new SharedState() ;
    SharedStateObj.ContinueProcess = true ;
    SharedStateObj.NumberOfClients = 0 ;
    SharedStateObj.Ev = new AutoResetEvent(false) ;    

    TcpListener listener = new TcpListener(portNum);
    try {
              listener.Start();
        
              int TestingCycle = 3 ; 
              int ClientNbr = 0 ;
        
              // Start listening for connections.
              Console.WriteLine("Waiting for a connection...");
              while ( TestingCycle > 0 ) {
                      
                        TcpClient handler = listener.AcceptTcpClient();
                        
                        if (  handler != null)  {
                                Console.WriteLine("Client#{0} accepted!", ++ClientNbr) ;
                                // An incoming connection needs to be processed.
                                ClientHandler client = new ClientHandler(handler) ;
                                ThreadPool.QueueUserWorkItem(new WaitCallback(client.Process), SharedStateObj);
                                
                                --TestingCycle ;
                        }
                        else 
                                break;                
              }
              
              listener.Stop();
                           
    } catch (Exception e) {
              Console.WriteLine(e.ToString());
    }
    
    // Stop and wait all client connections to end
    SharedStateObj.ContinueProcess = false ;
    SharedStateObj.Ev.WaitOne() ; 
    
    Console.WriteLine("\nHit enter to continue...");
    Console.Read();
    
  }

  public  static  int Main(String[] args) {
    StartListening();
    return 0;
  }
}

class ClientHandler {

	TcpClient ClientSocket ;

	public ClientHandler (TcpClient ClientSocket) {
		this.ClientSocket = ClientSocket ;
	}

	public  void Process(Object O) {
                
                // Add no. of clients by one
                Interlocked.Increment(ref ((SharedState) O).NumberOfClients );

                SharedState SharedStateObj = (SharedState) O ;
                
		// Incoming data from the client.
		string data = null;

		// Data buffer for incoming data.
		byte[] bytes;

		if ( ClientSocket != null ) {
                        NetworkStream networkStream = ClientSocket.GetStream();
                        ClientSocket.ReceiveTimeout = 100 ; // 1000 miliseconds

			while ( SharedStateObj.ContinueProcess ) {
                                bytes = new byte[ClientSocket.ReceiveBufferSize];
                                try {
                                        int BytesRead = networkStream.Read(bytes, 0, (int) ClientSocket.ReceiveBufferSize);
                                        if ( BytesRead > 0 ) {
                                                data = Encoding.ASCII.GetString(bytes, 0, BytesRead);
                
                                                // Show the data on the console.
                                                Console.WriteLine( "Text received : {0}", data);
                
                                                // Echo the data back to the client.
                                                byte[] sendBytes = Encoding.ASCII.GetBytes(data);
                                                networkStream.Write(sendBytes, 0, sendBytes.Length);
                                                
                                                if ( data == "quit" ) break ;

                                        }
                                }
                                catch  ( IOException ) { } // Timeout
                                catch  ( SocketException ) {
                                        Console.WriteLine( "Conection is broken!");
                                        break ;
                                }
                                Thread.Sleep(200) ;
	               } // while ( ContinueProcess )
                       networkStream.Close() ;
        	       ClientSocket.Close();			
		}

                // Deduct no. of clients by one
                Interlocked.Decrement(ref SharedStateObj.NumberOfClients );
                
                Console.WriteLine("A client left, number of connections is {0}", SharedStateObj.NumberOfClients) ;
                
                // Signal main process if this is the last client connections main thread requested to stop.
                if ( !SharedStateObj.ContinueProcess && SharedStateObj.NumberOfClients == 0 ) SharedStateObj.Ev.Set();

	}  // Process()
        
} // class ClientHandler 
