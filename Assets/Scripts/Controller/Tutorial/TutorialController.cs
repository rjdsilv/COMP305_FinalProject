using UnityEngine;

/// <summary>
/// Class used to control 
/// </summary>
public class TutorialController : MonoBehaviour
{
    // TODO Create a mission system.
    private const string _gameTutorialText_P1 = "Welcome to The Shrine of Ehernal Destinies. Use the keyboard's " +
        "left, right, up, and down arrow keys to move you character. Your 1st mission is to find the temple. " +
        "Once you find it, kill the boss to win the game.\n\nPress B when you are ready to begin.";

    private const string _gameTutorialText_P2 = "Welcome to The Shrine of Ehernal Destinies. Use your XBox 360 / One Controller DPAD " +
        "to move you character. Your 1st mission is to find the temple. " +
        "Once you find it, kill the boss to win the game.\n\nPress B when you are ready to begin.";

    private const string _battleTutorialText = "To battle, select the enemy to attack using the up and down arrow keys. Then, " +
        "choose the ability to use using the left and right arrow keys. To attack, press the space bar." +
        "\n\nPress B when you are ready to begin.";

    public GameObject dialogPanelPlayerOne;
    public GameObject dialogPanelPlayerTwo;

    private DialogPanel _dialogPanelPlayerOne = null;
    private DialogPanel _dialogPanelPlayerTwo = null;

    /// <summary>
    /// Listens for the escape key to hide the tutorial panel.
    /// </summary>
    private void Update()
    {
        if (ControlUtils.ButtonB(1) || ControlUtils.ButtonB(2))
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
        _dialogPanelPlayerOne.Hide();
        if (SceneData.numberOfPlayers == 2 && null != _dialogPanelPlayerTwo)
        {
            _dialogPanelPlayerTwo.Hide();
        }
        SceneData.shouldStop = SceneData.isInBattle;
    }

    /// <summary>
    /// Loads the Dialog panel that will be used to show the text for specific game actions.
    /// </summary>
    private void LoadDialogPanel()
    {
        if (null == _dialogPanelPlayerOne)
        {
            _dialogPanelPlayerOne = new DialogPanel(dialogPanelPlayerOne);
            _dialogPanelPlayerOne.Load(null, null);
        }

        if (SceneData.numberOfPlayers == 2 && null == _dialogPanelPlayerTwo)
        {
            _dialogPanelPlayerTwo = new DialogPanel(dialogPanelPlayerTwo);
            _dialogPanelPlayerTwo.Load(null, null);
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
            _dialogPanelPlayerOne.DisplayMessage(_gameTutorialText_P1, new bool[] { false, false });
            if (SceneData.numberOfPlayers == 2 && null != _dialogPanelPlayerTwo)
            {
                _dialogPanelPlayerTwo.DisplayMessage(_gameTutorialText_P2, new bool[] { false, false });
            }
        }
        else
        {
            _dialogPanelPlayerOne.DisplayMessage(_battleTutorialText, new bool[] { false, false });
            if (SceneData.numberOfPlayers == 2 && null != _dialogPanelPlayerTwo)
            {
                _dialogPanelPlayerTwo.DisplayMessage(_battleTutorialText, new bool[] { false, false });
            }
        }
    }
}
