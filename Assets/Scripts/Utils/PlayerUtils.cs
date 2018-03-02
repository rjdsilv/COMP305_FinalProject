using UnityEngine;

/// <summary>
/// Utilitary class used to deal with common player operations.
/// </summary>
public static class PlayerUtils
{
    private const string MAGE = "Mage";     // The mage's name.

    /// <summary>
    /// Method to indicate whether a given player is a mage or not.
    /// </summary>
    /// <param name="player">The player to be checked.</param>
    /// <returns><b>true</b> if the player is a mage. <b>false</b> otherwise.</returns>
    public static bool IsMage(this GameObject player)
    {
        return MAGE == player.name;
    }

    /// <summary>
    /// Method to evaluate if the given actor is a player.
    /// </summary>
    /// <param name="actor">The actor to be evaluated.</param>
    /// <returns><b>true</b> if the actor is a player. <b>false</b> otherwise.</returns>
    public static bool IsPlayer(this GameObject actor)
    {
        return actor.IsMage();
    }

    /// <summary>
    /// Gets the controller component for the given player.
    /// </summary>
    /// <param name="player">The player to be used to get the controller.</param>
    /// <returns>The controller component for the given the enemy. Retrns null if it is not a known player.</returns>
    public static IPlayerController GetPlayerControllerComponent(this GameObject player)
    {
        if (player.IsMage())
        {
            return player.GetComponent<MageController>();
        }

        return null;
    }
}
