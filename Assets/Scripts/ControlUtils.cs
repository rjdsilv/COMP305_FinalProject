using UnityEngine;

public class ControlUtils
{
    public static float Horizontal()
    {
        return Input.GetAxis("Horizontal");
    }
    public static float Vertical()
    {
        return Input.GetAxis("Vertical");
    }

    public static float Attack()
    {
        return Input.GetAxis("Attack");
    }

    public static float SwapAbility()
    {
        return Input.GetAxis("Swap Ability");
    }

    public static bool SwapEnemyUp()
    {
        return Input.GetButton("Swap Enemy Up") || Input.GetAxis("Swap Enemy Up") > 0;
    }

    public static bool SwapEnemyDown()
    {
        return Input.GetButton("Swap Enemy Down") || Input.GetAxis("Swap Enemy Down") < 0;
    }
}
