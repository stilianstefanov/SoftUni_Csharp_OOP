namespace Composite
{
    using Contracts;


    public class CompositeGift : GiftBase, IGiftOperations
    {
        private ICollection<GiftBase> _gifts;

        public CompositeGift(string name, int price) 
            : base(name, price)
        {
            _gifts= new HashSet<GiftBase>();
        }

        public void Add(GiftBase gift)
            => _gifts.Add(gift);

        public void Remove(GiftBase gift)
            => _gifts.Remove(gift);

        public override int CalculateTotalPrice()
        {
            int total = 0;

            Console.WriteLine($"{name} contains the following products with prices:");

            foreach (var gift in _gifts)
            {
                total += gift.CalculateTotalPrice();
            }

            return total;
        }
    }
}
