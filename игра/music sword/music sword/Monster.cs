using System.Drawing;


namespace music_sword
{
    public class Monster
    {
        public int x, y;
        public int monsterVid;
        public int monsterDirection;
        public int time;
        public int speed;
        public int size;
        public Rectangle model;
        public string attack;
        public bool shot = false;
       

        public Image monsterImg;

        public bool isAlive;
        public int counter;

        public Monster(int x, int y, int T, int B, int speed ,int time)
        {

            this.x = x;
            this.y = y;
            this.monsterVid = T;
            this.monsterDirection = B;
   
            this.speed = speed;
            this.time = time;
            size = 140;
            attack = "stay";
            isAlive = true;
            counter = 12;
        }

    }
}
