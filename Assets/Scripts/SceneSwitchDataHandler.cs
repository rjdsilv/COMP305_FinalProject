using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that will store all the necessary data from transitioning between the original scene and a battle scene.
/// </summary>
public class SceneSwitchDataHandler
{
    // The players on the scene
    private static IDictionary<string, PlayerHolder> players = new Dictionary<string, PlayerHolder>();

    public static bool isComingBackFromBattle = false;
    public static List<EnemyHolder> enemiesInBattle;
    public static List<EnemyHolder> enemiesNotInBattle;
    public static List<EnemyHolder> enemiesIndestructible = new List<EnemyHolder>(); 

    public static void AddPlayer(string name, Vector3 position, PlayerAttributes attributes)
    {
        if (!players.ContainsKey(name))
        {
            players.Add(name, new PlayerHolder(position, attributes));
        }
        else
        {
            players[name].Position = position;
            players[name].Attributes = attributes;
        }
    }

    public static PlayerHolder GetPlayer(string name)
    {
        if (players.ContainsKey(name))
        {
            return players[name];
        }

        return null;
    }
}
