using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BattleController : MonoBehaviour
{
    public string originalScene;

    // Mage objects.
    public GameObject playerMage;
    public Slider playerMageMana;
    public Slider playerMageLife;

    private BattlePlayerController mageController;

	// Use this for initialization
	void Start ()
    {
        mageController = playerMage.GetComponent<BattlePlayerController>();
        SpawnEnemies();
        StartCoroutine(WaitAndReturn(5));
	}

    void InitializeMage()
    {
        BattlePlayerController playerMageController = playerMage.GetComponent<BattlePlayerController>().Initialize();
        PlayerAttributes mageAttrs = playerMageController.GetAttributes();
        playerMageMana.maxValue = mageAttrs.GetLevelAttributes().MaxMana;
        playerMageMana.value = mageAttrs.CurrentMana;
        playerMageLife.maxValue = mageAttrs.GetLevelAttributes().MaxLife;
        playerMageLife.value = mageAttrs.CurrentLife;
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
