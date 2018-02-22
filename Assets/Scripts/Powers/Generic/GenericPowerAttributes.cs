/// <summary>
/// Class representing a generic power that a player or a enemy can have.
/// </summary>
public abstract class GenericPowerAttributes
{
    // The minimum power the power has.
    public int MinPower { get; set; }

    // The maximum power the power has.
    public int MaxPower { get; set; }

    // The power's name
    public string Name { get; set; }

    // The power's current level.
    public int Level { get; set; }

    // The power type: ATTACK or DEFENSE.
    public PowerType Type { get; set; }
}
