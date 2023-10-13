using SFML.Graphics;
using SFML.System;
using System;

namespace SpaceInvaders
{
    /**
     * Representation of an individual Invader.
     * Sprite dependant.
     */
    internal class Invader
    {
        internal Sprite Sprite { get; }
        private readonly Texture[] textures;
        private readonly InvaderType type;
        private BinaryState index;
        internal bool IsAlive { get; private set; } = true;
        internal bool IsDying { get; private set; } = false;

        /**
         * <param name="textures">An array of two textures which are used for animation.</param>
         * <param name="index">Determins the starting texture. Either 0 or 1.</param>
         * <param name="type">The type of the Invader is used to determine what shots are generated.</param>
         */
        internal Invader(Texture[] textures, BinaryState index, InvaderType type) 
        {
            this.type = type;
            this.index = index;
            this.textures = textures;
            Sprite = new Sprite(textures[(int)index])
            {
                Scale = new Vector2f(1f / 4f, 1f / 4f)
            };
        }

        /**
         * <summary>
         * This method organises the generation of an InvaderShot instance 
         * based on the location and type of the Invader represented by this object.
         * The Invader does not remember the shot or act of shooting.
         * </summary>
         * <returns>The new InvaderShot instance</returns>
         */
        internal InvaderShot Shoot()
        {
            Vector2f shotPosition = Sprite.Position
                + new Vector2f
                (
                    Sprite.GetGlobalBounds().Width / 2, 
                    Sprite.GetGlobalBounds().Height
                );

            return GamePieces.GenerateInvaderShot(type, shotPosition);
        }

        /**
         * <summary>
         * All invaders with two textures swap between them
         * by calling this method.
         * </summary>
         * <returns>Returns the updated sprite</returns>
         */
        internal Sprite Animate()
        {
            if (index.Equals(BinaryState.Zero))
                index = BinaryState.One;
            else
                index = BinaryState.Zero;

            Sprite.Texture = textures[(int)index];
            return Sprite;
        }

        /**
         * <summary>
         * Starts the death animation and subsequent 
         * cleanup.
         * </summary>
         */
        internal void Die()
        {
            if (IsAlive)
            {
                IsAlive = false;
                IsDying = true;
                Sprite.Texture = Game.Textures.invaderDeathWhite;
                //TODO: Manipulate white texture to adjust to invader color
            }
            else
                Console.WriteLine("Dead Invader was hit: " 
                    + type + " at " + Sprite.Position.X + "," + Sprite.Position.Y);
        }

        /**
         * <summary>
         * Logic to avoid unwanted interactions with dead invaders.
         * </summary>
         */
        internal void ResolveDeath()
        {
            IsDying = false;
        }
    }
}
