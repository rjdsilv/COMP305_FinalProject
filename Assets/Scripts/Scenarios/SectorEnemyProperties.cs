using System;
using UnityEngine;

/// <summary>
/// Enemy properties to be controlled by the sector.
/// </summary>
[Serializable]
public class SectorEnemyProperties
{
    public int spawnNumber;
    public float spawnRadius;
    public GameObject enemy;
}
