namespace MilitaryElite
{
    using Core;
    using Core.Contracts;
    using IO;
    using IO.Contracts;
    public class StartUp
    {
        static void Main(string[] args)
        {
            IWriter writer = new ConsoleWriter();
            IReader reader = new ConsoleReader();

            IEngine engine = new Engine(writer, reader);

            engine.Run();
        }
    }
}
