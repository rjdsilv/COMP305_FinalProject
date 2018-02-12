/// <summary>
/// This class will be responsible for making the possible player animations available for use
/// </summary>
public class AnimationState
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
        FACE_DOWN, // Player is facing the screen.
        FACE_LEFT,  // Player is facing to the left on the screen.
        FACE_UP,    // Player is facing back to the screen.
        FACE_RIGHT  // Player is facing to the right on the screeen.
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="animationName">The name of the animation to be played on the state.</param>
    private AnimationState(string animationName, Direction faceDirection)
    {
        AnimationName = animationName;
        FaceDirection = faceDirection;
    }

    public static AnimationState STAND_DOWN { get { return new AnimationState("StandFront", Direction.FACE_DOWN); } }       // The player is stopped facing the screen.
    public static AnimationState STAND_LEFT { get { return new AnimationState("StandLeft", Direction.FACE_LEFT); } }        // The player is stopped looking to the left on the screen.
    public static AnimationState STAND_UP { get { return new AnimationState("StandBack", Direction.FACE_UP); } }            // The player is stopped back to the screen.
    public static AnimationState STAND_RIGHT { get { return new AnimationState("StandRight", Direction.FACE_RIGHT); } }     // The player is stopped looking to the right on the screeen.
    public static AnimationState WALKING_DOWN { get { return new AnimationState("WalkingFront", Direction.FACE_DOWN); } }   // The player is walking facing the screen.
    public static AnimationState WALKING_LEFT { get { return new AnimationState("WalkingLeft", Direction.FACE_LEFT); } }    // The player is walking looking to the left on the screen.
    public static AnimationState WALKING_UP { get { return new AnimationState("WalkingBack", Direction.FACE_UP); } }        // The player is walking to the screen.
    public static AnimationState WALKING_RIGHT { get { return new AnimationState("WalkingRight", Direction.FACE_RIGHT); } } // The player is walking to the right on the screeen.
}
