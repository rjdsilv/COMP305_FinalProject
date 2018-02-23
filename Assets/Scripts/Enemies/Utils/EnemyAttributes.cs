using System;

/// <summary>
/// Class that will hold all the necessary enemy attributes.
/// </summary>
public class EnemyAttributes : CharacterAttributes
{
    public new int CurrentLevel
    {
        get
        {
            return _currentLevel;
        }

        set
        {
            _currentLevel = value;
        }
    }
}
