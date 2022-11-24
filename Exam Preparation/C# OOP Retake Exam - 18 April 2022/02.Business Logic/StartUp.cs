namespace Heroes
{
    using Heroes.Core;
    using Heroes.Core.Contracts;

    public class StartUp
    {
        public static void Main()
        {
            IEngine engine = new Engine();
            engine.Run();
        }
    }
}
