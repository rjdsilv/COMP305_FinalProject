using UnityEngine;

/// <summary>
/// Utilitary class to deal with game enemies.
/// </summary>
public static class ActorUtils
{
    private const string MAGE = "Mage";     // The mage's prefab name
    private const string WOLF = "Wolf";     // The wolf's prefab name.

    /// <summary>
    /// Method to indicate whether a given actor is a mage or not.
    /// </summary>
    /// <param name="actor">The actor to be checked.</param>
    /// <returns><b>true</b> if the actor is a mage. <b>false</b> otherwise.</returns>
    public static bool IsMage(this GameObject actor)
    {
        return MAGE == actor.name;
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
    /// Method to check if a given actor is a Wolf.
    /// </summary>
    /// <param name="actor">The actor to be checked</param>
    /// <returns><b>true</b> if the actor is a wolf. <b>false</b> otherwise.</returns>
    public static bool IsWolf(this GameObject actor)
    {
        return actor.name == WOLF;
    }

    /// <summary>
    /// Method to check if a given actor belongs to the given sector.
    /// </summary>
    /// <param name="actor">The actor to be checked.</param>
    /// <param name="sectorName">The sector to be checked.</param>
    /// <returns><b>true</b> if the given actor belongs to the given sector. <b>false</b> otherwise.</returns>
    public static bool BelongsToSector(this GameObject actor, string sectorName)
    {
        if (actor.IsWolf())
        {
            return actor.GetComponent<WolfController>().SectorName == sectorName;
        }

        return false;
    }

    /// <summary>
    /// Sets the attributes for the given game actor.
    /// </summary>
    /// <param name="actor">The actor to have parameters set.x`</param>
    public static void SetEnemyControllerParameters(this GameObject actor, SectorAttributes attributes)
    {
        if (actor.IsWolf())
        {
            actor.GetComponent<WolfController>().SectorName = attributes.sectorName;
            actor.GetComponent<WolfController>().BattleScene = attributes.battleScene;
        }
    }

    /// <summary>
    /// Gets the controller component for the given actor.
    /// </summary>
    /// <param name="actor">The actor to be used to get the controller.</param>
    /// <returns>The controller component for the given the actor. Retrns null if it is not a known actor.</returns>
    public static IController GetControllerComponent(this GameObject actor)
    {
        if (actor.IsPlayer())
        {
            return actor.GetPlayerControllerComponent();
        }

        return actor.GetEnemyControllerComponent();
    }

    /// <summary>
    /// Gets the enemy controller component for the given actor.
    /// </summary>
    /// <param name="actor">The actor to be used to get the controller.</param>
    /// <returns>The controller component for the given the actor. Retrns null if it is not a known actor.</returns>
    public static IEnemyController GetEnemyControllerComponent(this GameObject actor)
    {
        if (actor.IsWolf())
        {
            return actor.GetComponent<WolfController>();
        }

        return null;
    }

    /// <summary>
    /// Gets the player controller component for the given actor.
    /// </summary>
    /// <param name="actor">The actor to be used to get the controller.</param>
    /// <returns>The controller component for the given the actor. Retrns null if it is not a known actor.</returns>
    private static IPlayerController GetPlayerControllerComponent(this GameObject actor)
    {
        if (actor.IsMage())
        {
            return actor.GetComponent<MageController>();
        }

        return null;
    }
}
