using NUnit.Framework;
using System;
using System.Linq;

namespace BankSafe.Tests
{
    public class BankVaultTests
    {
        private Item item;
        private BankVault bankVault;

        [SetUp]
        public void Setup()
        {
            bankVault = new BankVault();
            item = new Item("Gosho", "11");
        }

        [Test]
        public void TestItemConstructorSetsValuesProperly()
        {
            Assert.AreEqual("Gosho", item.Owner);
            Assert.AreEqual("11", item.ItemId);
        }

        [Test]
        public void TestBankVaultConstructorSetsValuesProperly()
        {
            var vault = bankVault.VaultCells;

            Assert.IsTrue(vault != null);
            Assert.IsTrue(vault.ContainsKey("A1"));
        }

        [Test]
        public void Test_AddItemThrowsWithNotExistingCell()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                bankVault.AddItem("W2", item);
            });
        }

        [Test]
        public void TestAddItemThrowsWithTakenCell()
        {
            bankVault.AddItem("A1", item);

            Assert.Throws<ArgumentException>(() =>
            {
                bankVault.AddItem("A1", new Item("Pesho", "1"));
            });
        }

        [Test]
        public void TestAddItemThrowsWithDuplicateIdItem()
        {
            bankVault.AddItem("A1", item);

            Assert.Throws<InvalidOperationException>(() =>
            {
                bankVault.AddItem("B2", new Item("Pesho", "11"));
            });
        }

        [Test]
        public void Test_AddItemAddsNewItemToTheVault()
        {
            string output = bankVault.AddItem("A1", item);

            string expectedOutout = "Item:11 saved successfully!";

            var vault = bankVault.VaultCells;

            Assert.AreEqual(expectedOutout, output);
            Assert.IsTrue(vault.Values.Contains(item));
            Assert.AreEqual(vault["A1"] , item);
        }

        [Test]
        public void Test_RemoveItemThrowsWithNotExistingItem()
        {

            Assert.Throws<ArgumentException>(() =>
            {
                bankVault.RemoveItem("W2", item);
            });
        }

        [Test]
        public void Test_RemoveItemThrowsWithNotExistingItemInTheGivenCell()
        {
            bankVault.AddItem("A1", item);

            Assert.Throws<ArgumentException>(() =>
            {
                bankVault.RemoveItem("B1", item);
            });
        }

        [Test]
        public void Test_RemoveItemThrowsWithNotDifferentItemInTheGivenCell()
        {
            Item item2 = new Item("Pepu", "33");
            bankVault.AddItem("A1", item);
            bankVault.AddItem("B1", item2);

            Assert.Throws<ArgumentException>(() =>
            {
                bankVault.RemoveItem("B1", item);
            });
        }

        [Test]
        public void Test_RemoveItemRemovesTheitemFromTheCollection()
        {
            Item item2 = new Item("Pepu", "33");
            bankVault.AddItem("A1", item);
            bankVault.AddItem("B1", item2);
            string expectedOutput = "Remove item:11 successfully!";

            string output = bankVault.RemoveItem("A1", item);
            var vault = bankVault.VaultCells;

            Assert.AreEqual(expectedOutput, output);
            Assert.IsFalse(vault.Values.Contains(item));
            Assert.IsNull(vault["A1"]);
        }
    }
}