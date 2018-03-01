using UnityEngine;

public class WolfController : EnemyController<WolfAttributes, WolfLevelTree>
{
    [HideInInspector]
    public PowerBite powerBite;                     // The wolf's power bite ability.

    public PowerBiteLevelTree powerBiteLevelTree;   // The wolf's power bite level tree.

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

        // Initializes its attributes.
        attributes.health = levelTree.GetAttributesForCurrentLevel().health;
        attributes.level = levelTree.GetAttributesForCurrentLevel().level;
        attributes.managedByAI = levelTree.GetAttributesForCurrentLevel().managedByAI;
        attributes.maxAttack = levelTree.GetAttributesForCurrentLevel().maxAttack;
        attributes.maxDefense = levelTree.GetAttributesForCurrentLevel().maxDefense;
        attributes.minAttack = levelTree.GetAttributesForCurrentLevel().minAttack;
        attributes.minDefense = levelTree.GetAttributesForCurrentLevel().minDefense;

        // Initializes its abilities.
        powerBite = powerBiteLevelTree.GetAttributesForCurrentLevel();
    }
}
