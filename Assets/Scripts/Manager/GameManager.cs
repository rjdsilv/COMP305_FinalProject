using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Script responsible for managing the game.
/// </summary>
public class GameManager : MonoBehaviour
{
    // Public variable declaration.
    public PlayerAIManager[] playerAIManagers;

    // Private variable declaration.
    private TutorialController _tutorialController;

    private enum GameEndStatus
    {
        WIN,
        LOOSE
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelLoaded;
    }

    /// <summary>
    /// Starts all the necessary information for the game.
    /// </summary>
	private void Start ()
    {
        if (SceneData.playerList.Count == 0)
        {
            if (null != playerAIManagers)
            {
                for (int i = 0; i < playerAIManagers.Length; i++)
                {
                    InstantiatePlayer(i);
                    DontDestroyOnLoad(playerAIManagers[i].player);
                    DontDestroyOnLoad(this);
                    SceneData.SavePlayer(playerAIManagers[i].player);
                }
            }
        }

        if (!SceneData.gameStarted)
        {
            SceneData.gameStarted = true;
            StartCoroutine(GameLoop());
        }
	}

    /// <summary>
    /// Instantiate the player contained in the player index of the game manager.
    /// </summary>
    /// <param name="playerIndex"></param>
    private void InstantiatePlayer(int playerIndex)
    {
        string name = playerAIManagers[playerIndex].player.name;
        playerAIManagers[playerIndex].player = Instantiate(playerAIManagers[playerIndex].player);
        playerAIManagers[playerIndex].player.name = name;
        playerAIManagers[playerIndex].player.GetPlayerControllerComponent().SetIsManagedByAI(playerAIManagers[playerIndex].managedByAI);
        playerAIManagers[playerIndex].player.SetActive(!playerAIManagers[playerIndex].managedByAI);
    }

    /// <summary>
    /// Method that will be called whenever the scene is loaded.
    /// </summary>
    /// <param name="scene">The scene being loaded</param>
    /// <param name="mode">the mode the scene is being loaded.</param>
    private void OnLevelLoaded(Scene scene, LoadSceneMode mode)
    {
        if (!SceneData.isInBattle && !SceneData.killedFinalBoss)
        {
            ShowHideTutorial();
            if (null == playerAIManagers)
            {
                playerAIManagers = new PlayerAIManager[SceneData.playerList.Count];
            }

            for (int i = 0; i < SceneData.playerList.Count; i++)
            {
                playerAIManagers[i].player = SceneData.playerList[i];
                playerAIManagers[i].player.SetActive(!playerAIManagers[i].managedByAI);
                if (!playerAIManagers[i].player.GetControllerComponent().IsManagedByAI())
                {
                    Camera.main.transform.position = playerAIManagers[i].player.transform.position + Vector3.back * 10;
                }
            }
        }
    }

    /// <summary>
    /// Sets the state of the game in order to go to the battle scene.
    /// <param name="battleScene">The battle scene to be loaded.</param>
    /// <param name="mainScene">The main scene to be loaded after the battle.</param>
    /// <param name="enemy">The enemy to be put on the battle.</param>
    /// </summary>
    public void GoToBattle(string battleScene, string mainScene, GameObject enemy)
    {
        if (!SceneData.isInBattle)
        {
            SceneData.isInBattle = true;
            SceneData.shouldStop = true;
            SceneData.mainScene = mainScene;
            SceneData.enemyNotInBattleList.Remove(enemy);
            SceneData.enemyInBattle = enemy;
            StartCoroutine(ShakeCameraAndLoadScene(battleScene));
        }
    }

    private IEnumerator ShakeCameraAndLoadScene(string battleScene)
    {
        for (int i = 0; i < 14; i++)
        {
            if (i % 2 == 0)
            {
                Camera.main.transform.position = new Vector3(Camera.main.transform.position.x + 0.25f, Camera.main.transform.position.y, Camera.main.transform.position.z);
            }
            else
            {
                Camera.main.transform.position = new Vector3(Camera.main.transform.position.x - 0.25f, Camera.main.transform.position.y, Camera.main.transform.position.z);
            }
            yield return new WaitForSeconds(0.05f);
        }
        SceneManager.LoadScene(battleScene);
    }

    /// <summary>
    /// The game loop controller.
    /// </summary>
    private IEnumerator GameLoop()
    {
        while(!GameEnd())
        {
            yield return new WaitForSeconds(Time.deltaTime);
        }

        yield return new WaitForSeconds(2f);
        DestroyAllObjects();
        GameEndStatus endStatus = GetGameEndStatus();

        if (endStatus == GameEndStatus.WIN)
        {
            SceneManager.LoadScene("GameEnd");
        }
        else
        {
            SceneManager.LoadScene("GameOver");
        }
    }

    /// <summary>
    /// Method to check if the game ended or not.
    /// </summary>
    /// <returns></returns>
    private bool GameEnd()
    {
        return SceneData.killedFinalBoss || !IsAnyPlayerAlive();
    }

    private GameEndStatus GetGameEndStatus()
    {
        if (SceneData.killedFinalBoss)
        {
            return GameEndStatus.WIN;
        }

        return GameEndStatus.LOOSE;
    }

    /// <summary>
    /// Shows the tutorial for the game.
    /// </summary>
    private void ShowHideTutorial()
    {
        if (null == _tutorialController)
        {
            _tutorialController = GetComponent<TutorialController>();
        }
 
        if (SceneData.showGameTutorial)
        {
            _tutorialController.ShowTutorial();
            SceneData.showGameTutorial = false;
        }
        else
        {
            _tutorialController.HideTutorial();
        }
    }

    private bool IsAnyPlayerAlive()
    {
        foreach (GameObject player in SceneData.playerList)
        {
            if (null != player && player.GetControllerComponent().IsAlive())
            {
                return true;
            }
        }

        return false;
    }

    private void DestroyAllObjects()
    {
        foreach (GameObject player in SceneData.playerList)
        {
            if (null != player)
            {
                Destroy(player);
            }
        }

        foreach (GameObject enemy in SceneData.enemyNotInBattleList)
        {
            if (null != enemy)
            {
                Destroy(enemy);
            }
        }

        if (null != SceneData.enemyInBattle)
        {
            Destroy(SceneData.enemyInBattle);
        }
    }
}
