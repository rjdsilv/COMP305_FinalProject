using System;
using UnityEngine;

/// <summary>
/// Class responsiblefor dealing with spawn points in the battle scene.
/// </summary>
[Serializable]
public class PlayerSpawnPoint
{
    public string actorName;        // The actor name.
    public Vector3 spawnPoint;      // The actor spawn point.
}
