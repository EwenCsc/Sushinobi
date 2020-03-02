using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SplashScreen : MonoBehaviour
{
    [SerializeField] Image splashScreen = null;
    [SerializeField] Image titleScreen = null;
    [SerializeField] private float timer = 0.0f;

    private void Start()
    {
        StartCoroutine(LoadingFade(timer));
    }

    private IEnumerator FadeTo(Image toFade, float alphaValue, float time)
    {
        float alpha = toFade.color.a;
        for (float i = 0.0f; i < 1.0f; i += Time.deltaTime / time)
        {
            Color newColor = new Color(1.0f, 1.0f, 1.0f, Mathf.Lerp(alpha, alphaValue, i));
            toFade.color = newColor;
            yield return null;
        }
    }

    private IEnumerator LoadingFade(float time)
    {
        StartCoroutine(FadeTo(splashScreen, 1.0f, 1.0f));

        yield return new WaitForSeconds(time);

        StartCoroutine(FadeTo(splashScreen, 0.0f, 1.0f));

        yield return new WaitForSeconds(time / 2.0f);

        StartCoroutine(FadeTo(titleScreen, 1.0f, 1.0f));

        yield return new WaitForSeconds(time);

        StartCoroutine(FadeTo(titleScreen, 0.0f, 1.0f));

        yield return new WaitForSeconds(time / 2.0f);

        GetComponent<LoadSceneAsync>().enabled = true;
    }
}
