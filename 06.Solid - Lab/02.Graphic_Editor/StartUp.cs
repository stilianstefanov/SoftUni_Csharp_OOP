using System;

namespace P02.Graphic_Editor
{
    public class StartUp
    {
        static void Main()
        {
            IShape circle = new Circle();

            GraphicEditor graphicEditor = new GraphicEditor();

            graphicEditor.DrawShape(circle);
        }
    }
}
