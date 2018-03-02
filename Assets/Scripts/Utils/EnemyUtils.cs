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
    public static bool IsWolf(this GameObject enemy)
    {
        return enemy.name == WOLF;
    }

    /// <summary>
    /// Method to check if a given enemy belongs to the given sector.
    /// </summary>
    /// <param name="enemy">The enemy to be checked.</param>
    /// <param name="sectorName">The sector to be checked.</param>
    /// <returns><b>true</b> if the given enemy belongs to the given sector. <b>false</b> otherwise.</returns>
    public static bool BelongsToSector(this GameObject enemy, string sectorName)
    {
        if (enemy.IsWolf())
        {
            return enemy.GetComponent<WolfController>().SectorName == sectorName;
        }

        return false;
    }

    /// <summary>
    /// Sets the attributes for the given game enemy.
    /// </summary>
    /// <param name="enemy">The enemy to have parameters set.x`</param>
    public static void SetEnemyControllerParameters(this GameObject enemy, SectorAttributes attributes)
    {
        if (enemy.IsWolf())
        {
            enemy.GetComponent<WolfController>().SectorName = attributes.sectorName;
            enemy.GetComponent<WolfController>().BattleScene = attributes.battleScene;
        }
    }

    /// <summary>
    /// Gets the controller component for the given enemy.
    /// </summary>
    /// <param name="enemy">The enemy to be used to get the controller.</param>
    /// <returns>The controller component for the given the enemy. Retrns null if it is not a known enemy.</returns>
    public static IEnemyController GetEnemyControllerComponent(this GameObject enemy)
    {
        if (enemy.IsWolf())
        {
            return enemy.GetComponent<WolfController>();
        }

        return null;
    }
}
