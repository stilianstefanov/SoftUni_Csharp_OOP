namespace Book.Tests
{
    using System;

    using NUnit.Framework;

    public class Tests
    {
        [Test]
        public void Test_ConstructorSetsValuesProperly()
        {
            string bookName = "Origin";
            string author = "Dan Brown";

            var book = new Book(bookName, author);

            Assert.AreEqual(bookName, book.BookName);
            Assert.AreEqual(author, book.Author);
        }

        [Test]
        public void Test_CountReturnsProperValue()
        {
            string bookName = "Origin";
            string author = "Dan Brown";

            var book = new Book(bookName, author);

            book.AddFootnote(1, "asfsdfssdf");

            int expectedCount = 1;
            Assert.AreEqual(expectedCount, book.FootnoteCount);
        }

        [TestCase(null)]
        [TestCase("")]
        public void Test_BookNameShouldThrowWithNullOrEmptyInput(string bookName)
        {
            Assert.Throws<ArgumentException>(() => new Book(bookName, "Dan Brown"));
        }

        [TestCase(null)]
        [TestCase("")]
        public void Test_AuthorShouldThrowWithNullOrEmptyInput(string author)
        {
            Assert.Throws<ArgumentException>(() => new Book("Origin", author));
        }

        [Test]
        public void Test_AddFootNoteShouldAddNewFootNoteToTheCollection()
        {
            string bookName = "Origin";
            string author = "Dan Brown";

            var book = new Book(bookName, author);

            book.AddFootnote(1, "asfsdfssdf");

            int expectedCount = 1;
            Assert.AreEqual(expectedCount, book.FootnoteCount);
        }

        [Test]
        public void Test_AddFootNoteShouldThrowIfNoteAlreadyExists()
        {
            string bookName = "Origin";
            string author = "Dan Brown";

            var book = new Book(bookName, author);

            book.AddFootnote(1, "asfsdfssdf");

            Assert.Throws<InvalidOperationException>(() =>
            {
                book.AddFootnote(1, "asfsdfssdf");
            });
        }

        [Test]
        public void Test_FindFootNoteFindsTheRightNoteInTheCollection()
        {
            string bookName = "Origin";
            string author = "Dan Brown";

            var book = new Book(bookName, author);

            book.AddFootnote(1, "asfsdfssdf");
            book.AddFootnote(2, "adsadasdasdads");

            string expectedOutput = "Footnote #2: adsadasdasdads";

            Assert.AreEqual(expectedOutput, book.FindFootnote(2));
        }

        [Test]
        public void Test_FindFootNoteThrowsWhenNoteNumberNotExisting()
        {
            string bookName = "Origin";
            string author = "Dan Brown";

            var book = new Book(bookName, author);

            book.AddFootnote(1, "asfsdfssdf");
            book.AddFootnote(2, "adsadasdasdads");

            Assert.Throws<InvalidOperationException>(() =>
            {
                book.FindFootnote(3);
            });
        }

        [Test]
        public void Test_AlterFootNoteUpdatesFootNoteTextCorrectly()
        {
            string bookName = "Origin";
            string author = "Dan Brown";

            var book = new Book(bookName, author);

            book.AddFootnote(1, "asfsdfssdf");

            book.AlterFootnote(1, "newText");

            string expectedText = "Footnote #1: newText";

            Assert.AreEqual(expectedText, book.FindFootnote(1));
        }

        [Test]
        public void Test_AlterFootNoteShouldThrowWhenNoteNotExisting()
        {
            string bookName = "Origin";
            string author = "Dan Brown";

            var book = new Book(bookName, author);

            book.AddFootnote(1, "asfsdfssdf");

            Assert.Throws<InvalidOperationException>(() =>
            {
                book.AlterFootnote(2, "newText");
            });
        }
    }
}