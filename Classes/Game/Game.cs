using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace SpaceInvaders
{
    /**
     * <summary>
     * Master class overseeing all game related logic.
     * </summary>
     */
    internal class Game
    {
        private static Game _singleton = new Game();
        internal static Game GetInstance()
        {
            return _singleton;
        }
        /**
         * <summary>
         * Resets the state of the instance.
         * Textures are specifically not reloaded.
         * </summary>
         */
        internal static Game NewGame()
        {
            _singleton = new Game();
            return _singleton;
        }

        // Logical Pixels
        internal const int WIDTH = 285;
        internal const int HEIGHT = WIDTH * (4/3);
        private const int BUNKER_COUNT = 4;
        private const float BUNKER_BORDER_OFFSET = WIDTH/(BUNKER_COUNT + 1f);

        private bool stepRight;
        private readonly Random rng;
        private int extraLives;

        #region Sprites
        // Positions of all invaders, living and dead,
        // are updated and checked to facilitate smooth movement calculations.
        private readonly Invader[] invaders;
        // List of only the invaders considered alive is used
        // for shooting logic and display
        private readonly List<Invader> livingInvaders;
        private readonly List<InvaderShot> invaderShots;
        private readonly Sprite player;
        private readonly List<Sprite> extraLiveSprites;
        private Sprite playerShot = GamePieces.SHOT_EMPTY;
        private readonly List<Bunker> bunkers;
        private readonly RectangleShape topLine;
        private readonly RectangleShape bottomLine;
        #endregion

        private Game()
        {
            stepRight = true;
            rng = new Random();
            extraLives = 3;

            player = GamePieces.GetPlayer();
            invaders = GamePieces.GetInvaders();
            livingInvaders = new List<Invader>(invaders);
            invaderShots = new List<InvaderShot>();
            bunkers = new List<Bunker>();
            PlaceBunkers();
            
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
            
            extraLiveSprites = new List<Sprite>();
            PlacePlayerLives();
            return;

            void PlaceBunkers()
            {
                float height = HEIGHT - (player.GetGlobalBounds().Height * 6 + Bunker.HEIGHT);
                Vector2f startPosition = new Vector2f(BUNKER_BORDER_OFFSET - Bunker.WIDTH / 2f, height);
                for (int i = 0; i < BUNKER_COUNT; i++)
                {
                    bunkers.Add(new Bunker(startPosition));
                    startPosition.X += BUNKER_BORDER_OFFSET;
                }
            }
            
            void PlacePlayerLives()
            {
                Vector2f position = new(7, bottomLine.Position.Y + 5);
                for (int i = extraLives; i > 0; i--)
                {
                    Sprite tmp = GamePieces.GetPlayer();
                    tmp.Position = position;
                    extraLiveSprites.Add(tmp);
                    position.X = position.X + tmp.GetGlobalBounds().Width + 7;
                }
            }
        }

        /**
         * <summary>
         * Rolls to see if an invader should spawn a shot
         * and assigns the task to a random invader if so.
         * The shot is then added to a List for save-keeping.
         * </summary>
         */
        internal void RandomShot()
        {
            if (rng.Next(3) != 0)
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
        #region playerShot
            MoveSprites.Ascent(playerShot);
            foreach (Invader invader in livingInvaders.Where(invader => DetectCollisionShot(playerShot, invader.Sprite)))
            {
                // The dying invader is left in the attribute List "livingInvaders"
                // for the purpose of the death animation. It is then removed 
                // the next time Step() is called.
                invader.Die();
                //TODO: impact
                playerShot = GamePieces.SHOT_EMPTY;
            }
            // check if playerShot out of bounds
            if (playerShot.Position.Y <= topLine.Position.Y)
            {
                //TODO: impact
                playerShot = GamePieces.SHOT_EMPTY;
            }
        #endregion playerShot

        #region invaderShots
            for (int i = invaderShots.Count - 1; i >= 0; i--)
            {
                InvaderShot shot = invaderShots[i];
                
                if (shot.NeedCleanUp)
                {
                    invaderShots.RemoveAt(i);
                    continue;
                }
                if (shot.IsDying)
                {
                    shot.Animate();
                    continue;
                }
                
                MoveSprites.Descent(shot);
                if (DetectCollisionShot(shot.Sprite, player))
                {
                    shot.Impact();
                    PlayerHit();
                }
                // check if Invader Shots out of bounds
                if ((shot.Sprite.Position.Y + shot.Sprite.GetGlobalBounds().Height) >= bottomLine.Position.Y)
                {
                    shot.Impact();
                }
            }
        #endregion invaderShots
        }

        private static bool DetectCollisionShot(Sprite shot, Sprite other)
        { 
            bool collisionX = (shot.Position.X + shot.GetGlobalBounds().Width) >= other.Position.X
                && (other.Position.X + other.GetGlobalBounds().Width) >= shot.Position.X;
            bool collisionY = (shot.Position.Y + shot.GetGlobalBounds().Height) >= other.Position.Y
                && (other.Position.Y + other.GetGlobalBounds().Height) >= shot.Position.Y;

            return collisionX && collisionY;
        }

        /**
         * <summary>
         * Functionality for after the player got hit.
         * </summary>
         */
        private void PlayerHit()
        {
            if (extraLives > 0)
            {
                extraLives--;
                extraLiveSprites.RemoveAt(extraLiveSprites.Count - 1);
            }
            else
            {
                //Player Death --TODO
            }
        }
        
        private delegate void StepAction(Invader invader);
        /**
         * <summary>
         * Determines and triggers the next movement action
         * for all Invaders as a block.
         * </summary>
         */
        [SuppressMessage("ReSharper", "InvertIf")]
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
                if (WIDTH - invaders[10].Sprite.Position.X - 12 < 13)
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
            all.AddRange(extraLiveSprites);
            all.AddRange(bunkers.Select(bunker => bunker.GetSprite()));
            all.AddRange(livingInvaders.Select(invader => invader.Sprite));
            all.AddRange(invaderShots.Select(shot => shot.Sprite));

            return all;
        }

        /**
         * <summary>
         * Checks for queued up user inputs and applies the
         * logical consequences to the game logic.
         * </summary>
         * <returns>Returns true only if a game pause was requested.</returns>
         */
        [SuppressMessage("ReSharper", "InvertIf")]
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