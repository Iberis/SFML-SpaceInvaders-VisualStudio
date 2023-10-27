using SFML.Graphics;
using SFML.Window;

namespace SpaceInvaders
{
    /**
     * This class sets any SFML-Window related settings.
     */
    public static class InitialiseSfml
    {
        private const string Title = "Space Invaders";
        private const uint WindowWidth = 570;
        private const uint WindowHeight = WindowWidth * (4 / 3);

        internal static RenderWindow InitialiseWindow()
        {
            ContextSettings settings = new ContextSettings
            {
                AntialiasingLevel = 8
            };
            VideoMode mode = new VideoMode(WindowWidth, WindowHeight);
            RenderWindow window = new RenderWindow(mode, Title, Styles.Default, settings);
            window.SetVerticalSyncEnabled(true);

            return window;
        }
    }
}