using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameNetwork;

namespace JungleTribesImplementation
{
    public class ServerListElements : ListElements
    {
        private List<Element> _newElements;
        private List<int> _removeElements;
        private Dictionary<int, Element> _list;

        public override Dictionary<int, Element> list
        {
            get { return _list; }
        }
        private Server server;
        public ServerListElements(Server s)
        {
            _list = new Dictionary<int, Element>();
            _newElements = new List<Element>();
            _removeElements = new List<int>();
            server = s;
        }

        public override void addElement(Element e)
        {
            _newElements.Add(e);
        }

        public void update(float delta)
        {
            MessageUpdateElements m = new MessageUpdateElements();
            foreach (Element e in _list.Select(p=>p.Value))
            {
                e.update(delta, _list);
                if (e.hasChanged)
                {
                    m.addUpdateElementInfo(e);
                    e.hasChanged = false;
                }
                if (!e.alive)
                    _removeElements.Add(e.id);
            }
            if (m.nbElem > 0)
            {
                foreach (User h in server.userList.Select(p=>p.Value))
                {
                    h.sendUdpMessage(m);
                }
            }
            clearNewList();
            foreach (int id in _removeElements)
            {
                _list.Remove(id);
            }
            _removeElements.Clear();
        }

        public void clearNewList()
        {
            foreach (Element e in _newElements)
            {
                _list.Add(e.id, e);
            }
            _newElements.Clear();
        }

        public void sendSkillLaunched(Hero h, byte id)
        {
            server.sendUdpMessage(h.owner, new MessageSkillLaunched(id));
        }
    }
}
