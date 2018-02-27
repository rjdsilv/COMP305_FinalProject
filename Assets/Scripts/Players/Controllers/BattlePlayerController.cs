using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class that will control the player actions on a battle scene.
/// </summary>
public class BattlePlayerController : MonoBehaviour
{
    // Public properties declaration.
    public GenericAbility mainAbility;
    public GenericAbility secondaryAbility;
    public GameObject mainAbilityHUD;
    public GameObject secondaryAbilityHUD;

    private PlayerAttributes _attributes;                   // The player's attributes.
    private GenericAbility _selectedAbility;                // The player's selected ability.
    private Outline _mainAbilityOutline;                    // The player's main ability HUD outlie.
    private Outline _secondaryAbilityOutline;               // The player's secondary ability HUD outlie.
    private Vector2 _outlineDistance = new Vector2(3, 3);   // The images outline distance;

    void LateUpdate()
    {
        SwapAbility();
    }

    /// <summary>
    /// Initializes the player for being used on the battle. The attributes are initialized getting what was stored
    /// in the SceneSwitcherDataHandlerObject.
    /// </summary>
    public BattlePlayerController Initialize()
    {
        _attributes = SceneSwitchDataHandler.GetPlayer(transform.name).Attributes;
        _attributes.CurrentLife = _attributes.GetLevelAttributes().MaxLife / 2;
        _selectedAbility = mainAbility;
        InitializeHUD();
        return this;
    }

    /// <summary>
    /// Gets the player attributes to be used on the battle.
    /// </summary>
    /// <returns></returns>
    public PlayerAttributes GetAttributes()
    {
        return _attributes;
    }

    void SwapAbility()
    {
        if ((ControlUtils.SwapAbility() > 0) && (_selectedAbility == mainAbility))
        {
            _selectedAbility = secondaryAbility;
            _secondaryAbilityOutline.enabled = true;
            _mainAbilityOutline.enabled = false;
        }
        else if ((ControlUtils.SwapAbility() < 0) && (_selectedAbility == secondaryAbility))
        {
            _selectedAbility = mainAbility;
            _secondaryAbilityOutline.enabled = false;
            _mainAbilityOutline.enabled = true;
        }
    }

    void InitializeHUD()
    {
        // Main ability.
        _mainAbilityOutline = mainAbilityHUD.AddComponent<Outline>();
        _mainAbilityOutline.effectColor = Color.black;
        _mainAbilityOutline.effectDistance = _outlineDistance;

        // Secondary ability.
        _secondaryAbilityOutline = secondaryAbilityHUD.AddComponent<Outline>();
        _secondaryAbilityOutline.effectColor = Color.black;
        _secondaryAbilityOutline.effectDistance = _outlineDistance;
        _secondaryAbilityOutline.enabled = false;
    }
}
