
using System;
using UnityEngine.UI;

[Serializable]
public class Mage : GenericPlayer
{
    public Slider manaHUD;

    public override void Initialize()
    {
        base.Initialize();
        manaHUD.maxValue = _attributes.GetLevelAttributes().MaxMana;
        manaHUD.value = _attributes.CurrentMana;
    }
}
