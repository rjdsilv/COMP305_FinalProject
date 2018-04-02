using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour
{
    public RectTransform textLogo;

    private float startScale;
    private float endScale;
    private float increaseFactor = 0.025f;

	// Use this for initialization
	void Start ()
    {
        startScale = 0.25f;
        endScale = 2f;
        StartCoroutine(ScaleLogo());
	}

    IEnumerator ScaleLogo()
    {
        while (startScale < endScale)
        {
            textLogo.localScale = new Vector3(startScale, startScale, startScale);
            yield return new WaitForSeconds(increaseFactor);
            startScale += increaseFactor;
        }
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("GameIntroduction");
    }
}
