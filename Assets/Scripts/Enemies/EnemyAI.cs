using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    // Public variable declaration.
    public float viewAngle;      // The maximum angle this enemy can see.
    public float turnDistance;   // The distance that will make the enemy turn when seeing a scenario object.
    public float minTimeSameDir; // Minimum range time before turning to another direcion.
    public float maxTimeSameDir; // Maximum range time before turning to another direcion.

    // Private constants declaration.
    private const int EYE_IDX = 0;

    // Private variables.
    private Transform _eye;                   // The enemy's eye.
    private EnemyController _enemyController; // The script responsible for moving and animating the enemy.
    private bool _isSeeingPlayer;             // Flag indicating if the enemy can actualy see the player.
    private bool _isSeeingObstacle;           // Indicates if the enemy is seeing an objstacle and must turn.
    private float _secondsInSameDirection;    // The maximum number of seconds the enemy will walk in the same direction before turning.
    private float _lastDirChangeTime;         // The last time the direction was changed.

    /// <summary>
    /// Initializes all the necessary variables for the AI to work properly.
    /// </summary>
    void Start()
    {
        _eye = transform.GetChild(EYE_IDX);
        _enemyController = GetComponent<EnemyController>();
        _secondsInSameDirection = Random.Range(minTimeSameDir, maxTimeSameDir);
        _lastDirChangeTime = 0;
    }

    /// <summary>
    /// This method will run after update and fixed update, so it guarantees that the player already moved
    /// when executing it, what implies that the frame will be in enemy's view or not.
    /// </summary>
    void LateUpdate()
    {
        if (_isSeeingPlayer)
        {
            Debug.Log("YESSS!!! I can see the Player!!!");
        }
        else
        {   
            Debug.Log("NOOOO!!! I cannot see the Player!!!");
        }

        if (_isSeeingObstacle)
        {
            TurnDueToScenarioObjectFound();
            _isSeeingObstacle = false;
        }
        else
        {
            TurnDueToTimeFrame();
        }
    }

    /// <summary>
    /// Method called whenever a trigger collision is detected.
    /// </summary>
    /// <param name="detectedObject">The object detected.</param>
    void OnTriggerStay2D(Collider2D detectedObject)
    {
        WhatAmISeeing(detectedObject);
    }

    /// <summary>
    /// Method called whenever the collision trigger finishes.
    /// </summary>
    /// <param name="collision">The object that left the colision area.</param>
    void OnTriggerExit2D(Collider2D collision)
    {
        if (IsPlayer(collision))
        {
            _isSeeingPlayer = false;
        }
    }

    /// <summary>
    /// Method called to decide if the enemy can see the player.
    /// </summary>
    /// <param name="detectedObject">The player to be checked</param>
    /// <returns><b>true</b> if the player can be seen. <b>false</b> otherwise.</returns>
    void WhatAmISeeing(Collider2D detectedObject)
    {
        // Player is in the enemy's range of detection.
        if (IsPlayer(detectedObject))
        {
            // Player is within the enemy's line of sight.
            if (CalculateAngleToPlayer(detectedObject) <= viewAngle)
            {
                // Checks if anything is blocking the enemy line of view.
                _isSeeingPlayer = !HasObjectsBlockingView(detectedObject);
            }
            else
            {
                _isSeeingPlayer = false;
            }
        }

        if (IsScenarioObjectOrSectorEdge(detectedObject.transform))
        {
            Vector3 ep = _eye.transform.position;
            Vector3 dp = detectedObject.transform.position;
            RaycastHit2D[] hits = _enemyController.RaycastAll(turnDistance);

            foreach (RaycastHit2D hit in hits)
            {
                if (hit.transform.tag == "Enemy") continue;
                if (IsScenarioObjectOrSectorEdge(hit.transform))
                {
                    _isSeeingObstacle = true;
                }
            }
        }
    }

    private void TurnDueToTimeFrame()
    {
        if (Time.time - _lastDirChangeTime >= _secondsInSameDirection)
        {
            TurnNow();
        }
    }

    private void TurnDueToScenarioObjectFound()
    {
        TurnNow();
    }

    private void TurnNow()
    {
        float direction = Random.Range(-1, 1);

        if (IsMovingHorizontally())
        {
            TurnVertical(direction);
        }
        else if (IsMovingVertically())
        {
            TurnHorizontal(direction);
        }

        // If turned, update the time for not going turn crazy!
        _lastDirChangeTime = Time.time;
    }

    private bool IsMovingVertically()
    {
        return _enemyController.IsMovingUp() || _enemyController.IsMovingDown();
    }

    private bool IsMovingHorizontally()
    {
        return _enemyController.IsMovingLeft() || _enemyController.IsMovingRight();
    }

    private void TurnVertical(float direction)
    {
        if (direction >= 0)
        {
            _enemyController.MoveUp();
        }
        else
        {
            _enemyController.MoveDown();
        }
    }

    private void TurnHorizontal(float direction)
    {
        if (direction >= 0)
        {
            _enemyController.MoveRight();
        }
        else
        {
            _enemyController.MoveLeft();
        }
    }


    /// <summary>
    /// Method that indicates if the object in question is a player.
    /// </summary>
    /// <param name="detectedObject">The object to be checked.</param>
    /// <returns><b>true</b> if the object detected is the player. <b>false</b> otherwise.</returns>
    bool IsPlayer(Collider2D detectedObject)
    {
        return detectedObject.tag == "Player";
    }

    /// <summary>
    /// Method to verify if the object seen by the enemy is a scenario object or a sector edge.
    /// </summary>
    /// <param name="detectedObject">The object detected by the enemy.</param>
    /// <returns><b>true</b> if the object is either a scenario object or a sector edge. <b>false</b>otherwise.</returns>
    bool IsScenarioObjectOrSectorEdge(Transform detectedObject)
    {
        return detectedObject.tag == "ScenarioObject" || detectedObject.tag == "SectorEdge";
    }

    /// <summary>
    /// This method calculates the angle between the player and the ortogonal line of sight from the enemy's eye.
    /// </summary>
    /// <param name="player">The player object</param>
    /// <returns>The calculated angle.</returns>
    float CalculateAngleToPlayer(Collider2D player)
    {
        Vector2 directionToPlayer = player.transform.position - transform.position;
        Vector2 lineOfSight = _enemyController.GetLineOfSight().position - transform.position;
        return Vector2.Angle(directionToPlayer, lineOfSight);
    }

    /// <summary>
    /// This method will check if there is any object blocking the view from the enemy to the player.
    /// </summary>
    /// <param name="player">The object being seen.</param>
    /// <returns><b>true</b> if there is any object blocking the line of view to the player. <b>false</b> otherwise.</returns>
    bool HasObjectsBlockingView(Collider2D player)
    {
        Vector2 eyePosition = _eye.transform.position;
        Vector2 playerPosition = player.transform.position;

        // Sends an imaginary beam from the enemy to the player.
        RaycastHit2D[] allHits = Physics2D.RaycastAll(eyePosition, playerPosition - eyePosition, Vector2.Distance(eyePosition, playerPosition));

        foreach (RaycastHit2D hit in allHits)
        {
            if (hit.collider != null)
            {
                // Hit an enemy, continue looking.
                if (hit.transform.tag == "Enemy") continue;

                // Hit anything that is not the player, so the line of view is blocked.
                if (hit.transform.tag != "Player") return true;
            }
        }

        // Nothing blocking the line of view.
        return false;
    }
}
