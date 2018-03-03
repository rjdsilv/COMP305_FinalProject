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

    void DecreaseHealth(int ammount);

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
}
