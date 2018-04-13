using System.Collections.Generic;
using UnityEngine;

public abstract class ActorController<A, L> : MonoBehaviour, IController
    where A : ActorAttributes
    where L : ActorLevelTree<A>
{
    // Public variable declaration.
    [HideInInspector]
    public A attributes;                            // The actor's attributes.
    public L levelTree;                             // The actor's level tree.

    // Protected variable declaration.
    protected List<ActorAbility> _abilityList;      // The list of abilities this enemy has.

    /// <summary>
    /// Initialize all the necessary variables.
    /// </summary>
    protected virtual void Init()
    {
        _abilityList = new List<ActorAbility>();
    }

    /// <see cref="IController"/>
    public virtual int Attack(GameObject opponent, ActorAbility ability)
    {
        int attack = Mathf.FloorToInt(Random.Range(attributes.minAttack, attributes.maxAttack + 0.99999f)) + Mathf.FloorToInt(Random.Range(ability.minPower, ability.maxPower + 0.99999f));
        int deffense = opponent.GetControllerComponent().CalculateDefense();
        int healthDrained = attack > deffense ? attack - deffense : 0;
        opponent.GetControllerComponent().DecreaseHealth(healthDrained);
        return healthDrained;
    }

    /// <see cref="IController"/>
    public int CalculateDefense()
    {
        return Mathf.FloorToInt(Random.Range(attributes.minDefense, attributes.maxDefense + 0.9999999f));
    }

    /// <see cref="IController"/>
    public bool IsManagedByAI()
    {
        return attributes.managedByAI;
    }

    /// <see cref="IController"/>
    public bool IsAlive()
    {
        return attributes.health > 0;
    }

    /// <see cref="IController"/>
    public void DecreaseHealth(int amount)
    {
        if (attributes.health - amount < 0)
        {
            attributes.health = 0;
        }
        else
        {
            attributes.health -= amount;
        }
    }

    /// <see cref="IController"/>
    public ActorAbility SelectAbility()
    {
        int selectedAbilityIndex = Mathf.FloorToInt(Random.Range(0, _abilityList.Count - 0.000001f));
        return _abilityList[selectedAbilityIndex];
    }

    /// <see cref="IController"/>
    public int GetCurrentLevel()
    {
        return attributes.level;
    }

    /// <summary>
    /// Levels up the actor if necessary.
    /// </summary>
    public abstract void LevelUp();

    /// <summary>
    /// Sets the players attributes for the current level.
    /// </summary>
    protected abstract void SetAttributesForCurrentLevel();
}
