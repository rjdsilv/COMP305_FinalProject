using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class responsible for managing the HUD on the screen.
/// </summary>
public class HUDManager : MonoBehaviour
{
    // Public variable declaration.
    public PlayerHUD mageHUD;                   // The mage HUD to be managed.

    // Private variable declaration.
    private Image _selectedImage;

    /// <summary>
    /// Method to initialize the HUD for all the players on the battle scene.
    /// </summary>
    /// <param name="players">The list of players to have HUD initialized.</param>
    public void InitializePlayersHUD(params GameObject[] players)
    {
        foreach (GameObject player in players)
        {
            if (player.IsMage())
            {
                player.GetComponent<MageController>().attributes.health = 50;
                InitializeMageHUD(player);
            }
        }
    }

    /// <summary>
    /// Method in charge of swapping the ability on the HUD.
    /// </summary>
    public void SwapAbility()
    {
        _selectedImage.gameObject.GetComponent<Outline>().enabled = false;

        if (_selectedImage == mageHUD.mainAbilityImage)
            _selectedImage = mageHUD.secondaryAbilityImage;
        else
            _selectedImage = mageHUD.mainAbilityImage;

        _selectedImage.gameObject.GetComponent<Outline>().enabled = true;
    }

    /// <summary>
    /// Initializes the mage's HUD as long as it's on the plyaers list.
    /// </summary>
    /// <param name="mage">The mage to have the HUD initialized.</param>
    private void InitializeMageHUD(GameObject mage)
    {
        MageController mageController = mage.GetComponent<MageController>();

        // Initialize the health.
        mageHUD.healthSlider.minValue = 0;
        mageHUD.healthSlider.maxValue = mageController.levelTree.GetAttributesForCurrentLevel().health;
        mageHUD.healthSlider.value = mageController.attributes.health;

        // Initialize the mana.
        mageHUD.consumableSlider.minValue = 0;
        mageHUD.consumableSlider.maxValue = mageController.levelTree.GetAttributesForCurrentLevel().mana;
        mageHUD.consumableSlider.value = mageController.attributes.mana;

        // Initializes the abilities.
        mageHUD.mainAbilityImage.gameObject.GetComponent<Outline>().enabled = true;
        _selectedImage = mageHUD.mainAbilityImage;
    }
}
