using UnityEngine;

/// <summary>
/// Class containing all the necessary attributes for random walking.
/// </summary>
[CreateAssetMenu(menuName = "Attributes/AI/Random Walk")]
public class RandomWalkAttributes : ScriptableObject
{
    // Public variable declaration.
    [Range(min: 0.5f, max: 3.0f)]
    public float turnDistance;      // The distance from which the actor must change the direction.
    public float minTimeSameDir;    // The minimum time the actor will walk on the same direction.
    public float maxTimeSameDir;    // The maximum time the actor will walk on the same direction.
}
