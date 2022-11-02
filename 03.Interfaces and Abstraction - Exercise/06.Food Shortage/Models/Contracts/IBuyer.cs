using System;
using System.Collections.Generic;
using System.Text;

namespace BorderControl
{
    public interface IBuyer
    {
        public int Food { get; }
        public string Name { get; }

        public void BuyFood();
    }
}
