using System.Collections.Generic;
using UnityEngine;

public class SectorController : MonoBehaviour
{
    // Public variable declaration.
    public SectorProperties sectorProperties;

    // Private variables declaration.
    private List<Transform> _enemiesSpawned;

	// Use this for initialization
	void Start ()
    {
        _enemiesSpawned = new List<Transform>();
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        SpawnEnemies();

    }

    void OnTriggerExit2D(Collider2D collision)
    {
        DestroyEnemies();

    }

    private void SpawnEnemies()
    {
        foreach (SectorEnemyProperties ep in sectorProperties.enemyProperties)
        {
            for (int i = 0; i < ep.spawnNumber; i++)
            {
                _enemiesSpawned.Add(
                    Instantiate(
                        ep.enemy.transform,
                        new Vector3(transform.position.x + Random.Range(-ep.spawnRadius, ep.spawnRadius), transform.position.y + Random.Range(-ep.spawnRadius, ep.spawnRadius), 0),
                        Quaternion.identity
                    )
                );
            }
        }
    }

    private void DestroyEnemies()
    {
        foreach(Transform t in _enemiesSpawned)
        {
            Destroy(t.gameObject);
        }
        _enemiesSpawned.Clear();
    }
}
