using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    // Public variable declaration.
    public float ViewAngle; // The maximum angle this enemy can see.

    // Private constants declaration.
    private const int END_OF_LINE_IDX = 0;
    private const int EYE_IDX = 1;

    // Private variables.
    private Transform _endOfLineView; // The position where the enemy becomes blind.
    private Transform _eye;           // The enemy's eye.
    private bool _isSeeingPlayer;     // Flag indicating if the enemy can actualy see the player.

    /// <summary>
    /// Initializes all the necessary variables for the AI to work properly.
    /// </summary>
    void Start()
    {
        _endOfLineView = transform.GetChild(END_OF_LINE_IDX);
        _eye = transform.GetChild(EYE_IDX);
    }

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
    }

    /// <summary>
    /// Method called whenever a trigger collision is detected.
    /// </summary>
    /// <param name="detectedObject">The object detected.</param>
    void OnTriggerStay2D(Collider2D detectedObject)
    {
        // Player is in the enemy's range of detection.
        if (IsPlayer(detectedObject))
        {
            // Player is within the enemy's line of sight.
            if (CalculateAngleToPlayer(detectedObject) <= ViewAngle)
            {
                // Checks if anything is blocking the enemy line of view.
                _isSeeingPlayer = !HasObjectsBlockingView(detectedObject);
            }
            // Player out of the enemy's line of sight.
            else
            {
                _isSeeingPlayer = false;
            }
        }
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
            // Hit an enemy, continue looking.
            if (hit.transform.tag == "Enemy") continue;

            // Hit anything that is not the player, so the line of view is blocked.
            if (hit.transform.tag != "Player") return true;
        }

        // Nothing blocking the line of view.
        return false;
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
    /// This method calculates the angle between the player and the ortogonal line of sight from the enemy's eye.
    /// </summary>
    /// <param name="player">The player object</param>
    /// <returns>The calculated angle.</returns>
    float CalculateAngleToPlayer(Collider2D player)
    {
        Vector2 directionToPlayer = player.transform.position - transform.position;
        Vector2 lineOfSight = _endOfLineView.position - transform.position;
        return Vector2.Angle(directionToPlayer, lineOfSight);
    }
}
