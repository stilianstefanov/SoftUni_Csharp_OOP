namespace Aquariums.Tests
{
    using NUnit.Framework;
    using System;
    using System.Xml.Linq;

    [TestFixture]
    public class AquariumsTests
    {
        private Fish fish1;
        private Aquarium aquarium;

        [SetUp]
        public void SetUp()
        {
            fish1 = new Fish("Nemo");
            aquarium = new Aquarium("SeaLand", 2);
        }

        [Test]
        public void Test_FishConstructorSetsValuesProperly()
        {
            Assert.AreEqual("Nemo", fish1.Name);
            Assert.IsTrue(fish1.Available);
        }

        [Test]
        public void Test_AquariumConstructorSetsValuesProperly()
        {
            Assert.AreEqual("SeaLand", aquarium.Name);
            Assert.AreEqual(2, aquarium.Capacity);
            Assert.AreEqual(0, aquarium.Count);
        }

        [TestCase("")]
        [TestCase(null)]
        public void Test_NameThrowsWithEmptyInput(string name)
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                aquarium = new Aquarium(name, 2);
            });
        }

        [TestCase(-1)]
        [TestCase(-50)]
        public void Test_CapacityThrowsWithNegativeNumber(int capacity)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                aquarium = new Aquarium("SeaLand", capacity);
            });
        }

        [Test]
        public void Test_CountReturnsValidFishCount()
        {
            aquarium.Add(fish1);

            Assert.AreEqual(1, aquarium.Count);
        }

        [Test]
        public void Test_AddThrowsWithFullAquarium()
        {
            aquarium.Add(fish1);
            aquarium.Add(new Fish("Lori"));

            Assert.Throws<InvalidOperationException>(() =>
            {
                aquarium.Add(new Fish("Zak"));
            });
        }

        [Test]
        public void Test_AddAddsNewFishToTheCollection()
        {
            aquarium.Add(fish1);

            Assert.AreEqual(1, aquarium.Count);
        }

        [Test]
        public void Test_RemoveFishThrowsWhenFishNotExisting()
        {
            aquarium.Add(fish1);

            Assert.Throws<InvalidOperationException>(() =>
            {
                aquarium.RemoveFish("Zak");
            });
        }

        [Test]
        public void Test_RemoveFishRemovesTheFishFromTheCollection()
        {
            aquarium.Add(fish1);
            aquarium.RemoveFish("Nemo");

            Assert.AreEqual(0, aquarium.Count);
        }

        [Test]
        public void Test_SellFishThrowsWhenFishNotExisting()
        {
            aquarium.Add(fish1);

            Assert.Throws<InvalidOperationException>(() =>
            {
                aquarium.SellFish("Zak");
            });
        }

        [Test]
        public void Test_SellFishReturnsTheRightFish()
        {
            aquarium.Add(fish1);

            Fish outputFish = aquarium.SellFish("Nemo");

            Assert.AreEqual(fish1, outputFish);
            Assert.AreEqual(fish1.Name, outputFish.Name);
        }

        [Test]
        public void Test_SellFishSetsTheAvaliablePropertyFalse()
        {
            aquarium.Add(fish1);

            Fish outputFish = aquarium.SellFish("Nemo");

            Assert.IsFalse(outputFish.Available);
        }

        [Test]
        public void Test_ReportReturnsTheCorrectOutput()
        {
            aquarium.Add(fish1);
            aquarium.Add(new Fish("Zak"));

            string expectedOutput = "Fish available at SeaLand: Nemo, Zak";
            string output = aquarium.Report();

            Assert.AreEqual(expectedOutput, output);
        }
    }
}
