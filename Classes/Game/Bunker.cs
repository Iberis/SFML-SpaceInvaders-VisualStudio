using SFML.Graphics;
using SFML.System;
using System;

namespace SpaceInvaders
{
    internal class Bunker
    {
        private static readonly bool[,] TEMPLATE = {
            { false, false, false, false, true, true, true, true, true, true, true, true, true, true, true, true, false, false, false, false},
            { false, false, false, true, true, true, true, true, true, true, true, true, true, true, true, true, true, false, false, false},
            { false, false, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, false, false},
            { false, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, false},
            { true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true},
            { true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true},
            { true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true},
            { true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true},
            { true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true},
            { true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true},
            { true, true, true, true, true, true, false, false, false, false, false, false, false, false, true, true, true, true, true, true},
            { true, true, true, true, true, false, false, false, false,false, false, false, false, false, false, true, true, true, true, true},
            { true, true, true, true, false, false, false, false, false,false, false, false, false, false, false, false, true, true, true, true},
            { true, true, true, true, false, false, false, false, false, false, false, false, false, false, false, false, true, true, true, true},
        };
        internal static readonly uint HEIGHT = (uint)TEMPLATE.GetLength(0);
        internal static readonly uint WIDTH = (uint)TEMPLATE.GetLength(1);

        //State of the bunker as a by pixel representation
        private bool[,] state;
        //Whether the texture needs updating
        private bool hasChanged;

        private Sprite sprite;
        

        internal Bunker(Vector2f position) 
        {
            state = new bool[HEIGHT, WIDTH];
            Array.Copy(TEMPLATE, state, state.Length);
            hasChanged = true; // initialises to true for initial loading via GetSprite()
            sprite = new Sprite
            {
                Position = position
            };
        }

        internal Sprite GetSprite() 
        {
            if (!hasChanged) return sprite;
            
            byte[] bytes = new byte[state.Length * 4];
            for (int j = 0; j < HEIGHT; j++)
            {
                for (int i = 0; i < WIDTH; i++)
                {
                    //red channel
                    bytes[j * WIDTH * 4 + 4 * i] = 0;

                    //green channel
                    uint color = 0;
                    if (state[j, i])
                        color = 255;
                    bytes[j * WIDTH * 4 + 4 * i + 1] = (byte)color;

                    //blue channel
                    bytes[j * WIDTH * 4 + 4 * i + 2] = 0;

                    //alpha channel
                    bytes[j * WIDTH * 4 + 4 * i + 3] = 255;
                }
            }
            Image image = new Image(WIDTH, HEIGHT, bytes);
            Texture texture = new Texture(image);
            sprite.Texture = texture;
            hasChanged = false;

            return sprite;
        }
    }
}