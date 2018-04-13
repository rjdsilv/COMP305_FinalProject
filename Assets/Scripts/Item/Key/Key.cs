using UnityEngine;

public class Key : MonoBehaviour
{
    public void UseKey()
    {
        SceneData.keys.Add(this);
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().CompleteCurrentMission();
        DontDestroyOnLoad(this);
        gameObject.SetActive(false);
    }
}
