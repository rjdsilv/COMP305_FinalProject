using System;
using UnityEngine;

/// <summary>
/// Class that will manage players on the game in order say if the player will be controlled by an AI or not
/// </summary>
[Serializable]
public class PlayerAIManager
{
    public GameObject player;
    public bool managedByAI;
}
