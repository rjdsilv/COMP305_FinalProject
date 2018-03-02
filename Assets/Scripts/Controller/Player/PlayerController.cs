/// <summary>
/// Class responsible for controlling a non AI player.
/// </summary>
public abstract class PlayerController<A, L> : ActorController<A, L>, IPlayerController
    where A : ActorAttributes
    where L : ActorLevelTree<A>
{
    /// <see cref="IPlayerController"/>
    public bool IsManagedByAI()
    {
        return attributes.managedByAI;
    }
}
