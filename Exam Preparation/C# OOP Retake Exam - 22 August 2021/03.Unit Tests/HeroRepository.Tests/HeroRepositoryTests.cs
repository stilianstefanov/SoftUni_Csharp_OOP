using System;
using System.Linq;
using NUnit.Framework;


[TestFixture]
public class HeroRepositoryTests
{
    private Hero hero1;
    private HeroRepository heroRep;

    [SetUp]
    public void SetUp()
    {
        hero1 = new Hero("Alucard", 10);
        heroRep = new HeroRepository();
    }

    [Test]
    public void Test_HeroConstructorSetsValuesProperly()
    {
        Assert.AreEqual("Alucard", hero1.Name);
        Assert.AreEqual(10, hero1.Level);
    }

    [Test]
    public void Test_HeroRepositoryConstructorInitializesCollection()
    {
        Assert.IsTrue(heroRep.Heroes != null);
    }

    [Test]
    public void Test_HeroesResturnsProperCollection()
    {
        heroRep.Create(hero1);
        Assert.IsTrue(heroRep.Heroes.Count == 1);
    }

    [Test]
    public void Test_CreateThrowsIfHeroIsNull()
    {
        Assert.Throws<ArgumentNullException>(() =>
        {
            heroRep.Create(null);
        });
    }

    [Test]
    public void Test_CreateThrowsWhenDuplicateHero()
    {
        heroRep.Create(hero1);

        Assert.Throws<InvalidOperationException>(() =>
        {            
            heroRep.Create(hero1);
        });
    }

    [Test]
    public void Test_CreateThrowsWhenDuplicateNameHero()
    {
        heroRep.Create(hero1);
        Assert.Throws<InvalidOperationException>(() =>
        {
            heroRep.Create(new Hero("Alucard", 7));
        });
    }

    [Test]
    public void Test_CreateSuccesfullyAddsHeroToCollection()
    {
        string expectedOutput = "Successfully added hero Alucard with level 10";

        string output = heroRep.Create(hero1);
        Hero outputHero = heroRep.GetHero("Alucard");

        Assert.AreEqual(expectedOutput, output);

        Assert.IsTrue(outputHero.Name == hero1.Name);
    }

    [TestCase("")]
    [TestCase(" ")]
    [TestCase(null)]
    public void Test_RemoveThrowsWhenInputIsNullOrWhiteSpace(string name)
    {
        Assert.Throws<ArgumentNullException>(() =>
        {
            heroRep.Remove(name);
        });
    }

    [Test]
    public void Test_RemoveRemovesTheRightHero()
    {
        heroRep.Create(hero1);

        bool output = heroRep.Remove("Alucard");

        Assert.IsTrue(output);

        Assert.IsFalse(heroRep.Heroes.Contains(hero1));
    }

    [Test]
    public void Test_RemoveReturnsFalseWhenHeroNotFound()
    {
        heroRep.Create(hero1);

        bool output = heroRep.Remove("Roger");

        Assert.IsFalse(output);
    }

    [Test]
    public void Test_GetHeroWithHighestLevelReturnsValidObject()
    {
        heroRep.Create(hero1);
        Hero hero2 = new Hero("Roger", 15);

        heroRep.Create(hero2);

        Hero outputHero = heroRep.GetHeroWithHighestLevel();

        Assert.AreEqual(hero2, outputHero);
        Assert.AreEqual(hero2.Name, outputHero.Name);
    }

    [Test]
    public void Test_GetHeroReturnsHeroFromTheCollection()
    {
        heroRep.Create(hero1);

        Hero outputHero = heroRep.GetHero("Alucard");

        Assert.AreEqual(hero1, outputHero);
        Assert.AreEqual(hero1.Name, outputHero.Name);
    }


}