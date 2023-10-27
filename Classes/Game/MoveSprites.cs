using SFML.Graphics;
using SFML.System;

namespace SpaceInvaders
{
    /**
     * <summary>
     * This class houses all logic (and numbers) for adjusting
     * the position value of sprites.
     * </summary>
     */
    internal static class MoveSprites
    {
        private const int ShotSpeed = 1;
        private const int StepDistance = 3;

        #region Invaders
        internal static void StepLeft(Invader invader)
        {
            Sprite sprite = invader.Sprite;
            float x = sprite.Position.X - StepDistance;
            float y = sprite.Position.Y;

            sprite.Position = new Vector2f(x, y);
            invader.Animate();
        }

        internal static void StepRight(Invader invader)
        {
            Sprite sprite = invader.Sprite;
            float x = sprite.Position.X + StepDistance;
            float y = sprite.Position.Y;

            sprite.Position = new Vector2f(x, y);
            invader.Animate();
        }

        internal static void StepDown(Invader invader)
        {
            Sprite sprite = invader.Sprite;
            float x = sprite.Position.X;
            float y = sprite.Position.Y + sprite.GetGlobalBounds().Height;

            sprite.Position = new Vector2f(x, y);
            invader.Animate();
        }
        #endregion

        #region Shots
        /**
         * <summary>
         * Moves shot sprite down by SHOT_SPEED logical pixels
         * and triggers its animation.
         * </summary>
         * <param name="shot">A, for the current game, valid instance of an InvaderShot</param>
         */
        internal static void Descent(InvaderShot shot)
        {
            shot.Sprite.Position = new Vector2f
                (
                    shot.Sprite.Position.X,
                    shot.Sprite.Position.Y + ShotSpeed
                );
            shot.Animate();
        }

        /**
         * <summary>
         * Moves the players shot up by SHOT_SPEED logical pixels
         * </summary>
         * <param name="playerShot">game.playerShot goes here. Nothing else.</param>
         */
        internal static void Ascent(Sprite playerShot)
        {
            if (!playerShot.Equals(GamePieces.SHOT_EMPTY))
            {
                playerShot.Position = new Vector2f
                (
                    playerShot.Position.X,
                    playerShot.Position.Y - ShotSpeed
                );
            }
        }
        #endregion
    }
}