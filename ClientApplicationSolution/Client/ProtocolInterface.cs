using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MessagePack;

namespace Protocol
{
    interface ProtocolInterface
    {
        void connect();
        void disconnect();
        Message getMessage();
        void sendMessage(Message msg);
    }
}
