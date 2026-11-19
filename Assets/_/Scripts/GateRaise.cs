using System.Collections;
using UnityEngine;

public class GateRaise : MonoBehaviour
{
    [Tooltip("De combien la grille monte (en mètres)")]
    [SerializeField] private float raiseHeight = 4f;

    [Tooltip("Durée de la montée (en secondes)")]
    [SerializeField] private float duration = 2f;

    private bool opened;

    public void Open()
    {
        if (opened) return;
        opened = true;
        StartCoroutine(Raise());
    }

    private IEnumerator Raise()
    {
        Vector3 start = transform.localPosition;
        Vector3 end = start + Vector3.up * raiseHeight;
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime / duration;
            transform.localPosition = Vector3.Lerp(start, end, Mathf.SmoothStep(0f, 1f, t));
            yield return null;
        }

        transform.localPosition = end;
    }
}