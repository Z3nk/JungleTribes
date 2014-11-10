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
using System.Threading;

namespace JungleTribes
{

    class Menu
    {
        private TextBox IP;
        private TextBox PORT;
        private TextBox pseudo;
        private const int TAILLEY_SUPPORT_BUTTON = 155;
        private const int TAILLEX_SUPPORT_BUTTON = 200;
        private const int SUPPORT_BUTTON_Y = 900 - TAILLEY_SUPPORT_BUTTON;
        private const int CENTRAGE_SUR_SUPPORT_BUTTON = 40;
        private const int DECALAGE_SUR_SUPPORT_BUTTON = 100;
        private List<Button> listButton;
        private ImageBox fond;
        private Window fenetre_Configuration;
        private Window fenetre_Lancer_Jeu;
        private Window selectionHero;
        private Window selectionTeam;
        private TextBox entreeTeam;

        public Menu()
        {
            Game1.activerMusiqueMenu();
            listButton = new List<Button>();
            initGraphics();
            initButtonPilier();
            initFenetreConfiguration();
            initFenetreLancerJeu();
        }




        public void LancerPartie(object sender, TomShane.Neoforce.Controls.EventArgs e)
        {
            fenetre_Lancer_Jeu.Show();
            // Game1.setMenu();
        }

        public void Option(object sender, TomShane.Neoforce.Controls.EventArgs e)
        {
            fenetre_Configuration.Show();
        }

        public void Quitter(object sender, TomShane.Neoforce.Controls.EventArgs e)
        {
            Game1.setExit();
        }

        public void Appliquer(object sender, TomShane.Neoforce.Controls.EventArgs e)
        {
            // TODO
            // Appliquer choix configuration utilisateur
        }

        public void Retour(object sender, TomShane.Neoforce.Controls.EventArgs e)
        {
            fenetre_Configuration.Hide();
        }

        public void OnGuerrier(object sender, TomShane.Neoforce.Controls.EventArgs e)
        {
            Global.classe = Classe.guerrier;
            //StartGame();
            ChoiceTeam();
        }

        public void OnChaman(object sender, TomShane.Neoforce.Controls.EventArgs e)
        {
            Global.classe = Classe.chaman;
            //StartGame();
            ChoiceTeam();
        }

        public void OnSorcier(object sender, TomShane.Neoforce.Controls.EventArgs e)
        {
            Global.classe = Classe.mage;
            //StartGame();
            ChoiceTeam();
        }

        private void ChoiceTeam()
        {
            selectionTeam = new Window(Global.M);
            selectionTeam.Init();
            selectionTeam.Text = "Selection Team";
            selectionTeam.Top = Global.graphics.PreferredBackBufferHeight / 4;
            selectionTeam.Left = (int)(Global.graphics.PreferredBackBufferWidth / 2.75);
            selectionTeam.Width = (int)(Global.graphics.PreferredBackBufferWidth / 3);
            selectionTeam.Height = (int)(Global.graphics.PreferredBackBufferHeight / 5);
            selectionHero.Hide();


            Label label_entree = new Label(Global.M);
            label_entree.Text = "Choix de team : ";
            label_entree.Init();
            label_entree.Left = 0;
            label_entree.Top = 0;
            label_entree.Width = 200;
            label_entree.Height = selectionTeam.Height / 2;
            label_entree.Passive = true;
            label_entree.Visible = true;
            label_entree.StayOnTop = true;

            Panel panel_entree = new Panel(Global.M);
            panel_entree.Init();
            panel_entree.Left = 0;
            panel_entree.Top = selectionTeam.Height / 4; ;
            panel_entree.Width = label_entree.Width;
            panel_entree.Height = label_entree.Height;
            panel_entree.Passive = true;
            panel_entree.BevelBorder = BevelBorder.All;
            panel_entree.BevelMargin = 1;
            panel_entree.BevelStyle = BevelStyle.Etched;
            panel_entree.Visible = true;
            panel_entree.TextColor = Color.Red;
            panel_entree.Color = new Color(0, 0, 0, 200);
            panel_entree.StayOnTop = true;
            //panel_entree_IP.Anchor = Anchors.Left | Anchors.Top | Anchors.Right;
            panel_entree.Add(label_entree);
            selectionTeam.Add(panel_entree);

            entreeTeam = new TextBox(Global.M);
            entreeTeam.Init();
            entreeTeam.Top = selectionTeam.Height / 4;
            entreeTeam.Text = "1";
            entreeTeam.Left = panel_entree.Width + 25;
            entreeTeam.Width = panel_entree.Width / 3;
            entreeTeam.Height = panel_entree.Height;
            entreeTeam.Color = new Color(255, 255, 255);
            selectionTeam.Add(entreeTeam);

            Button Appliquer_button = new Button(Global.M);
            Appliquer_button.Init();
            Appliquer_button.Text = "Appliquer";
            Appliquer_button.Width = 150;
            Appliquer_button.Height = 35;
            Appliquer_button.Top = 10;
            Appliquer_button.Left = selectionTeam.Width - Appliquer_button.Width - 50;
            Appliquer_button.Click += new TomShane.Neoforce.Controls.EventHandler(StartGame);
            selectionTeam.Add(Appliquer_button);

            Button retour_button = new Button(Global.M);
            retour_button.Init();
            retour_button.Text = "Retour";
            retour_button.Width = 150;
            retour_button.Height = 35;
            retour_button.Top = 55;
            retour_button.Left = selectionTeam.Width - Appliquer_button.Width - 50;
            retour_button.Click += new TomShane.Neoforce.Controls.EventHandler(RetourTeam);
            selectionTeam.Add(retour_button);

            selectionTeam.Show();
            Global.M.Add(selectionTeam);
        }

        public void StartGame(object sender, TomShane.Neoforce.Controls.EventArgs e)
        {
            Global.client = new Client(IPAddress.Parse(IP.Text), Convert.ToInt32(PORT.Text), pseudo.Text);
            Global.client.start();
            Thread.Sleep(1000);
            Global.client.sendTcpMessage(new MessageSelectionHero(Global.classe, Convert.ToInt32(entreeTeam.Text)));
            Global.animChargement = new Animation(Global.content.Load<Texture2D>("Image\\Menu\\Loading"), 1, 8, 816 , 102, 0.2, Global.mapWidth / 2 - ((816 / 8) / 2), (Global.mapHeight / 2) - 51);
            Game1.unsetMenu();
        }
        public void AppliquerIP(object sender, TomShane.Neoforce.Controls.EventArgs e)
        {
            selectionHero = new Window(Global.M);
            selectionHero.Init();
            selectionHero.Text = "Selection Hero";
            selectionHero.Top = Global.graphics.PreferredBackBufferHeight / 4;
            selectionHero.Left = (int)(Global.graphics.PreferredBackBufferWidth / 2.75);
            selectionHero.Width = (int)(Global.graphics.PreferredBackBufferWidth / 3);
            selectionHero.Height = (int)(Global.graphics.PreferredBackBufferHeight / 5);
            fenetre_Lancer_Jeu.Hide();


            int step = selectionHero.Width / 4;

            Button Guerrier = new Button(Global.M);
            Guerrier.Init();
            Guerrier.Text = "Guerrier";
            Guerrier.Width = 150;
            Guerrier.Height = 50;
            Guerrier.Top = selectionHero.Height / 2 - Guerrier.Height;
            Guerrier.Left = step - Guerrier.Width / 2 - 20;
            Guerrier.Click += new TomShane.Neoforce.Controls.EventHandler(OnGuerrier);
            selectionHero.Add(Guerrier);

            Button Chaman = new Button(Global.M);
            Chaman.Init();
            Chaman.Text = "Chaman";
            Chaman.Width = 150;
            Chaman.Height = 50;
            Chaman.Top = selectionHero.Height / 2 - Chaman.Height;
            Chaman.Left = (step * 2) - Chaman.Width / 2;
            Chaman.Click += new TomShane.Neoforce.Controls.EventHandler(OnChaman);
            selectionHero.Add(Chaman);

            Button Sorcier = new Button(Global.M);
            Sorcier.Init();
            Sorcier.Text = "Sorcier";
            Sorcier.Width = 150;
            Sorcier.Height = 50;
            Sorcier.Top = selectionHero.Height / 2 - Sorcier.Height;
            Sorcier.Left = (step * 3) - Sorcier.Width / 2 + 20;
            Sorcier.Click += new TomShane.Neoforce.Controls.EventHandler(OnSorcier);
            selectionHero.Add(Sorcier);


            selectionHero.Show();
            Global.M.Add(selectionHero);
        }

        public void RetourIP(object sender, TomShane.Neoforce.Controls.EventArgs e)
        {
            fenetre_Lancer_Jeu.Hide();
        }

        public void RetourTeam(object sender, TomShane.Neoforce.Controls.EventArgs e)
        {
            selectionTeam.Hide();
        }

        public void placerCoupleButtonLabelConfiguration(Window fenetre, int left, int top, int width, int height, string text, string textInput, int coefDiminution)
        {
            Label label_entree = new Label(Global.M);
            label_entree.Text = "        " + text;
            label_entree.Init();
            label_entree.Left = 0;
            label_entree.Top = 0;
            label_entree.Width = width;
            label_entree.Height = height;
            label_entree.Passive = true;
            label_entree.Visible = true;
            label_entree.StayOnTop = true;

            Panel panel_entree = new Panel(Global.M);
            panel_entree.Init();
            panel_entree.Left = left;
            panel_entree.Top = top;
            panel_entree.Width = label_entree.Width;
            panel_entree.Height = label_entree.Height;
            panel_entree.Passive = true;
            panel_entree.BevelBorder = BevelBorder.All;
            panel_entree.BevelMargin = 1;
            panel_entree.BevelStyle = BevelStyle.Etched;
            panel_entree.Visible = true;
            panel_entree.TextColor = Color.Red;
            panel_entree.Color = new Color(0, 0, 0, 200);
            panel_entree.StayOnTop = true;
            //panel_entree_IP.Anchor = Anchors.Left | Anchors.Top | Anchors.Right;
            panel_entree.Add(label_entree);
            fenetre.Add(panel_entree);

            TextBox entree = new TextBox(Global.M);
            entree.Init();
            entree.Top = top;
            entree.Text = textInput;
            entree.Left = panel_entree.Width + 25;
            entree.Width = panel_entree.Width / coefDiminution;
            entree.Height = panel_entree.Height;
            entree.Color = new Color(255, 255, 255);
            fenetre.Add(entree);
        }

        public void placerCoupleButtonLabelIP(Window fenetre, int left, int top, int width, int height, string text, string textInput, int coefDiminution)
        {
            Label label_entree = new Label(Global.M);
            label_entree.Text = "        " + text;
            label_entree.Init();
            label_entree.Left = 0;
            label_entree.Top = 0;
            label_entree.Width = width;
            label_entree.Height = height;
            label_entree.Passive = true;
            label_entree.Visible = true;
            label_entree.StayOnTop = true;

            Panel panel_entree = new Panel(Global.M);
            panel_entree.Init();
            panel_entree.Left = left;
            panel_entree.Top = top;
            panel_entree.Width = label_entree.Width;
            panel_entree.Height = label_entree.Height;
            panel_entree.Passive = true;
            panel_entree.BevelBorder = BevelBorder.All;
            panel_entree.BevelMargin = 1;
            panel_entree.BevelStyle = BevelStyle.Etched;
            panel_entree.Visible = true;
            panel_entree.TextColor = Color.Red;
            panel_entree.Color = new Color(0, 0, 0, 200);
            panel_entree.StayOnTop = true;
            //panel_entree_IP.Anchor = Anchors.Left | Anchors.Top | Anchors.Right;
            panel_entree.Add(label_entree);
            fenetre.Add(panel_entree);

            TextBox entree = new TextBox(Global.M);
            entree.Init();
            entree.Top = top;
            if (textInput.Equals("IP") || textInput.Equals("PORT") || textInput.Equals("Pseudo"))
            {
                if (textInput.Equals("IP")) IP = entree;
                else if (textInput.Equals("PORT")) PORT = entree;
                else pseudo = entree;
                entree.Text = "";
            }
            else
                entree.Text = textInput;
            entree.Left = panel_entree.Width + 25;
            entree.Width = panel_entree.Width / coefDiminution;
            entree.Height = panel_entree.Height;
            entree.Color = new Color(255, 255, 255);
            fenetre.Add(entree);
        }
        private void initGraphics()
        {
            // Initialisation du fond d ecran
            fond = new ImageBox(Global.M);
            fond.Text = "fond d'ecran";
            fond.Top = 0;
            fond.Left = 0;
            fond.Width = Global.graphics.PreferredBackBufferWidth;
            fond.Height = Global.graphics.PreferredBackBufferHeight;
            fond.Image = Global.M.Content.Load<Texture2D>("Content\\Image\\Menu\\font");
            fond.Init();
            fond.StayOnBack = true;
            Global.M.Add(fond);

            // Initalisation des supports boutons
            int decalage = 0;
            for (int i = 0; i < Global.graphics.PreferredBackBufferWidth / TAILLEX_SUPPORT_BUTTON; i++)
            {
                ImageBox sup_Button = new ImageBox(Global.M);
                sup_Button.Text = "support_Button";
                sup_Button.Top = SUPPORT_BUTTON_Y;
                sup_Button.Left = 0 + decalage;
                sup_Button.Width = TAILLEX_SUPPORT_BUTTON;
                sup_Button.Height = TAILLEY_SUPPORT_BUTTON;
                sup_Button.Image = Global.M.Content.Load<Texture2D>("Content\\Image\\Menu\\support_Button");
                sup_Button.Init();
                sup_Button.StayOnTop = true;
                sup_Button.Passive = true;
                Global.M.Add(sup_Button);
                decalage += TAILLEX_SUPPORT_BUTTON;
            }
            decalage = 0;
            for (int i = 0; i < Global.graphics.PreferredBackBufferWidth / TAILLEX_SUPPORT_BUTTON; i++)
            {
                ImageBox sup_Button = new ImageBox(Global.M);
                sup_Button.Text = "support_Button";
                sup_Button.Top = 0;
                sup_Button.Left = 0 + decalage;
                sup_Button.Width = TAILLEX_SUPPORT_BUTTON;
                sup_Button.Height = TAILLEY_SUPPORT_BUTTON;
                sup_Button.Image = Global.M.Content.Load<Texture2D>("Content\\Image\\Menu\\support_Button_haut");
                sup_Button.Init();
                sup_Button.StayOnTop = true;
                sup_Button.Passive = true;
                Global.M.Add(sup_Button);
                decalage += TAILLEX_SUPPORT_BUTTON;
            }
            ImageBox titre_Milieu = new ImageBox(Global.M);
            titre_Milieu.Text = "titre";
            titre_Milieu.Top = Global.graphics.PreferredBackBufferHeight / 3;
            titre_Milieu.Left = Global.graphics.PreferredBackBufferWidth / 3;
            titre_Milieu.Width = 600;
            titre_Milieu.Height = 255;
            titre_Milieu.Image = Global.M.Content.Load<Texture2D>("Content\\Image\\Menu\\titre1");
            titre_Milieu.Init();
            titre_Milieu.StayOnTop = true;
            titre_Milieu.Passive = true;
            Global.M.Add(titre_Milieu);

        }
        public void initConfigurationBoutons()
        {
            // entrée clavier pour action a completer avec la classe qui s'en occupe
            placerCoupleButtonLabelConfiguration(fenetre_Configuration, 0, 10, 150, 35, "Avancer", "Z", 3);
            placerCoupleButtonLabelConfiguration(fenetre_Configuration, 0, 55, 150, 35, "Reculer", "Q", 3);
            placerCoupleButtonLabelConfiguration(fenetre_Configuration, 0, 100, 150, 35, "Gauche", "S", 3);
            placerCoupleButtonLabelConfiguration(fenetre_Configuration, 0, 145, 150, 35, "Droite", "D", 3);
            placerCoupleButtonLabelConfiguration(fenetre_Configuration, 0, 190, 150, 35, "Sort 1", "1", 3);
            placerCoupleButtonLabelConfiguration(fenetre_Configuration, 0, 235, 150, 35, "Sort 2", "2", 3);
            placerCoupleButtonLabelConfiguration(fenetre_Configuration, 0, 280, 150, 35, "Sort 3", "3", 3);
            placerCoupleButtonLabelConfiguration(fenetre_Configuration, 0, 325, 150, 35, "Sort 4", "4", 3);
            placerCoupleButtonLabelConfiguration(fenetre_Configuration, 0, 370, 150, 35, "Sort 5", "5", 3);

            // Creation du bouton appliquer et retour
            Button Appliquer_button = new Button(Global.M);
            Appliquer_button.Init();
            Appliquer_button.Text = "Appliquer";
            Appliquer_button.Width = 150;
            Appliquer_button.Height = 35;
            Appliquer_button.Top = 10;
            Appliquer_button.Left = fenetre_Configuration.Width - Appliquer_button.Width - 50;
            Appliquer_button.Click += new TomShane.Neoforce.Controls.EventHandler(Appliquer);
            fenetre_Configuration.Add(Appliquer_button);

            Button retour_button = new Button(Global.M);
            retour_button.Init();
            retour_button.Text = "Retour";
            retour_button.Width = 150;
            retour_button.Height = 35;
            retour_button.Top = 55;
            retour_button.Left = fenetre_Configuration.Width - Appliquer_button.Width - 50;
            retour_button.Click += new TomShane.Neoforce.Controls.EventHandler(Retour);
            fenetre_Configuration.Add(retour_button);

        }


        public void initConfigurationLancerJeu()
        {
            // entrée clavier pour action a completer avec la classe qui s'en occupe
            placerCoupleButtonLabelIP(fenetre_Lancer_Jeu, 0, 10, 150, 35, "IP", "IP", 1);
            placerCoupleButtonLabelIP(fenetre_Lancer_Jeu, 0, 55, 150, 35, "PORT", "PORT", 1);
            placerCoupleButtonLabelIP(fenetre_Lancer_Jeu, 0, 100, 150, 35, "Pseudo", "Pseudo", 1);


            // Creation du bouton appliquer et retour
            Button Appliquer_button = new Button(Global.M);
            Appliquer_button.Init();
            Appliquer_button.Text = "Appliquer";
            Appliquer_button.Width = 150;
            Appliquer_button.Height = 35;
            Appliquer_button.Top = 10;
            Appliquer_button.Left = fenetre_Lancer_Jeu.Width - Appliquer_button.Width - 50;
            Appliquer_button.Click += new TomShane.Neoforce.Controls.EventHandler(AppliquerIP);
            fenetre_Lancer_Jeu.Add(Appliquer_button);

            Button retour_button = new Button(Global.M);
            retour_button.Init();
            retour_button.Text = "Retour";
            retour_button.Width = 150;
            retour_button.Height = 35;
            retour_button.Top = 55;
            retour_button.Left = fenetre_Lancer_Jeu.Width - Appliquer_button.Width - 50;
            retour_button.Click += new TomShane.Neoforce.Controls.EventHandler(RetourIP);
            fenetre_Lancer_Jeu.Add(retour_button);

        }

        public void initFenetreLancerJeu()
        {
            fenetre_Lancer_Jeu = new Window(Global.M);
            fenetre_Lancer_Jeu.Init();
            fenetre_Lancer_Jeu.Text = "Option";
            fenetre_Lancer_Jeu.Top = Global.graphics.PreferredBackBufferHeight / 4;
            fenetre_Lancer_Jeu.Left = (int)(Global.graphics.PreferredBackBufferWidth / 2.75);
            fenetre_Lancer_Jeu.Width = (int)(Global.graphics.PreferredBackBufferWidth / 3);
            fenetre_Lancer_Jeu.Height = (int)(Global.graphics.PreferredBackBufferHeight / 5);

            initConfigurationLancerJeu();
            fenetre_Lancer_Jeu.Hide();
            Global.M.Add(fenetre_Lancer_Jeu);
        }

        public void initFenetreConfiguration()
        {
            fenetre_Configuration = new Window(Global.M);
            fenetre_Configuration.Init();
            fenetre_Configuration.Text = "Configuration";
            fenetre_Configuration.Top = Global.graphics.PreferredBackBufferHeight / 4;
            fenetre_Configuration.Left = (int)(Global.graphics.PreferredBackBufferWidth / 2.75);
            fenetre_Configuration.Width = (int)(Global.graphics.PreferredBackBufferWidth / 3.5);
            fenetre_Configuration.Height = Global.graphics.PreferredBackBufferHeight / 2;

            initConfigurationBoutons();
            fenetre_Configuration.Hide();
            Global.M.Add(fenetre_Configuration);
        }
        public void initButtonPilier()
        {
            for (int i = 0; i < 3; i++)
            {
                listButton.Add(new Button(Global.M));
                listButton[i].Width = 200;
                listButton[i].Height = 50;
                listButton[i].Left = Global.graphics.PreferredBackBufferHeight / 3 + (TAILLEX_SUPPORT_BUTTON * 2) * i;//graphics.PreferredBackBufferWidth / 2 - listButton[i].Width / 2;
                listButton[i].Top = SUPPORT_BUTTON_Y + CENTRAGE_SUR_SUPPORT_BUTTON;//graphics.PreferredBackBufferHeight / 3 + (listButton[i].Height * 2) * i;
                listButton[i].StayOnTop = true;
                switch (i)
                {
                    case 0:
                        listButton[i].Text = "Jeu";
                        listButton[i].Click += new TomShane.Neoforce.Controls.EventHandler(LancerPartie);
                        break;
                    case 1:
                        listButton[i].Text = "Configuration";
                        listButton[i].Click += new TomShane.Neoforce.Controls.EventHandler(Option);
                        break;

                    case 2:
                        listButton[i].Text = "Quitter";
                        listButton[i].Click += new TomShane.Neoforce.Controls.EventHandler(Quitter);
                        break;
                }

                Global.M.Add(listButton[i]);
            }
        }
    }
}
