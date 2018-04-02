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
    public GameObject[] players;        // The players to be instantiated.

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
            if (null != players)
            {
                for (int i = 0; i < players.Length; i++)
                {
                    string name = players[i].name;
                    players[i] = Instantiate(players[i]);
                    players[i].name = name;
                    DontDestroyOnLoad(players[i]);
                    DontDestroyOnLoad(this);
                    SceneData.SavePlayer(players[i]);
                }
            }
        }

        if (!SceneData.gameStarted)
        {
            SceneData.gameStarted = true;
            StartCoroutine(GameLoop());
        }
	}

    private void OnLevelLoaded(Scene scene, LoadSceneMode mode)
    {
        if (!SceneData.isInBattle && !SceneData.killedFinalBoss)
        {
            ShowHideTutorial();
            if (null == players)
            {
                players = new GameObject[SceneData.playerList.Count];
            }

            for (int i = 0; i < SceneData.playerList.Count; i++)
            {
                players[i] = SceneData.playerList[i];
                players[i].SetActive(true);
                if (!players[i].GetControllerComponent().IsManagedByAI())
                {
                    Camera.main.transform.position = players[i].transform.position + Vector3.back * 10;
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
            SceneManager.LoadScene(battleScene);
        }
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
