using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBossMovement : ActorMovement
{
    public Vector2 leftChangeDirPoint;
    public Vector2 rightChangeDirPoint;

    private Vector2 _movementVector;

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //// EVENT METHODS

    /// <summary>
    /// Initializes the final boss movment accordingly.
    /// </summary>
    void Start ()
    {
        _animator = GetComponent<Animator>();
        movement.faceDirection = FaceDirection.LEFT;
        _movementVector = GetDirectionVector();
    }

    /// <summary>
    /// Method that will control the final boss' movement.
    /// </summary>
    private void LateUpdate()
    {
        TurnNow();
        movement.Move(gameObject, _animator, _movementVector.x, _movementVector.y, SceneData.shouldStop);
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //// NON EVENT METHODS

    /// <summary>
    /// Turns randomly the enemy.
    /// </summary>
    protected override void TurnNow()
    {
        if (ReachedRightTurnPoint())
        {
            TurnLeft();
        }
        else if (ReachedLeftTurnPoint())
        {
            TurnRight();
        }
    }

    private bool ReachedRightTurnPoint()
    {
        return transform.position.x >= rightChangeDirPoint.x;
    }

    private bool ReachedLeftTurnPoint()
    {
        return transform.position.x <= leftChangeDirPoint.x;
    }

    private void TurnRight()
    {
        _movementVector = Vector2.right;
    }

    private void TurnLeft()
    {
        _movementVector = Vector2.left;
    }
}
