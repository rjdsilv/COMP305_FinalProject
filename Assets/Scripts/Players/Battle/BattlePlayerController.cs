﻿using System;
using UnityEngine;

/// <summary>
/// Class that will control the player actions on a battle scene.
/// </summary>
public class BattlePlayerController : MonoBehaviour
{
    private PlayerAttributes _attributes;  // The player's attributes.

    /// <summary>
    /// Initializes the player for being used on the battle. The attributes are initialized getting what was stored
    /// in the SceneSwitcherDataHandlerObject.
    /// </summary>
    public BattlePlayerController Initialize()
    {
        _attributes = SceneSwitchDataHandler.GetPlayer(transform.name).Attributes;
        return this;
    }

    /// <summary>
    /// Gets the player attributes to be used on the battle.
    /// </summary>
    /// <returns></returns>
    public PlayerAttributes GetAttributes()
    {
        return _attributes;
    }
}
