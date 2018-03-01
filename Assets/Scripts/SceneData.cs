using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class will handle all the necessary data in the scene so scene changes will not
/// affect the game play.
/// </summary>
public class SceneData : ScriptableObject
{
    private List<GameObject> playerList = new List<GameObject>();               // The list or players.
    private List<GameObject> enemyInBattleList = new List<GameObject>();        // The list of enemies that are in battle.
    private List<GameObject> enemyNotInBattleList = new List<GameObject>();     // The list of enemies that are not in battle.

    /// <summary>
    /// Saves the player to be used in any scene.
    /// </summary>
    /// <param name="player">The player to be saved.</param>
    public void SavePlayer(GameObject player)
    {
        playerList.Add(player);
    }

    /// <summary>
    /// Saves an enemy as one that is not in the battle.
    /// </summary>
    /// <param name="enemy">The enemy to be saved.</param>
    public void SaveEnemyNotInBattle(GameObject enemy)
    {
        enemyNotInBattleList.Add(enemy);
    }

    /// <summary>
    /// Saves an enemy as one that is in the battle.
    /// </summary>
    /// <param name="enemy">The enemy to be saved.</param>
    public void SaveEnemyInBattle(GameObject enemy)
    {
        enemyInBattleList.Add(enemy);
    }

    /// <summary>
    /// Destroys all the enemies that belongs to the given sector.
    /// </summary>
    /// <param name="sectorName">The sector to be evaluated.</param>
    public void DestroyAllEnemiesOnSector(string sectorName)
    {
        // Destroys from the enemyInBattleList
        foreach (GameObject enemy in enemyInBattleList)
        {
            if (enemy.GetComponent<EnemyController>().SectorName == sectorName)
            {
                Destroy(enemy);
            }
        }

        // Destroys from the enemyNotInBattleList
        foreach (GameObject enemy in enemyNotInBattleList)
        {
            if (enemy.GetComponent<EnemyController>().SectorName == sectorName)
            {
                Destroy(enemy);
            }
        }
    }
}
