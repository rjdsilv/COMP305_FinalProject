using UnityEngine;

/// <summary>
/// Class responsible for storing vision attributes shared between objects.
/// </summary>
[CreateAssetMenu(menuName = "Attributes/AI/Vision")]
public class VisionAttributes : ScriptableObject
{
    // Public variable declaration
    [Range(min: 0.5f, max: 5.0f)]
    public float viewDistance;      // The maximum distance that the actor will see.
    public float viewingAngle;      // The AI viewing angle.
}
