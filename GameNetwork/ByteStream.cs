using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GameNetwork
{
    /// <summary>
    /// 
    /// Cette classe est un outil pour simplifier la vie qui gère un flux d'octet.
    /// On peut y lire ou écrire un flux d'octet contenant des int, float, string ou autre.
    /// La lecture se fait à la position du curseur et l'écriture toujours à la fin du flux.
    /// 
    /// </summary>
    public class ByteStream
    {
        private byte[] _bytes;
        private int _cursorPosition;

        public byte[] bytes
        {
            get { return _bytes; }
        }

        public int cursorPosition
        {
            get { return _cursorPosition; }
            set { _cursorPosition = value; }
        }


        public ByteStream()
        {
            _bytes = new byte[0];
            _cursorPosition = 0;
        }

        public ByteStream(byte[] byteArray)
        {
            _bytes = byteArray;
            _cursorPosition = 0;
        }

        public ByteStream(params object[] values)
        {
            byte[][] tmp = new byte[values.Length][];

            for (int i = 0; i < values.Length; i++)
            {
                if (values[i] is long)
                {
                    tmp[i] = BitConverter.GetBytes((long)(values[i]));
                }
                else if (values[i] is int)
                {
                    tmp[i] = BitConverter.GetBytes((int)(values[i]));
                }
                else if (values[i] is float)
                {
                    tmp[i] = BitConverter.GetBytes((float)(values[i]));
                }
                else if (values[i] is byte)
                {
                    byte[] b = { (byte)values[i] };
                    tmp[i] = b;
                }
                else if (values[i] is bool)
                {
                    tmp[i] = BitConverter.GetBytes((bool)(values[i]));
                }
                else if (values[i] is string)
                {
                    tmp[i] = concat(BitConverter.GetBytes((ushort)((string)(values[i])).Length), Encoding.UTF8.GetBytes((string)(values[i])));
                }
                else if (values[i] is Vector2)
                {
                    tmp[i] = concat(BitConverter.GetBytes(((Vector2)values[i]).X),BitConverter.GetBytes(((Vector2)values[i]).Y));
                }
                else throw new ArgumentException("Le type " + values[i].GetType().ToString() + " n'est pas pris en charge par ByteStream");
            }

            _bytes = concatBytes(tmp);

            _cursorPosition = 0;
        }

        public int readInt()
        {
            int result = BitConverter.ToInt32(_bytes, _cursorPosition);
            _cursorPosition += 4;
            return result;
        }

        public long readLong()
        {
            long result = BitConverter.ToInt64(_bytes, _cursorPosition);
            _cursorPosition += 8;
            return result;
        }

        public float readFloat()
        {
            float result = BitConverter.ToSingle(_bytes, _cursorPosition);
            _cursorPosition += 4;
            return result;
        }

        public byte readByte()
        {
            byte result = _bytes[_cursorPosition];
            _cursorPosition++;
            return result;
        }
        public bool readBool()
        {
            bool result = BitConverter.ToBoolean(_bytes, _cursorPosition);
            _cursorPosition++;
            return result;
        }

        public string readString()
        {
            /* pour un string, il faut d'abord connaitre sa taille, c'est pourquoi un ushort précède toujours
             * les string. ( 'l'ushort est nombre entier non signé sur 16 bits, qui compte donc jusqu'à 65 535
             * ce qui est plus que suffisant pour une chaine de caractère )
             */

            ushort stringLenght = BitConverter.ToUInt16(_bytes, _cursorPosition);
            string result = Encoding.UTF8.GetString(_bytes, _cursorPosition + 2, (int)stringLenght);
            _cursorPosition += stringLenght + 2;
            return result;
        }
        public Vector2 readVector2()
        {
            Vector2 result = new Vector2(BitConverter.ToSingle(_bytes, _cursorPosition), BitConverter.ToSingle(_bytes, _cursorPosition+4));
            _cursorPosition += 8;
            return result;
        }

        public void write(int value)
        {
            _bytes = concat(_bytes, BitConverter.GetBytes(value));
        }

        public void write(long value)
        {
            _bytes = concat(_bytes, BitConverter.GetBytes(value));
        }

        public void write(float value)
        {
            _bytes = concat(_bytes, BitConverter.GetBytes(value));
        }

        public void write(byte value)
        {
            byte[] b = { value };
            _bytes = concat(_bytes, b);
        }

        public void write(string value)
        {
            _bytes = concatBytes(_bytes, BitConverter.GetBytes((ushort)value.Length), Encoding.UTF8.GetBytes(value));
        }

        public void write(bool value)
        {
            _bytes = concatBytes(_bytes, BitConverter.GetBytes(value));
        }

        public void write(Vector2 value)
        {
            _bytes = concatBytes(_bytes, BitConverter.GetBytes(value.X), BitConverter.GetBytes(value.Y));
        }

        public static byte[] concat(byte[] b1, byte[] b2)
        {
            int l1 = b1.Length, l2 = b2.Length;
            byte[] result = new byte[l1 + l2];
            for (int i = 0; i < l1; i++)
            {
                result[i] = b1[i];
            }
            for (int i = 0; i < l2; i++)
            {
                result[l1 + i] = b2[i];
            }
            return result;
        }

        public static byte[] concatBytes(params byte[][] tabs)
        {
            int totalLength = 0;

            foreach (byte[] t in tabs) totalLength += t.Length;

            int cursor = 0;
            byte[] result = new byte[totalLength];

            foreach (byte[] t in tabs)
            {
                foreach (byte b in t)
                {
                    result[cursor] = b;
                    cursor++;
                }
            }

            return result;
        }

        public static byte[] subarray(byte[] b, int start, int lenght)
        {
            byte[] result = new byte[lenght - start+1];

            for (int i = 0; i <= lenght - start; i++)
            {
                result[i] = b[i + start];
            }

            return result;
        }
    }
}
