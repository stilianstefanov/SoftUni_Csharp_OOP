namespace FightingArena.Tests
{
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class ArenaTests
    {
        private Arena arena;
        private Warrior warrior;

        private const string NAME = "Alucard";
        private const int DAMAGE = 40;
        private const int HP = 60;

        [SetUp]
        public void SetUp()
        {
            arena = new Arena();
            warrior = new Warrior(NAME, DAMAGE, HP);
        }

        [Test]
        public void Test_ConstructorWorksProperply()
        {
            Assert.IsTrue(arena.Warriors != null);
        }

        [Test]
        public void Test_CountReturnsWarriorsCount()
        {
            Assert.AreEqual(arena.Warriors.Count, arena.Count);
        }

        [Test]
        public void Test_EnrollAddsWarriorProperly()
        {
            arena.Enroll(warrior);
            int expectecCount = 1;

            Assert.AreEqual(expectecCount, arena.Count);
        }

        [Test]
        public void Test_EnrollDuplicateNameShouldThrow()
        {
            arena.Enroll(warrior);

            Assert.Throws<InvalidOperationException>(() =>
            {
                arena.Enroll(warrior);
            });
        }

        [Test]
        public void Test_FightShouldThrowWhenAtackerNotFound()
        {
            arena.Enroll(warrior);

            Assert.Throws<InvalidOperationException>(() =>
            {
                arena.Fight("Roger", warrior.Name);
            });
        }

        [Test]
        public void Test_FightShouldThrowWhenDefenderNotFound()
        {
            arena.Enroll(warrior);

            Assert.Throws<InvalidOperationException>(() =>
            {
                arena.Fight(warrior.Name, "Roger");
            });
        }

        [Test]
        public void Test_FightWorksProperly()
        {
            Warrior secondWarrior = new Warrior("Gusion", 40, 50);

            arena.Enroll(warrior);
            arena.Enroll(secondWarrior);

            int expectedFirstWarriorHP = warrior.HP - secondWarrior.Damage;
            int expectedSecondWarriorHP = secondWarrior.HP - warrior.Damage;

            arena.Fight(warrior.Name, secondWarrior.Name);

            Assert.AreEqual(expectedFirstWarriorHP, warrior.HP);
            Assert.AreEqual(expectedSecondWarriorHP, secondWarrior.HP);
        }
    }
}
