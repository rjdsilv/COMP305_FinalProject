using UnityEngine;
/// <summary>
/// Interface implemented by any controller.
/// </summary>
public interface IController
{
    /// <summary>
    /// Method in charge of attacking the given opponent.
    /// </summary>
    /// <param name="opponent">The opponent to be attacked.</param>
    int Attack(GameObject opponent, ActorAbility ability);

    /// <summary>
    /// Calculates the defense power for the given controller.
    /// </summary>
    /// <returns>The calculated defense power.</returns>
    int CalculateDefense();

    /// <summary>
    /// Method that will be used for decreasing the actor's health.
    /// </summary>
    /// <param name="amount">The amount of health the actor will have decreased.</param>
    void DecreaseHealth(int amount);

    /// <summary>
    /// Method that indicates whether a player is managed by AI or not.
    /// </summary>
    /// <returns><b>true</b> if player is managed by ai. <b>false</b> otherwise.</returns>
    bool IsManagedByAI();

    /// <summary>
    /// Method to indicate if an actor is still alive.
    /// </summary>
    /// <returns><b>true</b> if the actor is alive. <b>false</b> otherwise.</returns>
    bool IsAlive();

    /// <summary>
    /// Method that will randomly select the ability that will be used for the enemy to attack.
    /// </summary>
    /// <returns>The ability to use on the attack.</returns>
    ActorAbility SelectAbility();

    /// <summary>
    /// Gets the actor's current level.
    /// </summary>
    /// <returns>The actor's current level</returns>
    int GetCurrentLevel();

    /// <summary>
    /// Levels up the actor if necessary.
    /// </summary>
    void LevelUp();

    void PlayDamageSound();
}
