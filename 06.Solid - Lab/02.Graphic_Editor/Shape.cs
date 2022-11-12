

using System;

namespace P02.Graphic_Editor
{
    public abstract class Shape : IShape
    {
        public Shape()
        {

        }

        public void Draw()
        {
            Console.WriteLine($"I'm {this.GetType().Name}");
        }
    }
}
