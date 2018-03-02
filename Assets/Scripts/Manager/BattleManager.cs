using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class will manage all the actions that will happen in a battle.
/// </summary>
public class BattleManager : MonoBehaviour
{
    // Constants declaration.
    private const float SCALE_FACTOR = 1.25f;
    private const float MIN_SEL_TIME = 0.25f;

        // Public variable declaration.
    public PlayerSpawnPoint[] playerSpawnPoints;    // The points where the players will spawn in the battle scene.
    public Vector3[] enemySpawnPoints;              // The points where the enemies will spawn in the battle scene.
    public HUDManager hudManager;                   // The HUD manager to be used.
    public Material enemyMaterial;                  // The material to be used on the battle scene.

    // Private variable declaration.
    // Mage variables.
    private Vector3 _mageOldPos;
    private GameObject _mage;
    private PlayerAbility _mageSelectedAbility;
    private MageController _mageController;

    // Generic variables.
    private GameObject _actorPlaying;
    private float _lastSwapTime;

    // Enemies variables.
    private int _selectedEnemyIndex;
    private GameObject[] _enemies;

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //// EVENT METHODS

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

        // Initialize the HUD for the players.
        hudManager.InitializePlayersHUD(_mage);

        // Initializes the battle turn.
        _actorPlaying = _mage;

        // Initializes the last selection time.
        _lastSwapTime = Time.time;

	}

    /// <summary>
    /// Method that will run after all the normal update and fixed update runs in order.
    /// </summary>
    private void LateUpdate()
    {
        SwapAbility();
        SwapEnemy();
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //// NON EVENT METHODS

    /// <summary>
    /// Method responsible for swapping the player ability as commanded by
    /// </summary>
    public void SwapAbility()
    {
        if ((Time.time - _lastSwapTime > MIN_SEL_TIME))
        {
            // Is it a player that has the turn
            if (_actorPlaying.IsPlayer())
            {
                // The player is the mage.
                if (_actorPlaying.IsMage())
                {
                    SwapMageAbility();
                }
            }
        }
    }

    /// <summary>
    /// Method responsible for swapping between enemies when its a player turn.
    /// </summary>
    private void SwapEnemy()
    {
        if ((Time.time - _lastSwapTime > MIN_SEL_TIME))
        {
            if (_actorPlaying.IsPlayer())
            {
                if (!_actorPlaying.GetPlayerControllerComponent().IsManagedByAI())
                {
                    int max = _enemies.Length - 1;
                    _enemies[_selectedEnemyIndex].GetEnemyControllerComponent().GetSelectionLight().intensity = 10f;

                    if (ControlUtils.SwapEnemyDown())
                    {
                        _selectedEnemyIndex = ClampIndex(--_selectedEnemyIndex, max, false);
                        _lastSwapTime = Time.time;
                    }
                    else if (ControlUtils.SwapEnemyUp())
                    {
                        _selectedEnemyIndex = ClampIndex(++_selectedEnemyIndex, max, true);
                        _lastSwapTime = Time.time;
                    }

                    _enemies[_selectedEnemyIndex].GetEnemyControllerComponent().GetSelectionLight().intensity = 30f;
                }
            }
        }
    }

    /// <summary>
    /// Clamps the index based on the given parameters so it avoid array out of bounds.
    /// </summary>
    /// <param name="index">The index to be clamped</param>
    /// <param name="max">The maximum index value.</param>
    /// <param name="isUp">Flag indicating if the selection is up or down.</param>
    /// <returns></returns>
    private int ClampIndex(int index, int max, bool isUp)
    {
        if (index < 0 || index > max)
        {
            return isUp ? 0 : max;
        }

        return index;
    }

    /// <summary>
    /// Method responsible for spawning all the players from the previous scene on the battle scene.
    /// </summary>
    private void SpawnPlayers()
    {
        foreach (GameObject player in SceneData.playerList)
        {
            if (player.IsMage())
            {
                _mage = player;

                // Saves the old position to restore by the end of the battle.
                _mageOldPos = _mage.transform.position;

                // Sets the selected ability for the mage's main ability.
                _mageController = _mage.GetComponent<MageController>();
                _mageSelectedAbility = _mageController.fireBall;

                // Fixes the player for the battle.
                _mage.transform.position = FindSpawnPointForActor(player.name);
                _mage.transform.localScale *= SCALE_FACTOR;
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
                WolfController controller = enemy.GetEnemyControllerComponent() as WolfController;
                int enemiesInBattle = Mathf.FloorToInt(Random.Range(controller.minEnemiesInBattle, controller.maxEnemiesInBattle + 0.999999f));

                for (int i = 0; i < enemiesInBattle; i++)
                {
                    GameObject instantiatedEnemy = Instantiate(enemy, enemySpawnPoints[i], Quaternion.identity);
                    instantiatedEnemy.transform.localScale *= SCALE_FACTOR;
                    instantiatedEnemy.name = enemy.name;
                    instantiatedEnemy.GetComponent<SpriteRenderer>().material = enemyMaterial;
                    enemies.Add(instantiatedEnemy);
                }
            }
        }

        _enemies = enemies.ToArray();

        if (_enemies.Length > 0)
        {
            _selectedEnemyIndex = 0;
            _enemies[_selectedEnemyIndex].GetEnemyControllerComponent().GetSelectionLight().intensity = 30;
        }
    }

    /// <summary>
    /// Inactivates all the actors from the previous scene.
    /// </summary>
    private void InactivateActorsFromPreviousScene()
    {
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

    /// <summary>
    /// Method to swap a mage ability no matter if controlled by the AI or not.
    /// </summary>
    private void SwapMageAbility()
    {
        // The mage is being controlled by some humam player.
        if (!_mageController.attributes.managedByAI)
        {
            // The player chose to swap the hability.
            if (ControlUtils.SwapAbility() != 0)
            {
                if (_mageSelectedAbility == _mageController.fireBall)
                    _mageSelectedAbility = _mageController.lightningBall;
                else
                    _mageSelectedAbility = _mageController.lightningBall;

                hudManager.SwapAbility();
                _lastSwapTime = Time.time;
            }
        }
    }
}
