using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JungleTribesImplementation;

namespace JungleTribes
{
    public abstract class ElementDrawer
    {
        protected Element _element;


        public ElementDrawer(Element e)
        {
            _element = e;
        }

        public abstract void draw();
    }
}
