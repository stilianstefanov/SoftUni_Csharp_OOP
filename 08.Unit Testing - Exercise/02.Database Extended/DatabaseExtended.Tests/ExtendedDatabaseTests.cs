namespace DatabaseExtended.Tests
{
    using ExtendedDatabase;
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class ExtendedDatabaseTests
    {
        private Person[] testInput;
        private Database database;

        [SetUp]
        public void SetUp()
        {
            testInput = new Person[15];

            for (int i = 0; i < testInput.Length; i++)
            {
                testInput[i] = new Person(i, ((char)('A' + i)).ToString());
            }

            database = new Database(testInput);
        }

        [Test]
        public void Test_ConstructorSetInitialDataProperly()
        {
            Assert.AreEqual(testInput.Length, database.Count, "Test input length is different from the databse count!");
        }

        [Test]
        public void Test_ConstructorThrowErrorWithLargerData()
        {
            Person[] invalidInput = new Person[17];

            for (int i = 0; i < invalidInput.Length; i++)
            {
                invalidInput[i] = new Person(i, ((char)('A' + i)).ToString());
            }

            Assert.Throws<ArgumentException>(() =>
            {
                database = new Database(invalidInput);
            }, "Database can store more than 16 elements");
        }

        [Test]
        public void Test_AddElementWhenDatabaseFullShouldThrow()
        {
            database.Add(new Person(43221, "Max"));

            Assert.Throws<InvalidOperationException>(() =>
            {
                database.Add(new Person(43243, "Lewis"));
            }, "Database doesn't throw exception when full");
        }

        [Test]
        public void Test_AddElemetDuplicateUsernameShouldThrow()
        {
            database = new Database(new Person(1, "Max"));

            Assert.Throws<InvalidOperationException>(() =>
            {
                database.Add(new Person(2, "Max"));
            }, "Database doesn't throw excpetion when adding element with duplicate Username");
        }

        [Test]
        public void Test_AddElementDuplicateIDShouldThrow()
        {
            database = new Database(new Person(1, "Max"));

            Assert.Throws<InvalidOperationException>(() =>
            {
                database.Add(new Person(1, "George"));
            }, "Database doesn't throw excpetion when adding element with duplicate ID");
        }

        [Test]
        public void Test_RemoveElement()
        {
            int expectedCount = database.Count - 1;

            database.Remove();

            Assert.AreEqual(expectedCount, database.Count, "Count doesn't decrease when element is removed");
        }

        [Test]
        public void Test_RemoveShouldThrowWhenDatabaseEmpty()
        {
            database = new Database();

            Assert.Throws<InvalidOperationException>(() =>
            {
                database.Remove();
            }, "Remove doesn't throw exception when empty" );
        }

        [Test]
        public void Test_FindByUserName()
        {
            database = new Database(new Person(1, "Max"));

            Person person = database.FindByUsername("Max");

            Assert.AreEqual(person.UserName, database.FindByUsername("Max").UserName, "FindByUserName doesn't return element properly");
        }

        [Test]
        public void Test_FindByUserNameEmptyInputShouldThrow()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                database.FindByUsername("");
            }, "Does not throw excpetion when empty input");
        }

        [Test]
        public void Test_FindByUserNameNotExistingInput()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                database.FindByUsername("Charles");
            }, "Does not throw when non - existing input is given");
        }

        [Test]
        public void Test_FindByID()
        {
            database = new Database(new Person(1, "Max"));

            Person person = database.FindById(1);

            Assert.AreEqual(person.Id, database.FindById(1).Id, "FindById doesn't return element properly");
        }

        [Test]
        public void Test_FindByIDWithNegativeNumber()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                database.FindById(-1);
            }, "FindById doesn't throw excpetion with negative Id");
        }

        [Test]
        public void Test_FindByIDNotExistingInput()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                database.FindById(9999999);
            }, "Does not throw when non - existing input is given");
        }
    }
}