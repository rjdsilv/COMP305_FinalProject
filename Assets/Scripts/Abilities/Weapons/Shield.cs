using UnityEngine;

/// <summary>
/// Class that will be used to store all the shield attributes for the game.
/// </summary>
[CreateAssetMenu(menuName = "Ability/Weapon/Shield")]
public class Shield : GenericWeapon
{
    /// <summary>
    /// Builds a new instance of ShieldAttributes.
    /// </summary>
    public Shield() : base()
    {
        type = AbilityType.DEFENSE;
    }
}
