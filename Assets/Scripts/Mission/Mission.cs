using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Scriptable object class to deal with missions.
/// </summary>
[CreateAssetMenu(menuName = "Mission")]
public class Mission : ScriptableObject
{
    public string description;
    public Mission nextMission;
}
