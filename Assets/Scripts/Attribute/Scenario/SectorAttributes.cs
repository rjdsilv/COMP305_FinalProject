using System;

/// <summary>
/// Configuration properties for the sector.
/// </summary>
[Serializable]
public class SectorAttributes
{
    public string sectorName;                       // The sector's name.
    public string battleScene;                      // The battle scene that will load in this sector.
    public string originalScene;                    // The scene that should be loaded once the battle finishes.
    public bool spawnEnemies = false;               // Indicates whether the sector should spawn enemies or not.
    public SectorEnemyAttributes[] enemyAttributes; // The definition for each type of enemy in the sector.
}
