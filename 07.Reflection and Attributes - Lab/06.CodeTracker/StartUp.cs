namespace AuthorProblem
{
    using System;

    [Author("Victor")]
    public class StartUp
    {
        [Author("George")]
        public static void Main(string[] args)
        {
            var tracker = new Tracker();

            tracker.PrintMethodsByAuthor();
        }

        
    }
}
