using NUnit.Framework;
using System;
using System.Xml.Linq;

namespace FootballTeam.Tests
{
    public class Tests
    {
        private FootballPlayer player;
        private FootballTeam team;

        [SetUp]
        public void Setup()
        {
            player = new FootballPlayer("Mbappe", 10, "Goalkeeper");
            team = new FootballTeam("PSG", 16);
        }

        [Test]
        public void Test1()
        {
            Assert.AreEqual("Mbappe", player.Name);
            Assert.AreEqual(10, player.PlayerNumber);
            Assert.AreEqual("Goalkeeper", player.Position);
            Assert.AreEqual(0, player.ScoredGoals);
        }

        [TestCase(null)]
        [TestCase("")]
        public void Test2(string name)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                player = new FootballPlayer(name, 10, "Goalkeeper");
            }, "Name cannot be null or empty!");
        }

        [TestCase(0)]
        [TestCase(22)]
        public void Test3(int playerNumber)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                player = new FootballPlayer("Mbappe", playerNumber, "Goalkeeper");
            }, "Player number must be in range [1,21]");
        }

        [Test]
        public void Test4()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                player = new FootballPlayer("Mbappe", 11, "half");
            }, "Invalid Position");
        }

        [TestCase("Midfielder")]
        [TestCase("Forward")]
        public void Test5(string postion)
        {
            player = new FootballPlayer("Mbappe", 11, postion);

            Assert.AreEqual(postion, player.Position);
        }

        [Test]
        public void Test6 ()
        {
            player.Score();
            Assert.AreEqual(1, player.ScoredGoals);
        }

        [Test]
        public void Test7()
        {
            Assert.AreEqual("PSG", team.Name);
            Assert.AreEqual(16, team.Capacity);
            Assert.IsNotNull(team.Players);
            Assert.AreEqual(0, team.Players.Count);
        }

        [TestCase(null)]
        [TestCase("")]
        public void Test8(string name)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                team = new FootballTeam(name, 16);
            }, "Name cannot be null or empty!");
        }

        [TestCase(1)]
        [TestCase(14)]
        public void Test9(int capacity)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                team = new FootballTeam("PSG", capacity);
            }, "Capacity min value = 15");
        }

        [Test]
        public void Test10()
        {
            for (int i = 0; i < 16; i++)
            {
                team.AddNewPlayer(player);
            }

            string expectedOutput = "No more positions available!";

            string output = team.AddNewPlayer(player);

            Assert.AreEqual(expectedOutput, output);
        }

        [Test]
        public void Test11()
        {
            string expectedOutput = "Added player Mbappe in position Goalkeeper with number 10";

            string output = team.AddNewPlayer(player);

            Assert.AreEqual(expectedOutput, output);
        }

        [Test]
        public void Test12()
        {
            string output = team.AddNewPlayer(player);

            Assert.AreEqual(1, team.Players.Count);

            var players = team.Players;
            Assert.IsTrue(players.Contains(player));
            Assert.AreEqual(1, players.Count);
        }

        [Test]
        public void Test13()
        {
            team.AddNewPlayer(player);

            FootballPlayer outputPlayer = team.PickPlayer("Mbappe");

            Assert.IsNotNull(outputPlayer);
            Assert.IsTrue(team.Players.Contains(outputPlayer));
            Assert.AreEqual(player, outputPlayer);
            Assert.AreEqual(player.Name, outputPlayer.Name);
        }

        [Test]
        public void Test14()
        {
            team.AddNewPlayer(player);

            FootballPlayer outputPlayer = team.PickPlayer("Gosho");

            Assert.IsNull(outputPlayer);
        }

        [Test]
        public void Test15()
        {
            team.AddNewPlayer(player);

            string expectedOutput = "Mbappe scored and now has 1 for this season!";

            string output = team.PlayerScore(10);
            var playerOut = team.PickPlayer("Mbappe");

            Assert.AreEqual(expectedOutput, output);

            Assert.AreEqual(1, player.ScoredGoals);
            Assert.AreEqual(1, playerOut.ScoredGoals);
        }
    }
}