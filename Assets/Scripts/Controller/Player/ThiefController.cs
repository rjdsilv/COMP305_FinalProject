using UnityEngine;

/// <summary>
/// Class responsible for controlling a thief on the game.
/// </summary>
public class ThiefController : PlayerController<ThiefAttributes, ThiefLevelTree>
{
    [HideInInspector]
    public Bow bow;                            // The mage's fire ball ability.

    [HideInInspector]
    public Dagger dagger;                      // The mage's lightning ball ability.

    public BowLevelTree bowLevelTree;          // The mage's fire ball level tree.
    public DaggerLevelTree daggerLevelTree;    // The mage's lighting ball level tree.

    /// <summary>
    /// Initializes all the necessary variables.
    /// </summary>
    private void Awake()
    {
        playerNumber = 2;
        Init();
        _audioSource = GetComponent<AudioSource>();
    }

    /// <see cref="ActorController{A, L}">
    protected override void Init()
    {
        base.Init();
        SetAttributesForCurrentLevel();

        // Initializes the mage's abilities.
        bow = bowLevelTree.GetAttributesForCurrentLevel();
        dagger = daggerLevelTree.GetAttributesForCurrentLevel();
        _abilityList.Add(bow);
        _abilityList.Add(dagger);
    }

    /// <see cref="ActorController{A, L}"/>
    protected override void SetAttributesForCurrentLevel()
    {
        // Initializes the mage attributes.
        attributes.health = levelTree.GetAttributesForCurrentLevel().health;
        attributes.level = levelTree.GetAttributesForCurrentLevel().level;
        attributes.stamina = levelTree.GetAttributesForCurrentLevel().stamina;
        attributes.maxAttack = levelTree.GetAttributesForCurrentLevel().maxAttack;
        attributes.maxDefense = levelTree.GetAttributesForCurrentLevel().maxDefense;
        attributes.minAttack = levelTree.GetAttributesForCurrentLevel().minAttack;
        attributes.minDefense = levelTree.GetAttributesForCurrentLevel().minDefense;
    }
}
