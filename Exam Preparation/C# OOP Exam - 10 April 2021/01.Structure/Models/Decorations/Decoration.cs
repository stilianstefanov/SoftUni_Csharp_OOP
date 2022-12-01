﻿namespace AquaShop.Models.Decorations
{
    using Contracts;


    public abstract class Decoration : IDecoration
    {
        private int comfort;
        private decimal price;

        protected Decoration(int comfort, decimal price)
        {
            Comfort= comfort;
            Price= price;
        }

        public int Comfort
        {
            get => comfort;
            private set => comfort = value;
        }

        public decimal Price
        {
            get => price;
            private set => price = value;
        }
    }
}
