using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour
{
    [SerializeField] private int damage = 10;

    // Cibles déjà touchées pendant ce coup
    private readonly HashSet<Health> alreadyHit = new HashSet<Health>();

    private void OnEnable()
    {
        // Nouveau coup : on repart de zéro
        alreadyHit.Clear();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Ignore le propriétaire de la hitbox (le joueur lui-même)
        if (other.transform.root == transform.root) return;

        Health health = other.GetComponentInParent<Health>();
        if (health == null) return;

        // Un seul impact par cible et par coup
        if (!alreadyHit.Add(health)) return;

        Debug.Log("Dégâts infligés à : " + health.name);
        health.Remove(damage);
    }
}