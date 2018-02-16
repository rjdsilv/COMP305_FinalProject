using System;
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
            float enemyRadius = CalculateCircleRadiusForEnemy(enemy.Enemy, circleCenter);
            if (enemyRadius < sectorProperties.fightRadius && enemy.Sector == sectorName)
            {
                if (!_enemiesToFight.Contains(enemy))
                {
                    _enemiesToFight.Add(enemy);
                }
            }
        }
    }

    /// <summary>
    /// Saves the necessary data for switching between scenes.
    /// </summary>
    void SaveSceneData()
    {
        _enemiesSpawned.RemoveAll(enemy => _enemiesToFight.Contains(enemy));
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        foreach(GameObject player in players)
        {
            PlayerHolder playerHolder = new PlayerHolder(player.transform.name, new Vector3(player.transform.position.x, player.transform.position.y, 0));
            if (!SceneSwitchDataHandler.players.Contains(playerHolder))
            {
                SceneSwitchDataHandler.players.Add(playerHolder);
            }
        }

        SceneSwitchDataHandler.enemiesInBattle = _enemiesToFight;
        SceneSwitchDataHandler.enemiesNotInBattle = _enemiesSpawned;
        foreach (EnemyHolder holder in SceneSwitchDataHandler.enemiesInBattle) holder.Enemy.SetActive(false);
        foreach (EnemyHolder holder in SceneSwitchDataHandler.enemiesNotInBattle) holder.Enemy.SetActive(false);
        _isDataSaved = true;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="enemy"></param>
    /// <param name="center"></param>
    /// <returns></returns>
    float CalculateCircleRadiusForEnemy(GameObject enemy, Vector2 center)
    {
        return Mathf.Sqrt(Mathf.Pow((enemy.transform.position.x - center.x), 2.0f) + Mathf.Pow((enemy.transform.position.y - center.y), 2.0f));
    }

    void PositionPlayers()
    {
        if (SceneSwitchDataHandler.isComingBackFromBattle)
        {
            // Reposition the players in the original position
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

            foreach (GameObject player in players)
            {
                PlayerHolder oldPlayer = SceneSwitchDataHandler.players.Find(p => p.Name == player.transform.name);
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
                        DontDestroyOnLoad(enemy);

                        // Adds the enemy and its AI into lists.
                        _enemiesSpawned.Add(new EnemyHolder(sectorName, enemy));
                        _enemiesAI.Add(enemy.GetComponent<EnemyAI>());
                    }
                }
            }
        }
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
