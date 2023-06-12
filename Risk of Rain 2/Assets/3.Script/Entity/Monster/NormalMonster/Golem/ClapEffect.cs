using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ClapEffect : MonoBehaviour
{
    [SerializeField] private Golem _golem;
    private readonly float _clapCoefficents = 1.2f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Entity>().OnDamage(_golem.Damage * _clapCoefficents);
        }
    }
}
