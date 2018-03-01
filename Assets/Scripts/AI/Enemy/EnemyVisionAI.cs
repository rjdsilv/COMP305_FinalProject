using UnityEngine;

public class EnemyVisionAI : VisionAI
{
    // Private variable declaration.
    private bool _isSeeingPlayer;
    private bool _isSeeingObstacle;

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //// EVENT METHODS

    /// <summary>
    /// Initializes all the necessary variables.
    /// </summary>
    private void Start()
    {
        GetComponent<CircleCollider2D>().radius = visionAttributes.viewDistance;
        _isSeeingPlayer = false;
        _isSeeingObstacle = false;
        _eye = transform.GetChild(EYE_IDX);
        _movement = GetComponent<EnemyMovemet>().movement;
    }

    /// <summary>
    /// This method will be executed when some object on the game enters the AI view circle area.
    /// </summary>
    /// <param name="other">The other object that triggered the event.</param>
    private void OnTriggerStay2D(Collider2D other)
    {
        See(other.transform);
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //// NON EVENT METHODS

    /// <summary>
    /// Indicates whether the enemy is seeing an obstacle or not.
    /// </summary>
    /// <returns><b>true</b> if the enemy is seeing an obstacle. <b>false</b> otherwise.</returns>
    public bool IsSeeingObstacle()
    {
        return _isSeeingObstacle;
    }

    /// <summary>
    /// <see cref="VisionAI" />
    /// </summary>
    protected override void See(Transform other)
    {
        if (TagUtils.IsPlayer(other))
        {
            SeePlayer(other);
        }
        // Checking if should turn due to an obstacle ahead.
        else if (TagUtils.IsScenarioObject(other) || TagUtils.IsSectorEdge(other))
        {
            SeeObstacle();
        }
    }

    /// <summary>
    /// Method in charge of evaluating if the enemy can see the player.
    /// </summary>
    /// <param name="player">The player to check</param>
    private void SeePlayer(Transform player)
    {
        if (CalculateAngleToActor(player) <= visionAttributes.viewingAngle)
        {
            // Checks if anything is blocking the enemy line of view.
            _isSeeingPlayer = !HasObjectsBlockingView(player);
        }
        else
        {
            // Don't see the player.
            _isSeeingPlayer = false;
        }
    }

    /// <summary>
    /// Method in charge of evaluating if the enemy can see any obstacle.
    /// </summary>
    private void SeeObstacle()
    {
        foreach (RaycastHit2D hit in Physics2D.RaycastAll(_eye.position, GetRayDirection(), GetComponent<EnemyMovemet>().randomWalkAttributes.turnDistance))
        {
            if (TagUtils.IsEnemy(hit.transform)) continue;
            if (TagUtils.IsScenarioObject(hit.transform) || TagUtils.IsSectorEdge(hit.transform))
            {
                _isSeeingObstacle = true;
            }
            else
            {
                _isSeeingObstacle = false;
            }
        }
    }

    /// <summary>
    /// This method calculates the angle between the actor and the ortogonal line of sight from the enemy's eye.
    /// </summary>
    /// <param name="actor">The actor being saw.</param>
    /// <returns>The calculated angle.</returns>
    private float CalculateAngleToActor(Transform actor)
    {
        Vector2 directionToActor = actor.position - transform.position;
        Vector2 lineOfSight = GetLineOfSight().position - transform.position;
        return Vector2.Angle(directionToActor, lineOfSight);
    }

    /// <summary>
    /// This method will check if there is any object blocking the view from the enemy to the player.
    /// </summary>
    /// <param name="other">The object being seen.</param>
    /// <returns><b>true</b> if there is any object blocking the line of view to the player. <b>false</b> otherwise.</returns>
    bool HasObjectsBlockingView(Transform other)
    {
        // Sends an imaginary beam from the enemy to the player.
        foreach (RaycastHit2D hit in Physics2D.RaycastAll(_eye.position, other.position - _eye.position, Vector2.Distance(_eye.position, other.position)))
        {
            if (hit.collider != null)
            {
                // Hit an enemy or the camera, continue looking.
                if (TagUtils.IsEnemy(hit.transform) || TagUtils.IsCamera(hit.transform)) continue;

                // Hit anything that is not the player, so the line of view is blocked.
                if (!TagUtils.IsPlayer(hit.transform)) return true;
            }
        }

        // Nothing blocking the line of view.
        return false;
    }

    /// <summary>
    /// Gets the ray direction based on the position the enemy is looking at.
    /// </summary>
    /// <returns>The direction to where the ray must by cast.</returns>
    private Vector2 GetRayDirection()
    {
        switch (_movement.faceDirection)
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
}