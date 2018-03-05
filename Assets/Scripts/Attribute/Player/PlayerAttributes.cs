/// <summary>
/// Class responsible for storing extra attributes for the players.
/// </summary>
public class PlayerAttributes : ActorAttributes
{
    public int xp;              // The current player's amount of XP or the amount of XP needed to increase level in case of level tree.
    public int gold;            // The amount of gold the player is carrying or the maximum amount of gold in case of level tree.
    public float mana;            // The mana that the player currently has.
    public float stamina;         // The stamina that the player currently has.
}
