using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Scriptable object class to deal with missions.
/// </summary>
[CreateAssetMenu(menuName = "Mission")]
public class Mission : ScriptableObject
{
    public string missionName;
    public string description;
    public Mission nextMission;

    public override bool Equals(object other)
    {
        if (other is Mission)
        {
            return ((Mission)other).missionName == missionName;
        }

        return false;
    }

    public override int GetHashCode()
    {
        return missionName.GetHashCode();
    }
}
