using UnityEngine;

/// <summary>
/// Controller class to be used with any Golem in the game.
/// </summary>
public class GolemController : EnemyController<GolemAttributes, GolemLevelTree>
{
    // Public variable declaration.
    [HideInInspector]
    public StonePunch stonePunch;                   // The Golem stone punch ability.
    public StonePunchLevelTree stonePunchLevelTree; // The Golem's stone punch level tree.

    /// <summary>
    /// Method called when the 
    /// </summary>
    private void OnEnable()
    {
        _gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    /// <summary>
    /// Initializes all the necessary variables.
    /// </summary>
    private void Start()
    {
        Init();
    }
    /// <summary>
    /// Initialize all the necessary variables.
    /// </summary>
    protected override void Init()
    {
        // Initialize its parent first.
        base.Init();
        SetAttributesForCurrentLevel();

        // Initializes its abilities.
        stonePunch = stonePunchLevelTree.GetAttributesForCurrentLevel();
        _abilityList.Add(stonePunch);
    }
}
