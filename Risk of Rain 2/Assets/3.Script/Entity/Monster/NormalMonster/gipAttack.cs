using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gipAttack : MonoBehaviour
{
    [SerializeField] private Entity _gipEntity;

    private float _damageCoefficient = 1.5f;

    private WaitForSeconds _offdelay = new WaitForSeconds(0.6f);
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.TryGetComponent(out Entity entity))
            {
                entity.OnDamage(_gipEntity.Damage * _damageCoefficient);
            }
        }
    }
    private void OnEnable()
    {
        StartCoroutine(OffattAckColider_co());
    }
    private IEnumerator OffattAckColider_co()
    {
        yield return _offdelay;
        gameObject.SetActive(false);
    }
}
