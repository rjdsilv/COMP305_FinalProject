/// <summary>
/// The base attributes for all character in the game.
/// </summary>
public abstract class CharacterAttributes
{
    // The character's current level.
    protected int _currentLevel;

    // The character's current ammount life.
    public int CurrentLife { get; set; }

    // Indicates if the attributes are alread initialized.
    public bool IsInitialized { get; set; }

    // Flag indicating whether the player has a mana attribute or not.
    public bool HasMana { get; set; }

    // The ammount of mana the player has.
    public int CurrentMana { get; set; }

    // Flag indicating whether the player has a stamina attribute or not.
    public bool HasStamina { get; set; }

    // The ammout of stamina the player has.
    public int CurrentStamina { get; set; }

    // The charactere level dictionary to be consulted.
    public LevelDictionary LevelDictionary { get; set; }

    /// <summary>
    /// Creates a new instance or CharacterAttributes.
    /// </summary>
    protected CharacterAttributes()
    {
        _currentLevel = 1;
        LevelDictionary = new LevelDictionary();
        IsInitialized = false;
    }

    // The character's current level property accessors.
    public int CurrentLevel
    {
        get
        {
            return _currentLevel;
        }
    }

    /// <summary>
    /// Gets the attribute values for the current level.
    /// </summary>
    /// <returns>The attributes for the current level.</returns>
    public LevelAttributes GetLevelAttributes()
    {
        return LevelDictionary.GetLevelAttributes(CurrentLevel);
    }
}
