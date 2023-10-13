using SFML.Graphics;
using SFML.Window;

namespace SpaceInvaders
{
    /**
     * This class sets any SFML-Window related settings.
     */
    internal class InitialiseSFML
    {
        private const string TITLE = "Space Invaders";
        private const uint WINDOW_WIDTH = 570;
        private const uint WINDOW_HEIGHT = WINDOW_WIDTH * (4 / 3);

        internal static RenderWindow InitialiseWindow()
        {
            ContextSettings settings = new ContextSettings
            {
                AntialiasingLevel = 8
            };
            VideoMode mode = new VideoMode(WINDOW_WIDTH, WINDOW_HEIGHT);
            RenderWindow window = new RenderWindow(mode, TITLE, Styles.Default, settings);
            window.SetVerticalSyncEnabled(true);

            return window;
        }
    }
}