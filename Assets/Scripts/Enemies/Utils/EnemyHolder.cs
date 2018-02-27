using UnityEngine;

/// <summary>
/// Class responsible for holding enemies on the sector.
/// </summary>
public class EnemyHolder
{
    // The sector where the enemy belong to.
    public string Sector { get; set; }

    // The enemy being hold.
    public GameObject Enemy { get; set; }

    /// <summary>
    /// Constructor for the enemy holder object.
    /// </summary>
    /// <param name="sector">The sector to where the enemy belongs to.</param>
    /// <param name="enemy">The enemy to be hold.</param>
    public EnemyHolder(string sector, GameObject enemy)
    {
        Sector = sector;
        Enemy = enemy;
    }
}
