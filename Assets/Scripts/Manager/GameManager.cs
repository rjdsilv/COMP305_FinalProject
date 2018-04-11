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
    private bool _isLevelLoadMethodSet = false;

    private enum GameEndStatus
    {
        WIN,
        LOOSE
    }

    private void OnEnable()
    {
        if (!_isLevelLoadMethodSet)
        {
            SceneManager.sceneLoaded += OnLevelLoaded;
            _isLevelLoadMethodSet = true;
        }
    }

    private void OnDisable()
    {
        if (_isLevelLoadMethodSet)
        {
            SceneManager.sceneLoaded -= OnLevelLoaded;
            _isLevelLoadMethodSet = false;
        }
    }

    /// <summary>
    /// Starts all the necessary information for the game.
    /// </summary>
	private void Start ()
    {
        InstantiateAndSavePlayers();
        StartGame();
	}

    /// <summary>
    /// Instantiates and save the players on scene data class.
    /// </summary>
    private void InstantiateAndSavePlayers()
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
    }

    /// <summary>
    /// Method to start the game loop.
    /// </summary>
    private void StartGame()
    {
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

        for (int i = 0; i < SceneData.chosenPlayers.Length; i++)
        {
            if (playerAIManagers[playerIndex].player.name == SceneData.chosenPlayers[i])
            {
                playerAIManagers[playerIndex].managedByAI = false;
                playerAIManagers[playerIndex].player.transform.position = Vector3.up * playerIndex * 20;
                SetPlayerNumber(playerAIManagers[playerIndex].player, i + 1);
                ManageCameras(playerAIManagers[playerIndex].player);
            }
        }

        playerAIManagers[playerIndex].player.GetPlayerControllerComponent().SetIsManagedByAI(playerAIManagers[playerIndex].managedByAI);
        playerAIManagers[playerIndex].player.SetActive(!playerAIManagers[playerIndex].managedByAI);
    }

    /// <summary>
    /// Sets the player number according to the selection made.
    /// </summary>
    /// <param name="player">The player to have the number set.</param>
    /// <param name="playerNumber">The number to be set to the player.</param>
    private void SetPlayerNumber(GameObject player, int playerNumber)
    {
        if (player.IsMage())
        {
            player.GetComponent<MageController>().playerNumber = playerNumber;
        }
        else if (player.IsThief())
        {
            player.GetComponent<ThiefController>().playerNumber = playerNumber;
        }
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
                playerAIManagers[i].player.SetActive(!playerAIManagers[i].player.GetControllerComponent().IsManagedByAI());
                if (!playerAIManagers[i].player.GetControllerComponent().IsManagedByAI())
                {
                    ManageCameras(playerAIManagers[i].player);
                }
            }
        }
    }

    /// <summary>
    /// Method implemented to manage the game cameras for 1 or 2 players.
    /// </summary>
    /// <param name="player">The player to have the camera managed.</param>
    private void ManageCameras(GameObject player)
    {
        foreach (Camera c in Camera.allCameras)
        {
            if (SceneData.numberOfPlayers == 1)
            {
                if (c.name == "Player_01")
                {
                    c.rect = new Rect(0f, 0f, 1, 1);
                    c.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, c.transform.position.z);
                    c.orthographicSize = 10;
                }
                else if (c.name == "Player_02")
                {
                    c.gameObject.SetActive(false);
                }
            }
            else if (SceneData.numberOfPlayers == 2)
            {
                if (c.name == "Player_01" && player.GetPlayerControllerComponent().IsPlayerOne())
                {
                    c.rect = new Rect(0f, 0f, 0.5f, 1f);
                    c.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, c.transform.position.z);
                }
                else if (c.name == "Player_02" && player.GetPlayerControllerComponent().IsPlayerTwo())
                {
                    c.rect = new Rect(0.5f, 0f, 0.5f, 1f);
                    c.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, c.transform.position.z);
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

    /// <summary>
    /// Method made to Shake the cameras and load the battle scene scene.
    /// </summary>
    /// <param name="battleScene">The battle scene to be loaded.</param>
    private IEnumerator ShakeCameraAndLoadScene(string battleScene)
    {
        for (int i = 0; i < 14; i++)
        {
            foreach (Camera c in Camera.allCameras)
            {
                if (i % 2 == 0)
                {
                    c.transform.position = new Vector3(c.transform.position.x + 0.25f, c.transform.position.y, c.transform.position.z);
                }
                else
                {
                    c.transform.position = new Vector3(c.transform.position.x - 0.25f, c.transform.position.y, c.transform.position.z);
                }
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

        _tutorialController.ResetPanels();
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

    /// <summary>
    /// Checks if there any of the players alive.
    /// </summary>
    /// <returns><b>true</b> if there is at least 1 player alive. <b>false</b> otherwise.</returns>
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

    /// <summary>
    /// Destroys all the objects in the screen
    /// TODO Destroy the dropped items.
    /// </summary>
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
