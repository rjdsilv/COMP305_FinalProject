using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This class will manage all the actions that will happen in a battle.
/// </summary>
public class BattleManager : MonoBehaviour
{
    // Constants declaration.
    private const float SCALE_FACTOR = 1.25f;
    private const float MIN_SEL_TIME = 0.25f;
    private const float MIN_ATTACK_TIME = 0.25f;

    // Public variable declaration.
    public int turnTime;                            // The time the turn will endure.
    public PlayerSpawnPoint[] playerSpawnPoints;    // The points where the players will spawn in the battle scene.
    public Vector3[] enemySpawnPoints;              // The points where the enemies will spawn in the battle scene.
    public HUDManager hudManager;                   // The HUD manager to be used.
    public Material enemyMaterial;                  // The material to be used on the battle scene.

    // Private variable declaration.
    // Mage variables.
    private Vector3 _mageOldPos;
    private GameObject _mage;
    private MageController _mageController;

    // Generic variables.
    private GameObject _actorPlaying;
    private ActorAbility _selectedAbility;
    private float _lastSwapTime;
    private float _lastAttackTime;
    private float _turnRemainingTime;
    private bool _turnStarted;
    private bool _attackExecuted;

    // Enemies variables.
    private int _enemyPlayerIndex = -1;
    private int _selectedEnemyIndex = 0;
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

        // Initialize the HUD.
        hudManager.InitializePlayersHUD(_mage);
        hudManager.InitializeTurnTimer(turnTime);

        // Initializes the last selection time.
        _lastSwapTime = Time.time;
        _lastAttackTime = Time.time;
        
        StartCoroutine(BattleLoop());
	}

    /// <summary>
    /// Method that will run the game logic.
    /// </summary>
    private void Update()
    {
        if (_turnStarted)
        {
            SwapAbility();
            SwapEnemy();
            Attack();
        }
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //// NON EVENT METHODS

    /// <summary>
    /// Method that will control the battle turn flow.
    /// </summary>
    private IEnumerator BattleLoop()
    {
        while (!BattleEnd())
        {
            yield return StartTurn();
            yield return PlayTurn();
        }

        if (_mage.GetControllerComponent().IsAlive())
        {
            hudManager.DisplayEndOfBattleText();
            yield return new WaitForSeconds(1.5f);
            hudManager.HideTurnText();
            _mage.transform.position = _mageOldPos;
            SceneData.isInBattle = false;
            SceneManager.LoadScene("ForestMain");
        }
    }

    /// <summary>
    /// Resets everything to start a new turn.
    /// </summary>
    private IEnumerator StartTurn()
    {
        _turnStarted = false;
        _attackExecuted = false;

        // Initializes the battle turn.
        if ((null == _actorPlaying) || !_actorPlaying.IsPlayer())
        {
            _actorPlaying = _mage;
        }
        else
        {
            int counter = 0;
            do
            {
                _enemyPlayerIndex = ClampIndex(++_enemyPlayerIndex, _enemies.Length - 1, true);
                _actorPlaying = _enemies[_enemyPlayerIndex];
                counter++;
            }
            while (!_enemies[_enemyPlayerIndex].GetControllerComponent().IsAlive() && counter < _enemies.Length);
        }

        // Setting the turn start message.
        hudManager.DisplayTurnText(string.Format("{0}_{1}", _actorPlaying.name, _enemyPlayerIndex));
        yield return new WaitForSeconds(2);
        hudManager.HideTurnText();

        _turnRemainingTime = turnTime;
        _turnStarted = true;
    }

    /// <summary>
    /// Sets up things so the turn can be played.
    /// </summary>
    private IEnumerator PlayTurn()
    {
        while (!TurnEnd() && !BattleEnd())
        {
            yield return new WaitForSecondsRealtime(0.025f);
            _turnRemainingTime -= 0.025f;
            hudManager.UpdateTurnTimer(_turnRemainingTime);
        }
    }

    /// <summary>
    /// Method to indicate whether a turn finished or not.
    /// </summary>
    /// <returns></returns>
    private bool TurnEnd()
    {
        return _turnRemainingTime <= 0 || _attackExecuted;
    }

    /// <summary>
    /// Method to indicate whether a battle finished or not.
    /// </summary>
    /// <returns></returns>
    private bool BattleEnd()
    {
        foreach (GameObject enemy in _enemies)
        {
            if (enemy.GetControllerComponent().IsAlive())
                return false;
        }

        return true;
    }

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
                if (!_actorPlaying.GetControllerComponent().IsManagedByAI())
                {
                    int max = _enemies.Length - 1;
                    _enemies[_selectedEnemyIndex].GetEnemyControllerComponent().GetSelectionLight().intensity = 10f;

                    if (ControlUtils.SwapEnemyDown())
                    {
                        int counter = 0;
                        do
                        {
                            _selectedEnemyIndex = ClampIndex(--_selectedEnemyIndex, max, false);
                            _lastSwapTime = Time.time;
                            counter++;
                        }
                        while (!_enemies[_selectedEnemyIndex].GetControllerComponent().IsAlive() && counter < _enemies.Length);
                    }
                    else if (ControlUtils.SwapEnemyUp())
                    {
                        int counter = 0;
                        do
                        {
                            _selectedEnemyIndex = ClampIndex(++_selectedEnemyIndex, max, true);
                            _lastSwapTime = Time.time;
                            counter++;
                        }
                        while (!_enemies[_selectedEnemyIndex].GetControllerComponent().IsAlive() && counter < _enemies.Length);
                    }

                    _enemies[_selectedEnemyIndex].GetEnemyControllerComponent().GetSelectionLight().intensity = 30f;
                }
            }
        }
    }

    /// <summary>
    /// Perform an attack during the turn.
    /// </summary>
    private void Attack()
    {
        if ((Time.time - _lastAttackTime > MIN_ATTACK_TIME) && !_attackExecuted)
        {
            if (_actorPlaying.IsPlayer())
            {
                if (!_actorPlaying.GetControllerComponent().IsManagedByAI())
                {
                    if (ControlUtils.Attack())
                    {
                        GameObject selectedEnemy = _enemies[_selectedEnemyIndex];
                        int healthDrained = _actorPlaying.GetControllerComponent().Attack(selectedEnemy, _selectedAbility);
                        selectedEnemy.GetEnemyControllerComponent().DecreaseHealthHUD(healthDrained);

                        if (!selectedEnemy.GetControllerComponent().IsAlive())
                        {
                            int counter = 0;
                            do
                            {
                                _selectedEnemyIndex = ClampIndex(++_selectedEnemyIndex, _enemies.Length - 1, true);
                                _lastSwapTime = Time.time;
                                counter++;
                            }
                            while (!_enemies[_selectedEnemyIndex].GetControllerComponent().IsAlive() && counter < _enemies.Length);
                            _enemies[_selectedEnemyIndex].GetEnemyControllerComponent().GetSelectionLight().intensity = 30f;
                            selectedEnemy.SetActive(false);
                        }

                        Debug.Log("Health Drainned: " + healthDrained);
                        _attackExecuted = true;
                    }
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
                _selectedAbility = _mageController.fireBall;

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
                int enemiesInBattle = Mathf.FloorToInt(UnityEngine.Random.Range(controller.minEnemiesInBattle, controller.maxEnemiesInBattle + 0.999999f));

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
                if (_selectedAbility == _mageController.fireBall)
                    _selectedAbility = _mageController.lightningBall;
                else
                    _selectedAbility = _mageController.lightningBall;

                hudManager.SwapAbility();
                _lastSwapTime = Time.time;
            }
        }
    }
}
