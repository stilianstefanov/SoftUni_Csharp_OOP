namespace Database.Tests
{
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class DatabaseTests
    {
        private Database database;
        private int[] testInput;

        [SetUp]
        public void SetUP()
        {
            testInput = new int[] { 1, 2, 3 };

            database = new Database(testInput);
        }

        [Test]
        public void Test_ConstructorAddingProperly()
        {
            Assert.AreEqual(testInput.Length, database.Count, "Input not added to database properly!");
        }

        [Test]
        public void Test_ConstructorWithInvalidInputThrow()
        {
            testInput = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17 };

            Assert.Throws<InvalidOperationException>(() =>
            {
                database = new Database(testInput);
            }, "Invalid operation exception should be thrown");
        }

        [Test]
        public void Test_AddElement()
        {
            int numToAdd = 4;
            int expectedDataBaseCount = 4;

            database.Add(numToAdd);

            Assert.AreEqual(expectedDataBaseCount, database.Count, "Method Add not adding properly");
        }

        [Test]
        public void Test_AddElementWhen16ElementsInDataBase()
        {
            testInput = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };
            database = new Database(testInput);

            Assert.Throws<InvalidOperationException>(() =>
            {
                database.Add(1);
            }, "The database should not accept more elements when it's full");
        }

        [Test]
        public void Test_RemoveElement()
        {
            int expectedCount = 2;

            database.Remove();
            Assert.AreEqual(expectedCount, database.Count, "Remove method not working properly!");
        }

        [Test]
        public void Test_RemoveElementEmptyDataBase()
        {
            database = new Database();

            Assert.Throws<InvalidOperationException>(() =>
            {
                database.Remove();
            }, "Invalid Operation Exception should be thrown when the database is empty!");
        }

        [Test]
        public void Test_FetchMethodReturnsProperElemets()
        {
            int[] expextedArr = { 1, 2, 3, 4, 5, 6, 7, };
            database = new Database(expextedArr);

            int[] copyArray = database.Fetch();

            Assert.AreEqual(expextedArr, copyArray);
        }
    }
}
