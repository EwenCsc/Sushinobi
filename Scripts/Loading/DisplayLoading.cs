using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DisplayLoading : MonoBehaviour
{
    [SerializeField] private Image load1 = null;
    [SerializeField] private Image load2 = null;
    [SerializeField] private Image load3 = null;

    private void Start()
    {
        StartCoroutine(LoadingFade(0.5f));
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
        while (true)
        {
            StartCoroutine(FadeTo(load1, 1.0f, 1.0f));

            yield return new WaitForSeconds(time);

            StartCoroutine(FadeTo(load2, 1.0f, 1.0f));

            yield return new WaitForSeconds(time);

            StartCoroutine(FadeTo(load3, 1.0f, 1.0f));

            yield return new WaitForSeconds(time);

            StartCoroutine(FadeTo(load1, 0.0f, 1.0f));

            yield return new WaitForSeconds(time);

            StartCoroutine(FadeTo(load2, 0.0f, 1.0f));

            yield return new WaitForSeconds(time);

            StartCoroutine(FadeTo(load3, 0.0f, 1.0f));

            yield return new WaitForSeconds(time);
        }
    }
}
