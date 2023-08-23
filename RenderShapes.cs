using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;

namespace SFML_First
{
    internal class RenderShapes
    {

        private readonly RenderWindow window;
        private readonly List<Shape> shapes;

        internal RenderShapes(RenderWindow window, List<Shape> shapes)
        {
            this.shapes = shapes;
            this.window = window;
        }

        internal void Render()
        {
            while (window.IsOpen)
            {
                window.DispatchEvents();
                window.Clear(Color.Black);

                foreach (dynamic element in shapes)
                {
                    window.Draw(element);
                }

                Animate();

                window.Display();
            }
        }

        private void Animate()
        {
            MoveShape.MoveRight(shapes[0]);
            MoveShape.MoveLeft(shapes[1]);
        }
    }
}