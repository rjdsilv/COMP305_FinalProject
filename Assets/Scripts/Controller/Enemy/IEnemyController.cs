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
    /// <param name="amount">The amount of health to be decreased.</param>
    void DecreaseHealthHUD(int amount);

    /// <summary>
    /// Gets the ammount of XP earned by killing the given enemy.
    /// </summary>
    /// <returns>The ammount of XP earned by killing the given enemy.</returns>
    int GetXpEarnedForKilling();

    /// <summary>
    /// Gets the ammount of Gold earned by killing the given enemy.
    /// </summary>
    /// <returns>The ammount of Gold earned by killing the given enemy.</returns>
    int GetGoldEarnedForKilling();

    /// <summary>
    /// Gets the minimum number of enemies that will be spawn in a battle scene.
    /// </summary>
    /// <returns>The minimum number of enemies that will be spawn in a battle scene.</returns>
    int GetMinEnemiesInBattle();

    /// <summary>
    /// Gets the maximum number of enemies that will be spawn in a battle scene.
    /// </summary>
    /// <returns>The maximum number of enemies that will be spawn in a battle scene.</returns>
    int GetMaxEnemiesInBattle();

    /// <summary>
    /// Method that will determine if the enemy will drop any health pot to be collected.
    /// </summary>
    /// <returns><b>true</b> if a health pot is dropped/ <b>false</b> otherwise.</returns>
    bool DropHealthPot();

    /// <summary>
    /// Method that will determine if the enemy will drop any mana pot to be collected.
    /// </summary>
    /// <returns><b>true</b> if a mana pot is dropped/ <b>false</b> otherwise.</returns>
    bool DropManaPot();

    /// <summary>
    /// Method that will determine if the enemy will drop any stamina pot to be collected.
    /// </summary>
    /// <returns><b>true</b> if a stamina pot is dropped/ <b>false</b> otherwise.</returns>
    bool DropStaminaPot();

    bool DropKey();

    void PlayDamageSound();
}
