using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameNetwork;
using System.Net.Sockets;
using System.Net;
using JungleTribesImplementation;
using System.Threading;
using Microsoft.Xna.Framework;

namespace GameServer
{
    class Program
    {
        static Server serveur;
        static int _fps = 60;
        static bool continuer = true;
        static Dictionary<int, Classe> userIdClasse;
        static Dictionary<int, int> userIdTeam;

        static void Main(string[] args)
        {
            MessageManager.addMessageType(typeof(MessageInitConnection));
            MessageManager.addMessageType(typeof(MessageHostConnected));
            MessageManager.addMessageType(typeof(MessageHostDisconnected));
            MessageManager.addMessageType(typeof(MessagePing));
            MessageManager.addMessageType(typeof(MessageChat));
            MessageManager.addMessageType(typeof(MessageUpdateElements));
            MessageManager.addMessageType(typeof(MessageUserCommand));
            MessageManager.addMessageType(typeof(MessageSelectionHero));
            MessageManager.addMessageType(typeof(MessageSkillLaunched));
            MessageManager.addMessageType(typeof(MessageHeroTeam));

            ElementManager.initialize();
            userIdClasse = new Dictionary<int, Classe>();
            userIdTeam = new Dictionary<int, int>();
            string IP, port;
            Console.Write("IP du serveur : ");
            IP=Console.ReadLine();
            Console.Write("Port : ");
            port=Console.ReadLine();
            serveur = new Server(IPAddress.Parse(IP), Convert.ToInt32(port));
            serveur.startServer();

            Thread t = new Thread(new ThreadStart(threadReadLine));
            t.Start();

            Thread tonton = new Thread(new ThreadStart(listenLoop));
            tonton.Start();
        
            while (true) { Thread.Sleep(500); }

        }

        public static void listenLoop()
        {
            while (continuer)
            {
                while (serveur.haveMoreHostMessage())
                {
                    HostMessage hm = serveur.deQueueHostMessage();
                    if (hm.message is MessageHostConnected)
                    {
                       Console.WriteLine(hm.host.username + " s'est connecté ");
                    }
                    else if (hm.message is MessageHostDisconnected)
                    {
                        Console.WriteLine(hm.host.username + " s'est déconnecté ");
                    }
                    else if (hm.message is MessageSelectionHero)
                    {
                        userIdClasse.Add(hm.host.id, ((MessageSelectionHero)hm.message).selection);
                        userIdTeam.Add(hm.host.id, ((MessageSelectionHero)hm.message).team);
                       Console.WriteLine("Vous avez selectionné :" + (byte)(((MessageSelectionHero)hm.message).selection));

                    }
                }

            }
        }
        public static void threadReadLine()
        {
            while(true)
            {
                if (Console.ReadLine().Equals("Start"))
                {
                    foreach (User H in serveur.userList.Select(p=>p.Value))
                    {
                        H.sendTcpMessage(new MessageChat("start"));
                    }
                    continuer = false;

                    Thread fred = new Thread(new ThreadStart(threadGame));
                    fred.Priority = ThreadPriority.Highest;
                    fred.Start();
                }
            }
        }

        public static void threadGame()
        {
            ServerListElements listElements = new ServerListElements(serveur);
            Element.currentListElements = listElements;
            long previousTime;
            float delta = 0;
            Dictionary<int, int> heroDic = new Dictionary<int, int>();

            int i = 200;
            MessageHeroTeam mht = new MessageHeroTeam();

            foreach (User host in serveur.userList.Select(p=>p.Value))
            {
                if (!heroDic.ContainsKey(host.id))
                {
                    //Hero h = new Hero();
                    Hero h;
                    switch (userIdClasse[host.id])
                    {
                        case Classe.guerrier :
                            h = new Guerrier();
                            break;

                        case Classe.mage:
                            h = new Mage();
                            break;

                        case Classe.chaman:
                            h = new Chaman();
                            break;
                        default:
                            h = new Guerrier();
                            break;

                    }          
          
                    h.position = new Vector2(i, 200);
                    h.owner = host;
                    h.teamId = userIdTeam[host.id];
                    i += 200;
                    heroDic.Add(host.id, h.id);
                    listElements.addElement(h);
                    host.sendTcpMessage(new MessageChat("Hero : " + h.id));
                    mht.addElement(h);
                }
            }

            foreach (User host in serveur.userList.Select(p => p.Value))
            {
                host.sendTcpMessage(mht);
            }

            /*Mage m = new Mage();
            m.position = new Vector2(500, 500);
            listElements.addElement(m);
            m = new Mage();
            m.position = new Vector2(550, 550);
            listElements.addElement(m);
            m.teamId = -1;
            m = new Mage();
            m.position = new Vector2(200, 700);
            listElements.addElement(m);
            m.teamId = 200;*/
            listElements.clearNewList();

            

            while (true)
            {
                previousTime = DateTime.Now.Ticks;

                HostMessage hm;
                while ((hm = serveur.deQueueHostMessage()) != null)
                {
                    if (hm.message is MessageUserCommand)
                    {
                        ((Hero)listElements.list[heroDic[hm.host.id]]).commandByMessage((MessageUserCommand)hm.message);
                    }
                }

                listElements.update(delta);

                if ((float)(DateTime.Now.Ticks - previousTime) < (float)((10000000) - 50000) / (float)_fps)
                    Thread.Sleep((int)((10000000 - 50000 - (DateTime.Now.Ticks - previousTime)) / (float)_fps) / 10000);
                while ((float)(DateTime.Now.Ticks - previousTime) < (float)(10000000) / (float)_fps) { }
                delta = ((float)(DateTime.Now.Ticks - previousTime)) / 10000;
            }
        }
    }
}
