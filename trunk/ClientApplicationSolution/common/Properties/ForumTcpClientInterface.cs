using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Client
{
    interface ForumTcpClientInterface
    {
        void    send(string massage);
        string  receive();
        void    connect();
        void    disconnect();
    }
}
