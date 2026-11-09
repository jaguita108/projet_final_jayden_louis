using UnityEngine;
using UnityEngine.Events;

public class KillCounter : MonoBehaviour
{
    [Tooltip("Nombre de morts à atteindre (ennemis + boss)")]
    [SerializeField] private int enemiesToKill = 5;

    [Tooltip("Actions lancées quand tous sont morts : ouvrir une porte, désactiver une barrière...")]
    [SerializeField] private UnityEvent onAllKilled;

    private int killed;

    // À appeler depuis l'événement On Death de chaque ennemi
    public void OnEnemyKilled()
    {
        killed++;
        Debug.Log($"Ennemis tués : {killed} / {enemiesToKill}");

        if (killed == enemiesToKill)
            onAllKilled.Invoke();
    }
}