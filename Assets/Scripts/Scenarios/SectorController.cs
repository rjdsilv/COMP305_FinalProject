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
    public SectorProperties sectorProperties;
    public string battleScene;

    // Private variables declaration.
    private bool _isDataSaved = false;
    private bool _spawnEnemies = true;        // Indicates if the enemies were destroyed or not.
    private List<GameObject> _enemiesSpawned; // The enemies spawned.
    private List<GameObject> _enemiesToFight; // The enemies that are going to be on the battle.
    private List<EnemyAI> _enemiesAI;         // The enemy AI script.

	// Use this for initialization
	void Start ()
    {
        _enemiesSpawned = new List<GameObject>();
        _enemiesToFight = new List<GameObject>();
        _enemiesAI = new List<EnemyAI>();
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
            Debug.Log(string.Format("#Players on the Battle: {0} - #Enemies on the Battle: {1} - #Enemies Remaining: {2}\n",
                SceneSwitchDataHandler.playersOnScene.Count, SceneSwitchDataHandler.enemiesInBattle.Count, SceneSwitchDataHandler.enemiesNotInBattle.Count));
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
        foreach(GameObject enemy in _enemiesSpawned)
        {
            Vector2 circleCenter = new Vector2(ai.GetEnemyPosition().x, ai.GetEnemyPosition().y);
            float enemyRadius = CalculateCircleRadiusForEnemy(enemy, circleCenter);
            if (enemyRadius < sectorProperties.fightRadius)
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
            if (!SceneSwitchDataHandler.playersOnScene.Contains(player))
            {
                SceneSwitchDataHandler.playersOnScene.Add(player);
            }
        }

        SceneSwitchDataHandler.enemiesInBattle = _enemiesToFight;
        SceneSwitchDataHandler.enemiesNotInBattle = _enemiesSpawned;
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

    /// <summary>
    /// Spawns all the enemies based on the sector configuration.
    /// </summary>
    void SpawnEnemies()
    {
        if (sectorProperties.spawnEnemies && _spawnEnemies)
        {
            foreach (SectorEnemyProperties ep in sectorProperties.enemyProperties)
            {
                for (int i = 0; i < ep.spawnNumber; i++)
                {
                    // Instantiate the enemy.
                    GameObject enemy = Instantiate(
                        ep.enemy,
                        new Vector3(
                            transform.position.x + UnityEngine.Random.Range(-ep.spawnRadius, ep.spawnRadius), 
                            transform.position.y + UnityEngine.Random.Range(-ep.spawnRadius, ep.spawnRadius),
                            0
                        ),
                        Quaternion.identity
                    );

                    // Adds the enemy and its AI into lists.
                    _enemiesSpawned.Add(enemy);
                    _enemiesAI.Add(enemy.GetComponent<EnemyAI>());
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
        foreach(GameObject go in _enemiesSpawned)
        {
            Destroy(go.gameObject);
        }
        _enemiesSpawned.Clear();
        _enemiesAI.Clear();
        _spawnEnemies = true;
    }
}
