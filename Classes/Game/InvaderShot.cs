using SFML.Graphics;

namespace SpaceInvaders
{
    /**
     * Representation of a shot fired by an Invader.
     * Sprite dependant.
     */
    internal class InvaderShot
    {
        internal Sprite Sprite { get; }
        private readonly Texture[] textures;
        private int index;
        private int movementTracker;
        
        /**
         * <param name="textures">Contains the textures used to cycle through for animation.
         * The length is arbitrary, but must at least contain one texture.</param>
         * <param name="sprite">An initialised sprite ready for use.</param>
         */
        internal InvaderShot(Texture[] textures, Sprite sprite)
        {
            this.textures = textures;
            this.Sprite = sprite;
            movementTracker = 0;
            index = 0;
        }

        /**
         * <summary>
         * Cycles the texture of the InvaderShot.
         * A texture swap happens every 8th call,
         * to allow for decoupling from movement rendering.
         * </summary>
         */
        internal void Animate()
        {
            if (movementTracker < 8)
            {
                movementTracker++;
                return;
            }
            movementTracker = 0;

            index++;
            if (index >= textures.Length)
                index = 0;

            Sprite.Texture = textures[index];
        }

    }
}
