using UnityEngine;
/// <summary>
/// Class responsible for storing the extra attributes for enemies.
/// </summary>
public class EnemyAttributes : ActorAttributes
{
    public int xpForKilling;                    // Amount of XP the players will receive after a battle.
    public int minGoldForKilling;               // Minimum amount of gold this enemy will drop after a battle.
    public int maxGoldForKilling;               // Maximum amount of gold this enemy will drop after a battle.

    [Range(min: 0.0f, max: 1.0f)]
    public float healthRecoverDropChance;       // The chance this enemy will drop a health recover item.

    [Range(min: 0.0f, max: 1.0f)]
    public float staminaRecoverDropChance;      // The chance this enemy will drop a stamina recover item.

    [Range(min: 0.0f, max: 1.0f)]
    public float manaRecoverDropChance;         // The chance this enemy will drop a mana recover item.

    [Range(min: 0.0f, max: 1.0f)]
    public float keyDropChance;                 // The chance this enemy will drop a key.
}
