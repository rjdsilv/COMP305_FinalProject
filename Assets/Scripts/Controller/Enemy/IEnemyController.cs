﻿using UnityEngine;
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
}
