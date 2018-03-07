using UnityEngine;

/// <summary>
/// Class containing all the vision AI commom methods and attributes.
/// </summary>
public abstract class VisionAI : GenericAI
{
    // Constant declaration.
    protected const int EYE_IDX = 0;
    protected const int VIEW_RIGHT_IDX = 1;
    protected const int VIEW_DOWN_IDX = 2;
    protected const int VIEW_LEFT_IDX = 3;
    protected const int VIEW_UP_IDX = 4;

    // Public variable declaration.
    public VisionAttributes visionAttributes;

    // Protected variable declaration.
    protected Transform _eye;               // The actor's eye.
    protected EnemyMovement _enemyMovement;           // The actor's movement attributes.

    /// <summary>
    /// Execute the logic for when some other object enters the field of view.
    /// </summary>
    /// <param name="other">The object that entered the field of view.</param>
    protected abstract void See(Transform other);

    /// <summary>
    /// Method responsible for returning the correct line of sight according to the direction
    /// the actor is looking at.
    /// </summary>
    /// <returns>The correct line of sight.</returns>
    protected Transform GetLineOfSight()
    {
        switch (_enemyMovement.movement.faceDirection)
        {
            case FaceDirection.RIGHT:
                return transform.GetChild(VIEW_RIGHT_IDX);

            case FaceDirection.DOWN:
                return transform.GetChild(VIEW_DOWN_IDX);

            case FaceDirection.LEFT:
                return transform.GetChild(VIEW_LEFT_IDX);

            case FaceDirection.UP:
                return transform.GetChild(VIEW_UP_IDX);
        }

        return _eye;
    }
}
