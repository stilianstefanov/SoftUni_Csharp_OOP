namespace OnlineShop.Core
{
    using System.Linq;
    using System;
    using System.Collections.Generic;

    using Models.Products.Computers;
    using Models.Products.Components;
    using Models.Products.Peripherals;
    using Common.Constants;   


    public class Controller : IController
    {
        List<IComputer> computers;
        List<IComponent> components;
        List<IPeripheral> peripherals;

        public Controller()
        {
            computers= new List<IComputer>();
            components= new List<IComponent>();
            peripherals= new List<IPeripheral>();
        }

        public string AddComponent(int computerId, int id, string componentType, string manufacturer, string model, decimal price, double overallPerformance, int generation)
        {
            var computerToAddComponent = computers.FirstOrDefault(c => c.Id == computerId);
            if (computerToAddComponent == null)
                throw new ArgumentException(ExceptionMessages.NotExistingComputerId);

            if (components.Any(c => c.Id == id))
                throw new ArgumentException(ExceptionMessages.ExistingComponentId);

            IComponent newComponent = null;
            switch (componentType)
            {
                case "CentralProcessingUnit":
                    newComponent = new CentralProcessingUnit(id, manufacturer, model, price, overallPerformance, generation); break;
                case "Motherboard":
                    newComponent = new Motherboard(id, manufacturer, model, price, overallPerformance, generation); break;
                case "PowerSupply":
                    newComponent = new PowerSupply(id, manufacturer, model, price, overallPerformance, generation); break;
                case "RandomAccessMemory":
                    newComponent = new RandomAccessMemory(id, manufacturer, model, price, overallPerformance, generation); break;
                case "SolidStateDrive":
                    newComponent = new SolidStateDrive(id, manufacturer, model, price, overallPerformance, generation); break;
                case "VideoCard":
                    newComponent = new VideoCard(id, manufacturer, model, price, overallPerformance, generation); break;
                default: 
                    throw new ArgumentException(ExceptionMessages.InvalidComponentType);
            }

            computerToAddComponent.AddComponent(newComponent);
            components.Add(newComponent);

            return string.Format(SuccessMessages.AddedComponent, componentType, id, computerId);
        }

        public string AddComputer(string computerType, int id, string manufacturer, string model, decimal price)
        {
            if (computers.Any(c => c.Id == id))
                throw new ArgumentException(ExceptionMessages.ExistingComputerId);

            IComputer newComputer = null;
            switch (computerType)
            {
                case "Laptop":
                    newComputer = new Laptop(id, manufacturer, model, price);
                    break;
                case "DesktopComputer":
                    newComputer = new DesktopComputer(id, manufacturer, model, price);
                    break;
                default:
                    throw new ArgumentException(ExceptionMessages.InvalidComputerType);                    
            }

            computers.Add(newComputer);
            return string.Format(string.Format(SuccessMessages.AddedComputer, id));
        }

        public string AddPeripheral(int computerId, int id, string peripheralType, string manufacturer, string model, decimal price, double overallPerformance, string connectionType)
        {
            var computerToAddPeripheral = computers.FirstOrDefault(c => c.Id == computerId);
            if (computerToAddPeripheral == null)
                throw new ArgumentException(ExceptionMessages.NotExistingComputerId);

            if (peripherals.Any(c => c.Id == id))
                throw new ArgumentException(ExceptionMessages.ExistingPeripheralId);

            IPeripheral newPeripheral = null;
            switch (peripheralType)
            {
                case "Headset":
                    newPeripheral = new Headset(id, manufacturer, model, price, overallPerformance, connectionType); break;
                case "Keyboard":
                    newPeripheral = new Keyboard(id, manufacturer, model, price, overallPerformance, connectionType); break;
                case "Monitor":
                    newPeripheral = new Monitor(id, manufacturer, model, price, overallPerformance, connectionType); break;
                case "Mouse":
                    newPeripheral = new Mouse(id, manufacturer, model, price, overallPerformance, connectionType); break;
                default:
                    throw new ArgumentException(ExceptionMessages.InvalidPeripheralType);
            }

            computerToAddPeripheral.AddPeripheral(newPeripheral);
            peripherals.Add(newPeripheral);

            return string.Format(SuccessMessages.AddedPeripheral, peripheralType, id, computerId);
        }

        public string BuyBest(decimal budget)
        {
            IComputer computerToBuy = null;

            foreach (var computer in computers.OrderByDescending(c => c.OverallPerformance))
            {
                if (computer.Price <= budget)
                {
                    computerToBuy = computer;
                    break;
                }
            }

            if (computerToBuy == null)
                throw new ArgumentException(string.Format(ExceptionMessages.CanNotBuyComputer, budget));

            computers.Remove(computerToBuy);
            return computerToBuy.ToString();
        }

        public string BuyComputer(int id)
        {
            var computer = computers.FirstOrDefault(c => c.Id == id);
            if (computer == null)
                throw new ArgumentException(ExceptionMessages.NotExistingComputerId);

            computers.Remove(computer);

            return computer.ToString();
        }

        public string GetComputerData(int id)
        {
            var computer = computers.FirstOrDefault(c => c.Id == id);
            if (computer == null)
                throw new ArgumentException(ExceptionMessages.NotExistingComputerId);

            return computer.ToString();
        }

        public string RemoveComponent(string componentType, int computerId)
        {
            var computer = computers.FirstOrDefault(c => c.Id == computerId);
            if (computer == null)
                throw new ArgumentException(ExceptionMessages.NotExistingComputerId);

            var componentToRemove = computer.RemoveComponent(componentType);
            components.Remove(componentToRemove);

            return string.Format(SuccessMessages.RemovedComponent, componentType, componentToRemove.Id);
        }

        public string RemovePeripheral(string peripheralType, int computerId)
        {
            var computer = computers.FirstOrDefault(c => c.Id == computerId);
            if (computer == null)
                throw new ArgumentException(ExceptionMessages.NotExistingComputerId);

            var peripheralToRemove = computer.RemovePeripheral(peripheralType);
            peripherals.Remove(peripheralToRemove);

            return string.Format(SuccessMessages.RemovedPeripheral, peripheralType, peripheralToRemove.Id);
        }
    }
}
