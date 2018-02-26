using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BattleController : MonoBehaviour
{
    // Battle setup.
    public int turnTime;
    public string originalScene;
    public TurnSchema turnSchema;
    public Mage mage;

    private TurnPlayer _turnPlayer;

	// Use this for initialization
	void Start ()
    {
        _turnPlayer = Random.Range(0, 1) >= 0.5f ? TurnPlayer.PLAYER : TurnPlayer.ENEMY;
        InitializePlayers();
        SpawnEnemies();
        //StartCoroutine(WaitAndReturn(30));
	}

    void Update()
    {
        switch (turnSchema)
        {
            case TurnSchema.ALL_AT_ONCE:
                PlayAllAtOnce();
                break;

            case TurnSchema.ALTERNATE:
                PlayAlternate();
                break;
        }
    }

    void PlayAlternate()
    {
        switch (_turnPlayer)
        {
            case TurnPlayer.PLAYER:
                break;

            case TurnPlayer.ENEMY:
                break;
        }
    }

    void PlayAllAtOnce()
    {
        
    }

    void InitializePlayers()
    {
        mage.Initialize();
    }

    void LoadOriginalScene()
    {
        StartCoroutine(LoadOriginalSceneAsync());
    }

    IEnumerator WaitAndReturn(float time)
    {
        SceneSwitchDataHandler.isComingBackFromBattle = true;
        yield return new WaitForSeconds(time);
        LoadOriginalScene();
    }

    /// <summary>
    /// Reloads asynchronously the original scene.
    /// </summary>
    /// <returns>The number of seconds to wait until the next check.</returns>
    IEnumerator LoadOriginalSceneAsync()
    {
        AsyncOperation asyncSceneLoad = SceneManager.LoadSceneAsync(originalScene);

        // Wait 50ms to check again.
        while (!asyncSceneLoad.isDone)
        {
            yield return new WaitForSeconds(0.05f);
        }
    }

    /// <summary>
    /// Spawns the enemies found to fight in the battle arena.
    /// </summary>
    void SpawnEnemies()
    {
        int posController = 1;

        float xPos = 5.5f;
        float yPos = -2.0f;
        float zPos = 0.0f;
        float xPosVar = 0.75f;

        foreach (EnemyHolder enemy in SceneSwitchDataHandler.enemiesInBattle)
        {
            EnemyController enemyController = enemy.Enemy.GetComponent<EnemyController>();
            Instantiate(enemyController.GetBattleEnemy(), new Vector3(xPos + Mathf.Pow(-1, posController) * xPosVar, yPos, zPos), Quaternion.identity);
            yPos += 2.0f;

            // Moves the enemy spawn position in the x axis.
            if (posController %4 == 0)
            {
                yPos = -2.0f;
                xPos -= 3.0f;
            }

            posController++;
        }
    }
}
