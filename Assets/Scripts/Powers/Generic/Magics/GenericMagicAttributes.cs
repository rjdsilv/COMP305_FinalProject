/// <summary>
/// Class containing all the attributes that will be considered when a character
/// uses a magic during a battle.
/// </summary>
public abstract class GenericMagicAttributes : GenericPowerAttributes
{
    /// <summary>
    /// Creates a new instance of GenericMagicAttribute.
    /// </summary>
    protected GenericMagicAttributes()
    {
        Consumable = PowerConsumable.MANA;
    }

    /// <see cref="GenericPowerAttributes"/>
    public override int UsePower(PlayerController playerController)
    {
        if (CanUsePower(playerController))
        {
            playerController.GetAttributes().CurrentMana -= PowerCost;
            return CalculateAppliedPower(playerController);
        }

        return 0;
    }
}
