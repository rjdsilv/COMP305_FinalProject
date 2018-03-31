using UnityEngine;
/// <summary>
/// Class responsible for dealing with enemy movement.
/// </summary>
public class EnemyMovement : ActorMovement
{
    // Public variable declaration.
    public RandomWalkAttributes randomWalkAttributes;       // The random walk attributes specification.

    // Private variable declaration.
    private float _secondsInSameDirection;                  // The maximum number of seconds the enemy will walk in the same direction before turning.
    private float _lastDirChangeTime;                       // The last time the direction was changed.
    private Vector2 _movementVector;                        // The movement vector to be used for the enemy movement.
    private EnemyVisionAI _visionAI;                        // The enemy's vision AI script.

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //// EVENT METHODS

    /// <summary>
    /// Initializes the enemy movment accordingly.
    /// </summary>
    private void Start()
    {
        _animator = GetComponent<Animator>();
        _visionAI = GetComponent<EnemyVisionAI>();
        _secondsInSameDirection = Random.Range(randomWalkAttributes.minTimeSameDir, randomWalkAttributes.maxTimeSameDir);
        movement.faceDirection = SceneData.shouldStop ? FaceDirection.LEFT : Mathf.FloorToInt(Random.Range(0, 3.9999999999f));
        _movementVector = GetDirectionVector();
    }

    /// <summary>
    /// Method that will control the enemy's movement.
    /// </summary>
    private void LateUpdate()
    {
        // The enemy will turn due to be near to an obstacle.
        if (_visionAI.IsSeeingObstacle())
        {
            TurnNow();
            _visionAI.NotSeeingObstacle();
        }
        // The enemy will turn due to timeframe constraints.
        else
        {
            TurnDueToTimeFrame();
        }

        movement.Move(gameObject, _animator, _movementVector.x, _movementVector.y, SceneData.shouldStop);
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //// NON EVENT METHODS

    /// <summary>
    /// Gets the ray direction based on the position the enemy is looking at.
    /// </summary>
    /// <returns>The direction to where the ray must by cast.</returns>
    public Vector2 GetDirectionVector()
    {
        switch (movement.faceDirection)
        {
            case FaceDirection.DOWN:
                return Vector2.down;

            case FaceDirection.UP:
                return Vector2.up;

            case FaceDirection.LEFT:
                return Vector2.left;

            case FaceDirection.RIGHT:
                return Vector2.right;
        }

        return Vector2.zero; ;
    }

    /// <summary>
    /// Turns the enemy due to the time elapsed.
    /// </summary>
    private void TurnDueToTimeFrame()
    {
        if (Time.time - _lastDirChangeTime >= _secondsInSameDirection)
        {
            TurnNow();
        }
    }

    /// <summary>
    /// Turns randomly the enemy.
    /// </summary>
    private void TurnNow()
    {
        float direction = Random.Range(-1, 1);

        if (movement.IsMovingHorizontally())
        {
            TurnVertical(direction);
        }
        else if (movement.IsMovingVerticaally())
        {
            TurnHorizontal(direction);
        }

        // If turned, update the time for not going turn crazy!
        _lastDirChangeTime = Time.time;
    }

    /// <summary>
    /// Turn the enemy to vertical direction.
    /// </summary>
    /// <param name="direction">The direction (up or down) to turn.</param>
    private void TurnVertical(float direction)
    {
        if (direction >= 0)
        {
            _movementVector = Vector2.up;
        }
        else
        {
            _movementVector = Vector2.down;
        }
    }

    /// <summary>
    /// Turn the enemy to horizontal direction.
    /// </summary>
    /// <param name="direction">The direction (left or right) to turn.</param>
    private void TurnHorizontal(float direction)
    {
        if (direction >= 0)
        {
            _movementVector = Vector2.right;
        }
        else
        {
            _movementVector = Vector2.left;
        }
    }
}
