﻿using UnityEngine;

/// <summary>
/// Utilitary class to deal with game enemies.
/// </summary>
public static class ActorUtils
{
    public const string MAGE = "Mage";             // The mage's prefab name
    public const string THIEF = "Thief";           // The mage's prefab name
    public const string KNIGHT = "Knight";         // The mage's prefab name
    public const string CLERIC = "Cleric";         // The mage's prefab name

    private const string WOLF = "Wolf";            // The wolf's prefab name.
    private const string GOLEM = "Golem";          // The golem's prefab name.
    private const string ORC = "Orc";              // The golem's prefab name.
    private const string FINAL_BOSS = "FinalBoss"; // The golem's prefab name.

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
    /// Method to indicate whether a given actor is a thief or not.
    /// </summary>
    /// <param name="actor">The actor to be checked.</param>
    /// <returns><b>true</b> if the actor is a thief. <b>false</b> otherwise.</returns>
    public static bool IsThief(this GameObject actor)
    {
        return THIEF == actor.name;
    }

    /// <summary>
    /// Method to indicate whether a given actor is a knight or not.
    /// </summary>
    /// <param name="actor">The actor to be checked.</param>
    /// <returns><b>true</b> if the actor is a knight. <b>false</b> otherwise.</returns>
    public static bool IsKnight(this GameObject actor)
    {
        return KNIGHT == actor.name;
    }
    /// <summary>
    /// Method to indicate whether a given actor is a cleric or not.
    /// </summary>
    /// <param name="actor">The actor to be checked.</param>
    /// <returns><b>true</b> if the actor is a cleric. <b>false</b> otherwise.</returns>
    public static bool IsCleric(this GameObject actor)
    {
        return CLERIC == actor.name;
    }

    /// <summary>
    /// Method to evaluate if the given actor is a player.
    /// </summary>
    /// <param name="actor">The actor to be evaluated.</param>
    /// <returns><b>true</b> if the actor is a player. <b>false</b> otherwise.</returns>
    public static bool IsPlayer(this GameObject actor)
    {
        return actor.IsMage() || actor.IsThief() || actor.IsKnight() || actor.IsCleric();
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
    /// Method to check if a given actor is a Golem.
    /// </summary>
    /// <param name="actor">The actor to be checked</param>
    /// <returns><b>true</b> if the actor is a Golem. <b>false</b> otherwise.</returns>
    public static bool IsGolem(this GameObject actor)
    {
        return actor.name == GOLEM;
    }

    /// <summary>
    /// Method to check if a given actor is a Golem.
    /// </summary>
    /// <param name="actor">The actor to be checked</param>
    /// <returns><b>true</b> if the actor is an Orc. <b>false</b> otherwise.</returns>
    public static bool IsOrc(this GameObject actor)
    {
        return actor.name == ORC;
    }

    /// <summary>
    /// Method to check if a given actor is a Final Boss.
    /// </summary>
    /// <param name="actor">The actor to be checked</param>
    /// <returns><b>true</b> if the actor is a Final Boss. <b>false</b> otherwise.</returns>
    public static bool IsFinalBoss(this GameObject actor)
    {
        return actor.name == FINAL_BOSS;
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
        else if (actor.IsGolem())
        {
            return actor.GetComponent<GolemController>().SectorName == sectorName;
        }
        else if (actor.IsOrc())
        {
            return actor.GetComponent<OrcController>().SectorName == sectorName;
        }
        else if (actor.IsFinalBoss())
        {
            return actor.GetComponent<FinalBossController>().SectorName == sectorName;
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
            actor.GetComponent<WolfController>().MainScene = attributes.originalScene;
        }
        else if (actor.IsGolem())
        {
            actor.GetComponent<GolemController>().SectorName = attributes.sectorName;
            actor.GetComponent<GolemController>().BattleScene = attributes.battleScene;
            actor.GetComponent<GolemController>().MainScene = attributes.originalScene;
        }
        else if (actor.IsOrc())
        {
            actor.GetComponent<OrcController>().SectorName = attributes.sectorName;
            actor.GetComponent<OrcController>().BattleScene = attributes.battleScene;
            actor.GetComponent<OrcController>().MainScene = attributes.originalScene;
        }
        else if (actor.IsFinalBoss())
        {
            actor.GetComponent<FinalBossController>().SectorName = attributes.sectorName;
            actor.GetComponent<FinalBossController>().BattleScene = attributes.battleScene;
            actor.GetComponent<FinalBossController>().MainScene = attributes.originalScene;
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
        else if (actor.IsGolem())
        {
            return actor.GetComponent<GolemController>();
        }
        else if (actor.IsOrc())
        {
            return actor.GetComponent<OrcController>();
        }
        else if (actor.IsFinalBoss())
        {
            return actor.GetComponent<FinalBossController>();
        }

        return null;
    }

    /// <summary>
    /// Gets the player controller component for the given actor.
    /// </summary>
    /// <param name="actor">The actor to be used to get the controller.</param>
    /// <returns>The controller component for the given the actor. Retrns null if it is not a known actor.</returns>
    public static IPlayerController GetPlayerControllerComponent(this GameObject actor)
    {
        if (actor.IsMage())
        {
            return actor.GetComponent<MageController>();
        }
        else if (actor.IsThief())
        {
            return actor.GetComponent<ThiefController>();
        }
        // TODO Create the controllers for Knight and Cleric.

        return null;
    }
}
