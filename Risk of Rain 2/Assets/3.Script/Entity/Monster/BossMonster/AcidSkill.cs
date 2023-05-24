using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidSkill : MonoBehaviour
{
    private BeetleQueen _beetleQueen;
    [SerializeField] private GameObject _beetleQueenObject;
    private float _shootingSpeed = 20f;
    private float _damage = 0f; // 공격력의 130%

    private void OnEnable()
    {
        _beetleQueen = FindObjectOfType<BeetleQueen>();
        StartCoroutine(Shoot_co());
    }

    private IEnumerator Shoot_co() // 발사
    {
        float time = 0;
        while(time < 5f)
        {
            transform.position += transform.forward * _shootingSpeed * Time.deltaTime;
            time += Time.deltaTime;
            yield return null;
        }
        DeleteAcidBile();
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
            if(collObj.CompareTag("Player"))
            {
                Debug.Log("플레이어 아야");
                collObj.GetComponent<Entity>().OnDamage(_damage);
            }
            else
            {
                GameObject obj = _beetleQueen.AcidPoolPool.GetObject();
                obj.transform.position = collObj.GetComponent<Collider>().ClosestPointOnBounds(transform.position);
                // TODO : 산성웅덩이 등장 / 15초 지속
            }
            // TODO : 터져서 사라지는 효과 코루틴으로 넣기
            _beetleQueen.AcidBallPool.ReturnObject(gameObject);
        }
    }
}
