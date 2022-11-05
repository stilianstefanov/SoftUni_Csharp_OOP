

namespace Vehicles.Core
{
    using System;

    using Contracts;
    using Vehicles.IO.Contracts;
    using Vehicles.Models;
    using Vehicles.Models.Contracts;

    public class Engine : IEngine
    {
        private readonly IReader reader;
        private readonly IWriter writer;

        public Engine(IReader reader, IWriter writer)
        {
            this.reader = reader;
            this.writer = writer;
        }

        public void Run()
        {
            string[] carArgs = reader.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);
            string[] truckArgs = reader.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);
            string[] busArgs = reader.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);

            IVehicle car = new Car(double.Parse(carArgs[1]), double.Parse(carArgs[2]), double.Parse(carArgs[3]));
            IVehicle truck = new Truck(double.Parse(truckArgs[1]), double.Parse(truckArgs[2]), double.Parse(truckArgs[3]));
            IVehicle bus = new Bus(double.Parse(busArgs[1]), double.Parse(busArgs[2]), double.Parse(busArgs[3]));

            int commandsCnt = int.Parse(reader.ReadLine());

            for (int i = 0; i < commandsCnt; i++)
            {
                string[] commandArgs = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);
                string command = commandArgs[0];
                string vehicleType = commandArgs[1];
                double value = double.Parse(commandArgs[2]);

                IVehicle vehicle = null;

                if (vehicleType == "Car")
                    vehicle = car;
                else if (vehicleType == "Bus")
                    vehicle = bus;
                else
                    vehicle = truck;

                try
                {
                    if (command == "Drive")
                        writer.WriteLine(vehicle.Drive(value));
                    else if (command == "DriveEmpty")
                        writer.WriteLine(((Bus)vehicle).DriveEmpty(value));
                    else
                        vehicle.Refuel(value);
                }
                catch(Exception e)
                {
                    writer.WriteLine(e.Message);
                }
            }

            writer.WriteLine(car.ToString());
            writer.WriteLine(truck.ToString());
            writer.WriteLine(bus.ToString());
        }
    }
}
