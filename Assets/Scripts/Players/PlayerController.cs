using UnityEngine;

/// <summary>
/// This class will be responsible for controlling the player.
/// </summary>
public class PlayerController : MonoBehaviour
{
    // Public properties declaration.
    public float speed;

    // Private properties declaration.
    private AnimationState _currAnimState; // The player's current animation state.
    private Animator _animator;            // The player's rigid body.
    private Rigidbody2D _rigidBody;        // The player's animator.

    /// <summary>
    /// Method being used for the player movement initialization.
    /// </summary>
    void Start ()
    {
        // Initalize the player objects.
        _animator = GetComponent<Animator>();
        _rigidBody = GetComponent<Rigidbody2D>();

        // The player starts looking to the right.
        _currAnimState = AnimationState.STAND_RIGHT;

        // Starts the player animation looking to the right.
        _animator.Play(_currAnimState.AnimationName);
	}

    /// <summary>
    /// Method responsible for updating the each frame based on the user selected actions.
    /// </summary>
    void FixedUpdate()
    {
        MovePlayer();
    }

    /// <summary>
    /// Method responsible for moving the player to one specific direction.
    /// </summary>
    void MovePlayer()
    {
        float horizontalMovement = Input.GetAxis("KB_Horizontal");
        float verticalMovement = Input.GetAxis("KB_Vertical");
        _rigidBody.velocity = new Vector2(horizontalMovement, verticalMovement) * speed;
        ChangePlayerAnimation(horizontalMovement, verticalMovement);
    }

    /// <summary>
    /// Method responsible for changing the player animations based on the keys pressed and released.
    /// </summary>
    /// <param name="hMove">The value of the horizontal movement</param>
    /// <param name="vMove">The value of the vertical movement</param>
    void ChangePlayerAnimation(float hMove, float vMove)
    {
        // Movement animations.
        if (hMove > 0)
        {
            _currAnimState = AnimationState.WALKING_RIGHT;
        }
        else if (hMove < 0)
        {
            _currAnimState = AnimationState.WALKING_LEFT;
        }
        else if (vMove > 0)
        {
            _currAnimState = AnimationState.WALKING_UP;
        }
        else if (vMove < 0)
        {
            _currAnimState = AnimationState.WALKING_DOWN;
        }
        // Stand animations.
        else
        {
            switch (_currAnimState.FaceDirection)
            {
                case AnimationState.Direction.FACE_RIGHT:
                    _currAnimState = AnimationState.STAND_RIGHT;
                    break;

                case AnimationState.Direction.FACE_DOWN:
                    _currAnimState = AnimationState.STAND_DOWN;
                    break;

                case AnimationState.Direction.FACE_LEFT:
                    _currAnimState = AnimationState.STAND_LEFT;
                    break;

                case AnimationState.Direction.FACE_UP:
                    _currAnimState = AnimationState.STAND_UP;
                    break;
            }
        }

        // Plas the animation.
        _animator.Play(_currAnimState.AnimationName);
    }
}
