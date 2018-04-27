using UnityEngine;

/// <summary>
/// Class responsible for controlling a mage on the game.
/// </summary>
public class MageController : PlayerController<MageAttributes, MageLevelTree>
{
    [HideInInspector]
    public FireBall fireBall;                               // The mage's fire ball ability.

    [HideInInspector]
    public LightningBall lightningBall;                     // The mage's lightning ball ability.

    public FireBallLevelTree fireBallLevelTree;             // The mage's fire ball level tree.
    public LightningBallLevelTree lightningBallLevelTre;    // The mage's lighting ball level tree.

    /// <summary>
    /// Initializes all the necessary variables.
    /// </summary>
    private void Awake()
    {
        Init();
        _audioSource = GetComponent<AudioSource>();
    }

    /// <see cref="ActorController{A, L}">
    protected override void Init()
    {
        base.Init();
        SetAttributesForCurrentLevel();

        // Initializes the mage's abilities.
        fireBall = fireBallLevelTree.GetAttributesForCurrentLevel();
        lightningBall = lightningBallLevelTre.GetAttributesForCurrentLevel();
        _abilityList.Add(fireBall);
        _abilityList.Add(lightningBall);
    }

    /// <see cref="ActorController{A, L}"/>
    protected override void SetAttributesForCurrentLevel()
    {
        // Initializes the mage attributes.
        attributes.health = levelTree.GetAttributesForCurrentLevel().health;
        attributes.level = levelTree.GetAttributesForCurrentLevel().level;
        attributes.mana = levelTree.GetAttributesForCurrentLevel().mana;
        attributes.maxAttack = levelTree.GetAttributesForCurrentLevel().maxAttack;
        attributes.maxDefense = levelTree.GetAttributesForCurrentLevel().maxDefense;
        attributes.minAttack = levelTree.GetAttributesForCurrentLevel().minAttack;
        attributes.minDefense = levelTree.GetAttributesForCurrentLevel().minDefense;
    }
}
