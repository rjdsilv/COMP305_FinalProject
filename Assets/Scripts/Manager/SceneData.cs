using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class will handle all the necessary data in the scene so scene changes will not
/// affect the game play.
/// </summary>
public static class SceneData
{
    public static bool isInBattle = false;                                            // Flat indicating if we are in a battle scene.
    public static bool isCommingBackFronBattle = false;                               // Flat indicating if the player is comming back from a batlle.
    public static string mainScene = "";                                              // Stores the name of the scene that invoked the battle scene.
    public static List<GameObject> playerList = new List<GameObject>();               // The list of players.
    public static List<GameObject> enemyInBattleList = new List<GameObject>();        // The list of enemies that are in battle.
    public static List<GameObject> enemyNotInBattleList = new List<GameObject>();     // The list of enemies that are not in battle.

    /// <summary>
    /// Saves the player to be used in any scene.
    /// </summary>
    /// <param name="player">The player to be saved.</param>
    public static void SavePlayer(GameObject player)
    {
        playerList.Add(player);
    }

    /// <summary>
    /// Destroys all the enemies that belongs to the given sector.
    /// </summary>
    /// <param name="sectorName">The sector to be evaluated.</param>
    public static void DestroyAllEnemiesOnSector(string sectorName)
    {
        List<GameObject> enemiesToDestroy = new List<GameObject>();
        
        // Destroys from the enemyInBattleList
        foreach (GameObject enemy in enemyInBattleList)
        {
            if (enemy.BelongsToSector(sectorName))
            {
                enemiesToDestroy.Add(enemy);
            }
        }
        DestroyEnemies(enemiesToDestroy, enemyInBattleList);

        // Destroys from the enemyNotInBattleList
        foreach (GameObject enemy in enemyNotInBattleList)
        {
            if (enemy.BelongsToSector(sectorName))
            {
                enemiesToDestroy.Add(enemy);
            }
        }
        DestroyEnemies(enemiesToDestroy, enemyNotInBattleList);
    }

    /// <summary>
    /// Destroy the enemies on the list enemiesToDestroy and removes them from the list listToRemove.
    /// </summary>
    /// <param name="enemiesToDestroy">The list containing enemies to be destroyed.</param>
    /// <param name="listToRemove">The list containing enemies to be removed.</param>
    private static void DestroyEnemies(List<GameObject> enemiesToDestroy, List<GameObject> listToRemove)
    {
        foreach (GameObject enemy in enemiesToDestroy)
        {
            listToRemove.Remove(enemy);
            Object.Destroy(enemy);
        }
    }
}
