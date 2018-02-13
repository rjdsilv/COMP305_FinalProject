using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class responsible for controlling the sectors created, spawning and destroying enemies according
/// to its configurations.
/// </summary>
public class SectorController : MonoBehaviour
{
    // Public variable declaration.
    public SectorProperties sectorProperties;

    // Private variables declaration.
    private List<GameObject> _enemiesSpawned; // The enemies spawned.
    private List<EnemyAI> _enemiesAI;         // The enemy AI script.

	// Use this for initialization
	void Start ()
    {
        _enemiesSpawned = new List<GameObject>();
        _enemiesAI = new List<EnemyAI>();
	}

    /// <summary>
    /// Checks what enemies are seeing the player.
    /// </summary>
    void LateUpdate()
    {
        int enemyIndex = 0;

        foreach (EnemyAI ai in _enemiesAI)
        {
            if (!ai.IsSeeingPlayer())
            {
                Debug.Log(string.Format("##### Enemy {0} IS NOT SEEING the player...", enemyIndex++));
            }
            else
            {
                Debug.LogWarning(string.Format("########## Enemy {0} IS SEEING the player...", enemyIndex++));
            }
        }
    }

    /// <summary>
    /// When the player enters into the sector, spawn all the enemies from that sector.
    /// </summary>
    /// <param name="other">The other collider.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        SpawnEnemies();

    }

    /// <summary>
    /// When the player leaves the sector, destroys all the enemies from that sector.
    /// </summary>
    /// <param name="other">The other collider.</param>
    void OnTriggerExit2D(Collider2D collision)
    {
        DestroyEnemies();

    }

    /// <summary>
    /// Spawns all the enemies based on the sector configuration.
    /// </summary>
    void SpawnEnemies()
    {
        if (sectorProperties.spawnEnemies)
        {
            foreach (SectorEnemyProperties ep in sectorProperties.enemyProperties)
            {
                for (int i = 0; i < ep.spawnNumber; i++)
                {
                    // Instantiate the enemy.
                    GameObject enemy = Instantiate(
                        ep.enemy,
                        new Vector3(transform.position.x + Random.Range(-ep.spawnRadius, ep.spawnRadius), transform.position.y + Random.Range(-ep.spawnRadius, ep.spawnRadius), 0),
                        Quaternion.identity
                    );

                    // Adds the enemy and its AI into lists.
                    _enemiesSpawned.Add(enemy);
                    _enemiesAI.Add(enemy.GetComponent<EnemyAI>());
                }
            }
        }
    }

    /// <summary>
    /// Destroys all the created enemies and clear the reference lists.
    /// </summary>
    private void DestroyEnemies()
    {
        foreach(GameObject go in _enemiesSpawned)
        {
            Destroy(go.gameObject);
        }
        _enemiesSpawned.Clear();
        _enemiesAI.Clear();
    }
}
