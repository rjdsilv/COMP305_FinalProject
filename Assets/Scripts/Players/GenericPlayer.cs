using UnityEngine;
using UnityEngine.UI;

public class GenericPlayer
{
    public GameObject player;
    public Slider lifeHUD;

    protected BattlePlayerController _playerBattleController;
    protected PlayerAttributes _attributes;

    public virtual void Initialize()
    {
        _playerBattleController = player.GetComponent<BattlePlayerController>();
        _playerBattleController.Initialize();
        _attributes = _playerBattleController.GetAttributes();
        lifeHUD.maxValue = _attributes.GetLevelAttributes().MaxLife;
        lifeHUD.value = _attributes.CurrentLife;
    }
}
