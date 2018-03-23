/// <summary>
/// Interface containing the contract for Usable Items.
/// </summary>
public interface IUsable
{
    /// <summary>
    /// Will apply the given usable into some player.
    /// </summary>
    /// <param name="playerController">The player to which the item will be applied.</param>
    void Use(IPlayerController playerController);
}
