using UnityEngine;
/// <summary>
/// Class that will be used to store all the Dagger attributes for the game.
/// </summary>
[CreateAssetMenu(menuName = "Ability/Weapon/Sword")]
public class Sword : GenericWeapon
{
    /// <summary>
    /// Builds a new instance of SwordAttributes.
    /// </summary>
    public Sword() : base()
    {
        type = AbilityType.ATTACK;
    }
}
