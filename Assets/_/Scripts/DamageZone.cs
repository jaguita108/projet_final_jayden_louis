using UnityEngine;

public class DamageZone : MonoBehaviour
{
    [SerializeField] private int damage = 10;

    private void OnTriggerEnter(Collider other)
    {
        // Ignore le propriétaire de la hitbox (le joueur lui-même)
        if (other.transform.root == transform.root) return;

        Debug.Log("Hitbox touche : " + other.name);

        Health health = other.GetComponentInParent<Health>();
        if (health == null) return;

        Debug.Log("Dégâts infligés à : " + health.name);
        health.Remove(damage);
        gameObject.SetActive(false);
    }
}