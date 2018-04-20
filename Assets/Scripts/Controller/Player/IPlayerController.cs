
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
    /// Method that will be used for increasing the players's health (mana or stamina).
    /// </summary>
    /// <param name="amount">The amount to be increased.</param>
    void IncreaseHealth(int amount);

    /// <summary>
    /// Method to indicate if the player still has consumable force to use he given ability.
    /// </summary>
    /// <param name="ability">The ability to be used.</param>
    /// <returns><b>true</b> if the player has consumable force and can use ability. <b>false</b> otherwise.</returns>
    bool CanAttack(PlayerAbility ability);

    /// <summary>
    /// Sets the player AI state.
    /// </summary>
    /// <param name="isManagedByAI">Flag indicating if the player is managed by AI or not.</param>
    void SetIsManagedByAI(bool isManagedByAI);

    /// <summary>
    /// Increases the ammount of XP this player has.
    /// <param name="amount">The amount of XP to increase.</param>
    /// </summary>
    void IncreaseXp(int amount);

    /// <summary>
    /// Increases the ammount of gold this player has.
    /// <param name="amount">The amount of gold to increase.</param>
    /// </summary>
    void IncreaseGold(int amount);

    /// <summary>
    /// Gets the player number 1 or 2.
    /// </summary>
    /// <returns>The player number</returns>
    int GetPlayerNumber();

    int GetGold();

    int GetXp();

    int GetHealth();

    int GetMaxHealth();

    float GetConsumable();

    float GetMaxConsumable();

    /// <summary>
    /// Returns the indication if the player is the player one or not.
    /// </summary>
    /// <returns><b>true</b> if the player is player one. <b>false</b> otherwise.</returns>
    bool IsPlayerOne();

    /// <summary>
    /// Returns the indication if the player is the player two or not.
    /// </summary>
    /// <returns><b>true</b> if the player is player two. <b>false</b> otherwise.</returns>
    bool IsPlayerTwo();
}
