using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameNetwork
{
    public class MessageHostConnected : MessageNet
    {
        public MessageHostConnected() { _bytes = new byte[0]; }
    }
}
