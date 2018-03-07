using System;
using UnityEngine.UI;

/// <summary>
/// Class responsible for managing the plyer HUD items.
/// </summary>
[Serializable]
public class PlayerHUD
{
    public Slider healthSlider;
    public Slider consumableSlider;
    public Image mainAbilityImage;
    public Image secondaryAbilityImage;
    public Image selectedImage;
}
