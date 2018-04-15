using UnityEngine;

public class Key : MonoBehaviour
{
    public string keyName;

    public virtual void UseKey()
    {
        if (!SceneData.keys.ContainsKey(keyName))
        {
            SceneData.keys.Add(keyName, this);
        }

        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().CompleteCurrentMission();
        DontDestroyOnLoad(this);
        gameObject.SetActive(false);
    }
}
