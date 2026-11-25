using UnityEngine;

[RequireComponent(typeof(Health))]
public class HitBlood : MonoBehaviour
{
    [SerializeField] private GameObject bloodPrefab;
    [SerializeField] private float heightOffset = 1f;

    private void Awake()
    {
        GetComponent<Health>().onDamaged.AddListener(Spawn);
    }

    private void Spawn()
    {
        if (bloodPrefab == null) return;
        Instantiate(bloodPrefab, transform.position + Vector3.up * heightOffset, Quaternion.identity);
    }
}