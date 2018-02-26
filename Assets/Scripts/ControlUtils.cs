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

    public static float SwapEnemy()
    {
        return Input.GetAxis("Swap Enemy");
    }
}
