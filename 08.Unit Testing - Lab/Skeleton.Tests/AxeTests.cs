using NUnit.Framework;
using System;

namespace Skeleton.Tests
{
    [TestFixture]
    public class AxeTests
    {
        private Axe axe;
        private Dummy dummy;
        private int durability;
        private int atack;

        [SetUp]
        public void SetUp()
        {
            durability = 10;
            atack = 10;

            axe = new Axe(atack, durability);
            dummy = new Dummy(10, 10);
        }

        [Test]
        public void Test_CheckConstructor()
        {
            Assert.AreEqual(durability, axe.DurabilityPoints);
            Assert.AreEqual(atack, axe.AttackPoints);
        }

        [Test]
        public void Test_AxeLosesDurabilityAfterEachAtack()
        {
            axe.Attack(dummy);

            Assert.AreEqual(9, axe.DurabilityPoints, "Axe Durability doesn't change after atack");
        }

        [Test]
        public void Test_AxeZeroDurabilityThrow()
        {
            Axe brokenAxe = new Axe(10, 0);

            Assert.Throws<InvalidOperationException>(() =>
            {
                brokenAxe.Attack(dummy);
            }
            );
        }
    }
}