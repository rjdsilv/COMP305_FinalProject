using UnityEngine;

/// <summary>
/// Class representing a generic power that a player or a enemy can have.
/// </summary>
public abstract class GenericAbility : ScriptableObject
{
    public int minPower;                    // The minimum power the ability has.
    public int maxPower;                    // The maximum power the ability has.
    public string powerNme;                 // The ability's name.
    public int level;                       // The ability's current level.
    public int powerCost;                   // The ammount of force that will be consumed when using the ability.
    public AbilityType type;                // The ability type: ATTACK or DEFENSE.
    public AbilityConsumable consumable;    // The force attribute that will be consumed by this ability : Mana or Stamina.

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
        switch (consumable)
        {
            case AbilityConsumable.MANA:
                return playerController.GetAttributes().HasMana && playerController.GetAttributes().CurrentMana >= powerCost;

            case AbilityConsumable.STAMINA:
                return playerController.GetAttributes().HasStamina && playerController.GetAttributes().CurrentStamina >= powerCost;
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

        switch (type)
        {
            // Attacking.
            case AbilityType.ATTACK:
                return Mathf.FloorToInt(Random.Range(levelAttributes.MinAttackPower, levelAttributes.MaxAttackPower));

            // Defending.
            case AbilityType.DEFENSE:
                return Mathf.FloorToInt(Random.Range(levelAttributes.MinDefensePower, levelAttributes.MaxDefensePower));
        }

        return 0;
    }
}
