using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using TomShane.Neoforce.Controls;
using GameNetwork;
using System.Net;
using JungleTribesImplementation;

namespace JungleTribes
{
    public static class Global
    {
        public static ClientListElements clientListElements;
        public static BonhommeTestAnimation bonhommeTestAnimation;
        public static GuerrierAnimation guerrierAnimation;
        public static Texture2D mage_bullet;
        public static Texture2D mage_blizzard;
        public static Texture2D mage_foudre;
        public static Texture2D hp_bar;
        public static Texture2D mage_fire;
        public static Texture2D mage_explosion;
        public static Texture2D chaman_soin;
        public static Texture2D chaman_blocage;
        public static Texture2D chaman_aide;
        public static Texture2D warrior_Attack;
        public static Texture2D Shield;
        public static Texture2D Charge;
        public static Texture2D EP;
        public static Texture2D TourbiLol;
        public static Texture2D lifebar;
        public static Texture2D teleport;
        public static Texture2D benediction;
        public static int heroId = -1;
        /// <summary>
        /// Setters desactivés par defaults.
        /// </summary>
        private static int _mapWidth = 1600;
        public static int mapWidth
        {
            get { return _mapWidth; }
            // set { _mapWidth = value; }            
        }

        private static int _mapHeight = 900;
        public static int mapHeight
        {
            get { return _mapHeight; }
            // set { _mapHeight = value; }
        }

        private static bool _mouseVisible = true;
        public static bool mouseVisible
        {
            get { return _mouseVisible; }
            // set { _mouseVisible = value; }
        }

        private static bool _isFullScreen = false;
        public static bool isFullScreen
        {
            get { return _isFullScreen; }
            // set { _mouseVisible = value; }
        }


        private static Client _client;
        public static Client client
        {
            get { return _client; }
            set { _client = value; }
        }

        //private static string _IP;
        //public static string IP
        //{
        //    get { return _IP; }
        //    set { _IP = value; }
        //}

        //private static string _Port;
        //public static string Port
        //{
        //    get { return _Port; }
        //    set { _Port = value; }
        //}


        private static Animation _animChargement;
        public static Animation animChargement
        {
            get { return _animChargement; }
            set { _animChargement = value; }
        }
        private static Map _map;
        public static Map map
        {
            get { return _map; }
            set { _map = value; }
        }

        private static GraphicsDeviceManager _graphics;
        public static GraphicsDeviceManager graphics
        {
            get { return _graphics; }
            set { _graphics = value; }
        }
        private static SpriteBatch _spriteBatch;
        public static SpriteBatch spriteBatch
        {
            get { return _spriteBatch; }
            set { _spriteBatch = value; }
        }

        private static Manager _M;
        public static Manager M
        {
            get { return _M; }
            set { _M = value; }
        }

        private static ContentManager _content;
        public static ContentManager content
        {
            get { return _content; }
            set { _content = value; }
        }

        private static Classe _classe;
        public static Classe classe
        {
            get { return _classe; }
            set { _classe = value; }
        }


        public static SpriteFont font;



        public static ChamanAnimation chamanAnimation;
        public static ChamanAnimation mageAnimation;
        public static Dictionary<int, int> heroIdTeam = new Dictionary<int, int>();
    }
}
