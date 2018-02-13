using System;

/// <summary>
/// Configuration properties for the sector.
/// </summary>
[Serializable]
public class SectorProperties
{
    public bool spawnEnemies = false;
    public SectorEnemyProperties[] enemyProperties;
}
