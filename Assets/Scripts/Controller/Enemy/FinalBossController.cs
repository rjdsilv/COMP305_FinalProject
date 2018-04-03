using UnityEngine;

/// <summary>
/// Controller class to be used with any Golem in the game.
/// </summary>
public class FinalBossController : EnemyController<FinalBossAttributes, FinalBossLevelTree>
{
    // Public variable declaration.
    [HideInInspector]
    public MagicalAxe magicalAxe;                   // The Final Boss magical axe ability.
    public MagicalAxeLevelTree magicalAxeLevelTree; // The Final Boss' magical axe level tree.

    /// <summary>
    /// Method called when the 
    /// </summary>
    private void OnEnable()
    {
        SetGameManager();
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
        magicalAxe = magicalAxeLevelTree.GetAttributesForCurrentLevel();
        _abilityList.Add(magicalAxe);
    }

    public override void PlayDamageSound()
    {
    }
}
