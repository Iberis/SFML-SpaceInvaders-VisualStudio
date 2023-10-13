using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;

namespace SpaceInvaders
{
    /**
     * <summary>
     * Master class overseeing all game related logic.
     * </summary>
     */
    internal class Game
    {
        private static Game singleton = new Game();
        internal static Game GetInstance()
        {
            return singleton;
        }
        /**
         * <summary>
         * Resets the state of the instance.
         * Textures are specifically not reloaded.
         * </summary>
         */
        internal static Game NewGame()
        {
            singleton = new Game();
            return singleton;
        }

        internal static Textures Textures { get; } = Textures.GetInstance();
        // Logical Pixels
        internal const int WIDTH = 285;
        internal const int HEIGHT = WIDTH * (4/3);
        private const int BUNKER_COUNT = 4;
        private const float BUNKER_BORDER_OFFSET = WIDTH/(BUNKER_COUNT + 1);

        private bool stepRight;
        private readonly Random rng;

        #region Sprites
        // Positions of all invaders, living and dead,
        // are updated and checked to facilitate smooth movement calculations.
        private readonly Invader[] invaders;
        // List of only the invaders considered alive is used
        // for shooting logic and display
        private readonly List<Invader> livingInvaders;
        private readonly List<InvaderShot> invaderShots;
        private readonly Sprite player;
        private Sprite playerShot = GamePieces.SHOT_EMPTY;
        private readonly List<Bunker> bunkers;
        private readonly RectangleShape topLine;
        private readonly RectangleShape bottomLine;
        #endregion

        private Game()
        {
            stepRight = true;
            rng = new Random();

            player = GamePieces.GetPlayer();
            invaders = GamePieces.GetInvaders();
            livingInvaders = new List<Invader>(invaders);
            invaderShots = new List<InvaderShot>();
            bunkers = new List<Bunker>();
            placeBunkers();

            bottomLine = new RectangleShape(new Vector2f(WIDTH, 1))
            {
                FillColor = new Color(32, 255, 32),
                Position = new Vector2f(0, HEIGHT - 30)
            };
            topLine = new RectangleShape(new Vector2f(WIDTH, 1))
            {
                FillColor = new Color(32, 255, 32),
                Position = new Vector2f(0, 25)
            };

            void placeBunkers()
            {
                float height = HEIGHT - (player.GetGlobalBounds().Height * 6 + Bunker.HEIGHT);
                Vector2f startpos = new Vector2f(BUNKER_BORDER_OFFSET - Bunker.WIDTH / 2, height);
                for (int i = 0; i < BUNKER_COUNT; i++)
                {
                    bunkers.Add(new Bunker(startpos));
                    startpos.X += BUNKER_BORDER_OFFSET;
                }
            }
        }

        // Triggered on playerShot hitting an Invader
        // The dying invader is left in the attribute List "livingInvaders"
        // for the purpose of the death animation. It is then removed 
        // the next time Step() is called.
        private void Kill(Invader invader)
        {
            invader.Die();
            playerShot = GamePieces.SHOT_EMPTY;
        }

        /**
         * <summary>
         * Rolls to see if an invader should spawn a shot
         * and assigns the task to a random invader if so.
         * The shot is then added to a List for savekeeping.
         * </summary>
         */
        internal void RandomShot()
        {
            if (!(rng.Next(3) == 0))
            {
                return;
            }

            int i = rng.Next(livingInvaders.Count);
            invaderShots.Add(livingInvaders[i].Shoot());
        }

        /**
         * <summary>
         * Takes care of logic for all shots in flight at time
         * of calling this method. For both Invaders and Player.
         * </summary>
         */
        internal void AdvanceShots()
        {
            MoveSprites.Ascent(playerShot);
                
            foreach (InvaderShot shot in invaderShots)
            {
                MoveSprites.Descent(shot);
            }
        }

        private delegate void StepAction(Invader invader);
        /**
         * <summary>
         * Determines and triggers the next movement action
         * for all Invaders as a block.
         * </summary>
         */
        internal void Step()
        {
            StepAction action;
            if (stepRight)
            {
                /* 
                * 12 is the width of the large alien
                * 13 is the distance in logical pixels to the edge of 
                * the game area after which movement in that direction stops
                */
                if (WIDTH - invaders[10].Sprite.Position.X 
                    - 12 < 13)
                {
                    action = MoveSprites.StepDown;
                    stepRight = false;
                }
                else
                {
                    action = MoveSprites.StepRight;
                }
            }
            else
            {
                if (invaders[0].Sprite.Position.X < 13)
                {
                    action = MoveSprites.StepDown;
                    stepRight = true;
                }
                else
                {
                    action = MoveSprites.StepLeft;
                }
            }

            foreach (Invader invader in invaders)
            {
                action(invader);

                if (invader.IsDying)
                {
                    livingInvaders.Remove(invader);
                    invader.ResolveDeath();
                }
            }
        }
        
        /**
         * <summary>
         * Returns all game elements which are drawable
         * </summary>
         */
        internal List<Drawable> Drawables()
        {
            List<Drawable> all = new List<Drawable>
            {
                player,
                playerShot,
                bottomLine,
                topLine
            };
            foreach (Bunker bunker in bunkers)
            {
                all.Add(bunker.GetSprite());
            }
            foreach (Invader invader in livingInvaders)
            {
                all.Add(invader.Sprite);
            }
            foreach (InvaderShot shot in invaderShots)
            {
                all.Add(shot.Sprite);
            }

            return all;
        }

        /**
         * <summary>
         * Checks for queued up user inputs and applies the
         * logical consequences to the game logic.
         * </summary>
         * <returns>Returns true only if a game pause was requested.</returns>
         */
        internal bool ParsePlayerInput()
        {
            if (UserInput.Pause)
            {
                UserInput.Pause = false;
                return true;
            }

            if (UserInput.InputStates["Left"])
            {
                player.Position += new Vector2f(-1.0f, 0.0f);
            }
            else if (UserInput.InputStates["Right"])
            {
                player.Position += new Vector2f(1.0f, 0.0f);
            }

            if (UserInput.InputStates["TryShoot"])
            {
                if (playerShot.Equals(GamePieces.SHOT_EMPTY))
                {
                    playerShot = GamePieces.GeneratePlayerShot(player);
                }
                UserInput.InputStates["TryShoot"] = false;
            }
            return false;
        }
    }
}