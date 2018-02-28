using UnityEngine;

/// <summary>
/// Class representing commom attributes for all game actors.
/// </summary>
public abstract class ActorAttributes : ScriptableObject
{
    public int health;              // The life the actor currently has.
    public int level;               // The level the actor curreltly has.
    public int minAttack;           // The minimum attack power the actor has.
    public int maxAttack;           // The maximum attack power the actor has.
    public int minDefense;          // The minimum defense power the actor has.
    public int maxDefense;          // The maximum defense power the actor has.
}
