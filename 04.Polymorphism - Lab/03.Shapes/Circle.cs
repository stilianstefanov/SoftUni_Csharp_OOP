using System;
using System.Collections.Generic;
using System.Text;

namespace Shapes
{
    public class Circle : Shape
    {
        private double radius;

        public Circle(double radius)
        {
            this.Radius = radius;
        }
       
        public double Radius
        {
            get { return radius; }
            private set { radius = value; }
        }


        public override double CalculateArea()
            => Math.PI * (Radius * Radius);


        public override double CalculatePerimeter()
            => 2 * Math.PI * Radius;

        public override string Draw()
            => base.Draw() + this.GetType().Name;
        
    }
}
