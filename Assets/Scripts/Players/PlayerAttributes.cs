/// <summary>
/// Class that will hold all the necessary player attributes.
/// </summary>
public class PlayerAttributes : CharacterAttributes
{
    // The player's XP points.
    public int CurrentXp { get; set; }

    // The player's ammount of gold.
    public int CurrentGold { get; set; }

    // Flag indicating whether the player has a mana attribute or not.
    public bool HasMana { get; set; }

    // The ammount of mana the player has.
    public int CurrentMana { get; set; }

    // Flag indicating whether the player has a stamina attribute or not.
    public bool HasStamina { get; set; }

    // The ammout of stamina the player has.
    public int CurrentStamina { get; set; }
}
