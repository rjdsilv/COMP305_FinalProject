using UnityEngine;

/// <summary>
/// Class that will be used to store all the giant axe attributes for the game.
/// </summary>
[CreateAssetMenu(menuName = "Ability/Weapon/GiantAxe")]
public class GiantAxe : GenericWeapon
{
    /// <summary>
    /// Builds a new instance of GiantAxeAttributes.
    /// </summary>
    public GiantAxe() : base()
    {
        type = AbilityType.ATTACK;
    }
}
