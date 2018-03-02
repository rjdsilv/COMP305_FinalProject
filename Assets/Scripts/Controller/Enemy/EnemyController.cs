using UnityEngine;

/// <summary>
/// Class responsible for controlling the enemy.
/// </summary>
public abstract class EnemyController<A, L> : ActorController<A, L>, IEnemyController
    where A : ActorAttributes
    where L : ActorLevelTree<A>
{
    // Constant declaration.
    protected const int SELECTION_LIGHT_IDX = 5;

    // Public variable declaration.
    public int minEnemiesInBattle;                // The minimum number of enemies that will be spawned in a battle scene.
    public int maxEnemiesInBattle;                // The maximum number of enemies that will be spawned in a battle scene.

    // Protected variable declaration.
    protected GameManager _gameManager;           // The game manager script to be used.
    protected EnemyVisionAI _enemyVisionAI;       // The enemy vision AI script to be used.

    // The sector where the enemy was spawned.
    public string SectorName { get; set; }

    // The battle scene to be loaded.
    public string BattleScene { get; set; }

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //// EVENT METHODS

    /// <summary>
    /// Method called after all scene updates occur. This method will set the game state to battle.
    /// </summary>
    private void LateUpdate()
    {
        if (_enemyVisionAI.IsSeeingPlayer())
        {
            _gameManager.GoToBattle(BattleScene, gameObject);
        }
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //// NON EVENT METHODS
    
    /// <summary>
    /// Returns the enemy selection light to be used.
    /// </summary>
    /// <returns>The enemy selection light to be used.</returns>
    public Light GetSelectionLight()
    {
        return transform.GetChild(SELECTION_LIGHT_IDX).GetComponent<Light>();
    }

    /// <summary>
    /// Initialize all the necessary variables.
    /// </summary>
    protected override void Init()
    {
        _gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        _enemyVisionAI = GetComponent<EnemyVisionAI>();
    }
}
