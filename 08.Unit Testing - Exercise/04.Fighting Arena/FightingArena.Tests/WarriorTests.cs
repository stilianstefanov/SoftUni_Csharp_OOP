namespace FightingArena.Tests
{
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class WarriorTests
    {
        private const string NAME = "Alucard";
        private const int DAMAGE = 40;
        private const int HP = 60;
        private Warrior warrior;

        private const string ENEMY_NAME = "Roger";
        private const int ENEMY_DAMAGE = 30;
        private const int ENEMY_HP = 50;
        private Warrior enemy;

        [SetUp]
        public void SetUp()
        {
            warrior = new Warrior(NAME, DAMAGE, HP);
            enemy = new Warrior(ENEMY_NAME, ENEMY_DAMAGE, ENEMY_HP);
        }

        [Test]
        public void Test_ConstructorWorksProperly()
        {
            Assert.AreEqual(NAME, warrior.Name);
            Assert.AreEqual(DAMAGE, warrior.Damage);
            Assert.AreEqual(HP, warrior.HP);
        }

        [TestCase("")]
        [TestCase(" ")]
        [TestCase(null)]
        public void Test_NameShouldNotAcceptNullOrWhiteSpace(string name)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                warrior = new Warrior(name, DAMAGE, HP);
            }, "Name should not accept null, whitespace or empty string");
        }

        [TestCase(0)]
        [TestCase(-1)]
        public void Test_DamageValueShouldBePositive(int damage)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                warrior = new Warrior(NAME, damage, HP);
            }, "Damage should not accept zero or negative values");
        }

        [Test]
        public void Test_HPValueShouldNotBeNegative()
        {
            int negativeHp = -1;

            Assert.Throws<ArgumentException>(() =>
            {
                warrior = new Warrior(NAME, DAMAGE, negativeHp);
            }, "Hp should be zero or positive number");
        }

        [TestCase(20)]
        [TestCase(30)]
        public void Test_HPTooLowToAtackShouldThrow(int lowHpTestInput)
        {            
            warrior = new Warrior(NAME, DAMAGE, lowHpTestInput);

            Assert.Throws<InvalidOperationException>(() =>
            {
                warrior.Attack(enemy);
            }, "Exception should be thrown when HP value is below the MIN_HP");
        }

        [TestCase(20)]
        [TestCase(30)]
        public void Test_EnemyHPTooLowToAtackShouldThrow(int lowHpTestInput)
        {
            enemy = new Warrior(ENEMY_NAME, ENEMY_DAMAGE, lowHpTestInput);

            Assert.Throws<InvalidOperationException>(() =>
            {
                warrior.Attack(enemy);
            }, "Exception should be thrown when enemy HP value is below the MIN_HP");
        }

        [Test]
        public void Test_EnemyDamageHigherThanAtackerHPShouldThrow()
        {
            int higherDamage = 70;
            enemy = new Warrior(ENEMY_NAME, higherDamage, ENEMY_HP);

            Assert.Throws<InvalidOperationException>(() =>
            {
                warrior.Attack(enemy);
            }, "Exception should be thrown when HP is below enemy damage");
        }

        [Test]
        public void Test_AttackEnemyShouldDecreaseHP()
        {
            int expectedHpLeft = warrior.HP - enemy.Damage;

            warrior.Attack(enemy);

            Assert.AreEqual(expectedHpLeft, warrior.HP);
        }

        [Test]
        public void Test_AttackEnemyWithLowerHP()
        {
            int lowerHP = 35;
            enemy = new Warrior(ENEMY_NAME, ENEMY_DAMAGE, lowerHP);

            int expectedEnemyHP = 0;
            warrior.Attack(enemy);

            Assert.AreEqual(expectedEnemyHP, enemy.HP);
        }

        [Test]
        public void Test_AtackShoudlDecreaseEnemyHP()
        {
            int expectedEnemyHp = enemy.HP - warrior.Damage;

            warrior.Attack(enemy);

            Assert.AreEqual(expectedEnemyHp, enemy.HP);
        }
    }
}