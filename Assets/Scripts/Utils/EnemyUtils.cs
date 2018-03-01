using UnityEngine;

/// <summary>
/// Utilitary class to deal with game enemies.
/// </summary>
public static class EnemyUtils
{
    private const string WOLF = "Wolf";     // The wolf prefab name.

    /// <summary>
    /// Method to check if a given enemy is a Wolf.
    /// </summary>
    /// <param name="enemy">The enemy to be checked</param>
    /// <returns><b>true</b> if the enemy is a wolf. <b>false</b> otherwise.</returns>
    public static bool IsWolf(GameObject enemy)
    {
        return enemy.name == WOLF;
    }

    /// <summary>
    /// Method to check if a given enemy belongs to the given sector.
    /// </summary>
    /// <param name="enemy">The enemy to be checked.</param>
    /// <param name="sectorName">The sector to be checked.</param>
    /// <returns><b>true</b> if the given enemy belongs to the given sector. <b>false</b> otherwise.</returns>
    public static bool BelongsToSector(GameObject enemy, string sectorName)
    {
        if (IsWolf(enemy))
        {
            return enemy.GetComponent<WolfController>().SectorName == sectorName;
        }

        return false;
    }
}
