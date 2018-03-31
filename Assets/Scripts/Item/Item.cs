using UnityEngine;

/// <summary>
/// Generic class for any type of item on the game.
/// </summary>
public abstract class Item : MonoBehaviour, IUsable
{
    public int power;       // The item's power.

    /// <see cref="IUsable"/>
    public abstract void Use(IPlayerController playerController);

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
