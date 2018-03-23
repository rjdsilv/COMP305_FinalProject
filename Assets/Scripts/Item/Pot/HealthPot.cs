/// <summary>
/// Class representing the Health Pot attributes on the system.
/// </summary>
public class HealthPot : Item
{
    /// <see cref="IUsable"/>
    public override void Use(IPlayerController playerController)
    {
        RestoreHealth(playerController);
    }

    /// <summary>
    /// Restores the health by the ammount of power configured for the health pot.
    /// </summary>
    /// <param name="playerController">The player to have the health restored.</param>
    private void RestoreHealth(IPlayerController playerController)
    {
        playerController.IncreaseHealth(power);
        Destroy();
    }
}
