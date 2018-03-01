using UnityEngine;

/// <summary>
/// Class responsible for managing the level tree for all any actor on the system.
/// </summary>
/// <typeparam name="A"></typeparam>
public class ActorLevelTree<A> : ScriptableObject
    where A : ActorAttributes
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
}
