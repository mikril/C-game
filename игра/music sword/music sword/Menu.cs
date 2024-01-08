using System.Drawing;


namespace music_sword
{
    internal class Menu
    {
        public bool menu;

        public bool start=true;
        public int buttonIncrease1;
        public int buttonIncrease2;
        public int buttonIncrease3;
        public int buttonIncrease4;
        public int buttonIncrease5;
        public WMPLib.WindowsMediaPlayer WMP = new WMPLib.WindowsMediaPlayer();
        public Menu(bool Menu)
        {
            this.menu = Menu;
            this.buttonIncrease1 = 0;
            this.buttonIncrease2 = 0;
            this.buttonIncrease3 = 0;
            this.buttonIncrease4 = 0;
            this.buttonIncrease5 = 0;

        }

        public void DrawMenu(Graphics graphics)
        {
            graphics.DrawImage(Image.FromFile(@"..\\..\\images\\image.png"), 0, 0, 600, 500);
            graphics.DrawImage(Image.FromFile(@"..\\..\\images\\image1.png"), 50 - buttonIncrease1, 150 - buttonIncrease1, 500 + buttonIncrease1 * 2, 250 + buttonIncrease1 * 2);
            if (!start)
                graphics.DrawImage(Image.FromFile(@"..\\..\\images\\image2.png"), 50 - buttonIncrease2, 235 - buttonIncrease2, 500 + buttonIncrease2 * 2, 250 + buttonIncrease2 * 2);
            graphics.DrawImage(Image.FromFile(@"..\\..\\images\\image3.png"), 50 - buttonIncrease3, 320 - buttonIncrease3, 500 + buttonIncrease3 * 2, 250 + buttonIncrease3 * 2);
            graphics.DrawImage(Image.FromFile(@"..\\..\\images\\creator.png"), 510 - buttonIncrease4, 20 - buttonIncrease4, 50+ buttonIncrease4 * 2, 50 + buttonIncrease4 * 2);
            graphics.DrawImage(Image.FromFile(@"..\\..\\images\\keyboard.png"), 20 - buttonIncrease5, 370 - buttonIncrease5, 50 + buttonIncrease5 * 2, 50 + buttonIncrease5 * 2);
        }
    }
}
