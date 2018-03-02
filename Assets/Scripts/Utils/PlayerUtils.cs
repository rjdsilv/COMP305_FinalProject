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
    public static bool IsMage(GameObject player)
    {
        return MAGE == player.name;
    }
}
