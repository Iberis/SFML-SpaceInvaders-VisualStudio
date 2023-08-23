using SFML.Graphics;
using System;

namespace SFML_First
{
    internal class MoveShape
    {
        internal static void MoveLeft(Shape shape)
        {
            float x = shape.Position.X - 1;
            float y = shape.Position.Y - 1;

            shape.Position = new SFML.System.Vector2f(x, y);
        }

        internal static void MoveRight(Shape shape)
        {
            float x = shape.Position.X + 1;
            float y = shape.Position.Y + 1;

            shape.Position = new SFML.System.Vector2f(x, y);
        }
    }
}