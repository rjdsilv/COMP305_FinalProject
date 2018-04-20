using UnityEngine;

public class FinalBossEntranceController : EntranceController {
    // The text that will be shown when the player enters the temple first time.
    private const string _noKeyText = "To open the door to the final boss, the 4 door keys are required.\n\nPress B to continue.";

    private const string _holdingKeyText = "You are about to face the final boss. Prepare your self and be ready to die in the fight! Are you sure you want to proceed?";

    private GameManager gameManager;

    // Public variable declaration.
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
            if (!_showingText && TagUtils.IsFinalBossDoor(_collisionObject.transform))
            {
                // Shows the dialog panel.
                LoadDialogPanel();
                _showingText = true;
                SceneData.shouldStop = true;
                if (SceneData.keys.Count < 5)
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

                if (SceneData.keys.Count == 5)
                {
                    while (!withKeyNextMission.Equals(gameManager.currentMission))
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
        Destroy(_collisionObject.gameObject);
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
