using System.Collections;
using UnityEngine;

public class AcidSkill : MonoBehaviour
{
    private BeetleQueen _beetleQueen;
    [SerializeField] private GameObject _beetleQueenObject;
    private float _shootingSpeed = 40f;
    private float _damage = 0;

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
        _beetleQueen = FindObjectOfType<BeetleQueen>();
        _acidShotEffect.Play();
        _acidArea.Stop();
    }

    private void Start()
    {
        if (_beetleQueen != null)
        {
            _damage = _beetleQueen.Damage * 1.3f;
        }
    }

    public void Shoot_co() // 발사
    {
        _acidShotRigidbody.velocity = this.transform.forward * _shootingSpeed;
        //float time = 0;
        //while (time < 5f)
        //{
        //    transform.position += transform.forward * _shootingSpeed * Time.deltaTime;
        //    time += Time.deltaTime;
        //    yield return null;
        //}
        //DeleteAcidBile();
    }

    // 산성담즙 풀에 반환
    private void DeleteAcidBile()
    {
        _beetleQueen.AcidBallPool.ReturnObject(gameObject);
    }
 
    private void OnParticleCollision(GameObject collObj)
    {
        if (collObj != _beetleQueenObject)
        {
            if (collObj.TryGetComponent(out Entity en))
            {
                if (en.CompareTag("Player"))
                {
                    Debug.Log("플레이어가 비틀퀸의 AcidSkill에 맞음");
                    Debug.Log("플레이어 Hit Sound는 여기");
                    en.OnDamage(_damage);
                    DeleteAcidBile();
                }
            }
            else
            {
                Debug.Log("AcidBall이 AcidPool로 변하는 사운드는 여기 (오브젝트와 부딪혀 폭발하는? 사운드)");
                _acidShotRigidbody.velocity = Vector3.zero;
                _acidShotRigidbody.useGravity = false;
                _acidShotEffect.Stop();
                _acidArea.Play();
            }
        }
    }
}
