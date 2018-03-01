using System;
using UnityEngine;

/// <summary>
/// Enemy properties to be controlled by the sector.
/// </summary>
[Serializable]
public class SectorEnemyAttributes
{
    public int spawnNumber;             // The number of enemies to be spawned.
    public float spawnRadius;           // The radius within the enemies should spawn.
    public GameObject enemy;            // The enemy to be spawned.
}
