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

    /// <summary>
    /// Starts all the necessary information for the game.
    /// </summary>
	private void Start ()
    {
        _tutorialController = GetComponent<TutorialController>();

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
        else
        {
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

        StartCoroutine(GameLoop());
	}

    /// <summary>
    /// Sets the state of the game in order to go to the battle scene.
    /// </summary>
    public void GoToBattle(string battleScene, GameObject enemy)
    {
        if (!SceneData.isInBattle)
        {
            SceneData.isInBattle = true;
            SceneData.shouldStop = true;
            SceneData.mainScene = "ForestMain";
            SceneData.enemyNotInBattleList.Remove(enemy);
            SceneData.enemyInBattle= enemy;
            SceneManager.LoadScene(battleScene);
        }
    }

    /// <summary>
    /// The game loop controller.
    /// </summary>
    private IEnumerator GameLoop()
    {
        ShowTutorial();
        while(!GameEnd())
        {
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }

    /// <summary>
    /// Method to check if the game ended or not.
    /// </summary>
    /// <returns></returns>
    private bool GameEnd()
    {
        return false;
    }

    /// <summary>
    /// Shows the tutorial for the game.
    /// </summary>
    private void ShowTutorial()
    {
        if (SceneData.showTutorial)
        {
            _tutorialController.ShowTutorial();
            SceneData.showTutorial = false;
        }
        else
        {
            _tutorialController.HideTutorial();
        }
    }
}
