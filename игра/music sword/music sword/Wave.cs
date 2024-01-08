using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace music_sword
{
    public class Wave
    {
        Player player;
        public Monsters myWave = new Monsters();

        public int time = 0;
        public int counter = 0;
        public int shot = 0;
        public int streak = 0;
        public int score = 0;
        public int criticalTime = 0;

        public int monsterSize1, monsterSize2, monsterSize3, monsterSize4 = 0;
        public bool monsterAdd1, monsterAdd2, monsterAdd3, monsterAdd4 = false;

        private int x,y;
        public void CheckClick(List<Monster> playerWave)
        {
            if (monsterAdd1 && time - criticalTime > 30)
            {
                playerWave.Add(new Monster(600, 300, 0, 1, 6, time + 3));
                criticalTime = time;
                x = 450;
                y = 250;
                monsterAdd1 = false;

            }
            if (monsterAdd2 && time - criticalTime > 30)
            {
                playerWave.Add(new Monster(0, 300 + 0, 0, -1, 6, time + 3));
                criticalTime = time;
                x = 170;
                y = 250;
                monsterAdd2 = false;
            }
            if (monsterAdd3 && time - criticalTime > 30)
            {
                playerWave.Add(new Monster(0, 200, -100, -1, 6, time + 3));
                criticalTime = time;
                x = 170;
                y = 130;
                monsterAdd3 = false;
            }
            if (monsterAdd4 && time - criticalTime > 30)
            {
                playerWave.Add(new Monster(600, 200, -100, 1, 6, time + 3));
                criticalTime = time;
                x = 450;
                y = 130;
                monsterAdd4 = false;
            }
        }
        public void CrateWave(Graphics graphics, bool pause, bool edit, List<Monster> playerWave)
        {
            if (!pause)
                time++;
            if (edit)
            {
                if (time==0)
                    myWave.monsters.Clear();
                graphics.DrawImage(Image.FromFile(@"..\\..\\images\\монстер" + (1).ToString() + "-removebg-preview.png"), 480, 300 - 90 - monsterSize1, -1 * 140 - 2 * monsterSize1, 140 + 2 * monsterSize1);
                graphics.DrawImage(Image.FromFile(@"..\\..\\images\\монстер" + (1).ToString() + "-removebg-preview.png"), 150 - monsterSize2, 300 - 90 - monsterSize2, 140 + 2 * monsterSize2, 140 + 2 * monsterSize2);
                graphics.DrawImage(Image.FromFile(@"..\\..\\images\\летун" + (1).ToString() + "-removebg-preview.png"), 250, 300 - 190 - monsterSize3, -1 * 140 - 2 * monsterSize3, 140 + 2 * monsterSize3);
                graphics.DrawImage(Image.FromFile(@"..\\..\\images\\летун" + (1).ToString() + "-removebg-preview.png"), 390 - monsterSize4, 300 - 190 - monsterSize4, 140 + 2 * monsterSize4, 140 + 2 * monsterSize4);
                if (criticalTime+25 > time)
                    graphics.DrawString("+1", new Font("Arial", criticalTime + 30 - time, FontStyle.Bold), Brushes.Green, new Point(x- criticalTime - 30 + time, y- criticalTime - 30 + time));
                CheckClick(playerWave);
            }
            else
            {
                if (playerWave.Count() > 0)
                    myWave.monsters = playerWave;

                for (var i = 0; i < myWave.monsters.Count(); i++)
                {
                    var monster = myWave.monsters[i];

                    if (time >= monster.time)
                    {
                        if (!pause)
                        {
                            monster.counter += 2;
                            if (monster.isAlive)
                                monster.x -= 1 * monster.monsterDirection * monster.speed;
                        }

                        if (monster.counter >= 100 && monster.monsterVid != -100)
                        {
                            monster.counter = 12;
                        }

                        if (monster.counter == 16 && monster.monsterVid == -100)
                        {
                            monster.counter = 12;
                        }

                        if (monster.monsterVid == 0)
                        {
                            if (monster.isAlive)
                                monster.monsterImg = Image.FromFile(@"..\\..\\images\\монстер" + (monster.counter / 10).ToString() + "-removebg-preview.png");
                            graphics.DrawImage(monster.monsterImg, monster.x, monster.y - 90, -1 * monster.monsterDirection * monster.size, monster.size);
                            monster.model = new Rectangle(monster.x + (-1 * (245 * 140) / 645 - 1 * monster.monsterDirection * (245 * 140) / 645) / 2 + -1 * monster.monsterDirection * (50 * 140) / 645, monster.y - 90 + (150 * 140) / 387+50, (245 * 140) / 645, (235 * 140) / 387-50);
                        }
                        if (monster.monsterVid == -100)
                        {

                            if (monster.isAlive)
                                monster.monsterImg = Image.FromFile(@"..\\..\\images\\летун" + ((monster.counter - 10) / 2).ToString() + "-removebg-preview.png");
                            graphics.DrawImage(monster.monsterImg, monster.x, monster.y - 90, monster.monsterDirection * monster.size, monster.size);
                            monster.model = new Rectangle(monster.x - ((180 * 140) / 500 - monster.monsterDirection * (180 * 140) / 500) / 2 + monster.monsterDirection * (160 * 140) / 500, monster.y - 90 + (170 * 140) / 500, (180 * 140) / 500, (225 * 140) / 500);
                        }

                        Dead(monster);
                        if (!monster.isAlive)
                        {

                            if (shot == 45)
                            {
                                monster.shot = false;
                            }
                            if (!monster.shot)
                            {
                                counter++;
                                monster.monsterImg = Image.FromFile(@"..\\..\\images\\бум" + (counter).ToString() + "-removebg-preview.png");

                            }
                            else
                            {
                                monster.monsterImg = Image.FromFile(@"..\\..\\images\\" + (shot).ToString() + ".png");
                                shot++;
                            }
                            if (counter == 10 && !monster.shot)
                            {
                                shot = 0;
                                myWave.monsters.Remove(monster);
                                counter = 1;
                            }

                            if (shot - 1 == 11)
                            {
                                myWave.monsters.Remove(monster);
                                shot = 0;
                                player.health -= 5;
                                player.blood = true;
                                streak = 0;
                            }


                        }
                    }
                }
            }
        }
        public void MonsterCollision( Rectangle mouse, bool edit)
        {
            var monster1 = new Rectangle(480 - 140, 300 - 90, 140, 140);
            var monster2 = new Rectangle(150, 300 - 90, 140, 140);
            var monster3 = new Rectangle(250 - 140, 300 - 190, 140, 140);
            var monster4 = new Rectangle(390, 300 - 190, 140, 140);

            if (edit)
            {
                if (monster1.IntersectsWith(mouse))
                    monsterSize1 = 10;
                else
                    monsterSize1 = 0;
                if (monster2.IntersectsWith(mouse))
                    monsterSize2 = 10;
                else
                    monsterSize2 = 0;
                if (monster3.IntersectsWith(mouse))
                    monsterSize3 = 10;
                else
                    monsterSize3 = 0;
                if (monster4.IntersectsWith(mouse))
                    monsterSize4 = 10;
                else
                    monsterSize4 = 0;
            }
        }
        public void MonsterClick(bool click, bool edit)
        {   
            if (edit && click)
            {

                    if (monsterSize1 == 10)
                        monsterAdd1 = true;
                    else
                        monsterAdd1 = false;
                    if (monsterSize2 == 10)
                        monsterAdd2 = true;
                    else
                        monsterAdd2 = false;
                    if (monsterSize3 == 10)
                        monsterAdd3 = true;
                    else
                        monsterAdd3 = false;
                    if (monsterSize4 == 10)
                        monsterAdd4 = true;
                    else
                        monsterAdd4 = false;
            }
        }

        public void Dead(Monster monster)
        {
            var coorection = 0;
            if (monster.monsterDirection == -1)
            {
                coorection = 140;
            }
            if ((monster.x  * monster.monsterDirection < 392 * monster.monsterDirection + coorection + monster.monsterVid) && monster.monsterVid == -100)
            {
                if (monster.y > 300)
                {
                    monster.isAlive = false;
                    player.health -= 5;
                    player.blood = true;
                    streak = 0;
                }
                monster.y += 20;
                monster.x +=monster.speed*monster.monsterDirection;

            }
            if ((monster.x * monster.monsterDirection < 480 * monster.monsterDirection + coorection) && monster.monsterVid == 0 && monster.monsterDirection>0)
            {
                monster.isAlive = false;
                monster.shot = true;
                
            }
            if ((monster.x * monster.monsterDirection < 310 * monster.monsterDirection + coorection) && monster.monsterVid == 0 && monster.monsterDirection < 0)
            {
                monster.isAlive = false;
                monster.shot = true;
                
            }
            if (player.attack != "stay" && monster.model.IntersectsWith(player.model))
            {
                monster.isAlive = false;
                shot = 45;
                streak++;
                score += 1;
            }

        }
     
        public Wave(Player player)
        {
            this.player = player;
      
        }
    }   
}
