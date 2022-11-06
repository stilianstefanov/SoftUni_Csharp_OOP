

namespace WildFarm.Models.Abstracts
{
    public abstract class Food
    {
        protected Food(int quantity)
        {
            Quantity = quantity;
        }

        public string Type { get { return this.GetType().Name; } }
        public int Quantity { get; private set; }
    }
}
