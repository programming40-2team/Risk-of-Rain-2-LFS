using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//의식용 단검 3개의 유도 단검 발사
public class item1014Skill : ItemPrimitiive
{
  //  List<GameObject> Enemys;
    private float damage;
    float movespeed = 5.0f;
    private bool isDectedEnemy;
    private GameObject myTargetEnemy;
    private void Start()
    {
     //   Enemys = GameObject.FindGameObjectsWithTag("Monster").ToList();
      //  Enemys.Add(GameObject.FindGameObjectWithTag("Boss"));
        damage = Player.GetComponent<PlayerStatus>().Damage
            * 1.5f*(Managers.ItemInventory.WhenActivePassiveItem[Managers.ItemInventory.PassiveItem[1014].WhenItemActive][1014].Count);

    }


    //방법 1. 모든 적을 긁어 모아서 가장 가까운 적에게 공격을 가하는 방식 ( 콜라이더를 조금 작게 해서 Trigger처리)
    // 발생가능한 문제 1. 적 죽음 , 타겟 없음 -> 타겟 별로 Die 인지 및 체력 확인하여 넘겨야함 
    // 근데 찾아서 넘겼는데 죽었을 수 있음 
    //방법 2. 콜라이더를 매우 크게 만들어서 Stay 에 있는 거 계쏙 찾음 .. 그리고 거리가 일정 거리 0.5f? 정도로 가까워지면 삭제시킴
    // 스킬 설명에도 인식범위가 넓다고 나오니  우선 2방법으로 구현
    //private void SetTarget()
    //{
    //    foreach(GameObject enemy in Enemys)
    //    {
    //        if(enemy.H)
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {

        //other의 테그를 가져오는 부분은 일단 납두고 플레이어도 만약
        //IDamge를 같이 구현하면 테그 써야대고 안하면 태그 구지 안써도 될듯?
        if (!isDectedEnemy&&other.CompareTag("Monster"))
        {
            isDectedEnemy = true;
            myTargetEnemy = other.gameObject;
            Vector3 movedir = other.transform.position - gameObject.transform.position;

            gameObject.transform.Translate(movedir.normalized * movespeed * Time.deltaTime);

            if ((other.transform.position-gameObject.transform.position).sqrMagnitude < 1.1f)
            {
                Debug.Log("몬스터 의 IDmage 컴포넌트 가져와서 떄리기");
                Managers.Resource.Destroy(gameObject);
            }

        }
       

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
