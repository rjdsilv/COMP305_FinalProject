using UnityEngine;

/// <summary>
/// Class that will be used to store all the Dagger attributes for the game.
/// </summary>
[CreateAssetMenu(menuName = "Ability/Weapon/Dagger")]
public class Dagger : GenericWeapon
{
    /// <summary>
    /// Builds a new instance of DaggerAttributes.
    /// </summary>
    public Dagger() : base()
    {
        type = AbilityType.ATTACK;
    }
}
