using UnityEngine;

/// <summary>
///  Stores the player data for transition between scenes.
/// </summary>
public class PlayerHolder
{
    // The player's position on the previous scene.
    public Vector3 Position { get; set; }

    // The player attributes before transitioning from one scene to another.
    public PlayerAttributes Attributes { get; set; }

    /// <summary>
    /// Creates a new instance of PlayerHolder
    /// </summary>
    /// <param name="position">The position to be set.</param>
    /// <param name="attributes">The attributes to be set.</param>
    public PlayerHolder(Vector3 position, PlayerAttributes attributes)
    {
        Position = position;
        Attributes = attributes;
    }
}
