using UnityEngine;

/// <summary>
/// Class that will be used to store all the Fire Magic attributes for the game.
/// </summary>
[CreateAssetMenu(menuName = "Ability/Magic/Fire")]
public class FireMagic : GenericMagic
{
    /// <summary>
    /// Builds a new instance of FireMagicAttributes.
    /// </summary>
    public FireMagic() : base()
    {
        type = AbilityType.ATTACK;
    }
}
