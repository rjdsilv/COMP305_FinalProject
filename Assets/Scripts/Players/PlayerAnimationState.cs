/// <summary>
/// This class will be responsible for making the possible player animations available for use
/// </summary>
public class PlayerAnimationState
{
    // Property containing the value.
    public string AnimationName { get; set; }

    // The direction the player is looking at.
    public Direction FaceDirection { get; set; }

    /// <summary>
    /// Enumeration indicating the possible facing directions.
    /// </summary>
    public enum Direction
    {
        FACE_FRONT, // Player is facing the screen.
        FACE_LEFT,  // Player is facing to the left on the screen.
        FACE_BACK,  // Player is facing back to the screen.
        FACE_RIGHT  // Player is facing to the right on the screeen.
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="animationName">The name of the animation to be played on the state.</param>
    private PlayerAnimationState(string animationName, Direction faceDirection)
    {
        AnimationName = animationName;
        FaceDirection = faceDirection;
    }

    public static PlayerAnimationState STAND_FRONT { get { return new PlayerAnimationState("StandFront", Direction.FACE_FRONT); } }     // The player is stopped facing the screen.
    public static PlayerAnimationState STAND_LEFT { get { return new PlayerAnimationState("StandLeft", Direction.FACE_LEFT); } }        // The player is stopped looking to the left on the screen.
    public static PlayerAnimationState STAND_BACK { get { return new PlayerAnimationState("StandBack", Direction.FACE_BACK); } }        // The player is stopped back to the screen.
    public static PlayerAnimationState STAND_RIGHT { get { return new PlayerAnimationState("StandRight", Direction.FACE_RIGHT); } }     // The player is stopped looking to the right on the screeen.
    public static PlayerAnimationState WALKING_FRONT { get { return new PlayerAnimationState("WalkingFront", Direction.FACE_FRONT); } } // The player is walking facing the screen.
    public static PlayerAnimationState WALKING_LEFT { get { return new PlayerAnimationState("WalkingLeft", Direction.FACE_LEFT); } }    // The player is walking looking to the left on the screen.
    public static PlayerAnimationState WALKING_BACK { get { return new PlayerAnimationState("WalkingBack", Direction.FACE_BACK); } }    // The player is walking to the screen.
    public static PlayerAnimationState WALKING_RIGHT { get { return new PlayerAnimationState("WalkingRight", Direction.FACE_RIGHT); } } // The player is walking to the right on the screeen.
}
