using UnityEngine;

/// <summary>
/// Class responsible for controlling a mage on the game.
/// </summary>
public class MageController : PlayerController<MageAttributes, MageLevelTree>
{
    /// <summary>
    /// Initializes all the necessary variables.
    /// </summary>
    private void Start()
    {
        Init();
    }

    /// <see cref="ActorController{A, L}">
    protected override void Init()
    {
        attributes.health = levelTree.GetAttributesForCurrentLevel().health;
        attributes.level = levelTree.GetAttributesForCurrentLevel().level;
        attributes.mana = levelTree.GetAttributesForCurrentLevel().mana;
        attributes.managedByAI = levelTree.GetAttributesForCurrentLevel().managedByAI;
        attributes.maxAttack = levelTree.GetAttributesForCurrentLevel().maxAttack;
        attributes.maxDefense = levelTree.GetAttributesForCurrentLevel().maxDefense;
        attributes.minAttack = levelTree.GetAttributesForCurrentLevel().minAttack;
        attributes.minDefense = levelTree.GetAttributesForCurrentLevel().minDefense;
    }
}
