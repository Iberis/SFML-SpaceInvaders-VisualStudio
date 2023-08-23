using SFML.Graphics;
using SFML.Window;
using System.Collections.Generic;

namespace SFML_First
{
    internal class InitialiseSFML
    {
        private const int HEIGHT = 480;
        private const string TITLE = "Second";
        private const int WIDTH = 640;

        internal static RenderWindow InitialiseWindow()
        {
            ContextSettings settings = new ContextSettings();
            settings.AntialiasingLevel = 8;
            VideoMode mode = new VideoMode(WIDTH, HEIGHT);
            RenderWindow window = new RenderWindow(mode, TITLE, Styles.Default, settings);
            window.SetVerticalSyncEnabled(true);

            return window;
        }
    }
}