﻿using UnityEngine;
/// <summary>
/// Class responsible for controlling a non AI player.
/// </summary>
public abstract class PlayerController<A, L> : ActorController<A, L>, IPlayerController
    where A : PlayerAttributes
    where L : PlayerLevelTree<A>
{
    [HideInInspector]
    public int playerNumber = 1;            // Indicates the number of the selected player.
    public float sfxVolume = 0.5f;
    public AudioClip[] audioClips;

    protected int _clipToPlay = 0;
    protected AudioSource _audioSource;

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

    public override void PlayDamageSound()
    {
        if (null != audioClips && audioClips.Length > 0)
        {
            _clipToPlay %= audioClips.Length;
            _audioSource.clip = audioClips[_clipToPlay++];
            _audioSource.volume = sfxVolume;
            _audioSource.Play();
        }
    }

    /// <see cref="IPlayerController"/>
    public void IncreaseHealth(int amount)
    {
        // Every player has at least one ability of type player ability.
        if (attributes.health + amount <= levelTree.GetAttributesForCurrentLevel().health)
        {
            attributes.health += amount;
        }
        else
        {
            attributes.health = levelTree.GetAttributesForCurrentLevel().health;
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

    /// <see cref="IPlayerController{A, L}">
    public void SetIsManagedByAI(bool isManagedByAI)
    {
        attributes.managedByAI = isManagedByAI;
    }

    /// <see cref="IPlayerController{A, L}">
    public int GetPlayerNumber()
    {
        return playerNumber;
    }

    /// <see cref="IPlayerController{A, L}">
    public bool IsPlayerOne()
    {
        return playerNumber == 1;
    }

    /// <see cref="IPlayerController{A, L}">
    public bool IsPlayerTwo()
    {
        return playerNumber == 2;
    }

    /// <see cref="IPlayerController{A, L}">
    public int GetGold()
    {
        return attributes.gold;
    }

    /// <see cref="IPlayerController{A, L}">
    public int GetXp()
    {
        return attributes.xp;
    }

    /// <see cref="IPlayerController{A, L}">
    public int GetHealth()
    {
        return attributes.health;
    }

    /// <see cref="IPlayerController{A, L}">
    public int GetMaxHealth()
    {
        return levelTree.GetAttributesForCurrentLevel().health;
    }

    /// <see cref="IPlayerController{A, L}">
    public float GetConsumable()
    {
        return (_abilityList[0] as PlayerAbility).consumableType == ConsumableType.MANA 
            ? attributes.mana 
            : attributes.stamina;
    }

    /// <see cref="IController{A, L}"/>
    public float GetMaxConsumable()
    {
        return (_abilityList[0] as PlayerAbility).consumableType == ConsumableType.MANA 
            ? levelTree.GetAttributesForCurrentLevel().mana 
            : levelTree.GetAttributesForCurrentLevel().stamina;
    }

    /// <see cref="IController{A, L}"/>
    public override void LevelUp()
    {
        if (levelTree.CanLevelUp() && (attributes.xp >= levelTree.GetMinXpForNextLevel()))
        {
            levelTree.IncreaseLevel();
            SetAttributesForCurrentLevel();
            GetComponent<Animator>().SetInteger("health", attributes.health);
        }
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
