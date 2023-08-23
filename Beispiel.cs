using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace SFML_First
{
    internal class Beispiel
    {
        private const int WIDTH = 640;
        private const int HEIGHT = 480;
        private const string TITLE = "First";

        internal static void Stern()
        {
            ContextSettings settings = new ContextSettings();
            settings.AntialiasingLevel = 8;
            VideoMode mode = new VideoMode(WIDTH, HEIGHT);
            RenderWindow window = new RenderWindow(mode, TITLE, Styles.Default, settings);
            window.SetVerticalSyncEnabled(true);

            Texture texture = new Texture(@"H:\c#\SFML\SFML_First\Gold.png");
            texture.Smooth = true;

            CircleShape circ01 = new CircleShape(50);
            circ01.TextureRect = new IntRect(10, 10, 100, 100);
            circ01.Texture = texture;

            RectangleShape line = new RectangleShape(new Vector2f(80.0f, 1.0f));
            line.Position = new Vector2f(180.0f, 220.0f);

            //VertexArray lines = new VertexArray(PrimitiveType.LineStrip, 2);
            //lines.Clear();
            //lines.Append(new Vertex(new Vector2f(100, 10))
            //{
            //    Color = Color.Red
            //});
            //lines.Append(new Vertex(new Vector2f(120, 10))
            //{
            //    Color = Color.Red
            //});


            CircleShape circ02 = new CircleShape(30);
            circ02.FillColor = new Color(0, 0, 255, 255);
            circ02.Position = new Vector2f(120.0f, 20.0f);

            CircleShape octagon = new CircleShape(50, 8);
            octagon.FillColor = new Color(163, 73, 255, 164);
            octagon.Position = new Vector2f(430.0f, 20.0f);

            RectangleShape rect01 = new RectangleShape(new Vector2f(80.0f, 50.0f));
            rect01.FillColor = new Color(0, 255, 0, 255);
            rect01.OutlineThickness = 5;
            rect01.OutlineColor = new Color(255, 0, 0, 255);
            rect01.Position = new Vector2f(210.0f, 20.0f);

            RectangleShape rect02 = new RectangleShape(new Vector2f(80.0f, 50.0f));
            rect02.FillColor = new Color(255, 137, 39, 255);
            rect02.Position = new Vector2f(330.0f, 20.0f);

            ConvexShape cvs = new ConvexShape();
            cvs.SetPointCount(5);
            cvs.SetPoint(0, new Vector2f(0, 0));
            cvs.SetPoint(1, new Vector2f(150, 10));
            cvs.SetPoint(2, new Vector2f(120, 90));
            cvs.SetPoint(3, new Vector2f(30, 100));
            cvs.SetPoint(4, new Vector2f(0, 50));
            cvs.FillColor = new Color(128, 128, 255, 255);
            cvs.Position = new Vector2f(180.0f, 140.0f);

            dynamic[] shapes = new dynamic[6];
            shapes[0] = circ01;
            shapes[1] = circ02;
            shapes[2] = octagon;
            shapes[3] = rect01;
            shapes[4] = rect02;
            shapes[5] = cvs;

            while (window.IsOpen)
            {
                window.DispatchEvents();
                window.Clear(Color.Black);
                window.Draw(circ01);
                window.Draw(circ02);
                window.Draw(rect01);
                window.Draw(rect02);
                window.Draw(octagon);
                window.Draw(cvs);
                window.Draw(line);
                //lines.Draw(window, RenderStates.Default);
                window.Display();
            }
        }
    }
}