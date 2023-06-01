using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TapToBlop : MonoBehaviour
{
    public float scaleFactor = 1.2f;
    public float animDuration = 0.14f;

    private Image image;
    private Vector3 originalScale;
    private Coroutine currentCoroutine;

    void Start()
    {
        image = GetComponent<Image>();
        originalScale = image.rectTransform.localScale;
    }

    public void OnTap()
    {
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }
        currentCoroutine = StartCoroutine(BlopAnimation());
    }

    private IEnumerator BlopAnimation()
    {
        float timer = 0f;
        Vector3 targetScale = originalScale * scaleFactor;

        while (timer < animDuration)
        {
            float t = timer / animDuration;
            image.rectTransform.localScale = Vector3.Lerp(originalScale, targetScale, t);
            timer += Time.deltaTime;
            yield return null;
        }

        image.rectTransform.localScale = targetScale;

        yield return new WaitForSeconds(0.001f);

        timer = 0f;

        while (timer < animDuration)
        {
            float t = timer / animDuration;
            image.rectTransform.localScale = Vector3.Lerp(targetScale, originalScale, t);
            timer += Time.deltaTime;
            yield return null;
        }

        image.rectTransform.localScale = originalScale;
        currentCoroutine = null;
    }
}

