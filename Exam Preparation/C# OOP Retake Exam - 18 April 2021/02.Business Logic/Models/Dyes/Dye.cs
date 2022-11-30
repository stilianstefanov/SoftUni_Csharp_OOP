namespace Easter.Models.Dyes
{
    using Contracts;


    public class Dye : IDye
    {
        private int power;

        public Dye(int power)
        {
            Power= power;
        }

        public int Power
        {
            get => power;
            private set
            {
                if (value < 0)
                {
                    power = 0;
                }
                else
                {
                    power = value;
                }
            }
        }

        public bool IsFinished()
            => Power == 0;
       

        public void Use()
        {
            Power -= 10;
        }
    }
}
