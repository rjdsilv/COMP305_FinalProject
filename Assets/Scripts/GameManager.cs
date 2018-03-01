using UnityEngine;

/// <summary>
/// Script responsible for managing the game.
/// </summary>
public class GameManager : MonoBehaviour
{
    // Public variable declaration.
    public GameObject[] players;        // The players to be instantiated.
    public SceneData sceneData;

    /// <summary>
    /// Starts all the necessary information for the game.
    /// </summary>
	private void Start ()
    {
        sceneData = ScriptableObject.CreateInstance<SceneData>();

		if (null != players)
        {
            for (int i = 0; i < players.Length; i++)
            {
                string name = players[i].name;
                players[i] = Instantiate(players[i]);
                players[i].name = name;
                DontDestroyOnLoad(players[i]);
                sceneData.SavePlayer(players[i]);
            }
        }
	}
}
