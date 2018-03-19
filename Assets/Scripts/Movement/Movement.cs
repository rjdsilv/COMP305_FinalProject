using System;
using UnityEngine;

/// <summary>
/// Scriptable object class that will hold values for charactere movement
/// </summary>
[Serializable]
public class Movement
{
    // Public variable declaration.
    public int faceDirection;  // The movement direction.
    public float speed;        // The movement speed.

    // Protected variable declaration.
    protected string _currAnimation;

    /// <summary>
    /// Default constructor.
    /// </summary>
    public Movement() : base()
    {
        switch (faceDirection)
        {
            case FaceDirection.RIGHT:
                _currAnimation = AnimatorUtils.STAND_RIGHT;
                break;

            case FaceDirection.DOWN:
                _currAnimation = AnimatorUtils.STAND_DOWN;
                break;

            case FaceDirection.LEFT:
                _currAnimation = AnimatorUtils.STAND_LEFT;
                break;

            case FaceDirection.UP:
                _currAnimation = AnimatorUtils.STAND_UP;
                break;
        }
    }

    /// <summary>
    /// Method responsible for moving the actor based on the input provided by the user.
    /// </summary>
    /// <param name="actor">The charactere to move.</param>
    /// <param name="animator">The animator to set the properties in order to play the correct animation.</param>
    /// <param name="movementX">The movement on the X axis.</param>
    /// <param name="movementY">The movement on the Y axis.</param>
    /// <param name="shouldStop">Flag indicating whether the player is in battle or not.</param>
    public void Move(GameObject actor, Animator animator, float movementX, float movementY, bool shouldStop)
    {
        if (!shouldStop)
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
        else
        {
            Stop(actor, animator);
        }

        animator.Play(_currAnimation);
    }

    /// <summary>
    /// This method indicates if an actor is moving horizontally.
    /// </summary>
    /// <returns><b>true</b> if the actor is moving horizontally. <b>false</b> otherwise.</returns>
    public bool IsMovingHorizontally()
    {
        return IsWalkingLeft() || IsWalkingRight();
    }

    /// <summary>
    /// This method indicates if an actor is moving horizontally.
    /// </summary>
    /// <returns><b>true</b> if the actor is moving horizontally. <b>false</b> otherwise.</returns>
    public bool IsMovingVerticaally()
    {
        return IsWalkingUp() || IsWalkingDown();
    }

    /// <summary>
    /// This method indicates if an actor is moving to the left.
    /// </summary>
    /// <returns><b>true</b> if the actor is moving to the left. <b>false</b> otherwise.</returns>
    private bool IsWalkingLeft()
    {
        return _currAnimation == AnimatorUtils.WALK_LEFT;
    }

    /// <summary>
    /// This method indicates if an actor is moving upwards.
    /// </summary>
    /// <returns><b>true</b> if the actor is moving upwards. <b>false</b> otherwise.</returns>
    private bool IsWalkingUp()
    {
        return _currAnimation == AnimatorUtils.WALK_UP;
    }

    /// <summary>
    /// This method indicates if an actor is moving downwards.
    /// </summary>
    /// <returns><b>true</b> if the actor is moving downwards. <b>false</b> otherwise.</returns>
    private bool IsWalkingDown()
    {
        return _currAnimation == AnimatorUtils.WALK_DOWN;
    }

    /// <summary>
    /// This method indicates if an actor is moving to the right.
    /// </summary>
    /// <returns><b>true</b> if the actor is moving to the right. <b>false</b> otherwise.</returns>
    private bool IsWalkingRight()
    {
        return _currAnimation == AnimatorUtils.WALK_RIGHT;
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
            _currAnimation = AnimatorUtils.WALK_RIGHT;
        }
        else if (movementX < 0)
        {
            actor.GetComponent<Rigidbody2D>().velocity = Vector2.left * speed;
            faceDirection = FaceDirection.LEFT;
            _currAnimation = AnimatorUtils.WALK_LEFT;
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
            _currAnimation = AnimatorUtils.WALK_UP;
        }
        else if (movementY < 0)
        {
            actor.GetComponent<Rigidbody2D>().velocity = Vector2.down * speed;
            faceDirection = FaceDirection.DOWN;
            _currAnimation = AnimatorUtils.WALK_DOWN;
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
                _currAnimation = AnimatorUtils.STAND_RIGHT;
                break;

            case FaceDirection.DOWN:
                _currAnimation = AnimatorUtils.STAND_DOWN;
                break;

            case FaceDirection.LEFT:
                _currAnimation = AnimatorUtils.STAND_LEFT;
                break;

            case FaceDirection.UP:
                _currAnimation = AnimatorUtils.STAND_UP;
                break;
        }
    }
}
