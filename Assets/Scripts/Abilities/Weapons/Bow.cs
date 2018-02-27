using UnityEngine;

/// <summary>
/// Class that will be used to store all the bow attributes for the game.
/// </summary>
[CreateAssetMenu(menuName = "Ability/Weapon/Bow")]
public class Bow : GenericWeapon
{
    /// <summary>
    /// Builds a new instance of BowAttributes.
    /// </summary>
    public Bow() : base()
    {
        type = AbilityType.ATTACK;
    }
}
