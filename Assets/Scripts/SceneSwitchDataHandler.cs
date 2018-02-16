using System.Collections.Generic;
using UnityEngine;

public class SceneSwitchDataHandler
{
    public static bool isComingBackFromBattle = false;
    public static List<PlayerHolder> players = new List<PlayerHolder>();
    public static List<EnemyHolder> enemiesInBattle;
    public static List<EnemyHolder> enemiesNotInBattle;
    public static List<EnemyHolder> enemiesIndestructible = new List<EnemyHolder>();
}
