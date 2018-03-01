using UnityEngine;

/// <summary>
/// Generic ability for any given actor in the game.
/// </summary>
public abstract class ActorAbility : ScriptableObject
{
    public int level;                           // The current ability's level.
    public int minPower;                        // The ability's minimum power.
    public int maxPower;                        // The ability's maximum power.
}
