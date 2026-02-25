using UnityEngine;

public class Hazard : MonoBehaviour
{
    [Header("Damage Settings")]
    public float damage = 25f;               // 4 hits = dead
    public float damageInterval = 1f;        // seconds between damage ticks

    private float _nextDamageTime = 0f;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            DealDamage(other);
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Time.time >= _nextDamageTime)
                DealDamage(other);
        }
    }

    void DealDamage(Collider other)
    {
        PlayerHealth health = other.GetComponent<PlayerHealth>();
        if (health != null)
        {
            health.TakeDamage(damage);
            _nextDamageTime = Time.time + damageInterval;
        }
    }
}
