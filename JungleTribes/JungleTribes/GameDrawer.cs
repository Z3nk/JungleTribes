using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JungleTribesImplementation;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace JungleTribes
{
    class GameDrawer
    {
        private ClientListElements _listElements;
        private Dictionary<int, ElementDrawer> _drawerList;

        public GameDrawer(ClientListElements listElements)
        {
            _listElements = listElements;
            _drawerList = new Dictionary<int, ElementDrawer>();
            _listElements = listElements;
        }

        public void draw()
        {
            foreach (Element e in _listElements.list.Select(p=>p.Value))
            {
                if (e.alive)
                {
                    try
                    {
                        _drawerList[e.id].draw();
                    }
                    catch (KeyNotFoundException)
                    {
                        if (e is Mage)
                            _drawerList.Add(e.id, new MageDrawer((Hero)e));
                        else if(e is Chaman)
                            _drawerList.Add(e.id, new ChamanDrawer((Hero)e));
                        else if (e is Guerrier)
                            _drawerList.Add(e.id, new GuerrierDrawer((Hero)e));
                        else if (e is MageBullet || e is ChamanBullet)
                            _drawerList.Add(e.id, new BulletDrawer((Bullet)e));
                        else if (e is WarriorAttack)
                            _drawerList.Add(e.id, new WarriorAttackDrawer((WarriorAttack)e));
                        else if (e is Shield)
                            _drawerList.Add(e.id, new ShieldDrawer((Shield)e));
                        else if (e is Charge)
                            _drawerList.Add(e.id, new ChargeDrawer((Charge)e));
                        else if (e is EP)
                            _drawerList.Add(e.id, new EPDrawer((EP)e));
                        else if (e is TourbiLol)
                            _drawerList.Add(e.id, new TourbiLolDrawer((TourbiLol)e));
                        else if (e is MageBlizzard)
                            _drawerList.Add(e.id, new BlizzardDrawer((MageBlizzard)e));
                        else if (e is MageFoudre)
                            _drawerList.Add(e.id, new FoudreDrawer((MageFoudre)e));
                        else if (e is MageFire)
                            _drawerList.Add(e.id, new MageFireDrawer((MageFire)e));
                        else if (e is MageExplosion)
                            _drawerList.Add(e.id, new ExplosionDrawer((MageExplosion)e));
                        else if (e is ChamanSoin)
                            _drawerList.Add(e.id, new ChamanSoinDrawer(e));
                        else if (e is ChamanBenediction)
                            _drawerList.Add(e.id, new ChamanBenedictionDrawer(e));
                        else if (e is ChamanBlocage)
                            _drawerList.Add(e.id, new ChamanBlocageDrawer((ChamanBlocage)e));
                        else if (e is ChamanAide)
                            _drawerList.Add(e.id, new ChamanAideDrawer((ChamanAide)e));
                        else if (e is Teleport)
                            _drawerList.Add(e.id, new TeleportDrawer((Teleport)e));

                        _drawerList[e.id].draw();
                    }
                }
            }
        }
    }
}
