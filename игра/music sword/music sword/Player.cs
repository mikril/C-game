using System;
using System.Drawing;

namespace music_sword
{


    public class Player
    {
        public int x, y;

        public int direction;

        public Rectangle model;
        public int size;
        public int health;
        public Image playerImg;
        public int score;

        public string attack;
        public bool blood;

        public bool click;
        public int spriteNumber;
        public int spriteJumpNumber;

        public bool isAlive;

        public void Animated()
        {
            switch (attack)
            {
                case "BottomRight":
                    if (spriteNumber < 10)
                    {
                        click = false;
                        direction = 1;
                        playerImg = Image.FromFile(@"..\\..\\images\\спрайт" + Convert.ToString(spriteNumber) + "-removebg-preview.png");
                        spriteNumber += 1;
                        x = 285;
                    }
                    break;
                case "TopRight":
                    if (spriteJumpNumber < 11)
                    {
                        click = false;
                        direction = 1;
                        playerImg = Image.FromFile(@"..\\..\\images\\прыжок" + Convert.ToString(spriteJumpNumber) + "-removebg-preview.png");
                        spriteJumpNumber += 1;
                        x = 285;
                        y = 116;
                    }
                    break;
                case "TopLeft":
                    if (spriteJumpNumber < 11)
                    {
                        click = false;
                        direction = -1;
                        playerImg = Image.FromFile(@"..\\..\\images\\прыжок" + Convert.ToString(spriteJumpNumber) + "-removebg-preview.png");
                        spriteJumpNumber += 1;
                        x = 355;
                        y = 116;
                    }
                    break;
                case "BottomLeft":
                    if (spriteNumber < 10)
                    {
                        click = false;
                        x = 355;
                        direction = -1;
                        playerImg = Image.FromFile(@"..\\..\\images\\спрайт" + Convert.ToString(spriteNumber) + "-removebg-preview.png");
                        spriteNumber += 1;
                    }
                    break;
            }
            if (spriteNumber ==10)
            {
                playerImg = Image.FromFile(@"..\\..\\images\\спрайт1-removebg-preview.png");
                attack = "stay";
                click = true;
                spriteNumber = 1;
                x -= 60*direction; 
            }
            if (spriteJumpNumber == 11)
            {
                playerImg = Image.FromFile(@"..\\..\\images\\спрайт1-removebg-preview.png");
                attack = "stay";
                click = true;
                spriteJumpNumber = 1;
                x -= 60 * direction;
                y += 100;
            }
            model = new Rectangle(x + (60 * direction - 60) / 2 + 74 * direction, y + 77, 60, 65);
        }
        public Player(int x, int y)
        {
            playerImg = Image.FromFile(@"..\\..\\images\\спрайт1-removebg-preview.png");
            this.x = x;
            this.y = y;
            size = 200;
            health = 100;
            attack = "stay";
            isAlive = true;
            click = true;
            spriteNumber = 1;
            spriteJumpNumber = 1;
            score = 0;
            direction = 1;
            blood = false;
        }
    }
}