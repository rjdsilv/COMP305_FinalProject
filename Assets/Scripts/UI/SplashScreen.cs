using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SplashScreen : MonoBehaviour
{
    public RectTransform textLogo;
    public RectTransform imageLogo;
    //public Image imageLogo;

    private float startScale;
    private float endScale;
    private float increaseFactor = 0.025f;

	// Use this for initialization
	void Start ()
    {
        startScale = 0.25f;
        endScale = 1.5f;
        StartCoroutine(ScaleLogo());
	}

    IEnumerator ScaleLogo()
    {
        //while (startScale < endScale)
        //{
        //    textLogo.localScale = new Vector3(startScale, startScale, startScale);
        //    imageLogo.localScale = new Vector3(startScale, startScale, startScale);
        //    yield return new WaitForSeconds(increaseFactor);
        //    startScale += increaseFactor;
        //}
        while (startScale < endScale)
        {
            textLogo.localScale = new Vector3(startScale, startScale, startScale);
            imageLogo.localScale = new Vector3(startScale, startScale, startScale);
            yield return new WaitForSeconds(increaseFactor);
            startScale += increaseFactor + 0.02f;
        }
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("GameIntroduction");
    }
}
