using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Timers;


namespace SpaceInvaders
{
    /**
     * <summary>
     * This class contains the runtime loop, event handling,
     * as well as causing individual game logic to tick.
     * </summary>
     */
    internal class Renderer
    {
        private readonly RenderWindow window;
        private readonly Game game;
        private readonly Timer timer;

        internal Renderer(RenderWindow window, Game game)
        {
            this.game = game;
            this.window = window;
            this.window.KeyPressed += UserInput.Window_KeyPressed;
            this.window.KeyReleased += UserInput.Window_KeyReleased;
            this.timer = new Timer(400d);
            timer.Elapsed += Tick;
        }

        internal void Render()
        {
            View gameZone = new View(new FloatRect(0f, 0f, Game.WIDTH, Game.HEIGHT))
            {
                Viewport = new FloatRect(0f, 0f, 1f, 1f)
            };
            window.SetView(gameZone);
            timer.Start();

            while (window.IsOpen)
            {
                window.DispatchEvents();
                game.AdvanceShots();
                if (game.ParsePlayerInput())
                    Pause();

                window.Clear(new Color(25, 25, 25));
                foreach (Drawable element in game.Drawables())
                {
                    window.Draw(element);
                }

                //Draws the changes to the view
                window.SetView(gameZone);
                window.Display();
            }
        }

        private void Pause()
        {
            throw new NotImplementedException();
        }

        private void Tick(Object source, ElapsedEventArgs e)
        {
            game.Step();
            game.RandomShot();
        }
    }
}