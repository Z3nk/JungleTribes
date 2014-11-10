using System;
using System.Collections.Generic;
using System.Linq;
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
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {

        bool up, right, down, left, skill1, skill2, skill3, skill4, skill5, skill6;
        byte[] prevCommand = new byte[16];

        //Texture 2D
        Texture2D man;
        long previousTime = 0;
        float delta = 0;
        int _total_frames = 0;
        float _elapsed_time = 0.0f;
        int _fps = 0;
        GameDrawer gameDrawer;
        

        // Variables pilote de type booléene
        private bool gameInit = false;
        private static bool menu = true, exit = false;
        
        float[] skilltimer = new float[6];
        
        
        // Polices
        
        // Musique du menu
        private static Song musiqueMenu;

        
        #region METHODES D INITIALISATIONS
        public Game1()
        {
            Global.graphics = new GraphicsDeviceManager(this);
            Global.content = Content;
            Content.RootDirectory = "Content";
            
        }

        protected override void Initialize()
        {
            // Met à jours la fenêtre selon nos règle imposé dans la méthode initFenetre()
            initFenetre();
            // Instancie le constructeur avec le skin "Green"
            Global.M = new Manager(this, Global.graphics, "Green");
            Global.M.Initialize();
            Global.spriteBatch = new SpriteBatch(GraphicsDevice);
            Global.font = Content.Load<SpriteFont>("Police/font");
            Global.bonhommeTestAnimation = new BonhommeTestAnimation(Content.Load<Texture2D>("Image//jeu//Element//bonhomme"));
            Global.guerrierAnimation = new GuerrierAnimation(Content.Load<Texture2D>("Image//jeu//Element//Guerrier3"));
            Global.chamanAnimation = new ChamanAnimation(Content.Load<Texture2D>("Image//jeu//Element//chaman"));
            Global.mageAnimation = new ChamanAnimation(Content.Load<Texture2D>("Image//jeu//Element//mage"));
            for (int i = 0; i < 6; i++)
            {
                skilltimer[i] = 0;
            }
            // Outil permettant d'afficher les FPS    
            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Initialise la musique du menu, hommage aux loup, décédé avant même que le projet ne commence ...
            musiqueMenu = Global.content.Load<Song>("Musique/MusiqueMenu");
            man = Content.Load<Texture2D>("Image//jeu//Element//bonhomme");
            Global.mage_bullet = Content.Load<Texture2D>("Image//jeu//Element//mage_bullet");
            Global.mage_blizzard = Content.Load<Texture2D>("Image//jeu//Element//blizzard");
            Global.mage_foudre = Content.Load<Texture2D>("Image//jeu//Element//foudre");
            Global.mage_fire = Content.Load<Texture2D>("Image//jeu//Element//mage_fire");
            Global.mage_explosion = Content.Load<Texture2D>("Image//jeu//Element//explosion");
            Global.hp_bar = Content.Load<Texture2D>("Image//jeu//Element//hp");
            Global.chaman_soin = Content.Load<Texture2D>("Image//jeu//Element//soin");
            Global.chaman_blocage = Content.Load<Texture2D>("Image//jeu//Element//blocage");
            Global.chaman_aide = Content.Load<Texture2D>("Image//jeu//Element//aide");
            Global.warrior_Attack = Content.Load<Texture2D>("Image//jeu//Element//coupepee");
            Global.Shield = Content.Load<Texture2D>("Image//jeu//Element//Shield");
            Global.Charge = Content.Load<Texture2D>("Image//jeu//Element//charge");
            Global.EP = Content.Load<Texture2D>("Image//jeu//Element//sword");
            Global.TourbiLol = Content.Load<Texture2D>("Image//jeu//Element//tourbilol");
            Global.lifebar = Content.Load<Texture2D>("Image//jeu//Element//lifebar");
            Global.teleport = Content.Load<Texture2D>("Image//jeu//Element//teleport");
            Global.benediction = Content.Load<Texture2D>("Image//jeu//Element//benediction");
            // Initialise le menu dans un premier temps, permettant au joueur de faire ses choix
            Menu Menu = new Menu();
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        #endregion
        #region METHODES CORPS DE LA CLASSE
        protected override void Update(GameTime gameTime)
        {
            if(previousTime != 0)
                delta = ((float)(DateTime.Now.Ticks - previousTime)) / 10000;
            previousTime = DateTime.Now.Ticks;
            #region Quitter partie
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            if (exit) base.Exit();
            #endregion
            Global.M.Update(gameTime);
            if(!menu)
            {
                _elapsed_time += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                if (_elapsed_time >= 1000.0f)
                {
                    _fps = _total_frames;
                    _total_frames = 0;
                    _elapsed_time = 0;
                }
                // Ici nous faisons nos manipulations sur l'update
                UpdateGame(gameTime);
            }


            base.Update(gameTime);
        }

        /// <summary>
        /// Ici nous gérons l'update du jeu
        /// </summary>
        /// <param name="gameTime">Temps de jeu</param>
        private void UpdateGame(GameTime gameTime)
        {
            if (!gameInit && Global.client.haveMoreMessage())
            {
                MessageNet m = Global.client.deQueueMessage();
                if (m is MessageChat && ((MessageChat)m).text.Equals("start"))
                {
                    initGame();
                }
            }
            if(gameInit)
            {
                KeyboardState kbState = Keyboard.GetState();
                MouseState M = Mouse.GetState();

                if (kbState.IsKeyDown(Keys.Z)) up = true;
                else up = false;
                if (kbState.IsKeyDown(Keys.Q)) left = true;
                else left = false;
                if (kbState.IsKeyDown(Keys.S)) down = true;
                else down = false;
                if (kbState.IsKeyDown(Keys.D)) right = true;
                else right = false;
                if (M.LeftButton == ButtonState.Pressed)skill1 = true;
                else skill1 = false;
                if (M.RightButton == ButtonState.Pressed) skill2 = true;
                else skill2 = false;
                if (kbState.IsKeyDown(Keys.D1)) skill3 = true;
                else skill3 = false;
                if (kbState.IsKeyDown(Keys.D2)) skill4 = true;
                else skill4 = false;
                if (kbState.IsKeyDown(Keys.D3)) skill5 = true;
                else skill5 = false;
                if (kbState.IsKeyDown(Keys.D4)) skill6 = true;
                else skill6 = false;

                MessageUserCommand m = new MessageUserCommand(left, up, right, down, skill1, skill2, skill3, skill4, skill5, skill6, new Vector2(M.X, M.Y));
                if (!prevCommand.SequenceEqual(m.bytes))
                {
                    Global.client.sendUdpMessage(m);
                }
                prevCommand = m.bytes;

                while (Global.client.haveMoreMessage())
                {
                    MessageNet m2 = Global.client.deQueueMessage();
                    if (m2 is MessageUpdateElements)
                        Global.clientListElements.updateFromMessage((MessageUpdateElements)m2);
                    else if (m2 is MessageChat)
                    {
                        string message = ((MessageChat)m2).text;
                        if (message[0] == 'H' &&
                            message[1] == 'e' &&
                            message[2] == 'r' &&
                            message[3] == 'o' &&
                            message[4] == ' ' &&
                            message[5] == ':' &&
                            message[6] == ' ')
                        {
                            string[] s = message.Split(' ');
                            int id = Convert.ToInt32(s[2]);
                            Global.heroId = id;
                        }
                    }
                    else if (m2 is MessageSkillLaunched)
                    {
                        byte id = ((MessageSkillLaunched)m2).id;
                        if (Global.clientListElements.list.ContainsKey(Global.heroId))
                        {
                            Hero h = (Hero)Global.clientListElements.list[Global.heroId];
                            skilltimer[id] = h.skills[id].blockingTime + h.skills[id].cooldown;
                        }
                    }
                    else if (m2 is MessageHeroTeam)
                    {
                        MessageHeroTeam mht = (MessageHeroTeam)m2;
                        for (int i = 0; i < mht.nbElem; i++)
                        {
                            Global.heroIdTeam.Add(mht.readNext(), mht.readNext());
                        }
                    }
                }

                Global.clientListElements.update(delta);

            }

            
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
           
            Global.M.BeginDraw(gameTime);
            Global.M.Draw(gameTime);
            if(!menu)
            {
                _total_frames++;
                Global.spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);

                
                if (gameInit)
                {
                    DrawGame();
                }
                else
                {
                    Global.animChargement.Draw();
                }
                Global.spriteBatch.DrawString(Global.font, string.Format("FPS={0}", _fps),
               new Vector2(10.0f, 20.0f), Color.Red, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
                Global.spriteBatch.DrawString(Global.font, string.Format("Latence={0}", Global.client.latency),
               new Vector2(10.0f, 30.0f), Color.Red, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
                Global.spriteBatch.DrawString(Global.font, string.Format("DELTA={0}", delta),
               new Vector2(10.0f, 45.0f), Color.Red, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
                if(Global.clientListElements != null)
                Global.spriteBatch.DrawString(Global.font, string.Format("ELEMNUMBER={0}", Global.clientListElements.elemNumber),
               new Vector2(10.0f, 60.0f), Color.Red, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
                
                if (Global.clientListElements != null)
                {
                    if (Global.clientListElements.list.ContainsKey(Global.heroId))
                    {
                        Hero h = (Hero)Global.clientListElements.list[Global.heroId];
                        for (int i = 0; i < h.skills.Count; i++)
                        {
                            Global.spriteBatch.DrawString(Global.font, h.skills[i].name,
                                    new Vector2(400.0f + 170 * i, 700.0f), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
                            if (skilltimer[i] > 0)
                            {
                                skilltimer[i] -= delta;
                                Global.spriteBatch.DrawString(Global.font, "" + ((int)(skilltimer[i]/1000) + 1),
                                    new Vector2(400.0f + 170 * i, 740.0f), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
                            }
                            else
                                Global.spriteBatch.DrawString(Global.font, "Pret",
                                    new Vector2(400.0f + 170 * i, 740.0f), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);

                        }
                    }
                }
                Global.spriteBatch.End();

            }
            Global.M.EndDraw();
            
           
            base.Draw(gameTime);
        }

        public void DrawGame()
        {
            Global.map.Draw();
            gameDrawer.draw();
        }
        #endregion
        #region METHODES OUTILS

        /// <summary>
        /// Permet d'activer la musique du menu
        /// </summary>
        public static void activerMusiqueMenu()
        {
            MediaPlayer.Volume /= 2;
            //MediaPlayer.Play(musiqueMenu);            
        }

        /// <summary>
        /// Permet de désactivé la musique du menu
        /// </summary>
        public static void stopMusiqueMenu()
        {
            MediaPlayer.Stop();
        }
        /// <summary>
        /// Permet de passer en mode jeu
        /// </summary>
        public static void unsetMenu()
        {
            menu = false;
            #region Suppression Menu
            List<Control> listControl = new List<Control>(Global.M.Controls);
            foreach (Control B in listControl)
                Global.M.Remove(B);
            
            listControl = null;

            //Global.M = null;
            #endregion
        }

        /// <summary>
        /// Quitter
        /// </summary>
        public static void setExit()
        {
            Game1.stopMusiqueMenu();
            exit = true;
        }

        /// <summary>
        /// Initialise le jeu
        /// </summary>
        public void initFenetre()
        {
            Global.graphics.SynchronizeWithVerticalRetrace = true;
            Global.graphics.IsFullScreen = Global.isFullScreen;
            Global.graphics.PreferredBackBufferHeight = Global.mapHeight;
            Global.graphics.PreferredBackBufferWidth = Global.mapWidth;
            this.Window.AllowUserResizing = true;
            this.IsMouseVisible = Global.mouseVisible;
            Global.graphics.ApplyChanges();
        }

        
        /// <summary>
        /// Initialise les classes permettant au jeu de fonctionner
        /// </summary>
        /// 
        public void initGame()
        {
            // Chaque éléments qui nous ont permis à faire le menu vont être supprimé
            
            //Client C = new Client(IPAddress.Parse(Global.IP), Convert.ToInt32(Global.Port), "Zunk");
            //C.start();
            //bool continuer=true;
            //while (continuer)
            //{
            //    if (C.haveMoreMessage())
            //    {
            //        MessageNet m = C.deQueueMessage();
            //        if (m is MessageChat && ((MessageChat)m).text.Equals("start"))
            //        {
            //            continuer = false;
            //        }
            //    }
            //}
            // Instancification de tout le contenu de base du début de jeu
            #region Instancification Jeu
            stopMusiqueMenu();
            Chat Chat = new Chat();            
            Global.map = new Map();
            gameInit = true;
            Global.clientListElements = new ClientListElements();
            Element.currentListElements = Global.clientListElements;
            gameDrawer = new GameDrawer(Global.clientListElements);
            #endregion

        }
        #endregion
    }
}
