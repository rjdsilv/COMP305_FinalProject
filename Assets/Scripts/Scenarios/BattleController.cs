using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleController : MonoBehaviour
{
    public string originalScene;

	// Use this for initialization
	void Start ()
    {
        StartCoroutine(WaitAndReturn(5));
	}

    void LoadOriginalScene()
    {
        StartCoroutine(LoadOriginalSceneAsync());
    }

    IEnumerator WaitAndReturn(float time)
    {
        yield return new WaitForSeconds(time);
        SceneSwitchDataHandler.isComingBackFromBattle = true;
        LoadOriginalScene();
    }

    IEnumerator LoadOriginalSceneAsync()
    {
        AsyncOperation asyncSceneLoad = SceneManager.LoadSceneAsync(originalScene);

        // Wait 50ms to check again.
        while (!asyncSceneLoad.isDone)
        {
            yield return new WaitForSeconds(0.05f);
        }
    }
}
