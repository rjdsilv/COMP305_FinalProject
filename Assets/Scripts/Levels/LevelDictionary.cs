using System.Collections.Generic;

/// <summary>
/// Class that will store the attributes for each player / enemy based on its current level.
/// </summary>
public class LevelDictionary
{
    private const int MIN_LEVEL = 1;
    private const int MAX_LEVEL = 30;

    // The dictionary containing the values for the each attribute based on the character level.
    private IDictionary<int, LevelAttributes> levelAttributeDict = new Dictionary<int, LevelAttributes>();

    /// <summary>
    /// Gets the attribute values for the given level.
    /// </summary>
    /// <param name="level">The level to be used.</param>
    /// <returns>The attributes for the given level or the attributes for the first level in case the level is not on the dictionary.</returns>
    public LevelAttributes GetAttributeForLevel(int level)
    {
        if (!levelAttributeDict.ContainsKey(level))
        {
            return levelAttributeDict[1];
        }

        return levelAttributeDict[level];
    }

    /// <summary>
    /// Add a new attribute for the given level.
    /// </summary>
    /// <param name="level">The level to have the attribute added.</param>
    /// <param name="attributes">The attributes to add.</param>
    public void AddAttributesForLevel(int level, LevelAttributes attributes)
    {
        int fixedLevel = ClampLevel(level);

        if (!levelAttributeDict.ContainsKey(fixedLevel))
        {
            levelAttributeDict.Add(fixedLevel, attributes);
        }
        else
        {
            levelAttributeDict[fixedLevel] = attributes;
        }
    }

    /// <summary>
    /// Clampses the given level based on the current MIN_LEVEL and MAX_LEVEL
    /// </summary>
    /// <param name="level"></param>
    /// <returns></returns>
    private int ClampLevel(int level)
    {
        if (level < MIN_LEVEL)
        {
            return MIN_LEVEL;
        }
        else if (level > MAX_LEVEL)
        {
            return MAX_LEVEL;
        }

        return level;
    }
}