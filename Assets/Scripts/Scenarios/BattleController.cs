using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleController : MonoBehaviour
{
    public string originalScene;
    public GameObject playerMage;

    private BattlePlayerController mageController;

	// Use this for initialization
	void Start ()
    {
        mageController = playerMage.GetComponent<BattlePlayerController>();
        SpawnEnemies();
        //StartCoroutine(WaitAndReturn(5));
	}

    void LoadOriginalScene()
    {
        StartCoroutine(LoadOriginalSceneAsync());
    }

    IEnumerator WaitAndReturn(float time)
    {
        yield return new WaitForSeconds(time);
        SceneSwitchDataHandler.isComingBackFromBattle = true;
        LoadOriginalScene();
    }

    IEnumerator LoadOriginalSceneAsync()
    {
        AsyncOperation asyncSceneLoad = SceneManager.LoadSceneAsync(originalScene);

        // Wait 50ms to check again.
        while (!asyncSceneLoad.isDone)
        {
            yield return new WaitForSeconds(0.05f);
        }
    }

    void SpawnEnemies()
    {
        int yPosController = 1;
        int xPosController = 1;

        float xPos = 5.5f;
        float yPos = 1.5f;
        float zPos = 0.0f;

        foreach (EnemyHolder enemy in SceneSwitchDataHandler.enemiesInBattle)
        {
            EnemyController enemyController = enemy.Enemy.GetComponent<EnemyController>();
            Instantiate(enemyController.GetBattleEnemy(), new Vector3(xPos, yPos, zPos), Quaternion.identity);

            if (yPosController % 2 == 0)
            {
                yPos += 2.5f;
            }

            if (xPosController %4 == 0)
            {
                yPos = 1.5f;
                xPos -= 1.5f;
            }

            yPosController++;
            xPosController++;
        }
    }
}
