using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;

namespace SpaceInvaders
{
    /**
     * <summary>
     * This class houses all logic for generating new sprites,
     * or the sprite based logic for objects that require a sprite during creation.
     * </summary>
     */
    internal static class GamePieces
    {
        private const float PLAYER_SPRITE_SCALING_FACTOR = 0.0362f;
        //Space between two alien sprites. Size + Offset
        private const int OFFSET_HORIZONTAL = 12 + 5;
        private const int OFFSET_VERTICAL = 8 + 5;
        // 182 is the width of one alien row
        private static readonly Vector2f ALIEN_START_POSITION = new Vector2f(
            Game.WIDTH / 2f - 182f / 2f,
            57f
        );
        internal static readonly Sprite SHOT_EMPTY = new Sprite()
        {
            Position = new Vector2f(0, 0),
            Color = Color.Black
        };

        /**
         * <summary>
         * Generates a player shot based on the current position of the player.
         * </summary>
         */
        internal static Sprite GeneratePlayerShot(Sprite player)
        {
            Sprite shot = new Sprite(Textures.GetInstance().playerShot)
            {
                Scale = new Vector2f(1f / 2f, 1f / 2f)
            };

            float xOffset = (player.GetGlobalBounds().Width / 2) - (shot.GetGlobalBounds().Width / 2);
            shot.Position = player.Position + new Vector2f(xOffset, -shot.GetGlobalBounds().Height);

            return shot;
        }

        /**
         * <summary>
         * Generates an InvaderShot based on parameters provided by the Invader calling this method.
         * </summary>
         * <param name="type">Determines the shot textures. 
         * Also small type shots are slightly shorter.</param>
         * <param name="startPosition">Used to initialise the sprite position</param>
         */
        internal static InvaderShot GenerateInvaderShot(InvaderType type, Vector2f startPosition)
        {
            Texture[] textures = type switch
            {
                InvaderType.Small => Textures.GetInstance().invaderSmallShots,
                InvaderType.Medium => Textures.GetInstance().invaderMediumShots,
                InvaderType.Large => Textures.GetInstance().invaderLargeShots,
                _ => throw new NotImplementedException("Shot textures not implemented for InvaderType: " + type)
            };

            Sprite sprite = new Sprite(textures[0])
            {
                Position = startPosition,
                Scale = new Vector2f(1f / 2f, 1f / 2f)
            };

            return new InvaderShot(textures, sprite, type);
        }

        /**
         * <summary>
         * Generates a collection of all Invaders for starting a new game.
         * There are 1 row of small Invaders and 2 rows of both medium and large Invaders.
         * Each row contains 11 Invaders, and each in column the Invaders are centered.
         * </summary>
         */
        internal static Invader[] GetInvaders()
        {
            Vector2f currentPosition = ALIEN_START_POSITION;
            List<Invader> invaders = new List<Invader>();
            invaders.AddRange(GetSmallInvaders(ref currentPosition));
            invaders.AddRange(GetMediumInvaders(ref currentPosition));
            invaders.AddRange(GetLargeInvaders(ref currentPosition));
            
            return invaders.ToArray();
        }


        private static IEnumerable<Invader> GetSmallInvaders(ref Vector2f currentPosition)
        {
            List<Invader> list = new List<Invader>();
            for (int i = 0; i < 11; i++)
            {
                Invader invader = new Invader(Textures.GetInstance().invaderSmall, (BinaryState)(i % 2), InvaderType.Small);
                // X + 2 is to center the column
                invader.Sprite.Position = new Vector2f(currentPosition.X + 2, currentPosition.Y);
                list.Add(invader);
                // increment for next invader
                currentPosition.X += OFFSET_HORIZONTAL;
            }
            // increment for next row
            currentPosition = new Vector2f(ALIEN_START_POSITION.X, currentPosition.Y + OFFSET_VERTICAL);

            return list;
        }

        private static IEnumerable<Invader> GetMediumInvaders(ref Vector2f currentPosition)
        {
            List<Invader> list = new List<Invader>();
            for (int row = 0; row < 2; row++) 
            {
                for (int i = 0; i < 11; i++)
                {
                    Invader invader = new Invader(Textures.GetInstance().invaderMedium, (BinaryState)(i % 2), InvaderType.Medium)
                        {
                            Sprite =
                            {
                                // X + 1 is to center the column
                                Position = new Vector2f(currentPosition.X + 1, currentPosition.Y)
                            }
                        };
                    list.Add(invader);

                    currentPosition.X += OFFSET_HORIZONTAL;
                }
                // increment for next row
                currentPosition = new Vector2f(ALIEN_START_POSITION.X, currentPosition.Y + OFFSET_VERTICAL);
            }

            return list;
        }

        private static IEnumerable<Invader> GetLargeInvaders(ref Vector2f currentPosition)
        {
            List<Invader> list = new List<Invader>();
            for (int row = 0; row < 2; row++)
            {
                for (int i = 0; i < 11; i++)
                {
                    Invader invader = new Invader(Textures.GetInstance().invaderLarge, (BinaryState)(i % 2), InvaderType.Large)
                    {
                        Sprite = { Position = new Vector2f(currentPosition.X, currentPosition.Y) }
                    };
                    list.Add(invader);
                    // increment for next column
                    currentPosition.X += OFFSET_HORIZONTAL;
                }
                // increment for next row
                currentPosition = new Vector2f(ALIEN_START_POSITION.X, currentPosition.Y + OFFSET_VERTICAL);
            }

            return list;
        }

        /**
         * <summary>
         * Generates the player avatar for starting a new game.
         * </summary>
         */
        internal static Sprite GetPlayer()
        {
            Sprite sprite = new Sprite(Textures.GetInstance().player)
            {
                Scale = new Vector2f(PLAYER_SPRITE_SCALING_FACTOR, PLAYER_SPRITE_SCALING_FACTOR)
            };

            FloatRect size = sprite.GetGlobalBounds();
            sprite.Position = new Vector2f(
                    Game.WIDTH/2f - size.Width/2f, 
                    Game.HEIGHT - (size.Height + 35f)
                );

            return sprite;
        }
    }
}