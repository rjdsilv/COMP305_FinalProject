
/// <summary>
/// Interface containing commom methods for all player controllers.
/// </summary>
public interface IPlayerController
{
    /// <summary>
    /// Method that indicates whether a player is managed by AI or not.
    /// </summary>
    /// <returns><b>true</b> if player is managed by ai. <b>false</b> otherwise.</returns>
    bool IsManagedByAI();
}
