using UnityEngine;

/// <summary>
/// Script class that will control the player movement
/// </summary>
public class PlayerMovement : ActorMovement
{
    private int playerNumber = 1;

    /// <summary>
    /// Initializes the player movment accordingly.
    /// </summary>
    private void Start()
    {
        _animator = GetComponent<Animator>();
        movement.faceDirection = FaceDirection.RIGHT;
        playerNumber = gameObject.GetPlayerControllerComponent().GetPlayerNumber();
    }

    /// <summary>
    /// Method that will control the player's movement.
    /// </summary>
    private void FixedUpdate()
    {
        movement.Move(gameObject, _animator, ControlUtils.Horizontal(playerNumber), ControlUtils.Vertical(playerNumber), SceneData.shouldStop);
    }

    protected override void TurnNow()
    {
    }
}
