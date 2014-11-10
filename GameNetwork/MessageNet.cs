using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameNetwork
{
    public abstract class MessageNet
    {
        protected byte[] _bytes;

        public virtual byte[] bytes
        {
            get { return _bytes; }
        }
    }
}
