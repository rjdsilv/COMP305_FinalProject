using UnityEngine;
using UnityEngine.SceneManagement;

public class TempleEntranceController : EntranceController
{
    // The text that will be shown when the player enters the temple first time.
    private const string _noKeyText = "The temple door is locked and can only be opened with the proper key.\n\nPress B to continue.";

    private const string _holdingKeyText = "You are about to enter the temple of Zydis. This is a temple of pure evilness and disgrace to those who enters it. " +
        "Are you sure you want to enter the temple and die?";

    private GameManager gameManager;

    // Public variable declaration.
    public Vector3 playerOneSpawnPos;
    public Vector3 playerTwoSpawnPos;
    public Mission noKeyNextMission;
    public Mission withKeyNextMission;

    /// <summary>
    /// Initializes the object.
    /// </summary>
    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    /// <summary>
    /// This method will check if the player reached the temple entrance and perform the following actions:
    /// <ul>
    ///     <li>Show the temple entrance text.</li>
    ///     <li>Stop the player from moving.</li>
    ///     <li>Show the options to be chosen.</li>
    ///     <li>Wait for the player's decision.</li>
    /// </ul>
    /// </summary>
    private void Update()
    {
        _collisionObject = Physics2D.OverlapCircle(transform.position, entranceCheckRadius, entranceMask);
        if (null != _collisionObject)
        {
            if (!_showingText && TagUtils.IsTemple(_collisionObject.transform))
            {
                // Shows the dialog panel.
                LoadDialogPanel();
                _showingText = true;
                SceneData.shouldStop = true;
                if (SceneData.keys.Count == 0)
                {
                    _dialogPanel.DisplayMessage(_noKeyText, new bool[] { false, false });
                }
                else
                {
                    _dialogPanel.DisplayMessage(_holdingKeyText, new bool[] { true, true });
                }

                if (ControlUtils.ButtonB(1) || ControlUtils.ButtonB(2))
                {
                    HideDialogPanelAndWalk();
                }

                if (SceneData.keys.Count == 0)
                {
                    while (!noKeyNextMission.Equals(gameManager.currentMission) && !withKeyNextMission.Equals(gameManager.currentMission))
                    {
                        gameManager.CompleteCurrentMission();
                    }
                }
            }
        }
        else
        {
            _showingText = false;
        }
    }

    /// <summary>
    /// Do not enter the given entrance.
    /// </summary>
    public override void DoNotEnter()
    {
        HideDialogPanelAndWalk();
    }

    /// <summary>
    /// Enters the given entrance.
    /// </summary>
    public override void Enter()
    {
        HideDialogPanelAndWalk();
        SceneData.DestroyAllEnemiesInScene();
        foreach (GameObject player in SceneData.playerList)
        {
            if (player.GetPlayerControllerComponent().IsPlayerOne())
            {
                player.transform.position = playerOneSpawnPos;
            }
            else if (player.GetPlayerControllerComponent().IsPlayerTwo())
            {
                player.transform.position = playerTwoSpawnPos;
            }
        }
        gameManager.CompleteCurrentMission();
        SceneManager.LoadScene("Temple1stFloor");
    }

    /// <summary>
    /// Hides the dialog panel and allow the players and enemies to walk again
    /// </summary>
    private void HideDialogPanelAndWalk()
    {
        SceneData.shouldStop = false;
        _dialogPanel.Hide();
    }
}
