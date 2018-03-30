using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

/// <summary>
/// Class responsible for managing the DialogPanel that will be used on the scenes.
/// </summary>
public class DialogPanel
{
    public static GameObject dialogPanel;
    public static Button[] dialogPanelButtons;

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
        DialogPanel.dialogPanel = dialogPanel;
        dialogPanelButtons = dialogPanel.GetComponentsInChildren<Button>();
    }

    /// <summary>
    /// Loads the dialog panel data to be used.
    /// </summary>
    public void Load(UnityAction yesCall, UnityAction noCall)
    {
        dialogPanel.SetActive(true);
        _dialogText = dialogPanel.GetComponentInChildren<Text>();

        if (null != yesCall)
        {
            dialogPanelButtons[YES_IDX].onClick.AddListener(yesCall);
        }

        if (null != noCall)
        {
            dialogPanelButtons[NO_IDX].onClick.AddListener(noCall);
        }
    }

    /// <summary>
    /// Hides the dialog panel from the scene.
    /// </summary>
    public void Hide()
    {
        dialogPanel.SetActive(false);
    }

    /// <summary>
    /// Displays the given message showing or not the buttons depending on the parameters passed.
    /// </summary>
    /// <param name="message">The message to be displayed.</param>
    /// <param name="showButtons">The flag indicating if the buttons should be shown or not.</param>
    public void DisplayMessage(string message, bool[] showButtons)
    {
        dialogPanel.SetActive(true);
        _dialogText.text = message;

        for (int i = 0; i < dialogPanelButtons.Length; i++)
        {
            dialogPanelButtons[i].gameObject.SetActive(showButtons[i]);
        }
    }
}
