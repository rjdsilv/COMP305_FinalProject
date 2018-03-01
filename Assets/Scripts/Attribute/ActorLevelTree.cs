using UnityEngine;

/// <summary>
/// Class responsible for managing the level tree for all any actor on the system.
/// </summary>
/// <typeparam name="A">The actor attributes.</typeparam>
public class ActorLevelTree<A> : LevelTree<A>
    where A : ActorAttributes
{
}
