using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameNetwork;
namespace JungleTribesImplementation
{
    public enum Classe:byte{
        guerrier, chaman, mage
    }


    public class MessageSelectionHero : MessageNet
    {
        private Classe _selection;
        public Classe selection
        {
            get { return _selection; }
        }

        private int _team;
        public int team
        {
            get { return _team; }
        }
        public MessageSelectionHero(byte[] data)
        {
            _selection = (Classe)data[0];
            _team = (int)data[1];
        }

        public MessageSelectionHero(Classe c, int team)
        {
            _selection = c;
            _bytes = new byte[2];
            _bytes[0] = (byte)c;
            _bytes[1] = (byte)team;
        }
    }
}
