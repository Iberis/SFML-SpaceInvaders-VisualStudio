using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System.Collections.Generic;
using System.Net.Http.Headers;

namespace SFML_First
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            RenderWindow window = InitialiseSFML.InitialiseWindow();
            List<Shape> shapes = Shapes.GetShapes();
            RenderShapes renderer = new RenderShapes(window, shapes);
            renderer.Render();
        }
    }
}



