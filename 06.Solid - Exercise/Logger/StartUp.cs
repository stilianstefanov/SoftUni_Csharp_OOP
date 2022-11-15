
namespace Logger
{
    using Contracts;
    using Factories;
    using Factories.Contracts;

    public class StartUp
    {
        static void Main(string[] args)
        {
            IAppenderFactory appenderFactory = new AppenderFactory();
            ILayoutFactory layoutFactory= new LayoutFactory();

            IEngine test = new Engine(appenderFactory, layoutFactory);

            test.Run();       
        }
    }
}
