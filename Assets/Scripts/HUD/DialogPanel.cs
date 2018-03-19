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
    private GameObject dialogPanel;
    private Button[] dialogPanelButtons;
    private Text dialogText;

    /// <summary>
    /// Loads the dialog panel data to be used.
    /// </summary>
    public void Load(UnityAction yesCall, UnityAction noCall)
    {
        if (!SceneData.isInBattle)
        {
            if (null == dialogPanel)
            {
                dialogPanel = TagUtils.FindDialogPanel();
                dialogText = dialogPanel.GetComponentInChildren<Text>();
                dialogPanelButtons = dialogPanel.GetComponentsInChildren<Button>();
                dialogPanelButtons[YES_IDX].onClick.AddListener(yesCall);
                dialogPanelButtons[NO_IDX].onClick.AddListener(noCall);
                dialogPanel.SetActive(false);
            }
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
    public void DisplayMessage(string message, bool showButtons)
    {
        dialogPanel.SetActive(true);
        dialogText.text = message;

        foreach (Button b in dialogPanelButtons)
        {
            b.gameObject.SetActive(showButtons);
        }
    }
}
