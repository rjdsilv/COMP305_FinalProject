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
    public bool autoRecoverConsumable;              // Flag to indicate whether the players consumable should auto-recover when on battle.
    public PlayerSpawnPoint[] playerSpawnPoints;    // The points where the players will spawn in the battle scene.
    public Vector3[] enemySpawnPoints;              // The points where the enemies will spawn in the battle scene.
    public HUDManager hudManager;                   // The HUD manager to be used.
    public Material enemyMaterial;                  // The material to be used on the battle scene.

    // Private variable declaration.
    private PlayerAbility _selectedAbility;

    // Mage variables.
    private Vector3 _mageOldPos;
    private GameObject _mage;
    private MageController _mageController;

    // Generic variables.
    private GameObject _actorPlaying;
    private float _lastSwapTime;
    private float _lastAttackTime;
    private float _turnRemainingTime;
    private bool _turnStarted;
    private bool _attackExecuted;
    private bool _canAIAttack = false;

    // Enemies variables.
    private int _enemyPlayerIndex = -1;
    private int _selectedEnemyIndex = 0;
    private GameObject[] _enemies;

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //// EVENT METHODS

    /// <summary>
    /// Initializes all the necessary data on the manager.
    /// </summary>
    private void Start()
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
        if (_turnStarted && !IsBattleEnded())
        {
            SwapAbility();
            SwapEnemy();
            Attack();
        }
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
    /// Method that will control the battle turn flow.
    /// </summary>
    private IEnumerator BattleLoop()
    {
        while (!IsBattleEnded())
        {
            yield return StartTurn();
            yield return PlayTurn();
        }

        yield return EndBattle();
    }

    /// <summary>
    /// Resets everything to start a new turn.
    /// </summary>
    private IEnumerator StartTurn()
    {
        _turnStarted = false;
        _attackExecuted = false;

        // Selects the player to attack.
        if ((null == _actorPlaying) || !_actorPlaying.IsPlayer())
        {
            _actorPlaying = _mage;
        }
        // Selects the enemy to attack.
        else
        {
            SelectEnemyToAttack();
        }

        // Setting the turn start message.
        hudManager.DisplayTurnText(_actorPlaying.name);
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
        float startTime = Time.time;
        float turnTimeDecreaseRate = 0.05f;

        while (!IsTurnEnded() && !IsBattleEnded())
        {
            yield return new WaitForSecondsRealtime(turnTimeDecreaseRate);
            _turnRemainingTime -= turnTimeDecreaseRate;
            hudManager.UpdateTurnTimer(_turnRemainingTime);

            if (autoRecoverConsumable)
            {
                foreach (GameObject player in SceneData.playerList)
                {
                    player.GetPlayerControllerComponent().IncreaseConsumable(turnTimeDecreaseRate / 2);
                    hudManager.UpdateConsumableHUD(player, turnTimeDecreaseRate / 2, false);
                }
            }

            if (!_attackExecuted && _actorPlaying.GetControllerComponent().IsManagedByAI() && (Time.time - startTime) > turnTime / 2)
            {
                _canAIAttack = true;
            }
        }
    }

    private IEnumerator EndBattle()
    {
        if (IsAnyPlayerAlive())
        {
            // Displays the end of battle text.
            hudManager.DisplayEndOfBattleText();
            yield return new WaitForSeconds(1.5f);
            hudManager.HideTurnText();

            // Restores the old positions for all players.
            RestorePlayersPositions();

            // Destroys all the enemies that entered the battle.
            foreach (GameObject enemy in SceneData.enemyInBattleList)
            {
                Destroy(enemy);
            }

            // Restores the calling scene.
            SceneData.enemyInBattleList.Clear();
            SceneData.isInBattle = false;
            SceneData.isCommingBackFronBattle = true;
            SceneManager.LoadScene(SceneData.mainScene);
        }
        else
        {
            hudManager.DisplayGameOverText();
        }
    }

    /// <summary>
    /// Method that will check if any of the players is still alive.
    /// </summary>
    /// <returns><b>true</b> if at least one player is alive. <b>false</b> otherwise.</returns>
    private bool IsAnyPlayerAlive()
    {
        return _mage.GetControllerComponent().IsAlive();
    }

    /// <summary>
    /// Restores all the previous positions for the players.
    /// </summary>
    private void RestorePlayersPositions()
    {
        _mage.transform.position = _mageOldPos;
    }

    /// <summary>
    /// Method to indicate whether a turn finished or not.
    /// </summary>
    /// <returns></returns>
    private bool IsTurnEnded()
    {
        return _turnRemainingTime <= 0 || _attackExecuted;
    }

    /// <summary>
    /// Method to indicate whether a battle finished or not.
    /// </summary>
    /// <returns></returns>
    private bool IsBattleEnded()
    {
        // Is there players alive?
        if (!IsAnyPlayerAlive())
        {
            return true;
        }
        else
        {
            // Is there enemies alive?
            foreach (GameObject enemy in _enemies)
            {
                if (enemy.GetControllerComponent().IsAlive())
                    return false;
            }
        }

        return true;
    }

    /// <summary>
    /// Selects an alive enemy to perform the next attack.
    /// </summary>
    private void SelectEnemyToAttack()
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
                        SwapEnemyDown(max);
                    }
                    else if (ControlUtils.SwapEnemyUp())
                    {
                        SwapEnemyUp(max);
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
            // The player is attacking.
            if (_actorPlaying.IsPlayer())
            {
                IPlayerController attackerController = _actorPlaying.GetPlayerControllerComponent();

                // Checks if the player can attack, which means it has consumable.
                if (attackerController.CanAttack(_selectedAbility))
                {
                    // The player is controlled by human.
                    if (!attackerController.IsManagedByAI())
                    {
                        if (ControlUtils.Attack())
                        {
                            GameObject selectedEnemy = _enemies[_selectedEnemyIndex];
                            IEnemyController enemyController = selectedEnemy.GetEnemyControllerComponent();
                            enemyController.DecreaseHealthHUD(attackerController.Attack(selectedEnemy, _selectedAbility));
                            hudManager.UpdateConsumableHUD(_actorPlaying, _selectedAbility.consumptionValue, true);

                            if (!enemyController.IsAlive())
                            {
                                SwapEnemyUp(_enemies.Length - 1);
                                enemyController.GetSelectionLight().intensity = 30f;
                                selectedEnemy.SetActive(false);
                            }

                            _attackExecuted = true;
                            _lastAttackTime = Time.time;
                        }
                    }
                }
            }
            else
            {
                IEnemyController attackerController = _actorPlaying.GetEnemyControllerComponent();

                if (_canAIAttack)
                {
                    _attackExecuted = true;
                    ActorAbility selectedAbility = attackerController.SelectAbility();

                    // Finds an alive player to attack.
                    GameObject selectedPlayer = SceneData.playerList[Mathf.FloorToInt(UnityEngine.Random.Range(0, SceneData.playerList.Count - 0.00001f))];
                    while (!selectedPlayer.GetControllerComponent().IsAlive())
                    {
                        selectedPlayer = SceneData.playerList[Mathf.FloorToInt(UnityEngine.Random.Range(0, SceneData.playerList.Count - 0.00001f))];
                    }

                    hudManager.DecreaseHealthHUD(selectedPlayer, attackerController.Attack(selectedPlayer, selectedAbility));
                    _lastAttackTime = Time.time;
                    _canAIAttack = false;
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
    /// Swaps the selected enemy up in direction for the max maximum number of enemies until an alive enemy is found.
    /// </summary>
    /// <param name="max">The maximum number of enemies.</param>
    private void SwapEnemyUp(int max)
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

    /// <summary>
    /// Swaps the selected enemy down in direction for the max maximum number of enemies until an alive enemy is found.
    /// </summary>
    /// <param name="max">The maximum number of enemies.</param>
    private void SwapEnemyDown(int max)
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

                hudManager.SwapAbility(_mage);
                _lastSwapTime = Time.time;
            }
        }
    }
}
