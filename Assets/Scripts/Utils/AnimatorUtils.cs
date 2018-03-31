using System.Collections;
using UnityEngine;

/// <summary>
/// Class containing helper methods and attributes to work with animations.
/// </summary>
public class AnimatorUtils
{
    public const string STAND_RIGHT = "StandRight";
    public const string STAND_DOWN = "StandDown";
    public const string STAND_LEFT = "StandLeft";
    public const string STAND_UP = "StandUp";

    public const string WALK_RIGHT = "WalkRight";
    public const string WALK_DOWN = "WalkDown";
    public const string WALK_LEFT = "WalkLeft";
    public const string WALK_UP = "WalkUp";

    public const string BATTLE_DAMAGE = "BattleDamage";
    public const string BATTLE_DEATH = "BattleDeath";
    public const string BATTLE_ATTACK = "BattleAttack";

    /// <summary>
    /// Checks if the animator is currently playing the given animation.
    /// </summary>
    /// <param name="animator">The animator to be checked.</param>
    /// <param name="animationName">The animation to be checked.</param>
    /// <returns><b>true</b> if the animation is playing. <b>false</b> otherwise.</returns>
    public static bool IsPlaying(Animator animator, string animationName)
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsName(animationName);
    }
}
