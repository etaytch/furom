﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MessagePack;

namespace Protocol
{
    interface ProtocolInterface
    {
        Message getMessage();
        void sendMessage(Message msg);
        void startServer();
        void stopServer();

    }
}
