using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class responsible for managing the HUD on the screen.
/// </summary>
public class HUDManager : MonoBehaviour
{
    // Constant declaration.
    private const int HUD_CANVAS_IDX = 0;
    private const string TURN_TEXT = "Turn {0} - {1}";

    // Public variable declaration.
    public Text turnText;                       // The turn text to be displayed.
    public Slider turnTimer;                    // The turn timer to be managed.
    public PlayerHUD mageHUD;                   // The mage HUD to be managed.
    public PlayerHUD thiefHUD;                  // The thief HUD to be managed.

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
            // TODO remove null check when multiplayer is done.
            if (null != player)
            {
                if (player.IsMage())
                {
                    InitializeMageHUD(player);
                }
                else if (player.IsThief())
                {
                    InitializeThiefHUD(player);
                }
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
    public void DisplayBattleReport(int goldEarned, int xpEarned, int plyerLevel)
    {
        turnText.text = string.Format("\t\t\t\t\t\t\t\tBattle Report\n" +
            "\t\t\t\t\t\t\t\t\t\t\t- Gold Earned: {0}\n" +
            "\t\t\t\t\t\t\t\t\t\t\t- XP Earned: {1}\n" +
            "\t\t\t\t\t\t\t\t\t\t\t- Player Level: {2}", goldEarned, xpEarned, plyerLevel);
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
    public void SwapAbility(GameObject player, ActorAbility ability)
    {
        if (player.IsMage())
        {
            SwapMageAbility(ability);
        }
        else if (player.IsThief())
        {
            SwapThiefAbility(ability);
        }
    }

    /// <summary>
    /// Updates the player's healh HUD to the new value after an attack.
    /// </summary>
    /// <param name="player">The player to have the HUD updated.</param>
    /// <param name="healthDrained">The amount of health drained.</param>
    public void DecreaseHealthHUD(GameObject player, int healthDrained)
    {
        if (player.IsMage())
        {
            GetHUDHealthText(player).text = "-" + healthDrained.ToString();
            DecreaseMageHealthHUD(healthDrained);
        }
        else if (player.IsThief())
        {
            GetHUDHealthText(player).text = "-" + healthDrained.ToString();
            DecreaseThiefHealthHUD(healthDrained);
        }
    }

    /// <summary>
    /// Updates the player's consumable HUD to the new value after an attack.
    /// </summary>
    /// <param name="player">The player to have the HUD updated.</param>
    /// <param name="amount">The amount of consumable to be update.</param>
    /// <param name="decrease">Indicates if the amount should be decreased or increased.</param>
    public void UpdateConsumableHUD(GameObject player, float amount, bool decrease)
    {
        if (player.IsMage())
        {
            UpdateMageConsumableHUD(amount, decrease);
        }
        else if (player.IsThief())
        {
            UpdateThiefConsumableHUD(amount, decrease);
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
    /// Initializes the thief's HUD as long as it's on the plyaers list.
    /// </summary>
    /// <param name="thief">The thief to have the HUD initialized.</param>
    private void InitializeThiefHUD(GameObject thief)
    {
        ThiefController thiefController = thief.GetComponent<ThiefController>();

        // Initialize the health.
        thiefHUD.healthSlider.minValue = 0;
        thiefHUD.healthSlider.maxValue = thiefController.levelTree.GetAttributesForCurrentLevel().health;
        thiefHUD.healthSlider.value = thiefController.attributes.health;

        // Initialize the mana.
        thiefHUD.consumableSlider.minValue = 0;
        thiefHUD.consumableSlider.maxValue = thiefController.levelTree.GetAttributesForCurrentLevel().stamina;
        thiefHUD.consumableSlider.value = thiefController.attributes.stamina;

        // Initializes the abilities.
        thiefHUD.mainAbilityImage.gameObject.GetComponent<Outline>().enabled = true;
        thiefHUD.selectedImage = thiefHUD.mainAbilityImage;
    }

    /// <summary>
    /// Method in charge of swapping the mage's ability on the HUD.
    /// </summary>
    private void SwapMageAbility(ActorAbility ability)
    {
        mageHUD.selectedImage.gameObject.GetComponent<Outline>().enabled = false;

        if (ability is FireBall)
        {
            mageHUD.selectedImage = mageHUD.mainAbilityImage;
        }
        else if (ability is LightningBall)
        {
            mageHUD.selectedImage = mageHUD.secondaryAbilityImage;
        }

        mageHUD.selectedImage.gameObject.GetComponent<Outline>().enabled = true;
    }

    /// <summary>
    /// Method in charge of swapping the thief's ability on the HUD.
    /// </summary>
    private void SwapThiefAbility(ActorAbility ability)
    {
        thiefHUD.selectedImage.gameObject.GetComponent<Outline>().enabled = false;

        if (ability is Dagger)
        {
            thiefHUD.selectedImage = thiefHUD.mainAbilityImage;
        }
        else if (ability is Bow)
        {
            thiefHUD.selectedImage = thiefHUD.secondaryAbilityImage;
        }

        thiefHUD.selectedImage.gameObject.GetComponent<Outline>().enabled = true;
    }

    /// <summary>
    /// Updates the mage health HUD.
    /// </summary>
    /// <param name="healthDrained">The amount of health to be drained.</param>
    private void DecreaseMageHealthHUD(int healthDrained)
    {
        mageHUD.healthSlider.value -= healthDrained;
    }


    /// <summary>
    /// Updates the thief health HUD.
    /// </summary>
    /// <param name="healthDrained">The amount of health to be drained.</param>
    private void DecreaseThiefHealthHUD(int healthDrained)
    {
        thiefHUD.healthSlider.value -= healthDrained;
    }

    /// <summary>
    /// Updates the mage's consumable HUD to the new value after an attack.
    /// </summary>
    /// <param name="amount">The amount of consumable to be update.</param>
    /// <param name="decrease">Indicates if the amount should be decreased or increased.</param>
    private void UpdateMageConsumableHUD(float amount, bool decrease)
    {
        if (decrease)
        {
            mageHUD.consumableSlider.value -= amount;
        }
        else
        {
            mageHUD.consumableSlider.value += amount;
        }
    }

    /// <summary>
    /// Updates the thief's consumable HUD to the new value after an attack.
    /// </summary>
    /// <param name="amount">The amount of consumable to be update.</param>
    /// <param name="decrease">Indicates if the amount should be decreased or increased.</param>
    private void UpdateThiefConsumableHUD(float amount, bool decrease)
    {
        if (decrease)
        {
            thiefHUD.consumableSlider.value -= amount;
        }
        else
        {
            thiefHUD.consumableSlider.value += amount;
        }
    }

    /// <summary>
    /// Returns the enemy HUD canvas to be used.
    /// </summary>
    /// <returns>The enemy HUD canvas to be used.</returns>
    private Canvas GetHUDCanvas(GameObject player)
    {
        return player.transform.GetChild(HUD_CANVAS_IDX).GetComponent<Canvas>();
    }

    /// <summary>
    /// Returns the enemy HUD canvas health text to be used.
    /// </summary>
    /// <returns>The enemy HUD canvas health text to be used.</returns>
    private Text GetHUDHealthText(GameObject player)
    {
        return GetHUDCanvas(player).GetComponent<RectTransform>().GetChild(0).GetComponent<Text>();
    }
}
