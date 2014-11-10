using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace GameNetwork
{

    /// <summary>
    /// Cette classe permet de lancer un serveur tcp/udp et de communiquer avec les clients
    /// </summary>
    public class Server
    {
        private List<User> _hostArray;
        private Dictionary<IPEndPoint, User> _userList;
        private Dictionary<int, User> _userListId;
        private Dictionary<string, User> _userListName;
        private TcpListener _tcpListener;
        private UdpClient _udpClient;
        private Queue<HostMessage> _hostMessageList;
        private int _maxHost;
        private int _hostNumber;

        /// <summary>
        /// Permet de récupérer la liste des hôtes connectés.
        /// </summary>
        public Dictionary<int, User> userList
        {
            get { return _userListId; }
        }

        /// <summary>
        /// Permet de lire ou de modifier le nombre maximal d'hôte connecté
        /// </summary>
        public int maxHost
        {
            get { return _maxHost; }
            set { _maxHost = value; }
        }

        /// <summary>
        /// Constructeur d'un serveur, en y indiquant l'ip et le port utilisé.
        /// </summary>
        /// <param name="ipAdress">ip utilisé</param>
        /// <param name="port">port utilisé</param>
        public Server(IPAddress ipAdress, int port)
        {
            _hostArray = new List<User>();
            _userList = new Dictionary<IPEndPoint, User>();
            _userListId = new Dictionary<int, User>();
            _userListName = new Dictionary<string, User>();
            _tcpListener = new TcpListener(ipAdress, port);
            _udpClient = new UdpClient(port + 1);
            _hostMessageList = new Queue<HostMessage>();
            _maxHost = 999;
            _hostNumber = 0;
        }
        /// <summary>
        /// Démarre le serveur et lance l'écoute des connections d'hôte.
        /// </summary>
        public void startServer()
        {
            _tcpListener.Start();

            Thread t = new Thread(new ThreadStart(acceptTcpClient));
            t.Start();

            t = new Thread(new ThreadStart(udpListener));
            t.Start();

        }


        /// <summary>
        /// Récupère un hôte en fonction de son id.
        /// </summary>
        /// <param name="id">id de l'hôte</param>
        /// <returns>Instance de Hote ou null s'il n'a pas été trouvé</returns>
        public User getHost(int id)
        {
            try
            {
                return _userListId[id];
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Récupère un hôte en fonction de son nom d'utilisateur
        /// </summary>
        /// <param name="username">nom de l'utilisateur</param>
        /// <returns>Instance de Hote ou null s'il n'a pas été trouvé</returns>
        public User getHost(string username)
        {
            try
            {
                return _userListName[username];
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Envoie un message à un hôte en mode TCP.
        /// </summary>
        /// <param name="host">Hôte destinataire du message</param>
        /// <param name="message">message à envoyer (héritié de Message)</param>
        public void sendTcpMessage(User host, MessageNet message)
        {
            host.sendTcpMessage(message);
        }

        /// <summary>
        /// Envoie un message à un hôte en mode UDP.
        /// </summary>
        /// <param name="host">Hôte destinataire du message</param>
        /// <param name="message">message à envoyer (héritié de Message)</param
        public void sendUdpMessage(User host, MessageNet message)
        {
            host.sendUdpMessage(message);
        }

        /// <summary>
        /// Retourne et dépile le message reçue suivant.
        /// </summary>
        /// <returns>Instance de HostMessage contenant le message reçu et l'hôte concerné.</returns>
        public HostMessage deQueueHostMessage()
        {
            lock (_hostMessageList)
            {
                try
                {
                    return _hostMessageList.Dequeue();
                }
                catch
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Indique s'il y a ou non un message reçu non traité dans la liste des messages reçu.
        /// </summary>
        /// <returns>boolean</returns>
        public bool haveMoreHostMessage()
        {
            return _hostMessageList.Count != 0 ? true : false;
        }

        /// <summary>
        /// méthode utilisé comme thread d'écoute de connection TCP.
        /// </summary>
        private void acceptTcpClient()
        {

            while (true)
            {
                // SI on a pas dépassé le nombre maximal d'hôte
                if (_hostNumber < _maxHost)
                {
                    // On attend que quelqu'un se connecte
                    TcpClient tcpClient = _tcpListener.AcceptTcpClient();

                    //Lorsque quelqu'un s'est connecté, on lance son thread d'écoute
                    Thread t = new Thread(new ParameterizedThreadStart(tcpListener));
                    t.Start(new User(tcpClient, _udpClient));
                }
                else Thread.Sleep(2000);
            }
        }


        /// <summary>
        /// Thread d'écoute TCP d'un hôte passé en paramètre, un thread est lancé
        /// pour chaque hôte connecté.
        /// </summary>
        /// <param name="ohost">hôte que le thread va écouter</param>
        private void tcpListener(object ohost)
        {
            User host = (User)ohost;

            /*Avant toute chose il faut initialiser la connection,
             * c'est-à-dire recevoir un message de type MessageInitConnection qui
             * contient le pseudo de l'hôte et son port d'écoute udp.
             */
            MessageNet message;
            try
            {
                message = host.receiveTcpMessage();
            }
            catch
            {
                return;
            }
            try
            {
                host.initialize((MessageInitConnection)message);
            }
            catch
            {
                // Si le message n'est pas du bon type on annule la connection.
                return;
            }

            /* Une fois l'initialisation terminé on ajoute l'hôte a la liste des hôtes
             * (plusieur manière d'accéder à un hôte)
             */
            lock (_userList)
            {
                _userList.Add(host.udpEndPoint, host);
            }
            lock (_userListId)
            {
                _userListId.Add(host.id, host);
            }
            lock (_userListName)
            {
                _userListName.Add(host.username, host);
            }
            lock (_hostArray)
            {
                _hostArray.Add(host);
            }
            lock (_hostMessageList)
            {
                // On ajoute un message informant d'une connection.
                _hostMessageList.Enqueue(new HostMessage(host, new MessageHostConnected()));
            }
            _hostNumber++;

            //boucle d'écoute TCP.
            while (true)
            {
                // On attend la reception d'un message
                try
                {
                    message = host.receiveTcpMessage();
                    host.connected = true;

                    if (message is MessagePing)
                    {
                        host.sendTcpMessage(message);
                    }
                    else
                    {
                        lock (_hostMessageList)
                        {
                            // On ajoute le message et l'hôte concerné à la liste des messages reçu.
                            _hostMessageList.Enqueue(new HostMessage(host, message));

                        }
                    }
                }
                catch
                {
                    // Si la connection à été perdu
                    if (host.connected)
                    {
                        host.connected = false;
                        lock (_hostMessageList)
                        {
                            _hostMessageList.Enqueue(new HostMessage(host, new MessageHostDisconnected()));
                        }
                    }
                    Thread.Sleep(500);
                }
            }
        }

        /// <summary>
        /// thread qui écoute tout les messages reçu en UDP.
        /// </summary>
        private void udpListener()
        {
            IPEndPoint ipEndPoint;
            byte[] buffer;
            while (true)
            {
                ipEndPoint = new IPEndPoint(IPAddress.Any, 0);
                // On attend de recevoir le message
                buffer = _udpClient.Receive(ref ipEndPoint);
                User host = _userList[ipEndPoint];
                MessageNet message = MessageManager.create(buffer);

                lock (_hostMessageList)
                {
                    // On ajoute le message et l'hôte concerné à la liste des messages reçu.
                    _hostMessageList.Enqueue(new HostMessage(host, message));
                }
            }
        }
    }
}
