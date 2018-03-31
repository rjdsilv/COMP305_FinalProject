using UnityEngine;

/// <summary>
/// Controller class to be used with any Orc in the game.
/// </summary>
public class OrcController : EnemyController<OrcAttributes, OrcLevelTree>
{    // Public variable declaration.
    [HideInInspector]
    public DestinySpear destinySpear;                   // The Orc's stone punch ability.
    public DestinySpearLevelTree destinySpearLevelTree; // The Orc's stone punch level tree.

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
        destinySpear = destinySpearLevelTree.GetAttributesForCurrentLevel();
        _abilityList.Add(destinySpear);
    }

    public override void PlayDamageSound()
    {
    }
}
