using System.Collections;
using UnityEngine;

public class ScreenFade : MonoBehaviour
{
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public IEnumerator FadeIn(float duration)
    {
        float startAlpha = canvasGroup.alpha;
        float time = 0f;

        while (time < duration)
        {
            canvasGroup.alpha = Mathf.Lerp(startAlpha, 1, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = 1;
    }

    public IEnumerator FadeOut(float duration)
    {
        float startAlpha = canvasGroup.alpha;
        float time = 0f;

        while (time < duration)
        {
            canvasGroup.alpha = Mathf.Lerp(startAlpha, 0, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = 0;
    }
}
