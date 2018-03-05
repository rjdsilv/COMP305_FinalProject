/// <summary>
/// Class responsible for dealing with abilities for any given player.
/// </summary>
public abstract class PlayerAbility : ActorAbility
{
    public ConsumableType consumableType;       // The consumable type comsumed by this ability.
    public int consumptionValue;                // The value that the ability will consume from the consumable.
}
