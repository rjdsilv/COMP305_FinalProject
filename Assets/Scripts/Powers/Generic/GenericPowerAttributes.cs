using UnityEngine;

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

    // The ammount of force that will be consumed when using the power
    public int ForceConmsuption { get; set; }

    // The power type: ATTACK or DEFENSE.
    public PowerType Type { get; set; }

    // The force attribute that will be consumed by this power: Mana or Stamina.
    public PowerConsumable Consumable { get; set; }

    /// <summary>
    /// Uses the power by the given player and returns the power applied against the opponent.
    /// </summary>
    /// <param name="playerController">The player that will use the power.</param>
    /// <returns>The power applied against the enemy.</returns>
    public abstract int UsePower(PlayerController playerController);

    /// <summary>
    /// Method to indicates if the player is able to use the given power or not.
    /// </summary>
    /// <param name="playerController">The player who will use the power.</param>
    /// <returns><b>true</b> if the player can use the power. <b>false</b> otherwise.</returns>
    public bool CanUsePower(PlayerController playerController)
    {
        switch (Consumable)
        {
            case PowerConsumable.MANA:
                return playerController.GetAttributes().HasMana && playerController.GetAttributes().CurrentMana >= ForceConmsuption;

            case PowerConsumable.STAMINA:
                return playerController.GetAttributes().HasStamina && playerController.GetAttributes().CurrentStamina >= ForceConmsuption;
        }

        return false;
    }

    /// <summary>
    /// This method will calculate the applied power by the use of any kind of power by the player.
    /// </summary>
    /// <param name="playerController">The player who will use the power.</param>
    /// <returns>The force applied by the use of the poser.</returns>
    protected int CalculateAppliedPower(PlayerController playerController)
    {
        LevelAttributes levelAttributes = playerController.GetAttributes().GetLevelAttributes();

        switch (Type)
        {
            // Attacking.
            case PowerType.ATTACK:
                return Mathf.FloorToInt(Random.Range(levelAttributes.MinAttackPower, levelAttributes.MaxAttackPower));

            // Defending.
            case PowerType.DEFENSE:
                return Mathf.FloorToInt(Random.Range(levelAttributes.MinDefensePower, levelAttributes.MaxDefensePower));
        }

        return 0;
    }
}
