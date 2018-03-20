using UnityEngine;
using UnityEngine.SceneManagement;

public class TempleEntranceAI : EntranceAI
{
    // The text that will be shown when the player enters the temple first time.
    private string _dialogText = "You are about to enter the temple of Zydis. This is a temple of pure evilness and disgrace to those who enters it. " +
        "Are you sure you want to enter the temple and die?";

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
        LoadDialogPanel();
        _collisionObject = Physics2D.OverlapCircle(transform.position, entranceCheckRadius, entranceMask);
        if (null != _collisionObject)
        {
            if (!_showingText && TagUtils.IsTemple(_collisionObject.transform))
            {
                // Shows the dialog panel.
                _showingText = true;
                SceneData.shouldStop = true;
                _dialogPanel.DisplayMessage(_dialogText, true);
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
