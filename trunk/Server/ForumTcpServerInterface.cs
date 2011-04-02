using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using common;

namespace Server
{
    interface ForumTcpServerInterface
    {
        void            send(string massage, int uid);
        void            send(BasicMassage massage);
        BasicMassage    receive();
        void            startServer();
        void            stopServer();

    }
}




