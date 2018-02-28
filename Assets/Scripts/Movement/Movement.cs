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
    /// Method responsible for moving the game object based on the input provided by the user.
    /// </summary>
    /// <param name="movable">The charactere to move.</param>
    /// <param name="animator">The animator to set the properties in order to play the correct animation.</param>
    public void Move(GameObject movable, Animator animator, float movementX, float movementY)
    {
        if (movementX != 0)
        {
            if (movementX > 0)
            {
                movable.GetComponent<Rigidbody2D>().velocity = Vector2.right * speed;
                faceDirection = FaceDirection.RIGHT;
                animator.Play(AnimatorUtils.WALK_RIGHT);
            }
            else if (movementX < 0)
            {
                movable.GetComponent<Rigidbody2D>().velocity = Vector2.left * speed;
                faceDirection = FaceDirection.LEFT;
                animator.Play(AnimatorUtils.WALK_LEFT);
            }
        }
        else if (movementY != 0)
        {
            if (movementY > 0)
            {
                movable.GetComponent<Rigidbody2D>().velocity = Vector2.up * speed;
                faceDirection = FaceDirection.UP;
                animator.Play(AnimatorUtils.WALK_UP);
            }
            else if (movementY < 0)
            {
                movable.GetComponent<Rigidbody2D>().velocity = Vector2.down * speed;
                faceDirection = FaceDirection.DOWN;
                animator.Play(AnimatorUtils.WALK_DOWN);
            }
        }
        else
        {
            movable.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
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
}
