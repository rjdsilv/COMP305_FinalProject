using UnityEngine;

/// <summary>
/// Class used to control 
/// </summary>
public class TutorialController : MonoBehaviour
{
    // TODO Create a mission system.
    private const string _gameTutorialText_P1 = "Welcome to The Shrine of Ehernal Destinies. Use the keyboard's " +
        "left, right, up, and down arrow keys to move you character.\n\nPress B when you are ready to begin.";

    private const string _gameTutorialText_P2 = "Welcome to The Shrine of Ehernal Destinies. Use your XBox 360 / One Controller DPAD " +
        "to move you character.\n\nPress B when you are ready to begin.";

    private const string _battleTutorialText_P1 = "To battle, select the enemy to attack using the up and down arrow keys. Then, " +
        "choose the ability to use using the left and right arrow keys. To attack, press the space bar." +
        "\n\nPress B when you are ready to begin.";

    private const string _battleTutorialText_P2 = "To battle, select the enemy to attack using the RB and LB buttons. Then, " +
        "choose the ability to use using the RT and LT buttons. To attack, press the A button." +
        "\n\nPress B when you are ready to begin.";

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
    /// Resets all the tutorial panels.
    /// </summary>
    public void ResetPanels()
    {
        _dialogPanelPlayerOne = null;
        _dialogPanelPlayerTwo = null;
    }

    /// <summary>
    /// Shows the tutorial for the player.
    /// </summary>
    public void ShowTutorial()
    {
        LoadDialogPanel();
        DisplayTutorialMessage();
    }

    /// <summary>
    /// Hides the tutorial from the player.
    /// </summary>
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
            _dialogPanelPlayerOne = new DialogPanel(GetDialogPanel(1));
            _dialogPanelPlayerOne.Load(null, null);
        }

        if (SceneData.numberOfPlayers == 2 && null == _dialogPanelPlayerTwo)
        {
            GameObject panelPlayerTwo = GetDialogPanel(2);
            if (null != panelPlayerTwo)
            {
                _dialogPanelPlayerTwo = new DialogPanel(panelPlayerTwo);
                _dialogPanelPlayerTwo.Load(null, null);
            }
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
            _dialogPanelPlayerOne.DisplayMessage(_battleTutorialText_P1, new bool[] { false, false });
            if (SceneData.numberOfPlayers == 2 && null != _dialogPanelPlayerTwo)
            {
                _dialogPanelPlayerTwo.DisplayMessage(_battleTutorialText_P1, new bool[] { false, false });
            }
        }
    }

    /// <summary>
    /// Gets the correct panel for a given player number.
    /// </summary>
    /// <param name="playerNumber">The player number to be used.</param>
    /// <returns>The correct panel.</returns>
    private GameObject GetDialogPanel(int playerNumber)
    {
        foreach (Camera c in Camera.allCameras)
        {
            if (playerNumber == 1 && (c.name == "Player_01" || c.name == "Main Camera"))
            {
                return c.transform.GetChild(0).GetChild(0).gameObject;
            }
            else if (playerNumber == 2 && c.name == "Player_02")
            {
                return c.transform.GetChild(0).GetChild(0).gameObject;
            }
        }

        return null;
    }
}
