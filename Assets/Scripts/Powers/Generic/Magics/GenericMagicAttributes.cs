/// <summary>
/// Class containing all the attributes that will be considered when a character
/// uses a magic during a battle.
/// </summary>
public abstract class GenericMagicAttributes : GenericPowerAttributes
{
    // How much Mana this magic will consume when used.
    public int ManaComsuption { get; set; }
}
