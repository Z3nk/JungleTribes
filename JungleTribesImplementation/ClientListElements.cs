using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameNetwork;

namespace JungleTribesImplementation
{
    public class ClientListElements : ListElements
    {
        private int _elemNumber;
        public int elemNumber
        {
            get { return _elemNumber; }
        }
        private List<Element> _newList;
        private List<int> _removeList;
        private Dictionary<int, Element> _list;
        public override Dictionary<int, Element> list
        {
            get { return _list; }
        }


        public ClientListElements()
        {
            _newList = new List<Element>();
            _removeList = new List<int>();
            _list = new Dictionary<int, Element>();
            _elemNumber = 0;
        }

        public override void addElement(Element e)
        {
            _newList.Add(e);
        }

        public void updateFromMessage(MessageUpdateElements m)
        {
            ByteStream bs = m.byteStream;
            for (int i = 0; i < m.nbElem; i++)
            {
                int id = bs.readInt();
                byte typeElement = bs.readByte();
                try
                {
                    _list[id].updateElementFromBytes(bs);
                }
                catch (KeyNotFoundException)
                {
                    Element e=Element.createElementFromBytes(id, typeElement, bs);
                    if (e.alive)
                    {
                        _elemNumber++;
                        _list.Add(e.id, e);
                    }
                }
            }
        }
        public void update(float delta)
        {
            foreach (Element e in _list.Select(p => p.Value))
            {
                e.update(delta, _list);
                if (!e.alive)
                    _removeList.Add(e.id);
            }
            foreach (Element e in _newList)
            {
                _list.Add(e.id, e);
                _elemNumber++;
            }
            _newList.Clear();
            foreach (int id in _removeList)
            {
                _list.Remove(id);
                _elemNumber--;
            }
            _removeList.Clear();
        }
    }
}
