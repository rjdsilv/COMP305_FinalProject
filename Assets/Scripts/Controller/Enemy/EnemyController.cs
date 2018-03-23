﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class responsible for controlling the enemy.
/// </summary>
public abstract class EnemyController<A, L> : ActorController<A, L>, IEnemyController
    where A : EnemyAttributes
    where L : EnemyLevelTree<A>
{
    // Constant declaration.
    protected const int SELECTION_LIGHT_IDX = 5;
    protected const int HUD_CANVAS_IDX = 6;

    // Public variable declaration.
    public int minEnemiesInBattle;                // The minimum number of enemies that will be spawned in a battle scene.
    public int maxEnemiesInBattle;                // The maximum number of enemies that will be spawned in a battle scene.

    // Protected variable declaration.
    protected GameManager _gameManager;           // The game manager script to be used.
    protected EnemyVisionAI _enemyVisionAI;       // The enemy vision AI script to be used.

    // The sector where the enemy was spawned.
    public string SectorName { get; set; }

    // The battle scene to be loaded.
    public string BattleScene { get; set; }

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //// EVENT METHODS

    /// <summary>
    /// Method called after all scene updates occur. This method will set the game state to battle.
    /// </summary>
    private void LateUpdate()
    {
        if (null != _gameManager)
        {
            if ((null != BattleScene) && !SceneData.isInBattle && _enemyVisionAI.IsSeeingPlayer())
            {
                _gameManager.GoToBattle(BattleScene, gameObject);
            }
        }
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //// NON EVENT METHODS

    /// <see cref="IEnemyController"/>    
    public Light GetSelectionLight()
    {
        return transform.GetChild(SELECTION_LIGHT_IDX).GetComponent<Light>();
    }

    /// <see cref="IEnemyController"/>    
    public void DecreaseHealthHUD(int amount)
    {
        GetHUDHealthText().text = "-" + amount.ToString();
        GetHUDHealthSlider().value -= amount;
    }

    /// <see cref="IEnemyController"/>    
    public int GetXpEarnedForKilling()
    {
        return attributes.xpForKilling;
    }

    /// <see cref="IEnemyController"/>    
    public int GetGoldEarnedForKilling()
    {
        return Mathf.FloorToInt(Random.Range(attributes.minGoldForKilling, attributes.maxGoldForKilling + 0.99999f));
    }

    /// <see cref="IEnemyController"/>    
    public bool DropHealthPot()
    {
        return Random.Range(0.0f, 1.0f) <= levelTree.GetAttributesForCurrentLevel().healthRecoverDropChance;
    }

    /// <see cref="IEnemyController"/>    
    public bool DropManaPot()
    {
        return Random.Range(0.0f, 1.0f) <= levelTree.GetAttributesForCurrentLevel().manaRecoverDropChance;
    }

    /// <see cref="IEnemyController"/>    
    public bool DropStaminaPot()
    {
        return Random.Range(0.0f, 1.0f) <= levelTree.GetAttributesForCurrentLevel().staminaRecoverDropChance;
    }

    /// <see cref="IController"/>
    public override void LevelUp()
    {
        if (levelTree.CanLevelUp())
        {
            levelTree.IncreaseLevel();
            SetAttributesForCurrentLevel();
        }
    }

    /// <see cref="ActorController{A, L}"/>
    protected override void SetAttributesForCurrentLevel()
    {
        // Initializes its attributes.
        attributes.xpForKilling = levelTree.GetAttributesForCurrentLevel().xpForKilling;
        attributes.minGoldForKilling = levelTree.GetAttributesForCurrentLevel().minGoldForKilling;
        attributes.maxGoldForKilling = levelTree.GetAttributesForCurrentLevel().maxGoldForKilling;
        attributes.healthRecoverDropChance = levelTree.GetAttributesForCurrentLevel().healthRecoverDropChance;
        attributes.staminaRecoverDropChance = levelTree.GetAttributesForCurrentLevel().staminaRecoverDropChance;
        attributes.manaRecoverDropChance = levelTree.GetAttributesForCurrentLevel().manaRecoverDropChance;
    }

    /// <summary>
    /// Initialize all the necessary variables.
    /// </summary>
    protected override void Init()
    {
        base.Init();

        // Initializes the vision AI.
        _enemyVisionAI = GetComponent<EnemyVisionAI>();

        // Initializes the attributes.
        SetAttributesForCurrentLevel();

        // Initializes the HUD.
        GetHUDCanvas().enabled = true;
        GetHUDHealthSlider().maxValue = levelTree.GetAttributesForCurrentLevel().health;
        GetHUDHealthSlider().value = attributes.health;

        if (!SceneData.shouldStop)
        {
            GetHUDCanvas().enabled = false;
        }
    }

    /// <summary>
    /// Returns the enemy HUD canvas to be used.
    /// </summary>
    /// <returns>The enemy HUD canvas to be used.</returns>
    protected Canvas GetHUDCanvas()
    {
        return transform.GetChild(HUD_CANVAS_IDX).GetComponent<Canvas>();
    }

    /// <summary>
    /// Returns the enemy HUD canvas health slider to be used.
    /// </summary>
    /// <returns>The enemy HUD canvas health slider to be used.</returns>
    protected Slider GetHUDHealthSlider()
    {
        return GetHUDCanvas().GetComponent<RectTransform>().GetChild(0).GetComponent<Slider>();
    }

    /// <summary>
    /// Returns the enemy HUD canvas health text to be used.
    /// </summary>
    /// <returns>The enemy HUD canvas health text to be used.</returns>
    protected Text GetHUDHealthText()
    {
        return GetHUDCanvas().GetComponent<RectTransform>().GetChild(1).GetComponent<Text>();
    }
}
