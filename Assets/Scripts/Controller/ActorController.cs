using UnityEngine;

public abstract class ActorController<A, L> : MonoBehaviour, IController
    where A : ActorAttributes
    where L : ActorLevelTree<A>
{
    [HideInInspector]
    public A attributes;    // The actor's attributes.
    public L levelTree;     // The actor's level tree.

    /// <summary>
    /// Initialize all the necessary variables.
    /// </summary>
    protected abstract void Init();

    /// <see cref="IController"/>
    public int Attack(GameObject opponent, ActorAbility ability)
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

    public void DecreaseHealth(int ammount)
    {
        attributes.health -= ammount;
    }
}
