using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class will handle all the necessary data in the scene so scene changes will not
/// affect the game play.
/// </summary>
public static class SceneData
{
    public static int numberOfPlayers = 1;                                            // Number indicating the number of players that will be playing the game. 
    public static bool gameStarted = false;
    public static bool shouldStop = false;                                            // Flag indicating if should stop all the scene movement.
    public static bool isInBattle = false;                                            // Flag indicating if we are already in battle.
    public static bool isCommingBackFronBattle = false;                               // Flag indicating if the player is comming back from a batlle.
    public static bool showGameTutorial = true;                                       // Flag indicating if the game tutorial should be shown.
    public static bool showBattleTutorial = true;                                     // Flag indicating if the battle tutorial should be shown.
    public static bool killedFinalBoss = false;
    public static string mainScene = "";                                              // Stores the name of the scene that invoked the battle scene.
    public static string[] chosenPlayers = new string[1] { ActorUtils.MAGE };         // The players chosen players.
    public static List<GameObject> playerList = new List<GameObject>();               // The list of players.
    public static List<GameObject> enemyNotInBattleList = new List<GameObject>();     // The list of enemies that are not in battle.
    public static GameObject enemyInBattle;                                           // The enemy that are currently in battle.

    // Item related data.
    public static bool dropHealthPot = false;                                         // Indicates if the enemy dropped a health pot.
    public static bool dropManaPot = false;                                           // Indicates if the enemy dropped a mana pot.
    public static bool dropStaminaPot = false;                                        // Indicates if the enemy dropped a stamina pot.
    public static Vector3 dropPosition = Vector3.zero;                                // The position where the items will be dropped.

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
        if (null != enemyInBattle)
        {
            if (enemyInBattle.BelongsToSector(sectorName))
            {
                enemiesToDestroy.Add(enemyInBattle);
            }
            DestroyEnemies(enemiesToDestroy, null);
        }

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
    /// Destroys all enemies on the current scene.
    /// </summary>
    public static void DestroyAllEnemiesInScene()
    {
        enemyNotInBattleList.ForEach(enemy => Object.Destroy(enemy));
        enemyNotInBattleList.Clear();
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
            if (null != listToRemove)
            {
                listToRemove.Remove(enemy);
            }
            Object.Destroy(enemy);
        }
    }
}
