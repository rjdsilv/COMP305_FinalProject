using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class responsible for managing the HUD on the screen.
/// </summary>
public class HUDManager : MonoBehaviour
{
    // Constant declaration.
    private const string TURN_TEXT = "Turn {0} - {1}";

    // Public variable declaration.
    public Text turnText;                       // The turn text to be displayed.
    public Slider turnTimer;                    // The turn timer to be managed.
    public PlayerHUD mageHUD;                   // The mage HUD to be managed.

    // Private variable declaration.
    private int _turnNumber = 1;

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
                InitializeMageHUD(player);
            }
        }
    }

    /// <summary>
    ///  Initializes the timer to the maximum value configured.
    /// </summary>
    /// <param name="value"></param>
    public void InitializeTurnTimer(int value)
    {
        turnTimer.minValue = 0;
        turnTimer.maxValue = value;
    }

    /// <summary>
    /// Updates the value for the turn timer based on the configured timer.
    /// </summary>
    /// <param name="value">The value to be used.</param>
    public void UpdateTurnTimer(float value)
    {
        turnTimer.value = value;
    }

    /// <summary>
    /// Displays the turn text on the screen.
    /// </summary>
    /// <param name="actorName">The actor name to be displayed.</param>
    public void DisplayTurnText(string actorName)
    {
        turnText.text = string.Format(TURN_TEXT, _turnNumber++, actorName);
    }

    /// <summary>
    /// Displays the text for the end of battle.
    /// </summary>
    public void DisplayEndOfBattleText()
    {
        turnText.text = "Enemies Killed!";
    }

    /// <summary>
    /// Displays the text for game over.
    /// </summary>
    public void DisplayGameOverText()
    {
        turnText.text = "Game Over";
    }

    /// <summary>
    /// Hides the turn text.
    /// </summary>
    public void HideTurnText()
    {
        turnText.text = "";
    }

    /// <summary>
    /// Method in charge of swapping the ability on the HUD.
    /// </summary>
    public void SwapAbility(GameObject player)
    {
        if (player.IsMage())
        {
            SwapMageAbility();
        }
    }

    /// <summary>
    /// Updates the player's healh HUD to the new value after an attack.
    /// </summary>
    /// <param name="player">The player to have the HUD updated.</param>
    /// <param name="healthDrained">The ammount of health drained.</param>
    public void DecreaseHealthHUD(GameObject player, int healthDrained)
    {
        if (player.IsMage())
        {
            DecreaseMageHealthHUD(healthDrained);
        }
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
        mageHUD.selectedImage = mageHUD.mainAbilityImage;
    }

    /// <summary>
    /// Method in charge of swapping the mage's ability on the HUD.
    /// </summary>
    private void SwapMageAbility()
    {
        mageHUD.selectedImage.gameObject.GetComponent<Outline>().enabled = false;

        if (mageHUD.selectedImage == mageHUD.mainAbilityImage)
            mageHUD.selectedImage = mageHUD.secondaryAbilityImage;
        else
            mageHUD.selectedImage = mageHUD.mainAbilityImage;

        mageHUD.selectedImage.gameObject.GetComponent<Outline>().enabled = true;
    }

    /// <summary>
    /// Updates the mage health HUD.
    /// </summary>
    /// <param name="healthDrained">The ammount of health to be drained.</param>
    private void DecreaseMageHealthHUD(int healthDrained)
    {
        mageHUD.healthSlider.value -= healthDrained;
    }
}
