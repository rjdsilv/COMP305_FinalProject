/// <summary>
/// Class containing all the attributes that will be considered when a character
/// uses a weapon during a battle.
/// </summary>
public abstract class GenericWeaponAttributes : GenericPowerAttributes
{
    /// <summary>
    /// Creates a new instance of GenericWeaponAttributes.
    /// </summary>
    protected GenericWeaponAttributes()
    {
        Consumable = PowerConsumable.STAMINA;
    }

    /// <see cref="GenericPowerAttributes"/>
    public override int UsePower(PlayerController playerController)
    {
        if (CanUsePower(playerController))
        {
            playerController.GetAttributes().CurrentStamina -= PowerCost;
            return CalculateAppliedPower(playerController);
        }

        return 0;
    }
}
