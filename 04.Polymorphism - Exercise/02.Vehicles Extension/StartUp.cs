

namespace Vehicles
{
    using IO;
    using IO.Contracts;
    using Vehicles.Core;
    using Vehicles.Core.Contracts;

    public class StartUp
    {
        static void Main(string[] args)
        {
            IReader reader = new Reader();
            IWriter writer = new Writer();

            IEngine engine = new Engine(reader, writer);

            engine.Run();
        }
    }
}
