using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.IO;
namespace music_sword
{
    public partial class Form1 : Form
    {
        private Player player;
        private Wave wave ;
        private Menu myMenu;

        private WMPLib.WindowsMediaPlayer WMP = new WMPLib.WindowsMediaPlayer();
        private int counter;

        private bool control = false;
        private bool menu = true;
        private bool edit = false;
        private bool fail = false;
        private bool restart = false;
        private bool pause = true;
        private bool chouseMusic = false;

        private int background = -1;
        private int fire = 2;
        private int blood = 1;


        private int X, Y = 0;

        private List<Monster> playerWave = new List<Monster> { };
        
        public Form1()
        {
            InitializeComponent();
            Init();
            WMP.URL = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName+"/music.wav";
            WMP.controls.pause();
            Invalidate(); 
        }
        public void Init()
        {
            myMenu = new Menu(menu);
            Cursor.Position = new Point(500, 500);
            player = new Player(225, 216);
            wave = new Wave(player);


            timer1.Interval = 10;
            timer1.Tick += new EventHandler(update);
            timer1.Start();
        }

        private void update(object sender, EventArgs e)
        {

            if (counter == 0)
                WMP.controls.play();
            counter++;

            MouseCollision();
            if (!pause)
                if(!edit)            
                    player.Animated();
            if (pause)
                WMP.controls.pause();
            if(menu)
                WMP.controls.pause();
            
            
            if (player.health < 0)
                fail = true;
            if(fail)
            {
                player.health = 100;
                wave = new Wave(player);
                background = -1;
                fire = 2;
                pause = false;
                WMP.controls.stop();
            }
            if (restart)
            {
                fail = false;
                player.health = 100;
                wave = new Wave(player);
                background = -1;
                fire = 2;
                restart = false;
                menu = false;
                pause = false;
                myMenu.start = false;
                WMP.controls.stop();
                counter = 0;
            }
            Invalidate();
        }

        private void OnPaint(object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            if (menu)
                myMenu.DrawMenu(graphics);
            else
            {
                if (chouseMusic)
                {
                    graphics.DrawImage(Image.FromFile(@"..\\..\\images\\music.png"), 0, 0, 604 - 20, 493 - 20);
                }
                else
                {
                    if (!fail && (wave.myWave.monsters.Count() != 0 || edit == true))
                    {
                        if (!pause)
                        {
                            background++;
                            fire++;
                            blood++;
                        }
                        
                        if (fire == 14)
                            fire = 2;
                        
                        if (background == 800)
                            background = 0;
                        DrawBackground(graphics, background, fire);

                        graphics.DrawImage(player.playerImg, player.x, player.y, player.size * player.direction, player.size);
                        if (player.blood)
                        {
                            background += 1;
                            graphics.DrawImage(Image.FromFile(@"..\\..\\images\\blood" + (blood).ToString() + ".png"), player.x+ 145 * player.direction, player.y + 70, 50 * player.direction*-1, 50);
                        }
                        if (blood == 8)
                        {
                            blood = 1;
                            player.blood = false;
                        }

                        wave.CrateWave(graphics, pause, edit, playerWave);
                    }
                    if (fail)
                        graphics.DrawImage(Image.FromFile(@"..\\..\\images\\fail.png"), 0, 0, 604 - 20, 493 - 20);
                    if (control)
                        graphics.DrawImage(Image.FromFile(@"..\\..\\images\\control.png"), 0, 0, 604 - 20, 493 - 35);
                    if (wave.myWave.monsters.Count() == 0 && edit != true)
                        graphics.DrawImage(Image.FromFile(@"..\\..\\images\\win.jpg"), 0, 0, 604 - 20, 493 - 20);
                }
            }
            
        }
        private void DrawBackground(Graphics graphics, int background, int fire)
        {
            graphics.DrawImage(Image.FromFile(@"..\\..\\images\\фон.jpg"), 0 + background, -110, 800, 600);
            graphics.DrawImage(Image.FromFile(@"..\\..\\images\\фон.jpg"), -800 + background, -110, 800, 600);
            for (var i = 0; i < 20; i++)
                graphics.DrawImage(Image.FromFile(@"..\\..\\images\\платформа.png"), 10 * 5 * i, 345, 50, 20);
            if (wave.streak > 55)
            {
                graphics.DrawImage(Image.FromFile(@"..\\..\\images\\огонь" + (fire / 2).ToString() + ".png"), player.health * 2 - 20, -14, 300, 97);
                graphics.DrawImage(Image.FromFile(@"..\\..\\images\\megahp.png"), 15, 15, player.health * 2, 65);
            }
            else
                graphics.DrawImage(Image.FromFile(@"..\\..\\images\\hp.png"), 15, 15, player.health * 2, 65);
            graphics.DrawString(player.health.ToString(), new Font("Arial", 40), Brushes.Black, new Point(20, 20));
            graphics.DrawString(wave.score.ToString(), new Font("Arial", 32), Brushes.Black, new Point(481, 11));
            graphics.DrawString(wave.score.ToString(), new Font("Arial", 30), Brushes.Yellow, new Point(480, 10));
            if (pause)
                graphics.DrawImage(Image.FromFile(@"..\\..\\images\\pause.png"), 350, 20, 25, 25);
        }

        private void MouseCollision()
        {
            var mouse = new Rectangle(X, Y, 2, 2);

            //коллизия кнопок
            var button1 = new Rectangle(170, 150, 250, 100);
            var button2 = new Rectangle(170, 250, 250, 100);
            var button3 = new Rectangle(170, 350, 250, 100);
            var button4 = new Rectangle(510, 20, 50, 50);
            var button5 = new Rectangle(20, 370, 50, 50);
            if (menu && !edit)
            {
                if (button1.IntersectsWith(mouse))
                    myMenu.buttonIncrease1 = 10;
                else
                    myMenu.buttonIncrease1 = 0;

                if (button2.IntersectsWith(mouse))
                    myMenu.buttonIncrease2 = 10;
                else
                    myMenu.buttonIncrease2 = 0;
                if (button3.IntersectsWith(mouse))
                    myMenu.buttonIncrease3 = 10;
                else
                    myMenu.buttonIncrease3 = 0;
                if (button4.IntersectsWith(mouse))
                    myMenu.buttonIncrease4 = 10;
                else
                    myMenu.buttonIncrease4 = 0;
                if (button5.IntersectsWith(mouse))
                    myMenu.buttonIncrease5 = 10;
                else
                    myMenu.buttonIncrease5 = 0;

            }

            //коллизия монстров в редакторе
            wave.MonsterCollision(mouse, edit);
        }

        private void KeyPressing(object sender, KeyEventArgs e)
        {
            if (player.click == true)
            {
               
                switch (e.KeyCode.ToString())
                {
                    case "S":
                        player.attack = "TopLeft";
                        break;
                    case "X":
                        player.attack = "BottomLeft";
                        break;
                    case "J":
                        player.attack = "TopRight";
                        break;
                    case "N":
                        player.attack = "BottomRight";
                        break;
                    case "R":
                        restart = true;
                        break;
                    case "Oemtilde":
                        edit = false;
                        fail = false;
                        menu = true;
                        pause = true;
                        counter = 0;
                        control = false;
                        break;
                    case "Space":
                        pause = !pause;
                        counter = 0;
                        break;
                    case "Return":
                        edit = false;
                        pause = false;
                        myMenu.start = false;
                        menu = false;
                        counter = 0;
                        break;
                    case "D1":
                        WMP.URL = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + "/music.wav";
                        edit = true;
                        chouseMusic = false;
                        pause = false;
                        counter = 0;
                        break;
                    case "D2":
                        WMP.URL = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + "/tofubeats_-_CANDYLAND_70331882.wav";
                        edit = true;
                        chouseMusic = false;
                        pause = false;
                        counter = 0;
                        break;
                    case "D3":
                        WMP.URL = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + "/Tokyo_Machine_-_EPIC_63196272.wav";
                        edit = true;
                        chouseMusic = false;
                        pause = false;
                        counter = 0;
                        break;
                }
            }
        }

        private void KeyUpping(object sender, KeyEventArgs e)
        {
            if (player.click == true)
            {
                switch (e.KeyCode.ToString())
                {
                    case "S":
                        player.attack = "Stay";
                        break;
                    case "X":
                        player.attack = "Stay";
                        break;
                    case "J":
                        player.attack = "Stay";
                        break;
                    case "N":
                        player.attack = "Stay";
                        break;
            }
            }

        }
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            X = e.Location.X;
            Y = e.Location.Y;
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void axWindowsMediaPlayer1_Enter(object sender, EventArgs e)
        {

        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            wave.MonsterClick(e.Button == MouseButtons.Left, edit);
            
            if (e.Button == MouseButtons.Left)
            {
                if (menu && !edit)
                {
                    if (myMenu.buttonIncrease1 == 10)
                    {
                        counter = 0;
                        pause = false;
                        menu = false;
                        myMenu.start = false;
                        edit = false;
                    }
                    if (myMenu.buttonIncrease2 == 10)
                    {
                        edit = false;
                        restart = true;

                    }
                    if (myMenu.buttonIncrease3 == 10)
                    {
                        edit = false;
                        this.Close();
                    }
                    if (myMenu.buttonIncrease4 == 10)
                    {
                        playerWave.Clear();
                        wave.score = 0;
                        player.health = 100;
                        chouseMusic = true;
                        menu = false;
                        myMenu.start = false;
                        wave = new Wave(player);
                    }
                    if (myMenu.buttonIncrease5 == 10)
                    {
                        menu = false;
                        control = true;
                    }
                }
                
               

            }

        }
    }
}
