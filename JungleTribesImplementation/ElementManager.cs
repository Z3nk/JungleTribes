using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameNetwork;
using System.Reflection;

namespace JungleTribesImplementation
{
    public static class ElementManager
    {
        private static int idElementTypeCounter = 0;
        public static Dictionary<int, Type> typeList = new Dictionary<int, Type>();
        public static Dictionary<Type, int> idList = new Dictionary<Type, int>();

        public static void initialize()
        {
            addElementType(typeof(Guerrier));
            addElementType(typeof(Mage));
            addElementType(typeof(Chaman));
            addElementType(typeof(MageBullet));
            addElementType(typeof(MageBlizzard));
            addElementType(typeof(MageFoudre));
            addElementType(typeof(ChamanBullet));
            addElementType(typeof(MageFire));
            addElementType(typeof(MageExplosion));
            addElementType(typeof(ChamanSoin));
            addElementType(typeof(ChamanBenediction));
            addElementType(typeof(ChamanBlocage));
            addElementType(typeof(ChamanAide));
            addElementType(typeof(WarriorAttack));
            addElementType(typeof(Shield));
            addElementType(typeof(Charge));
            addElementType(typeof(EP));
            addElementType(typeof(TourbiLol));
            addElementType(typeof(Teleport));
        }

        public static void addElementType(Type messageType)
        {
            typeList.Add(idElementTypeCounter, messageType);
            idList.Add(messageType, idElementTypeCounter);
            idElementTypeCounter++;
        }
    }
}
