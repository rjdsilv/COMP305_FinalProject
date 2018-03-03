using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Interface containing a method to get the selection light from enemies.
/// </summary>
public interface IEnemyController : IController
{
    /// <summary>
    /// Returns the enemy selection light to be used.
    /// </summary>
    /// <returns>The enemy selection light to be used.</returns>
    Light GetSelectionLight();

    /// <summary>
    /// Decreases the health from int the actor's HUD.
    /// </summary>
    /// <param name="ammount">The ammount of health to be decreased.</param>
    void DecreaseHealthHUD(int ammount);
}
