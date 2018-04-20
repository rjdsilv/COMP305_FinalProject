using UnityEngine;

public class ItemCollectorController : MonoBehaviour
{
    // Public variable declaration
    public float itemCheckRadius;
    public LayerMask itemMask;

    // Protected variable declaration
    protected Collider2D _collisionObject;
    protected GameManager _gameManager;

    /// <summary>
    /// Draw the player overlaping circle to make life easier when debugging.
    /// </summary>
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, itemCheckRadius);
    }

    /// <summary>
    /// Initializes the controller.
    /// </summary>
    private void Start()
    {
        _gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

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
        _collisionObject = Physics2D.OverlapCircle(transform.position, itemCheckRadius, itemMask);
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
            else if (TagUtils.IsKey(_collisionObject.transform))
            {
                UseKey();
            }
        }
    }

    private void UseHealthPot()
    {
        // Applies the health for both players if in one player mode.
        if (SceneData.numberOfPlayers == 1)
        {
            foreach (GameObject player in SceneData.playerList)
            {
                ApplyHealth(player);
            }
        }
        else
        {
            ApplyHealth(gameObject);
        }
    }

    private void ApplyHealth(GameObject player)
    {
        if (player.IsMage())
        {
            _collisionObject.gameObject.GetComponent<HealthPot>().Use(player.GetComponent<MageController>());
        }
        else if (player.IsThief())
        {
            _collisionObject.gameObject.GetComponent<HealthPot>().Use(player.GetComponent<ThiefController>());
        }
        _gameManager.UpdateHUD(player);
    }

    private void UseConsumablePot()
    {
        if (gameObject.IsMage())
        {
            UseConsumableOnMage();
        }
        else if (gameObject.IsThief())
        {
            UseConsumableOnThief();
        }
        _gameManager.UpdateHUD(gameObject);
    }

    private void UseKey()
    {
        _collisionObject.gameObject.GetComponent<Key>().UseKey();
    }

    private void UseConsumableOnThief()
    {
        ConsumablePot pot = _collisionObject.gameObject.GetComponent<ConsumablePot>();
        if (ConsumableType.STAMINA == pot.consumableType)
        {
            pot.Use(gameObject.GetComponent<ThiefController>());
        }
        else
        {
            pot.Destroy();
        }
    }

    private void UseConsumableOnMage()
    {
        ConsumablePot pot = _collisionObject.gameObject.GetComponent<ConsumablePot>();
        if (ConsumableType.MANA == pot.consumableType)
        {
            pot.Use(gameObject.GetComponent<MageController>());
        }
        else
        {
            if (SceneData.numberOfPlayers == 1)
            {
                foreach (GameObject player in SceneData.playerList)
                {
                    if (player.IsThief())
                    {
                        pot.Use(player.GetComponent<ThiefController>());
                    }
                }
            }
            else
            {
                pot.Destroy();
            }
        }
    }
}
