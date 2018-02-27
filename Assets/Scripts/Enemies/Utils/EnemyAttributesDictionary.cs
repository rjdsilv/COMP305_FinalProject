using System.Collections.Generic;

public class EnemyAttributesDictionary
{
    private const string ENEMY_WOLF = "Enemy-Wolf";

    private static IDictionary<string, List<LevelEntry>> enemyAttrDictionary = new Dictionary<string, List<LevelEntry>>();

    public static void InitializeEnemyWolf()
    {
        if (!enemyAttrDictionary.ContainsKey(ENEMY_WOLF))
        { 
            enemyAttrDictionary.Add(ENEMY_WOLF, new List<LevelEntry>
            {
                new LevelEntry(1, new LevelAttributes
                {
                    MaxLife = 90,
                    MaxStamina = 50,
                    MinAttackPower = 20,
                    MaxAttackPower = 40,
                    MinDefensePower = 10,
                    MaxDefensePower = 20
                }),
                new LevelEntry(2, new LevelAttributes
                {
                    MaxLife = 105,
                    MaxStamina = 65,
                    MinAttackPower = 30,
                    MaxAttackPower = 50,
                    MinDefensePower = 15,
                    MaxDefensePower = 30
                })
            });
        }
    }

    public static List<LevelEntry> GetLevelEntryListForEnemy(string name)
    {
        return enemyAttrDictionary[name];
    }
}
