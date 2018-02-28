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
            if (CalculateAngleToActor(other) <= visionAttributes.viewingAngle)
            {
                _isSeeingPlayer = true;
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
}
