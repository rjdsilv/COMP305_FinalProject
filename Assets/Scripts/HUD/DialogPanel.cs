using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

/// <summary>
/// Class responsible for managing the DialogPanel that will be used on the scenes.
/// </summary>
public class DialogPanel
{
    private static Button[] _dialogPanelButtons;

    private static GameObject _dialogPanel;

    // Constant declaration.
    private const int YES_IDX = 0;
    private const int NO_IDX = 1;

    // Private variable declaration.
    private Text _dialogText;

    public DialogPanel()
    {
    }

    public DialogPanel(GameObject dialogPanel)
    {
        _dialogPanel = dialogPanel;
        _dialogPanelButtons = dialogPanel.GetComponentsInChildren<Button>();
    }

    /// <summary>
    /// Loads the dialog panel data to be used.
    /// </summary>
    public void Load(UnityAction yesCall, UnityAction noCall)
    {
        _dialogPanel.SetActive(true);
        _dialogText = _dialogPanel.GetComponentInChildren<Text>();

        if (null != yesCall)
        {
            _dialogPanelButtons[YES_IDX].onClick.AddListener(yesCall);
        }

        if (null != noCall)
        {
            _dialogPanelButtons[NO_IDX].onClick.AddListener(noCall);
        }
    }

    /// <summary>
    /// Hides the dialog panel from the scene.
    /// </summary>
    public void Hide()
    {
        if (null != _dialogPanel)
        {
            _dialogPanel.SetActive(false);
        }
    }

    /// <summary>
    /// Displays the given message showing or not the buttons depending on the parameters passed.
    /// </summary>
    /// <param name="message">The message to be displayed.</param>
    /// <param name="showButtons">The flag indicating if the buttons should be shown or not.</param>
    public void DisplayMessage(string message, bool[] showButtons)
    {
        _dialogPanel.SetActive(true);
        _dialogText.text = message;

        for (int i = 0; i < _dialogPanelButtons.Length; i++)
        {
            _dialogPanelButtons[i].gameObject.SetActive(showButtons[i]);
        }
    }
}
