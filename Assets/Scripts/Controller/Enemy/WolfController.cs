using UnityEngine;

public class WolfController : EnemyController<WolfAttributes, WolfLevelTree>
{
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
        base.Init();

        attributes.health = levelTree.GetAttributesForCurrentLevel().health;
        attributes.level = levelTree.GetAttributesForCurrentLevel().level;
        attributes.managedByAI = levelTree.GetAttributesForCurrentLevel().managedByAI;
        attributes.maxAttack = levelTree.GetAttributesForCurrentLevel().maxAttack;
        attributes.maxDefense = levelTree.GetAttributesForCurrentLevel().maxDefense;
        attributes.minAttack = levelTree.GetAttributesForCurrentLevel().minAttack;
        attributes.minDefense = levelTree.GetAttributesForCurrentLevel().minDefense;
    }
}
