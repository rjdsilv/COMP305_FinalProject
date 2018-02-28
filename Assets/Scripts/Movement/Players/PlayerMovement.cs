using UnityEngine;
/// <summary>
/// Script class that will control the player movement
/// </summary>
public class PlayerMovement : ActorMovement
{
    /// <summary>
    /// Initializes the player movment accordingly.
    /// </summary>
    private void Start()
    {
        _animator = GetComponent<Animator>();
        movement.faceDirection = FaceDirection.RIGHT;
    }

    /// <summary>
    /// Method that will control the player's movement.
    /// </summary>
    private void FixedUpdate()
    {
        movement.Move(gameObject, _animator, ControlUtils.Horizontal(), ControlUtils.Vertical(), false);
    }
}
