using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ClapEffect : MonoBehaviour
{
    [SerializeField] private Golem _golem;
    private float _clapDamage;
    private readonly float _clapCoefficents = 3f;
    private void OnEnable()
    {
        _clapDamage = _golem.Damage;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.GetComponent<Entity>().OnDamage(_clapDamage * _clapCoefficents);
        }
    }
}
