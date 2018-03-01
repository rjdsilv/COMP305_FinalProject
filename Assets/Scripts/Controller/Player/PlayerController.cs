using UnityEngine;

/// <summary>
/// Class responsible for controlling a non AI player.
/// </summary>
public abstract class PlayerController<A, L> : ActorController<A, L>
    where A : ActorAttributes
    where L : ActorLevelTree<A>
{
}
