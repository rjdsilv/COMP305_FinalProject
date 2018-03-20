using UnityEngine;

public class WolfController : EnemyController<WolfAttributes, WolfLevelTree>
{
    // Public variable declaration.
    [HideInInspector]
    public PowerBite powerBite;                     // The wolf's power bite ability.

    public PowerBiteLevelTree powerBiteLevelTree;   // The wolf's power bite level tree.

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
        powerBite = powerBiteLevelTree.GetAttributesForCurrentLevel();
        _abilityList.Add(powerBite);
    }

    /// <see cref="ActorController{A, L}"/>
    protected override void SetAttributesForCurrentLevel()
    {
        base.SetAttributesForCurrentLevel();

        // Initializes its attributes.
        attributes.health = levelTree.GetAttributesForCurrentLevel().health;
        attributes.level = levelTree.GetAttributesForCurrentLevel().level;
        attributes.maxAttack = levelTree.GetAttributesForCurrentLevel().maxAttack;
        attributes.maxDefense = levelTree.GetAttributesForCurrentLevel().maxDefense;
        attributes.minAttack = levelTree.GetAttributesForCurrentLevel().minAttack;
        attributes.minDefense = levelTree.GetAttributesForCurrentLevel().minDefense;
        attributes.managedByAI = true;
    }
}
