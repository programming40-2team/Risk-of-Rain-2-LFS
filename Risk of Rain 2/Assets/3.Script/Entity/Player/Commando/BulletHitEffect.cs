using UnityEngine;

public class BulletHitEffect : MonoBehaviour
{
    private ObjectPool _spFirehitEffectPool;
    private void Awake()
    {
        _spFirehitEffectPool = GameObject.Find("HitEffectPool").GetComponent<ObjectPool>();
    }

    private void OnDisable()
    {
        _spFirehitEffectPool.EnqueueObject(gameObject);
    }
}
