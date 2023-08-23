using SFML.Graphics;
using SFML.System;
using System.Collections.Generic;

namespace SFML_First
{
    internal class Shapes
    {

        internal static List<Shape> GetShapes()
        {
            List<Shape> shapes = new List<Shape>
            {
                new RectangleShape(new Vector2f(80.0f, 50.0f))
                {
                    FillColor = new Color(0, 255, 0, 255),
                    OutlineThickness = 5,
                    OutlineColor = new Color(255, 0, 0, 255),
                    Position = new Vector2f(0.0f, 0.0f),
                },

                new CircleShape(30)
                {
                    FillColor = new Color(0, 0, 255, 255),
                    Position = new Vector2f(120.0f, 20.0f),
                }
            };

            return shapes;
        }
    }
}