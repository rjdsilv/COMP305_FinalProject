using UnityEngine;
/// <summary>
/// Class representing the Health Pot attributes on the system.
/// </summary>
public class HealthPot : Item
{
    private float _singlePlayerRecoverFactor = 2.0f / 3.0f;

    /// <see cref="IUsable"/>
    public override void Use(IPlayerController playerController)
    {
        RestoreHealth(playerController);
    }

    /// <summary>
    /// Restores the health by the ammount of power configured for the health pot.
    /// </summary>
    /// <param name="playerController">The player to have the health restored.</param>
    private void RestoreHealth(IPlayerController playerController)
    {
        float useFactor = SceneData.numberOfPlayers == 1 ? _singlePlayerRecoverFactor : 1.0f;
        playerController.IncreaseHealth(Mathf.FloorToInt(_singlePlayerRecoverFactor * power));
        Destroy();
    }
}
