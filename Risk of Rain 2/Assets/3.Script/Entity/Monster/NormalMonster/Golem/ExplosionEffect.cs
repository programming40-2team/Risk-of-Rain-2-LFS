using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ExplosionEffect : MonoBehaviour
{
    [SerializeField] private Golem _golem;
    private readonly float _explosionCoefficents = 1f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Entity>().OnDamage(_golem.Damage * _explosionCoefficents);
        }
    }
}