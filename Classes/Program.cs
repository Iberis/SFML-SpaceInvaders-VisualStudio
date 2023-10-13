using SFML.Graphics;

namespace SpaceInvaders
{
    /**
     * <summary>
     * This class contains the Main.
     * </summary>
     */
    internal class Program
    {
        private static void Main(string[] args)
        {
            RenderWindow window = InitialiseSFML.InitialiseWindow();
            Renderer renderer = new Renderer(window, Game.GetInstance());
            renderer.Render();
        }
    }
}



