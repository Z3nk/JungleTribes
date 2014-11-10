using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameNetwork
{
    public static class MessageManager
    {
        private static int idMessageTypeCounter = 0;

        private static Dictionary<int, Type> _typeList = new Dictionary<int, Type>();
        private static Dictionary<Type, int> _idList = new Dictionary<Type, int>();

        public static void addMessageType(Type messageType)
        {
            _typeList.Add(idMessageTypeCounter, messageType);
            _idList.Add(messageType, idMessageTypeCounter);
            idMessageTypeCounter++;
        }

        public static MessageNet create(byte[] data)
        {
            int messageId = data[0];
            return (MessageNet)Activator.CreateInstance(_typeList[messageId], ByteStream.subarray(data, 1, data.Length - 1));
        }

        public static byte[] getBytes(MessageNet message)
        {
            byte[] b = { (byte)_idList[message.GetType()] };
            return ByteStream.concat(b, message.bytes);
        }
    }
}
