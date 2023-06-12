using System.Collections;
using UnityEngine;

public class GeepAttack : MonoBehaviour
{
    [SerializeField] private Entity _geepEntity;

    private float _damageCoefficient = 1.5f;

    private WaitForSeconds _offdelay = new WaitForSeconds(1.8f);
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.TryGetComponent(out Entity entity))
            {
                entity.OnDamage(_geepEntity.Damage * _damageCoefficient);
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
