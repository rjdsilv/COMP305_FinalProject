using UnityEngine;

/// <summary>
/// Class resonsible for dealing with any level tree on the game.
/// </summary>
public class LevelTree<A> : ScriptableObject
{
    [SerializeField]
    protected int currentLevel;     // The mage's current level.

    [SerializeField]
    protected A[] levelAttributes;    // The level tree for the mage.

    /// <summary>
    /// Retrieves the attributes values for the current mage level.
    /// </summary>
    /// <returns>The attributes values for the current mage level.</returns>
    public A GetAttributesForCurrentLevel()
    {
        return levelAttributes[currentLevel - 1];
    }

    /// <summary>
    /// Increases the current level for the given level tree. As it is shared, when some actor for a given type
    /// increses its level, all the actor for that specific type will have their levels increased as well.
    /// </summary>
    public void IncreaseLevel()
    {
        currentLevel++;
    }

    /// <summary>
    /// Method to indicate if the level tree can still level up.
    /// </summary>
    /// <returns><b>true</b> if the level can be increased. <b>false</b> otherwise.</returns>
    public bool CanLevelUp()
    {
        return currentLevel < levelAttributes.Length;
    }
}
