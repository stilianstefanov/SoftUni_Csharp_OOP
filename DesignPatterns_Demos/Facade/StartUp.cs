namespace Facade
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            var car = new CarBuilderFacade()
                .Info
                  .WithType("BMW")
                  .WithColor("Black")
                  .WithNumberOfDoors(5)
                .Built
                  .InCity("Leipzig")
                  .AtAddress("Some address")
                .Build();

            Console.WriteLine(car);
        }
    }
}