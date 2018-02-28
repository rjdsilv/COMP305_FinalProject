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

    /// <summary>
    /// Initializes all the necessary properties for the 
    /// </summary>
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    /// <summary>
    /// Method that will control the player's movement.
    /// </summary>
    void FixedUpdate()
    {
        movement.Move(gameObject, _animator, ControlUtils.Horizontal(), ControlUtils.Vertical(), false);
    }
}