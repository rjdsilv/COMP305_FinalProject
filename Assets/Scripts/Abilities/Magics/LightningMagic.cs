using UnityEngine;

/// <summary>
/// Class that will be used to store all the Lightning Magic attributes for the game.
/// </summary>
[CreateAssetMenu(menuName = "Ability/Magic/Lightning")]
public class LightningMagic : GenericMagic
{
    /// <summary>
    /// Builds a new instance of LightningMagic.
    /// </summary>
    public LightningMagic() : base()
    {
        type = AbilityType.ATTACK;
    }
}
