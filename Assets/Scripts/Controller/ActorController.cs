using UnityEngine;

public abstract class ActorController<A, L> : MonoBehaviour
    where A : ActorAttributes
    where L : ActorLevelTree<A>
{
    [HideInInspector]
    public A attributes;    // The actor's attributes.
    public L levelTree;     // The actor's level tree.

    /// <summary>
    /// Initialize all the necessary variables.
    /// </summary>
    protected abstract void Init();
}
