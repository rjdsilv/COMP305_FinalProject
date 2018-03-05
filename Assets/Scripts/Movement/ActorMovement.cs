using UnityEngine;

/// <summary>
/// Generic abstract class to encapsulate shared movement behaviour
/// </summary>
public abstract class ActorMovement : MonoBehaviour
{
    // Public variable declaration.
    public Movement movement;           // The actors's movement.

    // Protected variable declaration.
    protected Animator _animator;       // The actor's animator.
}