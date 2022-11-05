using System;
using System.Collections.Generic;
using System.Text;

namespace Shapes
{
    public class Rectangle : Shape
    {
        private double height;
        private double width;

        public Rectangle(double height, double width)
        {
            this.Height = height;
            this.Width = width;
        }

        
        public double Height
        {
            get { return height; }
            private set { height = value; }
        }
       
        public double Width
        {
            get { return width; }
            private set { width = value; }
        }

        public override double CalculateArea()
            => this.Height * this.Width;


        public override double CalculatePerimeter()
            => 2 * this.Height + 2 * this.Width;

        public override string Draw()
        {
            return base.Draw() + this.GetType().Name;
        }

    }
}
