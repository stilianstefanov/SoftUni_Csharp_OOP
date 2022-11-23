using NUnit.Framework;
using System;
using System.Linq;

namespace PlanetWars.Tests
{
    public class Tests
    {
        [TestFixture]
        public class PlanetWarsTests
        {
            private Weapon weapon;
            private Planet planet;

            private const string WeaponName = "Laser";
            private const double Price = 100;
            private const int DestructionLevel = 5;
         
            private const string PlanetName = "Pandora";
            private const double PlanetBudget = 500;

            [SetUp]
            public void SetUp()
            {
                

                weapon = new Weapon(WeaponName, Price, DestructionLevel);
                planet = new Planet(PlanetName, PlanetBudget);
            }

            [Test]
            public void Test_WeaponConstructorWorksProperly()
            {
                Assert.AreEqual(WeaponName, weapon.Name);
                Assert.AreEqual(Price, weapon.Price);
                Assert.AreEqual(DestructionLevel, weapon.DestructionLevel);
            }

            [Test]
            public void Test_PlanetConstructorWorksProperly()
            {
                Assert.AreEqual(PlanetName, planet.Name);
                Assert.AreEqual(PlanetBudget, planet.Budget);
                Assert.True(planet.Weapons != null);
            }

            [Test]
            public void Test_WeaponPriceNegativeShouldThrow()
            {
                Assert.Throws<ArgumentException>(() =>
                {
                    weapon = new Weapon(WeaponName, -1, DestructionLevel);
                }, "Price cannot be negative.");
            }

            [Test]
            public void Test_IncreaseDestructionLevelWorksProperly()
            {
                int expectedDestructionLevel = 6;

                weapon.IncreaseDestructionLevel();

                Assert.AreEqual(weapon.DestructionLevel, expectedDestructionLevel);
            }

            [Test]
            public void Test_IsNuclearReturnsValidValue()
            {
                bool expectedResult = false;
                Assert.AreEqual(expectedResult, weapon.IsNuclear);

                weapon = new Weapon(WeaponName, Price, 11);
                bool expectedTrueResult = true;

                Assert.AreEqual(expectedTrueResult, weapon.IsNuclear);
            }

            [TestCase("")]
            [TestCase(null)]
            public void Test_PlanetNameCannotBeNullOrEmpty(string planetName)
            {
                Assert.Throws<ArgumentException>(() =>
                {
                    planet = new Planet(planetName, PlanetBudget);
                }, "Invalid planet Name");
            }

            [Test]
            public void Test_BudgetCannotBeNegative()
            {
                double negativeBudget = -1;

                Assert.Throws<ArgumentException>(() =>
                {
                    planet = new Planet(PlanetName, negativeBudget);
                }, "Budget cannot drop below Zero!");
            }

            [Test]
            public void Test_MilitaryPowerRatioCalculatesProperly()
            {
                Weapon weapon2 = new Weapon("Bomb", 60, 10);

                planet.AddWeapon(weapon);
                planet.AddWeapon(weapon2);

                double expectedPowerRatio = 15;

                Assert.AreEqual(expectedPowerRatio, planet.MilitaryPowerRatio);
            }

            [Test]
            public void Test_ProfitIncreasesBudgetProperly()
            {
                double amount = 50;
                planet.Profit(amount);

                double expectedAmount = 550;

                Assert.AreEqual(expectedAmount, planet.Budget);
            }

            [Test]
            public void Test_SpendFundsWorksProperly()
            {
                double amount = 50;
                planet.SpendFunds(amount);

                double expectedBudgetLeft = 450;

                Assert.AreEqual(expectedBudgetLeft, planet.Budget);
            }

            [Test]
            public void Test_SpendFundsNotEnoughBudgetShouldThrow()
            {
                double amount = 550;
                Assert.Throws<InvalidOperationException>(() =>
                {
                    planet.SpendFunds(amount);
                }, "Not enough funds to finalize the deal.");                
            }

            [Test]
            public void Test_AddWeaponDuplicateNameShouldThrow()
            {
                Assert.Throws<InvalidOperationException>(() =>
                {
                    planet.AddWeapon(weapon);
                    planet.AddWeapon(weapon);
                }, "There is already a weapon with the same name.");
            }

            [Test]
            public void Test_RemoveWeaponRemovesTheWeapon()
            {
                planet.AddWeapon(weapon);
                planet.RemoveWeapon(WeaponName);

                Weapon weaponTest = planet.Weapons.FirstOrDefault(w => w.Name == WeaponName);

                Assert.AreEqual(null, weaponTest);
            }

            [Test]
            public void Test_UpgradeWeaponIncreasesWeaponDestructionLevel()
            {
                planet.AddWeapon(weapon);

                planet.UpgradeWeapon(WeaponName);

                int expectedDestructionLevel = 6;

                Assert.AreEqual(expectedDestructionLevel, weapon.DestructionLevel);
            }

            [Test]
            public void Test_UpgradeWeaponInvalidWeapnShouldThrow()
            {
                planet.AddWeapon(weapon);

                Assert.Throws<InvalidOperationException>(() =>
                {
                    planet.UpgradeWeapon("DeathStar");
                }, "Weapon does not exist in the weapon repository");
            }

            [Test]
            public void Test_DestructOpponentWorksProperly()
            {
                planet.AddWeapon(weapon);

                Weapon weapon2 = new Weapon("Bomb", 70, 3);
                Planet planet2 = new Planet("Earth", 400);
                planet2.AddWeapon(weapon2);

                string expectedResult = "Earth is destructed!";

                Assert.AreEqual(expectedResult, planet.DestructOpponent(planet2));
            }

            [Test]
            public void Test_DestructShouldThrowWithStrongerEnemy()
            {
                planet.AddWeapon(weapon);

                Weapon weapon2 = new Weapon("Bomb", 70, 10);
                Planet planet2 = new Planet("Earth", 400);
                planet2.AddWeapon(weapon2);

                Assert.Throws<InvalidOperationException>(() =>
                {
                    planet.DestructOpponent(planet2);
                }, "Opponent is too strong to declare war to!");
            }
        }
    }
}
