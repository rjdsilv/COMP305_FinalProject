using UnityEngine;

public class ControlUtils
{
    public static float Horizontal(int playerNumber)
    {
        return Input.GetAxis(string.Format("Horizontal P{0}", playerNumber));
    }
    public static float Vertical(int playerNumber)
    {
        return Input.GetAxis(string.Format("Vertical P{0}", playerNumber));
    }

    public static bool Attack(int playerNumber)
    {
        return Input.GetButton(string.Format("Attack P{0}", playerNumber));
    }

    public static bool ButtonB(int playerNumber)
    {
        return Input.GetButton(string.Format("ButtonB P{0}", playerNumber));
    }

    public static float SwapAbility(int playerNumber)
    {
        return Input.GetAxis(string.Format("Swap Ability P{0}", playerNumber));
    }

    public static bool SwapEnemyUp(int playerNumber)
    {
        return Input.GetButton(string.Format("Swap Enemy Up P{0}", playerNumber)) || Input.GetAxis(string.Format("Swap Enemy Up P{0}", playerNumber)) > 0;
    }

    public static bool SwapEnemyDown(int playerNumber)
    {
        return Input.GetButton(string.Format("Swap Enemy Down P{0}", playerNumber)) || Input.GetAxis(string.Format("Swap Enemy Down P{0}", playerNumber)) < 0;
    }
}
