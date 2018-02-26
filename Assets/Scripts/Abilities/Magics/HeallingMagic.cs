using UnityEngine;

/// <summary>
/// Class that will be used to store all the Healling Magic attributes for the game.
/// </summary>
[CreateAssetMenu(menuName = "Ability/Magic/Healling")]
public class HeallingMagic : GenericMagic
{
    /// <summary>
    /// Builds a new instance of HeallingMagicAttributes.
    /// </summary>
    public HeallingMagic() : base()
    {
        type = AbilityType.ATTACK;
    }
}
