using SFML.Graphics;

namespace SpaceInvaders
{
    /**
     * <summary>
     * This class contains the Main.
     * </summary>
     */
    public static class Program
    {
        private static void Main(string[] args)
        {
            RenderWindow window = InitialiseSfml.InitialiseWindow();
            Renderer renderer = new Renderer(window, Game.GetInstance());
            renderer.Render();
        }
    }
}



