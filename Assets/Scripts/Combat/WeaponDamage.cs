using RPG.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamage : MonoBehaviour
{
    [SerializeField] private Collider myCollider;

    private List<Collider> alreadyCollided = new List<Collider>();

    private void OnEnable()
    {
        alreadyCollided.Clear();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == myCollider) { return; }

        if (alreadyCollided.Contains(other)) { return; }

        alreadyCollided.Add(other);

        if (other.TryGetComponent<Health>(out Health health))
        {
            health.TakeDamage(10f);
        }
    }
}
