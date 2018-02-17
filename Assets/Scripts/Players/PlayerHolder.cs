using UnityEngine;

public class PlayerHolder
{
    public PlayerHolder(string name, Vector3 position)
    {
        Name = name;
        Position = position;
    }

    public string Name { get; set; }

    public Vector3 Position { get; set; }
}
