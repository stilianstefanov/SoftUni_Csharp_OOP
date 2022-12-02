// Use this file for your unit tests.
// When you are ready to submit, REMOVE all using statements to Festival Manager (entities/controllers/etc)
// Test ONLY the Stage class. 
namespace FestivalManager.Tests
{	
	using NUnit.Framework;
    using System;
	using System.Linq;

	[TestFixture]
	public class StageTests
    {
		private Stage stage;
		private Performer performer;
		private Song song;

		[SetUp]
		public void SetUp()
		{
			stage = new Stage();
			performer = new Performer("Marshal", "Matters", 35);
			song = new Song("Without me", TimeSpan.FromMinutes(4.10));
		}

		[Test]
	    public void Test_ConstructorSetsValueProperly()
	    {
			Assert.IsNotNull(stage.Performers);
		}

		[Test]
		public void Test_AddPerformerThrowsWithNullInput()
		{
			Assert.Throws<ArgumentNullException>(() => stage.AddPerformer(null));
		}

		[TestCase(17)]
        [TestCase(1)]
        public void Test_AddPerformerThrowsWithUnder18AgePerformer(int age)
		{
			var newPerformer = new Performer("random", "name", age);

			Assert.Throws<ArgumentException>(() =>
			{
				stage.AddPerformer(newPerformer);
			});
		}

		[Test]
		public void Test_AddPerformerAddsNewPerformerToTheCollection()
		{
			stage.AddPerformer(performer);

			Assert.IsTrue(stage.Performers.Contains(performer));
			Assert.IsTrue(stage.Performers.Any(p => p.FullName== performer.FullName));
			Assert.AreEqual(1, stage.Performers.Count);
		}

		[Test]
		public void Test_AddSongThrowsWithNullInput()
		{
            Assert.Throws<ArgumentNullException>(() => stage.AddSong(null));
        }

		[Test]
		public void Test_AddSongThrowsWithSongsLessThan1Min()
		{
			song = new Song("Without me", TimeSpan.FromMinutes(0.10));

			Assert.Throws<ArgumentException>(() => stage.AddSong(song));
        }

		[Test]
		public void Test_AddSongToPerformerAddsTheCorrectSongToPerformer()
		{
			stage.AddSong(song);
			stage.AddPerformer(performer);

			string expectedOutput = "Without me (04:06) will be performed by Marshal Matters";

            string output = stage.AddSongToPerformer("Without me", "Marshal Matters");

			Assert.AreEqual(expectedOutput, output);
			Assert.IsTrue(performer.SongList.Contains(song));
			Assert.IsTrue(performer.SongList.Any(s => s.Name == "Without me"));
		}

		[TestCase("random", null)]
        [TestCase(null, "random")]
        public void Test_AddSongToPerfomrmerThrowsWithNullInputs(string songName, string performerName)
		{
			Assert.Throws<ArgumentNullException>(() =>
			{
				stage.AddSongToPerformer(songName, performerName);
			});
		}

		[Test]
		public void Test_AddSongToPerformerThrowsWhenPerfomerNotExisting()
		{
            stage.AddSong(song);
            stage.AddPerformer(performer);

			Assert.Throws<ArgumentException>(() =>
			{
				string output = stage.AddSongToPerformer("Without me", "2pac");
			});			
        }

        [Test]
        public void Test_AddSongToPerformerThrowsWhenSongNotExisting()
        {
            stage.AddSong(song);
            stage.AddPerformer(performer);

            Assert.Throws<ArgumentException>(() =>
            {
                string output = stage.AddSongToPerformer("All eyez on me", "Marshal Matters");
            });
        }

		[Test]
		public void Test_PlayReturnsProperOutput()
		{
            stage.AddSong(song);
            stage.AddPerformer(performer);
            stage.AddSongToPerformer("Without me", "Marshal Matters");

            string expectedOutput = "1 performers played 1 songs";

			string output = stage.Play();
			Assert.AreEqual(expectedOutput, output);
        }
    }
}