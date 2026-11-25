using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ScreenFlash : MonoBehaviour
{
    [SerializeField] private float maxAlpha = 0.4f;
    [SerializeField] private float duration = 0.3f;

    private Image image;
    private Coroutine running;

    private void Awake()
    {
        image = GetComponent<Image>();
        SetAlpha(0f);
    }

    public void Flash()
    {
        if (running != null) StopCoroutine(running);
        running = StartCoroutine(Fade());
    }

    private IEnumerator Fade()
    {
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / duration;
            SetAlpha(Mathf.Lerp(maxAlpha, 0f, t));
            yield return null;
        }
        SetAlpha(0f);
    }

    private void SetAlpha(float a)
    {
        Color c = image.color;
        c.a = a;
        image.color = c;
    }
}