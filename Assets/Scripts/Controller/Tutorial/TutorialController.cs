using UnityEngine;

/// <summary>
/// Class used to control 
/// </summary>
public class TutorialController : MonoBehaviour
{
    private const string _gameTutorialText = "Welcome to The Shrine of Ehernal Destinies. Use the keyboard's " +
        "left, right, up, and down arrow keys to move you character.\n\nPress ESC when you are ready to begin.";

    private const string _battleTutorialText = "To battle, select the enemy to attack using the up and down arrow keys. Then, " +
        "choose the ability to use using the left and right arrow keys. To attack, press the space bar." +
        "\n\nPress ESC when you are ready to begin.";

    public GameObject dialogPanel;

    private DialogPanel _dialogPanel = null;

    /// <summary>
    /// Listens for the escape key to hide the tutorial panel.
    /// </summary>
    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            HideTutorial();
        }
    }

    /// <summary>
    /// Shows the tutorial for the player.
    /// </summary>
    public void ShowTutorial()
    {
        LoadDialogPanel();
        DisplayTutorialMessage();
    }

    public void HideTutorial()
    {
        LoadDialogPanel();
        _dialogPanel.Hide();
        SceneData.shouldStop = SceneData.isInBattle;
    }

    /// <summary>
    /// Loads the Dialog panel that will be used to show the text for specific game actions.
    /// </summary>
    private void LoadDialogPanel()
    {
        if (null == _dialogPanel)
        {
            _dialogPanel = new DialogPanel(dialogPanel);
            _dialogPanel.Load(null, null);
        }
    }

    /// <summary>
    /// Displays the tutorial message for the player.
    /// </summary>
    private void DisplayTutorialMessage()
    {
        // Shows the dialog panel.
        SceneData.shouldStop = true;

        if (!SceneData.isInBattle)
        {
            _dialogPanel.DisplayMessage(_gameTutorialText, new bool[] { false, false });
        }
        else
        {
            _dialogPanel.DisplayMessage(_battleTutorialText, new bool[] { false, false });
        }
    }
}
