using System.Collections;
using UnityEngine;

public class BisonAttack : MonoBehaviour
{
    [SerializeField] private Entity _BisonEntity;

    private float _damageCoefficient = 1.5f;

    private WaitForSeconds _offdelay = new WaitForSeconds(0.25f);
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.TryGetComponent(out Entity entity))
            {
                entity.OnDamage(_BisonEntity.Damage * _damageCoefficient);
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

