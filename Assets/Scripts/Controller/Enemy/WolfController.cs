using UnityEngine;

/// <summary>
/// Controller class to be used with any wolf in the game.
/// </summary>
public class WolfController : EnemyController<WolfAttributes, WolfLevelTree>
{
    // Public variable declaration.
    [HideInInspector]
    public PowerBite powerBite;                     // The wolf's power bite ability.
    public PowerBiteLevelTree powerBiteLevelTree;   // The wolf's power bite level tree.

    private AudioSource _audioSource;

    /// <summary>
    /// Method called when the 
    /// </summary>
    private void OnEnable()
    {
        SetGameManager();
        _audioSource = GetComponent<AudioSource>();
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

    public override void PlayDamageSound()
    {
        _clipToPlay %= audioClips.Length;
        _audioSource.clip = audioClips[_clipToPlay++];
        _audioSource.Play();
    }
}
