using UnityEngine;

/// <summary>
/// This class will be responsible for controlling the player.
/// </summary>
public class PlayerController : MonoBehaviour
{
    // Public properties declaration.
    public float speed;
    public GameObject player;

    // Private properties declaration.
    private PlayerAnimationState _currAnimState;
    private Animator _animator;
    private Rigidbody2D _rigidBody;

    /// <summary>
    /// Method being used for the player movement initialization.
    /// </summary>
    void Start ()
    {
        // Initalize the player objects.
        _animator = GetComponent<Animator>();
        _rigidBody = GetComponent<Rigidbody2D>();

        // The player starts looking to the right.
        _currAnimState = PlayerAnimationState.STAND_RIGHT;

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

    void ChangePlayerAnimation(float hMove, float vMove)
    {
        if (hMove > 0)
        {
            _currAnimState = PlayerAnimationState.WALKING_RIGHT;
        }
        else if (hMove < 0)
        {
            _currAnimState = PlayerAnimationState.WALKING_LEFT;
        }
        else if (vMove > 0)
        {
            _currAnimState = PlayerAnimationState.WALKING_BACK;
        }
        else if (vMove < 0)
        {
            _currAnimState = PlayerAnimationState.WALKING_FRONT;
        }
        else
        {
            switch (_currAnimState.FaceDirection)
            {
                case PlayerAnimationState.Direction.FACE_RIGHT:
                    _currAnimState = PlayerAnimationState.STAND_RIGHT;
                    break;

                case PlayerAnimationState.Direction.FACE_FRONT:
                    _currAnimState = PlayerAnimationState.STAND_FRONT;
                    break;

                case PlayerAnimationState.Direction.FACE_LEFT:
                    _currAnimState = PlayerAnimationState.STAND_LEFT;
                    break;

                case PlayerAnimationState.Direction.FACE_BACK:
                    _currAnimState = PlayerAnimationState.STAND_BACK;
                    break;
            }
        }
        _animator.Play(_currAnimState.AnimationName);
    }
}
