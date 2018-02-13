using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Public properties declaration.
    public float speed;

    // Private constants declaration.
    private const int EYE_IDX = 0;
    private const int LINE_OF_VIEW_LEFT_IDX = 1;
    private const int LINE_OF_VIEW_UP_IDX = 2;
    private const int LINE_OF_VIEW_RIGHT_IDX = 3;
    private const int LINE_OF_VIEW_DOWN_IDX = 4;

    // Private properties declaration.
    private AnimationState _currAnimState; // The enemy's current animation state.
    private Rigidbody2D _rigidBody;        // The enemy's rigid body.
    private Animator _animator;            // The enemy's animator.
    private Transform _eye;                // The enemy's eye.


    // Private Static declrations.
    private static AnimationState WALKING_LEFT = AnimationState.WALKING_LEFT;
    private static AnimationState WALKING_RIGHT = AnimationState.WALKING_RIGHT;
    private static AnimationState WALKING_UP = AnimationState.WALKING_UP;
    private static AnimationState WALKING_DOWN = AnimationState.WALKING_DOWN;

    /// <summary>
    /// Method responsible for starting all the neceaary variables.
    /// </summary>
    void Start ()
    {
        // Initalize the player objects.
        _eye = transform.GetChild(EYE_IDX);
        _rigidBody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        StartRandomMovement();
    }

    /// <summary>
    /// Checks if the enemy is moving to the left.
    /// </summary>
    /// <returns><b>true</b> if the enemy is moving to the left. <b>false</b> otherwise.</returns>
    public bool IsMovingLeft()
    {
        return _currAnimState == WALKING_LEFT;
    }

    /// <summary>
    /// Checks if the enemy is moving to the right.
    /// </summary>
    /// <returns><b>true</b> if the enemy is moving to the right. <b>false</b> otherwise.</returns>
    public bool IsMovingRight()
    {
        return _currAnimState == WALKING_RIGHT;
    }

    /// <summary>
    /// Checks if the enemy is moving to up.
    /// </summary>
    /// <returns><b>true</b> if the enemy is moving to up. <b>false</b> otherwise.</returns>
    public bool IsMovingUp()
    {
        return _currAnimState == WALKING_UP;
    }

    /// <summary>
    /// Checks if the enemy is moving to down.
    /// </summary>
    /// <returns><b>true</b> if the enemy is moving to down. <b>false</b> otherwise.</returns>
    public bool IsMovingDown()
    {
        return _currAnimState == WALKING_DOWN;
    }

    /// <summary>
    /// Moves the enemy to the left and sets the current animation to the corresponding one.
    /// </summary>
    public void MoveLeft()
    {
        _currAnimState = WALKING_LEFT;
        _rigidBody.velocity = Vector2.left * speed;
        _animator.Play(_currAnimState.AnimationName);
    }

    /// <summary>
    /// Moves the enemy to the right and sets the current animation to the corresponding one.
    /// </summary>
    public void MoveRight()
    {
        _currAnimState = WALKING_RIGHT;
        _rigidBody.velocity = Vector2.right * speed;
        _animator.Play(_currAnimState.AnimationName);
    }

    /// <summary>
    /// Moves the enemy up and sets the current animation to the corresponding one.
    /// </summary>
    public void MoveUp()
    {
        _currAnimState = WALKING_UP;
        _rigidBody.velocity = Vector2.up * speed;
        _animator.Play(_currAnimState.AnimationName);
    }

    /// <summary>
    /// Moves the enemy down and sets the current animation to the corresponding one.
    /// </summary>
    public void MoveDown()
    {
        _currAnimState = WALKING_DOWN;
        _rigidBody.velocity = Vector2.down * speed;
        _animator.Play(_currAnimState.AnimationName);
    }

    /// <summary>
    /// Stops the enemy in the correct position and sets the animation to the corresponding one.
    /// </summary>
    public void Stop()
    {
        switch(_currAnimState.FaceDirection)
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
        _rigidBody.velocity = new Vector2();
        _animator.Play(_currAnimState.AnimationName);
    }

    /// <summary>
    /// Sends a raycast and gets all the objects hit by it.
    /// </summary>
    /// <param name="distance">The maximum raycast distance.</param>
    /// <returns>All the colliders hit by the ray</returns>
    public RaycastHit2D[] RaycastAll(float distance)
    {
        return Physics2D.RaycastAll(_eye.position, GetRayDirection(), distance);
    }

    /// <summary>
    /// Based on the position the enemy is looking at, use a different line of view.
    /// </summary>
    /// <returns>The correct line of view for the enemy.</returns>
    public Transform GetLineOfSight()
    {
        switch (_currAnimState.FaceDirection)
        {
            case AnimationState.Direction.FACE_RIGHT:
                return transform.GetChild(LINE_OF_VIEW_RIGHT_IDX);

            case AnimationState.Direction.FACE_DOWN:
                return transform.GetChild(LINE_OF_VIEW_DOWN_IDX);

            case AnimationState.Direction.FACE_LEFT:
                return transform.GetChild(LINE_OF_VIEW_LEFT_IDX);

            case AnimationState.Direction.FACE_UP:
                return transform.GetChild(LINE_OF_VIEW_UP_IDX);
        }

        return null;
    }

    /// <summary>
    /// Gets the ray direction based on the position the enemy is looking at.
    /// </summary>
    /// <returns>The direction to where the ray must by cast.</returns>
    private Vector2 GetRayDirection()
    {
        switch(_currAnimState.FaceDirection)
        {
            case AnimationState.Direction.FACE_DOWN:
                return Vector2.down;

            case AnimationState.Direction.FACE_UP:
                return Vector2.up;

            case AnimationState.Direction.FACE_LEFT:
                return Vector2.left;

            case AnimationState.Direction.FACE_RIGHT:
                return Vector2.right;
        }

        return new Vector2();
    }

    /// <summary>
    /// Starts the enemy movement in a random direction based on the number generated.
    /// </summary>
    private void StartRandomMovement()
    {
        float direction = Random.Range(0, 4);

        if (direction >= 0 && direction < 1)
        {
            MoveLeft();
        }
        else if (direction >= 1 && direction < 2)
        {
            MoveUp();
        }
        else if (direction >= 2 && direction < 3)
        {
            MoveRight();
        }
        else if (direction >= 3 && direction <= 4)
        {
            MoveDown();
        }
    }
}
