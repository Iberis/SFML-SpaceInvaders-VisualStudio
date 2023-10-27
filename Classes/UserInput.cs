using System;
using System.Collections.Generic;
using SFML.Window;

namespace SpaceInvaders
{
    /**
     * This class contains all logic for window event handling.
     * Event flags are checked, and if neccessary reset, by the instance of the Game class.
     */
    internal static class UserInput
    {
        internal static bool Pause { get; set; }
        internal static readonly Dictionary<string, bool> InputStates = new Dictionary<string, bool>()
        {
            { "Left", false },
            { "Right", false },
            { "TryShoot", false }
        };

        /// <summary>
        /// Handle key presses. Set movement flags to true if a given directional arrow is pressed.
        /// </summary>
        /// <param name="sender"><see cref="Window.KeyPressed"/></param>
        /// <param name="e"><see cref="Window.KeyPressed"/></param>
        internal static void Window_KeyPressed(object sender, KeyEventArgs e)
        {
            //Window win = sender as Window;

            switch (e.Code)
            {
                case Keyboard.Key.Left:
                    InputStates["Left"] = true;
                    break;
                case Keyboard.Key.Right:
                    InputStates["Right"] = true;
                    break;
                case Keyboard.Key.Space:
                    InputStates["TryShoot"] = true;
                    break;
                case Keyboard.Key.Escape:
                    //fall-through
                case Keyboard.Key.Pause:
                    Pause = true;
                    break;
            }
        }

        /// <summary>
        /// Handle key releases. Set movement flags to false if a given directional arrow is pressed.
        /// </summary>
        /// <param name="sender"><see cref="Window"/></param>
        /// <param name="e"><see cref="Window.KeyEventArgs"/></param>
        internal static void Window_KeyReleased(object sender, KeyEventArgs e)
        {
            switch (e.Code)
            {
                case Keyboard.Key.Left:
                    InputStates["Left"] = false;
                    break;
                case Keyboard.Key.Right:
                    InputStates["Right"] = false;
                    break;
            }
        }
    }
}
