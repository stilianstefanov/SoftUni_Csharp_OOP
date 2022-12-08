namespace OnlineShop.Models.Products.Computers
{
    using System;
    using System.Collections.Generic; 
    using System.Linq;
    using System.Text;

    using Components;
    using Common.Constants;
    using Peripherals;
    

    public abstract class Computer : Product, IComputer
    {
        private List<IComponent> components;
        private List<IPeripheral> peripherals;

        protected Computer(int id, string manufacturer, string model, decimal price, double overallPerformance) 
            : base(id, manufacturer, model, price, overallPerformance)
        {
            components = new List<IComponent>();
            peripherals = new List<IPeripheral>();
        }

        public IReadOnlyCollection<IComponent> Components => components.AsReadOnly();

        public IReadOnlyCollection<IPeripheral> Peripherals => peripherals.AsReadOnly();

        public override double OverallPerformance
        {
            get
            {
                if (!components.Any())
                {
                    return base.OverallPerformance;
                }
                else
                {
                    return base.OverallPerformance + components.Average(c => c.OverallPerformance);
                }
            }
        }

        public override decimal Price
        {
            get
            {
                return base.Price + components.Sum(c => c.Price) + peripherals.Sum(p => p.Price);
            }
        }

        public void AddComponent(IComponent component)
        {
            if (components.Any(c => c.GetType().Name == component.GetType().Name))
                throw new ArgumentException(string.Format(ExceptionMessages.ExistingComponent,
                     component.GetType().Name, this.GetType().Name, this.Id));

            components.Add(component);
        }

        public void AddPeripheral(IPeripheral peripheral)
        {
            if (peripherals.Any(c => c.GetType().Name == peripheral.GetType().Name))
                throw new ArgumentException(string.Format(ExceptionMessages.ExistingPeripheral,
                     peripheral.GetType().Name, this.GetType().Name, this.Id));

            peripherals.Add(peripheral);
        }

        public IComponent RemoveComponent(string componentType)
        {
            var componentToRemove = components.FirstOrDefault(c => c.GetType().Name == componentType);
            if (componentToRemove == null)
                throw new ArgumentException(string.Format(ExceptionMessages.NotExistingComponent,
                    componentType, this.GetType().Name, this.Id ));

            components.Remove(componentToRemove);
            return componentToRemove;
        }

        public IPeripheral RemovePeripheral(string peripheralType)
        {
            var peripheralToRemove = peripherals.FirstOrDefault(c => c.GetType().Name == peripheralType);
            if (peripheralToRemove == null)
                throw new ArgumentException(string.Format(ExceptionMessages.NotExistingPeripheral,
                    peripheralType, this.GetType().Name, this.Id));

            peripherals.Remove(peripheralToRemove);
            return peripheralToRemove;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine(base.ToString());

            sb.AppendLine($" Components ({Components.Count}):");
            foreach (var component in Components)
            {
                sb.AppendLine($"  {component.ToString()}");
            }

            double averagePerformance = peripherals.Count > 0 ? peripherals.Average(x => x.OverallPerformance) : 0;

            sb.AppendLine($" Peripherals ({Peripherals.Count}); Average Overall Performance ({averagePerformance:F2}):");

            foreach (var peripheral in Peripherals)
            {
                sb.AppendLine($"  {peripheral.ToString()}");
            }

            return sb.ToString().TrimEnd();
        }
    }
}
