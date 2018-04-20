using UnityEngine;

public class TempleKey : Key
{
    public Mission nextMission;

    public override void UseKey()
    {
        GameManager gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();

        while (!gm.currentMission.Equals(nextMission))
        {
            gm.CompleteCurrentMission();
        }

        if (!SceneData.keys.ContainsKey(keyName))
        {
            SceneData.keys.Add(keyName, this);
        }
        DontDestroyOnLoad(this);
        gameObject.SetActive(false);
    }
}
