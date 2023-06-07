using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpFireHitEffect : MonoBehaviour
{
    private ObjectPool _spFirehitEffectPool;
    private void Awake()
    {
        _spFirehitEffectPool = GameObject.Find("SpFireHitEffect").GetComponent<ObjectPool>();
    }

    private void OnDisable()
    {
        _spFirehitEffectPool.EnqueueObject(gameObject);
    }
}
