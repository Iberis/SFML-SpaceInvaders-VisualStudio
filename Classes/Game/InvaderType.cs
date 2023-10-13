namespace SpaceInvaders
{
    /**
     * <summary>
     * Used to track and distinguish 
     * between the different types of Invaders.
     * </summary>
     */
    internal enum InvaderType
    {
        // If you add a new type it MUST also be handled at 
        // GamePieces.GenerateInvaderShot(InvaderType, Vector2f)
        Small,
        Medium,
        Large
    }
}
