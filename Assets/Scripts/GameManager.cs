using UnityEngine;

/// <summary>
/// Script responsible for managing the game.
/// </summary>
public class GameManager : MonoBehaviour
{
    public GameObject[] players;

    /// <summary>
    /// Starts all the necessary information for the game.
    /// </summary>
	void Start ()
    {
		if (null != players)
        {
            for (int i = 0; i < players.Length; i++)
            {
                players[i] = Instantiate(players[i]);
                players[i].GetComponent<PlayerMovement>().movement.faceDirection = FaceDirection.RIGHT;
            }
        }
	}
}
