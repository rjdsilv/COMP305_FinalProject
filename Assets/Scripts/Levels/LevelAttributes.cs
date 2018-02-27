/// <summary>
/// Class containing all the attributes that can be upgraded when the level changes.
/// </summary>
public class LevelAttributes
{    
    public int MinUpgradeXp { get; set; }    // The minimum ammount of XP to go to the given level.

    public int MaxLife { get; set; }         // The maximum ammount of life for the given level.

    public int MaxMana { get; set; }         // The maximum ammount of mana for the given level.

    public int MaxStamina { get; set; }      // The maximum ammount of stamina for the given level.

    public int MinAttackPower { get; set; }  // The minimum attack power for the given level.

    public int MaxAttackPower { get; set; }  // The maximum attack power for the given level.

    public int MinDefensePower { get; set; } // The minimum defense power for the given level.

    public int MaxDefensePower { get; set; } // The maximum attack power for the given level.
}
