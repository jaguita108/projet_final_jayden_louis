using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private string attackTrigger = "attack";
    [SerializeField] private float attackCooldown = 0.6f;
    [SerializeField] private GameObject hitbox;

    private float nextAttackTime;

    private void Start()
    {
        if (hitbox != null) hitbox.SetActive(false);
    }

    private void Update()
    {
        if (Mouse.current == null) return;

        if (Mouse.current.leftButton.wasPressedThisFrame && Time.time >= nextAttackTime)
        {
            nextAttackTime = Time.time + attackCooldown;
            animator.SetTrigger(attackTrigger);
        }
    }

    // Appelées par des événements d'animation dans le clip d'attaque
    public void HitboxOn() { if (hitbox != null) hitbox.SetActive(true); }
    public void HitboxOff() { if (hitbox != null) hitbox.SetActive(false); }
}