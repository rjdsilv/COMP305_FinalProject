using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Class responsible for controlling the sectors created, spawning and destroying enemies according
/// to its configurations.
/// </summary>
public class SectorController : MonoBehaviour
{
    // Public variable declaration.
    public string sectorName;
    public SectorProperties sectorProperties;
    public string battleScene;

    // Private variables declaration.
    private bool _isDataSaved = false;
    private bool _spawnEnemies = true;        // Indicates if the enemies were destroyed or not.
    private List<EnemyHolder> _enemiesSpawned; // The enemies spawned.
    private List<EnemyHolder> _enemiesToFight; // The enemies that are going to be on the battle.
    private List<EnemyAI> _enemiesAI;         // The enemy AI script.

	// Use this for initialization
	void Start ()
    {
        _enemiesSpawned = new List<EnemyHolder>();
        _enemiesToFight = new List<EnemyHolder>();
        _enemiesAI = new List<EnemyAI>();
        PositionPlayers();
	}

    /// <summary>
    /// When the player enters into the sector, spawn all the enemies from that sector.
    /// </summary>
    /// <param name="detectedObject">The detected object.</param>
    void OnTriggerEnter2D(Collider2D detectedObject)
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

    /// <summary>
    /// Checks what enemies are seeing the player and select enemies within a radius to join the battle.
    /// </summary>
    void LateUpdate()
    {
        bool shouldBattle = false;
        foreach (EnemyAI ai in _enemiesAI)
        {
            if (ai.IsSeeingPlayer())
            {
                SelectEnemiesForBattle(ai);
                shouldBattle = true;
            }
        }

        if (shouldBattle && !_isDataSaved)
        {
            SaveSceneData();
            LoadBattleScene();
        }
    }

    /// <summary>
    /// Loads the associated battle scene for this sector.
    /// </summary>
    void LoadBattleScene()
    {
        StartCoroutine(LoadBattleSceneAsync());
    }

    /// <summary>
    /// Loads asynchronously the related scene with this sector.
    /// </summary>
    /// <returns>An IEnumerator to be used by the coroutine manager.</returns>
    IEnumerator LoadBattleSceneAsync()
    {
        AsyncOperation asyncSceneLoad = SceneManager.LoadSceneAsync(battleScene);

        // Wait 50ms to check again.
        while (!asyncSceneLoad.isDone)
        {
            yield return new WaitForSeconds(0.05f);
        }
    }

    /// <summary>
    /// Searches enemies within the given radius in order to 
    /// </summary>
    /// <returns></returns>
    void SelectEnemiesForBattle(EnemyAI ai)
    {
        // Go through all the enemies on the sector to find the ones that will be on battle.
        foreach(EnemyHolder enemy in _enemiesSpawned)
        {
            Vector2 circleCenter = new Vector2(ai.GetEnemyPosition().x, ai.GetEnemyPosition().y);
            if (enemy.Sector == sectorName)
            {
                float enemyRadius = CalculateCircleRadiusForEnemy(enemy.Enemy, circleCenter);
                if (enemyRadius < sectorProperties.fightRadius)
                {
                    if (!_enemiesToFight.Contains(enemy))
                    {
                        _enemiesToFight.Add(enemy);
                    }
                }
            }
        }
    }

    /// <summary>
    /// Saves the necessary data for switching between scenes. Also calculates what must be
    /// destroyed from multiple sectors due to the camera sight of sectors.
    /// </summary>
    /// TODO improve this code!!!
    void SaveSceneData()
    {
        List<EnemyHolder> enemiesToDestroy = new List<EnemyHolder>();
        // Destroys all the enemies that are saved but ar not from the same sector.
        foreach (EnemyHolder holder in SceneSwitchDataHandler.enemiesIndestructible)
        {
            if (holder.Sector != sectorName)
            {
                enemiesToDestroy.Add(holder);
            }
        }

        // Clean the lists that will be reused.
        SceneSwitchDataHandler.enemiesIndestructible.RemoveAll(h => enemiesToDestroy.Contains(h));

        // Destroys the enemies marked to be destroyed.
        foreach (EnemyHolder holder in enemiesToDestroy) Destroy(holder.Enemy);

        // Removes from the enemies spawned, the enemies that will fight.
        _enemiesSpawned.RemoveAll(enemy => _enemiesToFight.Contains(enemy));

        // Saves all the players.
        foreach (GameObject player in TagUtils.FindAllPlayers())
        {
            PlayerController playerController = player.GetComponent<PlayerController>();
            SceneSwitchDataHandler.AddPlayer(player.transform.name, player.transform.position, playerController.GetAttributes());
        }

        // Saves the enemies in battle, not in battle, and the ones that are marked as not destroyable.
        SceneSwitchDataHandler.enemiesInBattle = _enemiesToFight;
        SceneSwitchDataHandler.enemiesNotInBattle = _enemiesSpawned;
        SceneSwitchDataHandler.enemiesIndestructible.AddRange(_enemiesToFight);
        SceneSwitchDataHandler.enemiesIndestructible.AddRange(_enemiesSpawned);

        // Prevent destruction and deactivate all the enemies from the sector.
        // They will be reloaded either in the battle scene or the original scene.
        foreach (EnemyHolder holder in SceneSwitchDataHandler.enemiesIndestructible)
        {
            holder.Enemy.SetActive(false);
            DontDestroyOnLoad(holder.Enemy);
        }
        _isDataSaved = true;
    }

    /// <summary>
    /// Calculates the radius where to pick enemies to go to the battle.
    /// </summary>
    /// <param name="enemy">The enemy to verify if should be put on the battle.</param>
    /// <param name="center">The circle center from where to look into.</param>
    /// <returns>The position of the enemy found.</returns>
    float CalculateCircleRadiusForEnemy(GameObject enemy, Vector2 center)
    {
        return Mathf.Sqrt(Mathf.Pow((enemy.transform.position.x - center.x), 2.0f) + Mathf.Pow((enemy.transform.position.y - center.y), 2.0f));
    }

    /// <summary>
    /// Positions the player in its previous spot before going to battle.
    /// </summary>
    void PositionPlayers()
    {
        if (SceneSwitchDataHandler.isComingBackFromBattle)
        {
            // Reposition the players in the original position
            foreach (GameObject player in TagUtils.FindAllPlayers())
            {
                PlayerHolder oldPlayer = SceneSwitchDataHandler.GetPlayer(player.transform.name);
                player.transform.position = oldPlayer.Position;
                Camera.main.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, Camera.main.transform.position.z) ;
            }
        }
    }

    /// <summary>
    /// Spawns all the enemies based on the sector configuration.
    /// </summary>
    void SpawnEnemies()
    {
        // If the scene is loading for the first time, just spawn the enemies.
        if (!SceneSwitchDataHandler.isComingBackFromBattle)
        {
            if (sectorProperties.spawnEnemies && _spawnEnemies)
            {
                foreach (SectorEnemyProperties ep in sectorProperties.enemyProperties)
                {
                    for (int i = 0; i < ep.spawnNumber; i++)
                    {
                        // Instantiate the enemy.
                        Vector3 position = new Vector3(
                            transform.position.x + UnityEngine.Random.Range(-ep.spawnRadius, ep.spawnRadius),
                            transform.position.y + UnityEngine.Random.Range(-ep.spawnRadius, ep.spawnRadius),
                            0
                        );
                        GameObject enemy = Instantiate(ep.enemy, position, Quaternion.identity);

                        // Adds the enemy and its AI into lists.
                        _enemiesSpawned.Add(new EnemyHolder(sectorName, enemy));
                        _enemiesAI.Add(enemy.GetComponent<EnemyAI>());
                    }
                }
            }
        }
        // Otherwise, coming from a battle, just reactivate the enemies that where deactivated and were not in battle.
        else
        {
            foreach (EnemyHolder holder in SceneSwitchDataHandler.enemiesNotInBattle)
            {
                holder.Enemy.SetActive(true);
                _enemiesSpawned.Add(holder);
                _enemiesAI.Add(holder.Enemy.GetComponent<EnemyAI>());
            }

            SceneSwitchDataHandler.isComingBackFromBattle = false;
        }

        _spawnEnemies = false;
    }

    /// <summary>
    /// Destroys all the created enemies and clear the reference lists.
    /// </summary>
    private void DestroyEnemies()
    {
        foreach(EnemyHolder holder in _enemiesSpawned)
        {
            Destroy(holder.Enemy);
        }
        _enemiesSpawned.Clear();
        _enemiesAI.Clear();
        _spawnEnemies = true;
    }
}
