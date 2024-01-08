using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Drawing;



namespace TestMuzikSord
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TopLeftAttackTest()
        {
            var player = new music_sword.Player(225, 216);
            var expected = 355;
            player.attack = "TopLeft";
            player.Animated();
            Assert.AreEqual(expected, player.x);
        }
        [TestMethod]
        public void BottomRightAttackTest()
        {
            var player = new music_sword.Player(225, 216);
            var expectedX = 285;
            var expectedY = 216;
            player.attack = "BottomRight";
            player.Animated();
            Assert.AreEqual(expectedX, player.x);
            Assert.AreEqual(expectedY, player.y);
        }
        [TestMethod]
        public void MonstorInPlayerTest()
        {
            var player = new music_sword.Player(225, 216);
            player.attack = "BottomRight";
            player.Animated();
            var wave = new music_sword.Wave(player);
            var monster = new music_sword.Monster(300, 300 , 0, 1, 6, 145);
            wave.Dead(monster);
            var expectedAlife = false;
            Assert.AreEqual(expectedAlife, monster.isAlive);
        }
        [TestMethod]
        public void MonstorNotInPlayerTest()
        {
            var player = new music_sword.Player(225, 216);
            player.attack = "BottomLeft";
            player.Animated();
            var wave = new music_sword.Wave(player);
            var monster = new music_sword.Monster(100, 300, 0, -1, 6, 145);
            wave.Dead(monster);
            var expectedAlife = true;
            Assert.AreEqual(expectedAlife, monster.isAlive);
        }

        [TestMethod]
        public void MonstorKickPlayerTest()
        {
            var player = new music_sword.Player(225, 216);
            var wave = new music_sword.Wave(player);
            var monster = new music_sword.Monster(353, 301, -100, -1, 6, 145);
            wave.Dead(monster);
            var expectedHealth = 95;
            Assert.AreEqual(expectedHealth, player.health);
        }
        [TestMethod]
        public void AllEditTests()
        {
            var player = new music_sword.Player(225, 216);
            var wave = new music_sword.Wave(player);
            wave.time = 50;
            var mouse = new Rectangle(440, 300, 2, 2);
            var playerWave =new List<music_sword.Monster> { };

            wave.MonsterCollision(mouse, true);
            var expectedIncrese = 10;
            Assert.AreEqual(expectedIncrese, wave.monsterSize1);

            wave.MonsterClick(true, true);
            var expectedAdd = true;
            Assert.AreEqual(expectedAdd, wave.monsterAdd1);

            wave.CheckClick(playerWave);
            var expectedTime = 50;
            var expectedMonsterX =600;
            var expectedMonsterY = 300;
            var expectedMonsterTime = 53;
            Assert.AreEqual(expectedTime, wave.criticalTime);
            Assert.AreEqual(expectedMonsterX, playerWave[0].x);
            Assert.AreEqual(expectedMonsterY, playerWave[0].y);
            Assert.AreEqual(expectedMonsterTime, playerWave[0].time);
        }
    }
}