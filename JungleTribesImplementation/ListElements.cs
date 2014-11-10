using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JungleTribesImplementation
{
    public abstract class ListElements
    {
        public abstract Dictionary<int, Element> list
        {
            get;
        }
        public abstract void addElement(Element e);
    }
}
