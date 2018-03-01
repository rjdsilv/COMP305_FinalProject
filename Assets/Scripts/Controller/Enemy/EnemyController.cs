using UnityEngine;

/// <summary>
/// Class responsible for controlling the enemy.
/// </summary>
public abstract class EnemyController<A, L> : ActorController<A, L>
    where A : ActorAttributes
    where L : ActorLevelTree<A>
{
    // Protected variable declaration.
    protected GameManager _gameManager;           // The game manager script to be used.
    protected EnemyVisionAI _enemyVisionAI;       // The enemy vision AI script to be used.

    // The sector where the enemy was spawned.
    public string SectorName { get; set; }

    // The battle scene to be loaded.
    public string BattleScene { get; set; }

    /// <summary>
    /// Initialize all the necessary variables.
    /// </summary>
    protected override void Init()
    {
        _gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        _enemyVisionAI = GetComponent<EnemyVisionAI>();
    }

    /// <summary>
    /// Method called after all scene updates occur. This method will set the game state to battle.
    /// </summary>
    private void LateUpdate()
    {
        if (_enemyVisionAI.IsSeeingPlayer())
        {
            _gameManager.GoToBattle(BattleScene);
        }
    }
}
