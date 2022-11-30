namespace Presents.Tests
{
    using NUnit.Framework;
    using System;
    using System.Linq;

    [TestFixture]
    public class PresentsTests
    {
        private Present present;
        private Bag bag;

        [SetUp]
        public void SetUp()
        {
            bag = new Bag();
            present = new Present("gift", 10);
        }

        [Test]
        public void Test_PresentConstructorSetsVauesProperly()
        {
            Assert.AreEqual("gift", present.Name);
            Assert.AreEqual(10, present.Magic);
        }

        [Test]
        public void Test_BagConstructorInitializesTheCollection()
        {
            var outputCollection = bag.GetPresents();
            Assert.IsNotNull(outputCollection);
        }

        [Test]
        public void Test_CreateThrowsWhenPresentIsNull()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                bag.Create(null);
            });
        }

        [Test]
        public void Test_CreateThrowsWhenPresentAlreadyExcists()
        {
            bag.Create(present);

            Assert.Throws<InvalidOperationException>(() =>
            {
                bag.Create(present);
            });
        }

        [Test]
        public void Test_CreateAddsProductToThecollectionAndReturnsCorrectOutput()
        {
            string expectedOutput = "Successfully added present gift.";

            string output = bag.Create(present);
            var outputCollection = bag.GetPresents();

            Assert.AreEqual(expectedOutput, output);
            Assert.IsTrue(outputCollection.Contains(present));
        }

        [Test]
        public void Test_RemoveRemovesElementFromTheCollection()
        {
            bag.Create(present);
            bool output = bag.Remove(present);
            var outputCollection = bag.GetPresents();

            Assert.IsTrue(output);
            Assert.IsFalse(outputCollection.Contains(present));
        }

        [Test]
        public void Test_GetElementWithLeastMagicReturnsTheCorrectElement()
        {
            var present2 = new Present("random", 20);
            var present3 = new Present("random2", 30);

            bag.Create(present);
            bag.Create(present2);
            bag.Create(present3);

            var outputPresent = bag.GetPresentWithLeastMagic();

            Assert.AreEqual(present, outputPresent);
            Assert.AreEqual(present.Name, outputPresent.Name);
        }

        [Test]
        public void Test_GetPresentReturnsTheCorrectPresent()
        {

            var present2 = new Present("random", 20);
            var present3 = new Present("random2", 30);

            bag.Create(present);
            bag.Create(present2);
            bag.Create(present3);
            var outputPresent = bag.GetPresent("random");

            Assert.AreEqual(present2, outputPresent);
            Assert.AreEqual(present2.Name, outputPresent.Name);
        }        
    }
}
