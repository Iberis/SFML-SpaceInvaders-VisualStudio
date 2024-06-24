using SFML.Graphics;
using SFML.Window;

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
        private readonly InvaderType type;
        private int index = 0;
        private int tickTracker = 0;
        internal bool NeedCleanUp { get; private set; } = false;
        internal bool IsDying { get; private set; } = false;

        /**
         * <param name="textures">Contains the textures used to cycle through for animation.
         * The length is arbitrary, but must at least contain one texture.</param>
         * <param name="sprite">An initialised sprite ready for use.</param>
         * <param name="type">InvaderType that spawned the shot</param>
         */
        internal InvaderShot(Texture[] textures, Sprite sprite, InvaderType type)
        {
            this.textures = textures;
            Sprite = sprite;
            this.type = type;
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
            // Change Animation State only every 8th tick
            if (tickTracker < 8)
            {
                tickTracker++;
                return;
            }
            tickTracker = 0;
            
            if (IsDying)
            {
                NeedCleanUp = true;
                return;
            }
            
            index++;
            if (index >= textures.Length)
                index = 0;

            Sprite.Texture = textures[index];
        }

        internal void Impact()
        {
            IsDying = true;
            Textures catalogue = Textures.GetInstance(); 
            Sprite.Texture = new Texture(catalogue.invaderShotExplosionWhite);
            Sprite.Color = catalogue.getColor(type);
        }
    }
}
