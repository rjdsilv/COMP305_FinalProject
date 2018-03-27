using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

/// <summary>
/// Class responsible for managing the DialogPanel that will be used on the scenes.
/// </summary>
public class DialogPanel
{
    // Constant declaration.
    private const int YES_IDX = 0;
    private const int NO_IDX = 1;

    // Private variable declaration.
    private GameObject _dialogPanel;
    private Button[] _dialogPanelButtons;
    private Text _dialogText;

    /// <summary>
    /// Loads the dialog panel data to be used.
    /// </summary>
    public void Load(UnityAction yesCall, UnityAction noCall)
    {
        if (!SceneData.isInBattle)
        {
            if (null == _dialogPanel)
            {
                _dialogPanel = TagUtils.FindDialogPanel();
                _dialogText = _dialogPanel.GetComponentInChildren<Text>();
                _dialogPanelButtons = _dialogPanel.GetComponentsInChildren<Button>();
                _dialogPanelButtons[YES_IDX].onClick.AddListener(yesCall);
                _dialogPanelButtons[NO_IDX].onClick.AddListener(noCall);
                _dialogPanel.SetActive(false);
            }
        }
    }

    /// <summary>
    /// Hides the dialog panel from the scene.
    /// </summary>
    public void Hide()
    {
        _dialogPanel.SetActive(false);
    }

    /// <summary>
    /// Displays the given message showing or not the buttons depending on the parameters passed.
    /// </summary>
    /// <param name="message">The message to be displayed.</param>
    /// <param name="showButtons">The flag indicating if the buttons should be shown or not.</param>
    public void DisplayMessage(string message, bool showButtons)
    {
        _dialogPanel.SetActive(true);
        _dialogText.text = message;

        foreach (Button b in _dialogPanelButtons)
        {
            b.gameObject.SetActive(showButtons);
        }
    }
}
