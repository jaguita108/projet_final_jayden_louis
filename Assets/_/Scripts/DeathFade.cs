using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Health))]
public class DeathFade : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private string dieTrigger = "die";
    [Tooltip("Matériau transparent (copie du matériau du personnage)")]
    [SerializeField] private Material fadeMaterial;
    [Tooltip("Secondes avant le début du fondu (durée de l'animation de mort)")]
    [SerializeField] private float delayBeforeFade = 2f;
    [SerializeField] private float fadeDuration = 1.5f;

    private void Awake()
    {
        GetComponent<Health>().onDeath.AddListener(() => StartCoroutine(Die()));
    }

    private IEnumerator Die()
    {
        // Le cadavre ne doit plus attaquer, bouger ni recevoir de coups
        NPCFollower follower = GetComponent<NPCFollower>();
        if (follower != null)
        {
            follower.HitboxOff();
            follower.enabled = false;
        }

        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        if (agent != null) agent.enabled = false;

        foreach (Collider c in GetComponentsInChildren<Collider>())
            c.enabled = false;

        if (animator != null)
        {
            animator.speed = 1f;
            animator.SetTrigger(dieTrigger);
        }

        yield return new WaitForSeconds(delayBeforeFade);

        // Remplace les matériaux par des copies transparentes
        List<Material> faded = new List<Material>();
        foreach (Renderer r in GetComponentsInChildren<Renderer>())
        {
            Material[] mats = new Material[r.sharedMaterials.Length];
            for (int i = 0; i < mats.Length; i++)
            {
                mats[i] = new Material(fadeMaterial);
                faded.Add(mats[i]);
            }
            r.materials = mats;
        }

        // Fondu : l'opacité passe de 1 à 0
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / fadeDuration;
            foreach (Material m in faded)
            {
                Color col = m.GetColor("_BaseColor");
                col.a = Mathf.Lerp(1f, 0f, t);
                m.SetColor("_BaseColor", col);
            }
            yield return null;
        }

        gameObject.SetActive(false);
    }
}