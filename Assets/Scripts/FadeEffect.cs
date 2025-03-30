using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeEffect : MonoBehaviour
{
    public Image fadeImage; // Imagen negra en el Canvas
    public float fadeDuration = 2f; // Tiempo de transición

    private void Start()
    {
        fadeImage.gameObject.SetActive(true);
    }

    public void FadeToBlack()
    {
        StartCoroutine(Fade(1)); // Desvanecer a negro
    }

    public void FadeFromBlack()
    {
        StartCoroutine(Fade(0)); // Desvanecer a normal
    }

    private IEnumerator Fade(float targetAlpha)
    {
        float startAlpha = fadeImage.color.a;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / fadeDuration);
            fadeImage.color = new Color(0, 0, 0, newAlpha);
            yield return null;
        }

        fadeImage.color = new Color(0, 0, 0, targetAlpha);
    }
}
