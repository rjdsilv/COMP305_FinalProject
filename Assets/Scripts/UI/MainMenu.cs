using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

    public GameObject loadingScreen;
    public Slider slider;
    public Text progressText;

	public void PlayGameSinglePlayer()
    {
        SceneData.numberOfPlayers = 1;
        SceneData.chosenPlayers = new string[1];
        SceneData.chosenPlayers[0] = ActorUtils.MAGE;
        StartCoroutine(LoadingScene());
    }

    public void PlayGameSingleTwoPlayers()
    {
        SceneData.numberOfPlayers = 2;
        SceneData.chosenPlayers = new string[2];
        SceneData.chosenPlayers[0] = ActorUtils.MAGE;
        SceneData.chosenPlayers[1] = ActorUtils.THIEF;
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

    }
}
