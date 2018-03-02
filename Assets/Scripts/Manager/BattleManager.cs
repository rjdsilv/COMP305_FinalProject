using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class will manage all the actions that will happen in a battle.
/// </summary>
public class BattleManager : MonoBehaviour
{
    // Public variable declaration.
    public PlayerSpawnPoint[] playerSpawnPoints;     // The points where the players will spawn in the battle scene.
    public Vector3[] enemySpawnPoints;               // The points where the enemies will spawn in the battle scene.

    // Private variable declaration.
    private GameObject _mage;
    private GameObject[] _enemies;

    /// <summary>
    /// Initializes all the necessary data on the manager.
    /// </summary>
    private void Start ()
    {
        // Sets the scene as being in a battle.
        SceneData.isInBattle = true;

        // Spawn all the players and enemies in the previous scene.
        SpawnPlayers();
        SpawnEnemies();

        // Inactivate all the players and enemies from the previous scene.
        InactivateActorsFromPreviousScene();
	}

    /// <summary>
    /// Method responsible for spawning all the players from the previous scene on the battle scene.
    /// </summary>
    private void SpawnPlayers()
    {
        foreach (GameObject player in SceneData.playerList)
        {
            if (PlayerUtils.IsMage(player))
            {
                _mage = Instantiate(player, FindSpawnPointForActor(player.name), Quaternion.identity);
                _mage.transform.localScale *= 1.25f;
                _mage.GetComponent<PlayerMovement>().movement.faceDirection = FaceDirection.RIGHT;
            }
        }
    }

    /// <summary>
    /// Spawn the enemies in a number that ranges between the define minimum and maximum defined for the enemy.
    /// </summary>
    private void SpawnEnemies()
    {
        List<GameObject> enemies = new List<GameObject>();

        foreach (GameObject enemy in SceneData.enemyInBattleList)
        {
            if (enemy.IsWolf())
            {
                WolfController controller = enemy.GetWolfControllerComponent();
                int enemiesInBattle = Mathf.FloorToInt(Random.Range(controller.minEnemiesInBattle, controller.maxEnemiesInBattle + 0.999999f));

                for (int i = 0; i < enemiesInBattle; i++)
                {
                    GameObject instantiatedEnemy = Instantiate(enemy, enemySpawnPoints[i], Quaternion.identity);
                    instantiatedEnemy.transform.localScale *= 1.25f;
                    enemies.Add(instantiatedEnemy);
                }
            }
        }

        _enemies = enemies.ToArray();
    }

    /// <summary>
    /// Inactivates all the actors from the previous scene.
    /// </summary>
    private void InactivateActorsFromPreviousScene()
    {
        foreach (GameObject o in SceneData.playerList)
            o.SetActive(false);

        foreach (GameObject o in SceneData.enemyInBattleList)
            o.SetActive(false);

        foreach (GameObject o in SceneData.enemyNotInBattleList)
            o.SetActive(false);
    }

    /// <summary>
    /// Retrieves the spawn point for a given actor in the scene.
    /// </summary>
    /// <param name="actorName">The actor name to be found.</param>
    /// <returns>The spawn point when found. Otherwise return the (0, 0, 0) position.</returns>
    private Vector3 FindSpawnPointForActor(string actorName)
    {
        foreach (PlayerSpawnPoint bsp in playerSpawnPoints)
        {
            if (bsp.actorName == actorName)
            {
                return bsp.spawnPoint;
            }
        }

        return Vector3.zero;
    }
}
