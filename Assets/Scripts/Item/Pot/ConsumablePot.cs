/// <summary>
/// Class representing the Consumable (Mana or Stamina) Pot attributes on the system.
/// </summary>
public class ConsumablePot : Item
{
    public ConsumableType consumableType;

    /// <see cref="IUsable"/>
    public override void Use(IPlayerController playerController)
    {
        RestoreConsumable(playerController);
    }

    /// <summary>
    /// Restores the consumable by the ammount of power configured for the health pot.
    /// </summary>
    /// <param name="playerController">The player to have the consumable restored.</param>
    private void RestoreConsumable(IPlayerController playerController)
    {
        playerController.IncreaseConsumable(power);
        Destroy();
    }
}
