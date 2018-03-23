/// <summary>
/// Class responsible for managing level tree for any player on the game.
/// </summary>
/// <typeparam name="A"></typeparam>
public class PlayerLevelTree<A> : ActorLevelTree<A>
    where A : PlayerAttributes
{
    public int GetMinXpForNextLevel()
    {
        return levelAttributes[currentLevel - 1].xp;
    }
}
