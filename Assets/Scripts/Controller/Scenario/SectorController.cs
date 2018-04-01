﻿using System;
using UnityEngine;

/// <summary>
/// Class responsible for controlling the sectors created, spawning and destroying enemies according
/// to its configurations.
/// </summary>
public class SectorController : MonoBehaviour
{
    // Public variable declaration.
    public GameObject healthPot;                // The health pot dropped by the enemy.
    public GameObject manaPot;                  // The mana pot dropped by the enemy.
    public GameObject staminaPot;               // The stamina pot dropped by the enemy.
    public SectorAttributes attributes;         // The attributes for the sector.

    // Private variable declaration.
    private bool _spawnEnemies = true;          // Indicates if the enemies were destroyed or not.
    private float _itemSpawnRadius = 1.5f;      // The radius arround the dead enemy where the items will be spawned.


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //// EVENT METHODS

    /// <summary>
    /// When the player enters into the sector, spawn all the enemies from that sector.
    /// </summary>
    /// <param name="detectedObject">The detected object.</param>
    private void OnTriggerEnter2D(Collider2D detectedObject)
    {
        if (TagUtils.IsPlayer(detectedObject.transform))
        {
            SpawnEnemies();
        }
    }

    /// <summary>
    /// When the player leaves the sector, destroys all the enemies from that sector.
    /// </summary>
    /// <param name="detectedObject">The detected object.</param>
    void OnTriggerExit2D(Collider2D detectedObject)
    {
        if (TagUtils.IsCamera(detectedObject.transform) && detectedObject.isTrigger)
        {
            DestroyEnemies();
            _spawnEnemies = true;
        }
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //// NON EVENT METHODS

    /// <summary>
    /// Spawns all the enemies based on the sector configuration.
    /// </summary>
    private void SpawnEnemies()
    {
        if (!SceneData.isCommingBackFronBattle)
        {
            // If the scene is loading for the first time, just spawn the enemies.
            if (attributes.spawnEnemies && _spawnEnemies)
            {
                foreach (SectorEnemyAttributes ea in attributes.enemyAttributes)
                {
                    for (int i = 0; i < ea.spawnNumber; i++)
                    {
                        // Instantiate the enemy.
                        Vector3 position = new Vector3(
                            transform.position.x + UnityEngine.Random.Range(-ea.spawnRadius, ea.spawnRadius),
                            transform.position.y + UnityEngine.Random.Range(-ea.spawnRadius, ea.spawnRadius),
                            0
                        );
                        GameObject enemy = Instantiate(ea.enemy, position, Quaternion.identity);
                        enemy.name = ea.enemy.name;
                        enemy.SetEnemyControllerParameters(attributes);
                        DontDestroyOnLoad(enemy);
                        SceneData.enemyNotInBattleList.Add(enemy);
                    }
                }
            }
        }
        else
        {
            SceneData.isCommingBackFronBattle = false;
            foreach (GameObject enemy in SceneData.enemyNotInBattleList)
            {
                enemy.SetActive(true);
            }
            SpawnDroppedItems();
        }
        _spawnEnemies = false;
    }

    /// <summary>
    /// Spawns all the enemy dropped items from the battle.
    /// </summary>
    private void SpawnDroppedItems()
    {
        SpawnDroppedHealthPot();
        SpawnDroppedManaPot();
        SpawnDroppedStaminahPot();
    }

    /// <summary>
    /// Spawns the health pot.
    /// </summary>
    private void SpawnDroppedHealthPot()
    {
        // Did the enemy drop a health pot?
        if (SceneData.dropHealthPot)
        {
            // Instantiate the pot.
            DontDestroyOnLoad(Instantiate(healthPot, CalculatePotSpawnPosition(), Quaternion.identity));
            SceneData.dropHealthPot = false;
        }
    }

    /// <summary>
    /// Spawns the mana pot.
    /// </summary>
    private void SpawnDroppedManaPot()
    {
        // Did the enemy drop a mana pot?
        if (SceneData.dropManaPot)
        {
            DontDestroyOnLoad(Instantiate(manaPot, CalculatePotSpawnPosition(), Quaternion.identity));
            SceneData.dropManaPot = false;
        }
    }

    /// <summary>
    /// Spawns the stamina pot.
    /// </summary>
    private void SpawnDroppedStaminahPot()
    {
        // Did the enemy drop a stamina pot?
        if (SceneData.dropStaminaPot)
        {
            DontDestroyOnLoad(Instantiate(staminaPot, CalculatePotSpawnPosition(), Quaternion.identity));
            SceneData.dropStaminaPot = false;
        }
    }

    private Vector3 CalculatePotSpawnPosition()
    {
        return new Vector3(
            SceneData.dropPosition.x + UnityEngine.Random.Range(-_itemSpawnRadius, _itemSpawnRadius),
            SceneData.dropPosition.y + UnityEngine.Random.Range(-_itemSpawnRadius, _itemSpawnRadius),
            0
        );
    }

    /// <summary>
    /// Destroys all the created enemies and clear the reference lists.
    /// </summary>
    private void DestroyEnemies()
    {
        SceneData.DestroyAllEnemiesOnSector(attributes.sectorName);
    }
}
