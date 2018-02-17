using System;

/// <summary>
/// Configuration properties for the sector.
/// </summary>
[Serializable]
public class SectorProperties
{
    public bool spawnEnemies = false;               // Indicates whether the sector should spawn enemies or not.
    public float fightRadius = 0;                   // The radius where to find enemies for the fight.
    public SectorEnemyProperties[] enemyProperties; // The enemy properties for the specific sector.
}
