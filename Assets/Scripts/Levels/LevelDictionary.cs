using System;
using System.Collections.Generic;

/// <summary>
/// Class that will store the attributes for each player / enemy based on its current level.
/// </summary>
public class LevelDictionary
{
    private const int MIN_LEVEL = 1;
    private const int MAX_LEVEL = 30;

    // The dictionary containing the values for the each attribute based on the character level.
    public List<LevelEntry> LevelList { get; set; }

    /// <summary>
    /// Creates a new instance of LevelDictionary.
    /// </summary>
    public LevelDictionary()
    {
        LevelList = new List<LevelEntry>();
    }

    /// <summary>
    /// Gets the attribute values for the given level.
    /// </summary>
    /// <param name="level">The level to be used.</param>
    /// <returns>The attributes for the given level or the attributes for the first level in case the level is not on the dictionary.</returns>
    public LevelAttributes GetLevelAttributes(int level)
    {
        return LevelList.Find(e => e.Level == level).Attributes;
    }

    /// <summary>
    /// Adds a new entry of attributes for the given level.
    /// </summary>
    /// <param name="level">The level to be added.</param>
    /// <param name="attributes">The atributes to be added.</param>
    public void AddEntry(int level, LevelAttributes attributes)
    {
        LevelEntry entry = new LevelEntry(level, attributes);
        LevelList.Add(entry);
    }
}