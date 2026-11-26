using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class KillCounter : MonoBehaviour
{
    [Tooltip("Nombre de morts à atteindre (ennemis + boss)")]
    [SerializeField] private int enemiesToKill = 5;

    [Tooltip("Actions lancées quand tous sont morts : ouvrir une porte, une barrière...")]
    [SerializeField] private UnityEvent onAllKilled;

    [Tooltip("Optionnel : le texte du compteur à l'écran (ex. 2 / 6)")]
    [SerializeField] private TMP_Text counterText;

    private int killed;

    private void Start()
    {
        UpdateText();
    }

    // À appeler depuis l'événement On Death de chaque ennemi
    public void OnEnemyKilled()
    {
        killed++;
        Debug.Log($"Ennemis tués : {killed} / {enemiesToKill}");
        UpdateText();

        if (killed == enemiesToKill)
            onAllKilled.Invoke();
    }

    private void UpdateText()
    {
        if (counterText != null)
            counterText.text = $"{killed} / {enemiesToKill}";
    }
}