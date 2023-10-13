using SFML.Graphics;
using SFML.System;
using System;
using System.Security.Policy;

namespace SpaceInvaders
{
    internal class Bunker
    {
        internal const uint WIDTH = 20;
        internal const uint HEIGHT = 14;
        // 22 x 16
        private static readonly bool[,] template = {
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

        //State of the bunker as a by pixel representation
        private bool[,] state;
        //Whether the texture needs updating
        private bool changed;

        private Sprite sprite;
        

        internal Bunker(Vector2f position) 
        {
            state = new bool[HEIGHT, WIDTH];
            Array.Copy(template, state, state.Length);
            changed = true; // initialises to true for initial loading via GetSprite()
            sprite = new Sprite
            {
                Position = position
            };
        }

        internal Sprite GetSprite() 
        {
            if (changed)
            {
                Byte[] bytes = new Byte[state.Length * 4];
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
                changed = false;
            }

            return sprite;
        }
    }
}