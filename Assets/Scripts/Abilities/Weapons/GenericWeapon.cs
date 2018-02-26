/// <summary>
/// Class containing all the attributes that will be considered when a character
/// uses a weapon during a battle.
/// </summary>
public abstract class GenericWeapon : GenericAbility
{
    /// <summary>
    /// Creates a new instance of GenericWeaponAttributes.
    /// </summary>
    protected GenericWeapon()
    {
        consumable = AbilityConsumable.STAMINA;
    }

    /// <see cref="GenericAbility"/>
    public override int UsePower(PlayerController playerController)
    {
        if (CanUsePower(playerController))
        {
            playerController.GetAttributes().CurrentStamina -= powerCost;
            return CalculateAppliedPower(playerController);
        }

        return 0;
    }
}
