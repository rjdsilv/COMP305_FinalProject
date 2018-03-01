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
    private GameManager _gameManager;           // The game manager script.


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //// EVENT METHODS

    /// <summary>
    /// This method will initialize the sector controller needed variables.
    /// </summary>
    private void Start ()
    {
        _gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
	}

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
                    enemy.GetComponent<EnemyController>().SectorName = attributes.sectorName;
                    DontDestroyOnLoad(enemy);
                    _gameManager.sceneData.SaveEnemyNotInBattle(enemy);
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
        _gameManager.sceneData.DestroyAllEnemiesOnSector(attributes.sectorName);
    }
}
