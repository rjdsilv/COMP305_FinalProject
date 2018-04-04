using UnityEngine;

/// <summary>
/// Generic abstract class to encapsulate shared movement behaviour
/// </summary>
public abstract class ActorMovement : MonoBehaviour
{
    // Public variable declaration.
    public Movement movement;           // The actors's movement.

    // Protected variable declaration.
    protected Animator _animator;       // The actor's animator.

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
    protected abstract void TurnNow();
 }