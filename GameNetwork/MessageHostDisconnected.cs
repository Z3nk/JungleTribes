using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameNetwork
{
    public class MessageHostDisconnected : MessageNet
    {
        public MessageHostDisconnected() { _bytes = new byte[0]; }
    }
}
