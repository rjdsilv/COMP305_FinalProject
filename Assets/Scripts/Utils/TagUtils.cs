using UnityEngine;

public class TagUtils
{
    /// <summary>
    /// Method that indicates if the object in question is a player.
    /// </summary>
    /// <param name="detectedObject">The object to be checked.</param>
    /// <returns><b>true</b> if the object detected is the player. <b>false</b> otherwise.</returns>
    public static bool IsPlayer(Transform detectedObject)
    {
        return detectedObject.tag == "Player";
    }

    /// <summary>
    /// Method that indicates if the object in question is a camera.
    /// </summary>
    /// <param name="detectedObject">The object to be checked.</param>
    /// <returns><b>true</b> if the object detected is the player. <b>false</b> otherwise.</returns>
    public static bool IsCamera(Transform detectedObject)
    {
        return detectedObject.tag == "MainCamera";
    }

    /// <summary>
    /// Method that indicates if the object in question is an enemy.
    /// </summary>
    /// <param name="detectedObject">The object to be checked.</param>
    /// <returns><b>true</b> if the object detected is the player. <b>false</b> otherwise.</returns>
    public static bool IsEnemy(Transform detectedObject)
    {
        return detectedObject.tag == "Enemy";
    }

    /// <summary>
    /// Method to verify if the object seen by the enemy is a sector edge.
    /// </summary>
    /// <param name="detectedObject">The object to be checked.</param>
    /// <returns><b>true</b> if the object is either a scenario object. <b>false</b>otherwise.</returns>
    public static bool IsScenarioObject(Transform detectedObject)
    {
        return detectedObject.tag == "ScenarioObject";
    }

    /// <summary>
    /// Method to verify if the object seen by the enemy is a sector edge.
    /// </summary>
    /// <param name="detectedObject">The object to be checked.</param>
    /// <returns><b>true</b> if the object is either a sector edge. <b>false</b>otherwise.</returns>
    public static bool IsSectorEdge(Transform detectedObject)
    {
        return detectedObject.tag == "SectorEdge";
    }

    /// <summary>
    /// Method to verify if the object seen by the enemy is a sector.
    /// </summary>
    /// <param name="detectedObject">The object to be checked.</param>
    /// <returns><b>true</b> if the object is either a sector. <b>false</b>otherwise.</returns>
    public static bool IsSector(Transform detectedObject)
    {
        return detectedObject.tag == "Sector";
    }

    /// <summary>
    /// Method to verify if the object overlaping the player circle is an entrance.
    /// </summary>
    /// <param name="detectedObject">The object to be checked.</param>
    /// <returns><b>true</b> if the object overlaping the player circle is an entrance. <b>false</b>otherwise.</returns>
    public static bool IsTemple(Transform detectedObject)
    {
        return detectedObject.tag == "Temple";
    }

    /// <summary>
    /// Method to verify if the object overlaping the player circle is a health pot.
    /// </summary>
    /// <param name="detectedObject">The object to be checked.</param>
    /// <returns><b>true</b> if the object overlaping the player circle is a health pot. <b>false</b>otherwise.</returns>
    public static bool IsHealthPot(Transform detectedObject)
    {
        return detectedObject.tag == "HealthPot";
    }

    /// <summary>
    /// Method to verify if the object overlaping the player circle is a consumable pot.
    /// </summary>
    /// <param name="detectedObject">The object to be checked.</param>
    /// <returns><b>true</b> if the object overlaping the player circle is a consumable pot. <b>false</b>otherwise.</returns>
    public static bool IsConsumablePot(Transform detectedObject)
    {
        return detectedObject.tag == "ConsumablePot";
    }

    /// <summary>
    /// Searches the world for the first occurrence of a player in it.
    /// </summary>
    /// <returns>The first occurrence of a player in the game world.</returns>
    public static GameObject FindOnePlayer()
    {
        return GameObject.FindGameObjectWithTag("Player");
    }

    /// <summary>
    /// Searches the world for all the occurrence of a player in it.
    /// </summary>
    /// <returns>All the occurrences of aplayers in the game world.</returns>
    public static GameObject[] FindAllPlayers()
    {
        return GameObject.FindGameObjectsWithTag("Player");
    }

    /// <summary>
    /// Searches the world for the object with the DialogPanel tag.
    /// </summary>
    /// <returns>The world for the object with the DialogPanel tag</returns>
    public static GameObject FindDialogPanel()
    {
        return GameObject.FindGameObjectWithTag("DialogPanel");
    }
}
