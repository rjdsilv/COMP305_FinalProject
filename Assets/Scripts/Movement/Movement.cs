using UnityEngine;

/// <summary>
/// Scriptable object class that will hold values for charactere movement
/// </summary>
[CreateAssetMenu(menuName = "Miscelaneous/Movement")]
public class Movement : ScriptableObject
{
    public int faceDirection;  // The movement direction.
    public float speed;        // The movement speed.

    /// <summary>
    /// Method responsible for moving the actor based on the input provided by the user.
    /// </summary>
    /// <param name="actor">The charactere to move.</param>
    /// <param name="animator">The animator to set the properties in order to play the correct animation.</param>
    /// <param name="movementX">The movement on the X axis.</param>
    /// <param name="movementY">The movement on the Y axis.</param>
    /// <param name="isInBattle">Flag indicating whether the player is in battle or not.</param>
    public void Move(GameObject actor, Animator animator, float movementX, float movementY, bool isInBattle)
    {
        if (!isInBattle)
        {
            if (movementX != 0)
            {
                MoveHorizontally(actor, animator, movementX);
            }
            else if (movementY != 0)
            {
                MoveVertically(actor, animator, movementY);
            }
            else
            {
                Stop(actor, animator);
            }
        }
    }

    /// <summary>
    /// Moves the actor horizontally
    /// </summary>
    /// <param name="actor">The player to be moved</param>
    /// <param name="animator">The animator to play animations</param>
    /// <param name="movementX">The movement on the X axis.</param>
    private void MoveHorizontally(GameObject actor, Animator animator, float movementX)
    {
        if (movementX > 0)
        {
            actor.GetComponent<Rigidbody2D>().velocity = Vector2.right * speed;
            faceDirection = FaceDirection.RIGHT;
            animator.Play(AnimatorUtils.WALK_RIGHT);
        }
        else if (movementX < 0)
        {
            actor.GetComponent<Rigidbody2D>().velocity = Vector2.left * speed;
            faceDirection = FaceDirection.LEFT;
            animator.Play(AnimatorUtils.WALK_LEFT);
        }
    }

    /// <summary>
    /// Moves the actor vertivally
    /// </summary>
    /// <param name="actor">The actor to be moved</param>
    /// <param name="animator">The animator to play animations</param>
    /// <param name="movementY">The movement on the Y axis.</param>
    private void MoveVertically(GameObject actor, Animator animator, float movementY)
    {
        if (movementY > 0)
        {
            actor.GetComponent<Rigidbody2D>().velocity = Vector2.up * speed;
            faceDirection = FaceDirection.UP;
            animator.Play(AnimatorUtils.WALK_UP);
        }
        else if (movementY < 0)
        {
            actor.GetComponent<Rigidbody2D>().velocity = Vector2.down * speed;
            faceDirection = FaceDirection.DOWN;
            animator.Play(AnimatorUtils.WALK_DOWN);
        }
    }

    /// <summary>
    /// Stops the actor movement.
    /// </summary>
    /// <param name="actor">The actor to be moved</param>
    /// <param name="animator">The animator to play animations</param>
    private void Stop(GameObject actor, Animator animator)
    {
        actor.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        switch (faceDirection)
        {
            case FaceDirection.RIGHT:
                animator.Play(AnimatorUtils.STAND_RIGHT);
                break;

            case FaceDirection.DOWN:
                animator.Play(AnimatorUtils.STAND_DOWN);
                break;

            case FaceDirection.LEFT:
                animator.Play(AnimatorUtils.STAND_LEFT);
                break;

            case FaceDirection.UP:
                animator.Play(AnimatorUtils.STAND_UP);
                break;
        }
    }
}
