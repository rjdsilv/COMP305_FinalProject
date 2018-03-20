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
    public void IncreaseConsumable(float amount)
    {
        // Every player has at least one ability of type player ability.
        if ((_abilityList[0] as PlayerAbility).consumableType == ConsumableType.MANA)
        {
            if (attributes.mana + amount <= levelTree.GetAttributesForCurrentLevel().mana)
            {
                attributes.mana += amount;
            }
            else
            {
                attributes.mana = levelTree.GetAttributesForCurrentLevel().mana;
            }
        }
        else
        {
            if (attributes.stamina + amount <= levelTree.GetAttributesForCurrentLevel().stamina)
            {
                attributes.stamina += amount;
            }
            else
            {
                attributes.stamina = levelTree.GetAttributesForCurrentLevel().stamina;
            }
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

    /// <see cref="IPlayerController{A, L}">
    public void IncreaseXp(int amount)
    {
        attributes.xp += amount;
        LevelUp();
    }

    /// <see cref="IPlayerController{A, L}">
    public void IncreaseGold(int amount)
    {
        attributes.gold += amount;
    }

    /// <see cref="ActorController{A, L}">
    protected override void Init()
    {
        base.Init();

        attributes.gold = 0;
        attributes.xp = 0;
    }

    /// <see cref="IController{A, L}"/>
    public override void LevelUp()
    {
        if (levelTree.CanLevelUp() && (attributes.xp >= levelTree.GetMinXpForNextLevel()))
        {
            levelTree.IncreaseLevel();
            SetAttributesForCurrentLevel();
        }
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
