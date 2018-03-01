using UnityEngine;

/// <summary>
/// Class responsible for controlling the sectors created, spawning and destroying enemies according
/// to its configurations.
/// </summary>
public class SectorController : MonoBehaviour
{
    // Public variable declaration.
    public SectorAttributes attributes;         // The attributes for the sector.

    // Private variable declaration.
    private bool _spawnEnemies = true;          // Indicates if the enemies were destroyed or not.


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //// EVENT METHODS

    /// <summary>
    /// When the player enters into the sector, spawn all the enemies from that sector.
    /// </summary>
    /// <param name="detectedObject">The detected object.</param>
    private void OnTriggerEnter2D(Collider2D detectedObject)
    {
        if (TagUtils.IsPlayer(detectedObject.transform))
        {
            SpawnEnemies();
        }
    }

    /// <summary>
    /// When the player leaves the sector, destroys all the enemies from that sector.
    /// </summary>
    /// <param name="detectedObject">The detected object.</param>
    void OnTriggerExit2D(Collider2D detectedObject)
    {
        if (TagUtils.IsCamera(detectedObject.transform) && detectedObject.isTrigger)
        {
            DestroyEnemies();
            _spawnEnemies = true;
        }
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //// NON EVENT METHODS

    /// <summary>
    /// Spawns all the enemies based on the sector configuration.
    /// </summary>
    private void SpawnEnemies()
    {
        // If the scene is loading for the first time, just spawn the enemies.
        if (attributes.spawnEnemies && _spawnEnemies)
        {
            foreach (SectorEnemyAttributes ea in attributes.enemyAttributes)
            {
                for (int i = 0; i < ea.spawnNumber; i++)
                {
                    // Instantiate the enemy.
                    Vector3 position = new Vector3(
                        transform.position.x + Random.Range(-ea.spawnRadius, ea.spawnRadius),
                        transform.position.y + Random.Range(-ea.spawnRadius, ea.spawnRadius),
                        0
                    );
                    GameObject enemy = Instantiate(ea.enemy, position, Quaternion.identity);
                    enemy.name = ea.enemy.name;
                    SetEnemyControllerParameters(enemy);
                    DontDestroyOnLoad(enemy);
                    SceneData.SaveEnemyNotInBattle(enemy);  
                }
            }
        }
        _spawnEnemies = false;
    }

    /// <summary>
    /// Destroys all the created enemies and clear the reference lists.
    /// </summary>
    private void DestroyEnemies()
    {
        SceneData.DestroyAllEnemiesOnSector(attributes.sectorName);
    }

    /// <summary>
    /// Sets the 
    /// </summary>
    /// <param name="enemy"></param>
    private void SetEnemyControllerParameters(GameObject enemy)
    {
        if (EnemyUtils.IsWolf(enemy))
        {
            enemy.GetComponent<WolfController>().SectorName = attributes.sectorName;
            enemy.GetComponent<WolfController>().BattleScene = attributes.battleScene;
        }
    }
}
