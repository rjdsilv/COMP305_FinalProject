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
    private bool _goToBattle = false;   // The scene should change to the battle scene.
    private string _battleScene = "";   // The battle scene that must be loaded.

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
            }
        }

        StartCoroutine(GameLoop());
	}

    /// <summary>
    /// Sets the state of the game in order to go to the battle scene.
    /// </summary>
    public void GoToBattle(string battleScene, GameObject enemy)
    {
        if (!_goToBattle)
        {
            _goToBattle = true;
            _battleScene = battleScene;
            SceneData.enemyNotInBattleList.Remove(enemy);
            SceneData.enemyInBattleList.Add(enemy);
        }
    }

    /// <summary>
    /// The game loop controller.
    /// </summary>
    private IEnumerator GameLoop()
    {
        while(!GameEnd())
        {
            if (_goToBattle)
            {
                SceneData.isInBattle = true;
                SceneManager.LoadScene(_battleScene);
            }

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
}
