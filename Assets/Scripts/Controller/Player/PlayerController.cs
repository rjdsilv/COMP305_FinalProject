using UnityEngine;
/// <summary>
/// Class responsible for controlling a non AI player.
/// </summary>
public abstract class PlayerController<A, L> : ActorController<A, L>, IPlayerController
    where A : PlayerAttributes
    where L : PlayerLevelTree<A>
{
    /// <see cref="ActorController{A, L}"/>
    public override int Attack(GameObject opponent, ActorAbility ability)
    {
        DecreaseConsumable(ability as PlayerAbility);
        return base.Attack(opponent, ability);
    }

    /// <see cref="IPlayerController"/>
    public void IncreaseConsumable(int amount)
    {
        // Every player has at least one ability of type player ability.
        if ((_abilityList[0] as PlayerAbility).consumableType == ConsumableType.MANA)
        {
            attributes.mana += amount;
        }
        else
        {
            attributes.stamina += amount;
        }
    }

    /// <see cref="IPlayerController{A, L}">
    public bool CanAttack(PlayerAbility ability)
    {
        if (ability.consumableType == ConsumableType.MANA)
        {
            return attributes.mana >= ability.consumptionValue;
        }
        
        return attributes.stamina >= ability.consumptionValue;
    }

    /// <see cref="ActorController{A, L}">
    protected override void Init()
    {
        base.Init();

        attributes.gold = 0;
        attributes.xp = 0;
    }

    /// <summary>
    /// Method that will be used for decreasing the players's consumable (mana or stamina).
    /// </summary>
    /// <param name="usedAbility">The ability used on the attack</param>
    protected void DecreaseConsumable(PlayerAbility ability)
    {
        if (ability.consumableType == ConsumableType.MANA)
        {
            attributes.mana -= ability.consumptionValue;
        }
        else
        {
            attributes.stamina -= ability.consumptionValue;
        }
    }
}
