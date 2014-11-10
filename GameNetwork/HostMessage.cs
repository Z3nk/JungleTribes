using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameNetwork
{
    public class HostMessage
    {
        private User _host;
        private MessageNet _message;

        public User host
        {
            get { return _host; }
        }

        public MessageNet message
        {
            get { return _message; }
        }

        public HostMessage(User host, MessageNet message)
        {
            _host = host;
            _message = message;
        }
    }
}
