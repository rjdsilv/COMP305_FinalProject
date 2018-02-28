using UnityEngine;

/// <summary>
/// Script class that will control the player movement
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    // Public variable declaration.
    public Movement movement;       // The player's movement.

    // Private variable declaration.
    private Animator _animator;     // The player's animator.

	/// <summary>
    /// Initializes all the necessary properties for the 
    /// </summary>
	void Start ()
    {
        _animator = GetComponent<Animator>();
	}
	
    /// <summary>
    /// Method that will control the player's movement.
    /// </summary>
	void FixedUpdate ()
    {
        movement.Move(gameObject, _animator, ControlUtils.Horizontal(), ControlUtils.Vertical());
	}
}
