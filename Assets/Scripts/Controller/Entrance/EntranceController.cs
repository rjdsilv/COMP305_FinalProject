using UnityEngine;

public abstract class EntranceController : MonoBehaviour
{
    // Public variable declaration
    public float entranceCheckRadius;
    public LayerMask entranceMask;

    // Protected variable declaration
    protected Collider2D _collisionObject;
    protected bool _showingText;
    protected DialogPanel _dialogPanel;

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
        if (null == _dialogPanel)
        {
            _dialogPanel = new DialogPanel();
        }

        _dialogPanel.Load(Enter, DoNotEnter);
    }
}
