using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item1014SkillComponent : ItemPrimitiive
{
    //  List<GameObject> Enemys;
    private float damage;
    float movespeed = 8.0f;
    private bool isDectedEnemy = false;
    private GameObject myTargetEnemy;
    private float rotateSpeed = 800.0f;

    private Entity enemyEntity;

    public override void Init()
    {
        base.Init();
        damage = Player.GetComponent<PlayerStatus>().Damage
          * 1.5f * Managers.ItemInventory.Items[1014].Count;

    }
    private void Start()
    {
        //   Enemys = GameObject.FindGameObjectsWithTag("Monster").ToList();
        //  Enemys.Add(GameObject.FindGameObjectWithTag("Boss"));
        Init();
        GetComponent<Rigidbody>().velocity = Player.transform.forward * movespeed;
        Managers.Resource.Destroy(gameObject, 10f);
    }


    //방법 1. 모든 적을 긁어 모아서 가장 가까운 적에게 공격을 가하는 방식 ( 콜라이더를 조금 작게 해서 Trigger처리)
    // 발생가능한 문제 1. 적 죽음 , 타겟 없음 -> 타겟 별로 Die 인지 및 체력 확인하여 넘겨야함 
    // 근데 찾아서 넘겼는데 죽었을 수 있음 
    //방법 2. 콜라이더를 매우 크게 만들어서 Stay 에 있는 거 계쏙 찾음 .. 그리고 거리가 일정 거리 0.5f? 정도로 가까워지면 삭제시킴
    // 스킬 설명에도 인식범위가 넓다고 나오니  우선 2방법으로 구현

    private void OnTriggerStay(Collider other)
    {

        //other의 테그를 가져오는 부분은 일단 납두고 플레이어도 만약
        //IDamge를 같이 구현하면 테그 써야대고 안하면 태그 구지 안써도 될듯?
        if (!other.CompareTag("Player"))
        {
            if (!isDectedEnemy && other.TryGetComponent(out Entity entity))
            {


                isDectedEnemy = true;
                myTargetEnemy = other.gameObject;
                enemyEntity = entity;

                GetComponent<Rigidbody>().velocity = Vector3.zero;
                GetComponent<Rigidbody>().velocity = movespeed * (myTargetEnemy.transform.position - gameObject.transform.position).normalized;


            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (isDectedEnemy && collision.gameObject.Equals(myTargetEnemy))
        {
            if ((myTargetEnemy.transform.position - gameObject.transform.position).sqrMagnitude < 5f)
            {
                Debug.Log("탐지됨!");
                enemyEntity.OnDamage(damage);
                Managers.Resource.Destroy(gameObject);
            }
        }
    }

    private void Update()
    {
        transform.RotateAround(transform.position, Vector3.forward, rotateSpeed * Time.deltaTime);
    }
    private void OnTriggerExit(Collider other)
    {
        if (other == myTargetEnemy)
        {
            isDectedEnemy = false;
            myTargetEnemy = null;
        }
    }

}
