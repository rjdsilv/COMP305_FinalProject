using UnityEngine;
using UnityEngine.UI;

public abstract class EntranceAI : GenericAI
{
    // Public variable declaration
    public float entranceCheckRadius;
    public LayerMask entranceMask;

    // Protected variable declaration
    protected Collider2D collisionObject;
    protected bool showingText;
    protected DialogPanel dialogPanel;

    /// <summary>
    /// Draw the player overlaping circle to make life easier when debugging.
    /// </summary>
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, entranceCheckRadius);
    }

    /// <summary>
    /// Method responsible for entering the found entrance.
    /// </summary>
    public abstract void Enter();

    /// <summary>
    /// Method responsible for not entering the found entrance.
    /// </summary>
    public abstract void DoNotEnter();

    /// <summary>
    /// Loads the Dialog panel that will be used to show the text for specific game actions.
    /// </summary>
    protected void LoadDialogPanel()
    {
        if (null == dialogPanel)
        {
            dialogPanel = new DialogPanel();
        }

        dialogPanel.Load(Enter, DoNotEnter);
    }
}
