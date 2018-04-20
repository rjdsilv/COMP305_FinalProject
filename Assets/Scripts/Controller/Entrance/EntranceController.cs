using System;
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
        if (null == _dialogPanel || _dialogPanel.IsNull())
        {
            _dialogPanel = new DialogPanel(GetDialogPanel());
        }
        _dialogPanel.Load(Enter, DoNotEnter);
    }

    private GameObject GetDialogPanel()
    {
        int playerNumber = GetPlayerNumber();

        foreach (Camera c in Camera.allCameras)
        {
            if (playerNumber == 1 && c.name == "Player_01")
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

    private int GetPlayerNumber()
    {
        if (gameObject.IsMage())
        {
            return gameObject.GetComponent<MageController>().playerNumber;
        }

        if (gameObject.IsThief())
        {
            return gameObject.GetComponent<ThiefController>().playerNumber;
        }

        return 1;
    }
}
