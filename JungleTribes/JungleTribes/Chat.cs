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

namespace JungleTribes
{
    class Chat
    {
        bool changementEtatBar = false;
        int valeur_scrollbar = 0;
        List<string> listMessage;
        Button envoyer;
        Panel P;
        Label L;
        TextBox textMessage;
        ScrollBar bar;

        public Chat()
        {
            listMessage = new List<string>();
            initMessagerie();
        }

        public void initMessagerie()
        {
            L = new Label(Global.M);
            L.Init();
            //L.Text = " Test Fenetre \n";
            L.Top = 680;
            L.Left = 15;
            L.Width = 350;
            L.Height = 170;
            L.StayOnTop = true;
            L.DesignMode = false;

            P = new Panel(Global.M);
            P.Init();
            P.Top = L.Top;
            P.Left = L.Left - 5;
            P.Width = L.Width;
            P.Height = L.Height;
            P.StayOnBack = true;
            P.BevelBorder = BevelBorder.All;
            P.BevelMargin = 1;
            P.BevelStyle = BevelStyle.Etched;
            P.Color = new Color(50, 0, 0, 50);
            P.Resizable = true;
            P.Anchor = Anchors.Bottom | Anchors.Top | Anchors.Left;
            P.Resize += new ResizeEventHandler(P_Resize);

            bar = new ScrollBar(Global.M, Orientation.Vertical);
            bar.Init();
            bar.Width = 32;
            bar.Left = P.Width + P.Left;
            bar.Top = P.Top;
            bar.Height = P.Height;
            bar.Anchor = Anchors.Left | Anchors.Top | Anchors.Right;
            bar.Range = 10;
            bar.Value = 10;

            bar.StayOnTop = true;
            bar.ValueChanged += new TomShane.Neoforce.Controls.EventHandler(trkMain_ValueChanged);
            bar.ValidateMove += new MoveEventHandler(bar_ValidateMove);


            envoyer = new Button(Global.M);
            envoyer.Init();
            envoyer.Text = "Envoyer";
            envoyer.Width = 100;
            envoyer.Height = 40;
            envoyer.Top = P.Top + P.Height;
            envoyer.Left = P.Left + P.Width /*+ bar.Width*/- envoyer.Width;
            envoyer.StayOnTop = true;
            envoyer.Click += new TomShane.Neoforce.Controls.EventHandler(envoyerMessage);

            textMessage = new TextBox(Global.M);
            textMessage.Init();
            textMessage.Width = P.Width /*+ bar.Width*/ - envoyer.Width;
            textMessage.Height = envoyer.Height;
            textMessage.Top = P.Top + L.Height;
            textMessage.Left = P.Left;
            textMessage.StayOnTop = true;
            textMessage.KeyDown += new KeyEventHandler(textMessage_KeyDown);

            //M.Add(bar);
            Global.M.Add(P);
            Global.M.Add(L);
            Global.M.Add(envoyer);
            Global.M.Add(textMessage);
        }

        public void bar_ValidateMove(object sender, TomShane.Neoforce.Controls.EventArgs e)
        {

        }
        public void trkMain_ValueChanged(object sender, TomShane.Neoforce.Controls.EventArgs e)
        {
            if (listMessage.Count > 10)
            {
                valeur_scrollbar--;
                if (!changementEtatBar) afficherChat();
            }

        }
        public void P_Resize(object sender, TomShane.Neoforce.Controls.EventArgs e)
        {
            envoyer.Top = P.Top + P.Height;
            envoyer.Left = P.Left + P.Width /*+ bar.Width*/ - envoyer.Width;
            textMessage.Top = P.Top + P.Height;
            textMessage.Left = P.Left;
            textMessage.Width = P.Width /*+ bar.Width*/ - envoyer.Width;
            L.Top = P.Top;
            L.Left = P.Left;
            L.Width = P.Width;
            L.Height = P.Height;
            bar.Left = P.Width + P.Left;
            bar.Top = P.Top;
            bar.Height = P.Height;
        }
        public void textMessage_KeyDown(object sender, TomShane.Neoforce.Controls.EventArgs e)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                envoyerMessage(sender, e);
            }
        }

        public void envoyerMessage(object sender, TomShane.Neoforce.Controls.EventArgs e)
        {
            listMessage.Add(textMessage.Text + "\n");
            L.Text = "";
            afficherChat();

            //L.Text += textMessage.Text + "\n";
            textMessage.Text = "";
        }

        public void afficherChat()
        {
            int length = listMessage.Count;
            int startIndex = 0;
            if (length > 10)
            {
                // changementEtatBar = false;
                startIndex = length - 10;
                //bar.Range = 10 * ((int)(length / 10) + 1);
                //changementEtatBar = true;
                //bar.Value = bar.Range;
                // changementEtatBar = false;
            }

            for (int i = startIndex + valeur_scrollbar; i <= listMessage.Count - 1 + valeur_scrollbar; i++)
            {
                string nonVerifMessage = "id" + i + " " + listMessage[i];
                int tailleChaine = nonVerifMessage.Length;
                if (tailleChaine >= 45)
                {
                    int step = 45;
                    int coef = 0;
                    while ((coef + 1) * step < tailleChaine)
                    {
                        L.Text += nonVerifMessage.Substring(coef * step, step) + "\n";
                        coef++;
                    }
                    L.Text += nonVerifMessage.Substring(coef * step, tailleChaine - (coef * step));

                }
                else L.Text += nonVerifMessage;
            }
        }
    }
}
