using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JungleTribesImplementation;

namespace JungleTribes
{
    class BarInfo
    {
        private float[] timers;

        public BarInfo(Hero h)
        {
            timers = new float[h.skills.Count];
            for (int i = 0; i < timers.Length; i++)
            {
                timers[i] = 0;
            }


        }
    }
}
