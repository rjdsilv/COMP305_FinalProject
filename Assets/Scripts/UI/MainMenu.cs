using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

    public GameObject loadingScreen;
    public Slider slider;
    public Text progressText;

	public void PlayGame()
    {
        //Loading Forest Scene
        //SceneManager.LoadScene("ForestMain");
        //SceneManager.LoadSceneAsync("ForestMain");
        StartCoroutine(LoadingScene());
    }

    IEnumerator LoadingScene()
    {
        //Asynchronously Loading the Scene
        AsyncOperation operation = SceneManager.LoadSceneAsync("ForestMain");

        //Enabling LoadingScreen Panel
        loadingScreen.SetActive(true);

        //While the Scene is Loading in Bg, displaying the Loading Percentage
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);

            //Rounding to 2 decimal Place
            progress = Mathf.Round(progress * 100f) / 100f;

            //Updating the Slider according to Progress
            slider.value = progress;
            
            //Displaying Progress Percentage
            progressText.text = progress * 100f + " %";
            yield return null;
        }
    }

    public void QuitGame()
    {
        Application.Quit();
        //EditorApplication.Exit(0);
    }

}
