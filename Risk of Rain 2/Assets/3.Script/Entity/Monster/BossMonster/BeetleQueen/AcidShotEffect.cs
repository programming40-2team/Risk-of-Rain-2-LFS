using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidShotEffect : MonoBehaviour
{
    private ParticleSystem _acidShotEffect;
    [SerializeField] private ParticleSystem _acidArea;

    private Rigidbody _acidShotRigidbody;

    private void Awake()
    {
        TryGetComponent(out _acidShotEffect);
        TryGetComponent(out _acidShotRigidbody);
    }

    private void OnEnable()
    {
        _acidShotEffect.Play();
        _acidArea.Stop();
    }

    private void OnTriggerEnter(Collider other)
    {
        _acidShotRigidbody.velocity = Vector3.zero;
        _acidShotRigidbody.useGravity = false;

        _acidShotEffect.Stop();
        _acidArea.Play();
    }
}
