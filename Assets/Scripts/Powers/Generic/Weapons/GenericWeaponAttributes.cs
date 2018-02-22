/// <summary>
/// Class containing all the attributes that will be considered when a character
/// uses a weapon during a battle.
/// </summary>
public abstract class GenericWeaponAttributes : GenericPowerAttributes
{
    // How much stamina this weapon will consume when used.
    public int StaminaComsuption { get; set; }
}
