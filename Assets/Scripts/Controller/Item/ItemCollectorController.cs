using UnityEngine;

public class ItemCollectorController : MonoBehaviour
{
    // Public variable declaration
    public float entranceCheckRadius;
    public LayerMask entranceMask;

    // Protected variable declaration
    protected Collider2D _collisionObject;

    /// <summary>
    /// This method will check if the player reached the temple entrance and perform the following actions:
    /// <ul>
    ///     <li>Show the temple entrance text.</li>
    ///     <li>Stop the player from moving.</li>
    ///     <li>Show the options to be chosen.</li>
    ///     <li>Wait for the player's decision.</li>
    /// </ul>
    /// </summary>
    private void Update()
    {
        _collisionObject = Physics2D.OverlapCircle(transform.position, entranceCheckRadius, entranceMask);
        if (null != _collisionObject)
        {
            if (TagUtils.IsHealthPot(_collisionObject.transform))
            {
                UseHealthPot();
            }
            else if (TagUtils.IsConsumablePot(_collisionObject.transform))
            {
                UseConsumablePot();
            }
        }
    }

    private void UseHealthPot()
    {
        if (gameObject.IsMage())
        {
            _collisionObject.gameObject.GetComponent<HealthPot>().Use(gameObject.GetComponent<MageController>());
        }
    }

    private void UseConsumablePot()
    {
        if (gameObject.IsMage())
        {
            ConsumablePot pot = _collisionObject.gameObject.GetComponent<ConsumablePot>();
            if (ConsumableType.MANA == pot.consumableType)
            {
                pot.Use(gameObject.GetComponent<MageController>());
            }
            else
            {
                pot.Destroy();
            }
        }
    }
}
