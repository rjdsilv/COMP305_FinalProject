/// <summary>
/// Class containing all the attributes that will be considered when a character
/// uses a magic during a battle.
/// </summary>
public abstract class GenericMagic : GenericAbility
{
    /// <summary>
    /// Creates a new instance of GenericMagicAttribute.
    /// </summary>
    protected GenericMagic()
    {
        consumable = AbilityConsumable.MANA;
    }

    /// <see cref="GenericAbility"/>
    public override int UsePower(PlayerController playerController)
    {
        if (CanUsePower(playerController))
        {
            playerController.GetAttributes().CurrentMana -= powerCost;
            return CalculateAppliedPower(playerController);
        }

        return 0;
    }
}
