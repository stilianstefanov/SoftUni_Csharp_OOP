using NUnit.Framework;
using System;

namespace Skeleton.Tests
{
    [TestFixture]
    public class DummyTests
    {
        private Dummy dummy;
        private int health;
        private int experience;

        [SetUp]
        public void SetUp()
        {
            health = 10;
            experience = 10;

            dummy = new Dummy(health, experience);
        }

        [Test]
        public void Test_CheckConstructor()
        {
            Assert.AreEqual(health, dummy.Health);           
        }

        [Test]
        public void Test_HealthDecreasingByAtack()
        {
            int atackPoints = 5;
            int expextedHealthLeft = 5;

            dummy.TakeAttack(atackPoints);

            Assert.AreEqual(expextedHealthLeft, dummy.Health);
        }

        [Test]
        public void Test_DummyIsDeadExceptionThrowZeroHealth()
        {
            int atackPoints = 10;
            dummy.TakeAttack(atackPoints);

            Assert.Throws<InvalidOperationException>(() =>
            {
                dummy.TakeAttack(atackPoints);
            });
        }

        [Test]
        public void Test_DummyIsDeadExceptionThrowNegativeHealth()
        {
            int atackPoints = 12;
            dummy.TakeAttack(atackPoints);

            Assert.Throws<InvalidOperationException>(() =>
            {
                dummy.TakeAttack(atackPoints);
            });
        }

        [Test]
        public void Test_GiveExperienceReturnsCorrectValueWhenDead()
        {
            int atackPoints = 10;

            dummy.TakeAttack(atackPoints);
            Assert.AreEqual(experience, dummy.GiveExperience());
        }

        [Test]
        public void Test_GiveExperienceThrowWhenNotDead()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                dummy.GiveExperience();
            });
        }
    }
}