
/// <summary>
/// Interface containing commom methods for all player controllers.
/// </summary>
public interface IPlayerController : IController
{
    /// <summary>
    /// Method that will be used for increasing the players's consumable (mana or stamina).
    /// </summary>
    /// <param name="amount">The amount to be increased.</param>
    void IncreaseConsumable(float amount);

    /// <summary>
    /// Method to indicate if the player still has consumable force to use he given ability.
    /// </summary>
    /// <param name="ability">The ability to be used.</param>
    /// <returns><b>true</b> if the player has consumable force and can use ability. <b>false</b> otherwise.</returns>
    bool CanAttack(PlayerAbility ability);
}
