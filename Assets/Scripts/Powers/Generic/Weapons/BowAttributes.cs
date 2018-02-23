/// <summary>
/// Class that will be used to store all the bow attributes for the game.
/// </summary>
public class BowAttributes : GenericWeaponAttributes
{
    /// <summary>
    /// Builds a new instance of BowAttributes.
    /// </summary>
    public BowAttributes() : base()
    {
        Type = PowerType.ATTACK;
    }
}
