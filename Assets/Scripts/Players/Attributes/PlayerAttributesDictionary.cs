using System.Collections.Generic;

public class PlayerAttributesDictionary
{
    private const string PLAYER_MAGE = "Player-Mage";

    private static IDictionary<string, List<LevelEntry>> enemyAttrDictionary = new Dictionary<string, List<LevelEntry>>();

    public static void InitializePlayerMage()
    {
        if (!enemyAttrDictionary.ContainsKey(PLAYER_MAGE))
        {
            enemyAttrDictionary.Add(PLAYER_MAGE, new List<LevelEntry>
            {
                new LevelEntry(1, new LevelAttributes
                {
                    MaxLife = 100,
                    MaxMana = 75,
                    MinAttackPower = 25,
                    MaxAttackPower = 50,
                    MinDefensePower = 15,
                    MaxDefensePower = 30
                }),
                new LevelEntry(2, new LevelAttributes
                {
                    MaxLife = 130,
                    MaxMana = 90,
                    MinAttackPower = 35,
                    MaxAttackPower = 70,
                    MinDefensePower = 25,
                    MaxDefensePower = 40
                })
            });
        }
    }

    public static List<LevelEntry> GetLevelEntryListForPlayer(string name)
    {
        return enemyAttrDictionary[name];
    }
}
