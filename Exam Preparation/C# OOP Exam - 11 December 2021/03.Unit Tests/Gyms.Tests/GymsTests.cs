using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Gyms.Tests
{
    public class GymsTests
    {
        [Test]
        public void Test_AthleteConstructorSetsValuesProperly()
        {
            string fullName = "George";

            Athlete athlete = new Athlete(fullName);

            Assert.AreEqual(fullName, athlete.FullName);
            Assert.True(!athlete.IsInjured);
        }

        [Test]
        public void Test_GymConstructorSetsValuesProperly()
        {
            string name = "Sunrise";
            int capacity = 50;

            var gym = new Gym(name, capacity);

            Assert.AreEqual(name, gym.Name);
            Assert.AreEqual(capacity, gym.Capacity);
            Assert.IsTrue(gym.Count == 0);
        }

        [TestCase("")]
        [TestCase(null)]
        public void Test_NameShouldThrowWithEmptyOrNullInput(string name)
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                var gym = new Gym(name, 30);
            });
        }

        [TestCase(-1)]
        [TestCase(-30)]
        public void Test_CapacityShouldThrowWithNegativeInput(int capacity)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                var gym = new Gym("Sunrice", capacity);
            });
        }

        [Test]
        public void Test_CountReturnsProperValue()
        {
            var gym = new Gym("Sunrice", 10);

            gym.AddAthlete(new Athlete("Gosho"));

            int expectedCnt = 1;

            Assert.AreEqual(expectedCnt, gym.Count);
        }

        [Test]
        public void Test_AddAthleteAddsAthleteToTheCollection()
        {
            var gym = new Gym("Sunrice", 10);

            gym.AddAthlete(new Athlete("Gosho"));

            int expectedCnt = 1;

            Assert.AreEqual(expectedCnt, gym.Count);
        }

        [Test]
        public void Test_AddAthleteShouldThrowWhenGymIsFull()
        {
            var gym = new Gym("Sunrice", 1);

            gym.AddAthlete(new Athlete("Gosho"));

            Assert.Throws<InvalidOperationException>(() =>
            {
                gym.AddAthlete(new Athlete("Pesho"));
            });
        }

        [Test]
        public void Test_RemoveAthleteRemovesAthleteFromTheCollection()
        {

            var gym = new Gym("Sunrice", 10);

            gym.AddAthlete(new Athlete("Gosho"));
            gym.RemoveAthlete("Gosho");

            int expectedCnt = 0;

            Assert.AreEqual(expectedCnt, gym.Count);
        }

        [Test]
        public void Test_RemoveAthleteShouldThrowWhenNotFound()
        {

            var gym = new Gym("Sunrice", 10);

            gym.AddAthlete(new Athlete("Gosho"));
         
            Assert.Throws<InvalidOperationException>(() =>
            {
                gym.RemoveAthlete("Pesho");
            });
        }

        [Test]
        public void Test_InjureAthleteSetsiSInjuredToTrue()
        {

            Gym gym = new Gym("Sunrice", 10);
            var athlete = new Athlete("Gosho");
            gym.AddAthlete(athlete);

            Athlete injuredAthlete = gym.InjureAthlete("Gosho");

            Assert.IsTrue(injuredAthlete.IsInjured && injuredAthlete.FullName == athlete.FullName);
        }

        [Test]
        public void Test_InjureAthleteShouldThrowWhenNotFound()
        {
            var gym = new Gym("Sunrice", 10);
            var athlete = new Athlete("Gosho");

            gym.AddAthlete(athlete);

            Assert.Throws<InvalidOperationException>(() =>
            {
                gym.InjureAthlete("Pesho");
            });
        }

        [Test]
        public void Test_ReportReturnsCorrectOutput()
        {
            var gym = new Gym("Sunrice", 10);

            Athlete athlete1 = new Athlete("Gosho");
            Athlete athlete2 = new Athlete("Pesho");

            List<Athlete> athletes = new List<Athlete>() { athlete1, athlete2 };

            gym.AddAthlete(athlete1);
            gym.AddAthlete(athlete2);

            gym.InjureAthlete(athlete2.FullName);

            string expectedString = $"Active athletes at Sunrice: {string.Join(", ", athletes.Where(x => !x.IsInjured).Select(x => x.FullName))}";

            Assert.AreEqual(expectedString, gym.Report());
        }
    }
}
