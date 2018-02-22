using UnityEngine;

/// <summary>
/// This class will be responsible for controlling the player.
/// </summary>
public class PlayerController : MonoBehaviour
{
    // Public properties declaration.
    public float speed;
    public bool hasMana;
    public bool hasStamina;

    // Private properties declaration.
    private AnimationState _currAnimState; // The player's current animation state.
    private Animator _animator;            // The player's rigid body.
    private Rigidbody2D _rigidBody;        // The player's animator.
    private PlayerAttributes _attributes;  // The player's attributes.

    /// <summary>
    /// Method being used for the player movement initialization.
    /// </summary>
    void Start ()
    {
        // Initalize the player objects.
        _animator = GetComponent<Animator>();
        _rigidBody = GetComponent<Rigidbody2D>();
        _currAnimState = AnimationState.STAND_RIGHT;
        _animator.Play(_currAnimState.AnimationName);
        InitializePlayer();
	}

    /// <summary>
    /// Method responsible for updating the each frame based on the user selected actions.
    /// </summary>
    void FixedUpdate()
    {
        MovePlayer();
    }

    /// <summary>
    /// Gets the player's attributes.
    /// </summary>
    /// <returns>The player's attributes.</returns>
    public PlayerAttributes GetAttributes()
    {
        return _attributes;
    }

    /// <summary>
    /// Method responsible for moving the player to one specific direction.
    /// </summary>
    void MovePlayer()
    {
        float horizontalMovement = Input.GetAxis("Horizontal");
        float verticalMovement = Input.GetAxis("Vertical");
        _rigidBody.velocity = new Vector2(horizontalMovement, verticalMovement) * speed;
        ChangePlayerAnimation(horizontalMovement, verticalMovement);
    }

    /// <summary>
    /// Initializes the player for being used on the game. The attributes are initialized depending on
    /// whether the DataHandler was already initialized or not.
    /// </summary>
    void InitializePlayer()
    {
        PlayerHolder player = SceneSwitchDataHandler.GetPlayer(transform.name);
        if (null == player)
        {
            _attributes = new PlayerAttributes();
        }
        else
        {
            _attributes = player.Attributes;
        }

        // If the attributes are not initialized yet, initialize it.
        if (!_attributes.IsInitialized)
        {
            LevelAttributes levelAttributes = _attributes.LevelDictionary.GetAttributeForLevel(_attributes.CurrentLevel);

            _attributes.IsInitialized = true;
            _attributes.CurrentGold = 0;
            _attributes.CurrentLife = levelAttributes.MaxLife;
            _attributes.HasMana = hasMana;
            _attributes.HasStamina = hasStamina;
            _attributes.CurrentXp = 0;

            if (_attributes.HasMana)
            {
                _attributes.CurrentMana = levelAttributes.MaxMana;
            }

            if (_attributes.HasStamina)
            {
                _attributes.CurrentStamina = levelAttributes.MaxStamina;
            }
        }

        SceneSwitchDataHandler.AddPlayer(transform.name, transform.position, _attributes);
    }

    /// <summary>
    /// Method responsible for changing the player animations based on the keys pressed and released.
    /// </summary>
    /// <param name="hMove">The value of the horizontal movement</param>
    /// <param name="vMove">The value of the vertical movement</param>
    void ChangePlayerAnimation(float hMove, float vMove)
    {
        // Movement animations.
        if (hMove > 0)
        {
            _currAnimState = AnimationState.WALKING_RIGHT;
        }
        else if (hMove < 0)
        {
            _currAnimState = AnimationState.WALKING_LEFT;
        }
        else if (vMove > 0)
        {
            _currAnimState = AnimationState.WALKING_UP;
        }
        else if (vMove < 0)
        {
            _currAnimState = AnimationState.WALKING_DOWN;
        }
        // Stand animations.
        else
        {
            switch (_currAnimState.FaceDirection)
            {
                case AnimationState.Direction.FACE_RIGHT:
                    _currAnimState = AnimationState.STAND_RIGHT;
                    break;

                case AnimationState.Direction.FACE_DOWN:
                    _currAnimState = AnimationState.STAND_DOWN;
                    break;

                case AnimationState.Direction.FACE_LEFT:
                    _currAnimState = AnimationState.STAND_LEFT;
                    break;

                case AnimationState.Direction.FACE_UP:
                    _currAnimState = AnimationState.STAND_UP;
                    break;
            }
        }

        // Plas the animation.
        _animator.Play(_currAnimState.AnimationName);
    }
}
