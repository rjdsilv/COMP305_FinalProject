using System;

public class LevelEntry
{
    public int Level { get; set; }

    public LevelAttributes Attributes { get; set; }

    public LevelEntry(int level, LevelAttributes attributes)
    {
        Level = level;
        Attributes = attributes;
    }
}
