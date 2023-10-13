using SFML.Graphics;

namespace SpaceInvaders
{
    /**
     * This class holds all textures required for the game, 
     * as well as some logic for generating them.
     * All logic happens during programm start.
     * Is a singleton to avoid uneccessary computation.
     * Specific textures meant for the same classes are bundled and retrieved as arrays.
     */
    internal class Textures
    {
        private static readonly Textures singleton = new Textures();
        internal static Textures GetInstance()
        {
            return singleton;
        }

        /**
         * <summary>
         * Loads image assets, sets transparency,
         * then converts into and stores textures as attributes.
         * Specific textures meant to be used together are bundled as arrays.
         * </summary>
         */
        private Textures()
        {
            // Player texture
            Image player = new Image(@"H:\c#\SFML\SpaceInvaders\Assets\Cannon.bmp");
            player.CreateMaskFromColor(Color.Black);
            this.player = new Texture(player);

            //Image playerDeath = new Image(@"");              <-- Add file once aquired.
            //playerDeath.CreateMaskFromColor(Color.Black);
            //this.playerDeath = new Texture(playerDeath); 

            #region Invaders
            // Load images and set transparency
            Image smallInvader1 = new Image(@"H:\c#\SFML\SpaceInvaders\Assets\Squid1.bmp");
            smallInvader1.CreateMaskFromColor(Color.Black);
            Image smallInvader2 = new Image(@"H:\c#\SFML\SpaceInvaders\Assets\Squid2.bmp");
            smallInvader2.CreateMaskFromColor(Color.Black);

            Image mediumInvader1 = new Image(@"H:\c#\SFML\SpaceInvaders\Assets\Crab1.bmp");
            mediumInvader1.CreateMaskFromColor(Color.Black);
            Image mediumInvader2 = new Image(@"H:\c#\SFML\SpaceInvaders\Assets\Crab2.bmp");
            mediumInvader2.CreateMaskFromColor(Color.Black);

            Image largeInvader1 = new Image(@"H:\c#\SFML\SpaceInvaders\Assets\Jelly1.bmp");
            largeInvader1.CreateMaskFromColor(Color.Black);
            Image largeInvader2 = new Image(@"H:\c#\SFML\SpaceInvaders\Assets\Jelly2.bmp");
            largeInvader2.CreateMaskFromColor(Color.Black);

            // Generate Textures
            invaderSmall = new Texture[2]
            {
                new Texture(smallInvader1),
                new Texture(smallInvader2)
            };

            invaderMedium = new Texture[2]
            {
                new Texture(mediumInvader1),
                new Texture(mediumInvader2)
            };

            invaderLarge = new Texture[2]
            {
                new Texture(largeInvader1),
                new Texture(largeInvader2)
            };
            #endregion

            #region Effects
            // Effects include shots and death/destruction textures
            // Load sprite map image and set transparency
            Image Effects = new Image(@"H:\C#\SFML\SpaceInvaders\Assets\Effects.bmp");
            Effects.CreateMaskFromColor(Color.Black);

            // Generate textures
            playerShot = new Texture(Effects, new IntRect(0, 0, 3, 12));
            playerShotExplosion = new Texture(Effects, new IntRect(37, 13, 20, 20));

            invaderShotExplosionWhite = new Texture(Effects, new IntRect(0, 13, 15, 20));
            invaderDeathWhite = new Texture(Effects, new IntRect(75, 13, 32, 20));
            invaderSmallShots = new Texture[4]
            {
                new Texture(Effects, new IntRect(0, 41, 9, 16)),
                new Texture(Effects, new IntRect(36, 41, 9, 16)),
                new Texture(Effects, new IntRect(72, 41, 9, 16)),
                new Texture(Effects, new IntRect(108, 41, 9, 16))
            };

            invaderMediumShots = new Texture[4]
            {
                new Texture(Effects, new IntRect(0, 93, 9, 18)),
                new Texture(Effects, new IntRect(36, 93, 9, 18)),
                new Texture(Effects, new IntRect(72, 93, 9, 18)),
                new Texture(Effects, new IntRect(108, 93, 9, 18))
            };

            invaderLargeShots = new Texture[4]
            {
                new Texture(Effects, new IntRect(0, 144, 7, 18)),
                new Texture(Effects, new IntRect(36, 144, 8, 18)),
                new Texture(Effects, new IntRect(73, 144, 7, 18)),
                new Texture(Effects, new IntRect(109, 144, 8, 18))
            };
            #endregion
        }

        #region Player
        internal readonly Texture player;
        internal readonly Texture playerShot;
        internal readonly Texture playerShotExplosion;
        //internal readonly Texture playerDeath;
        #endregion

        #region Invaders
        internal readonly Texture[] invaderSmall;
        internal readonly Texture[] invaderMedium;
        internal readonly Texture[] invaderLarge;

        internal readonly Texture invaderShotExplosionWhite;
        internal readonly Texture invaderDeathWhite;
        internal readonly Texture[] invaderSmallShots;
        internal readonly Texture[] invaderMediumShots;
        internal readonly Texture[] invaderLargeShots;
        #endregion
    }
}
