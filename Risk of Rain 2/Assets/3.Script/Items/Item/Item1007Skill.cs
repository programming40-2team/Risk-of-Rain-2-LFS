using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Item1007Skill : ItemPrimitiive
{
    private float damage;
    private float movespeed = 10.0f;
    private bool isDectedEnemy;
    private GameObject myTargetEnemy;

    // GameItemImage Target;
    //유도 미사일 아님!
    private void Start()
    {
        myTargetEnemy = GameObject.FindGameObjectWithTag("Monster");
        if (myTargetEnemy == null)
        {
            myTargetEnemy = GameObject.FindGameObjectWithTag("Boss");
            if (myTargetEnemy == null)
            {
                return;
            }
        }

        damage = Player.GetComponent<PlayerStatus>().Damage
            * 3 * (Managers.ItemInventory.WhenActivePassiveItem[Managers.ItemInventory.PassiveItem[1007].WhenItemActive][1007].Count);


        //플레이어 방향으로 앞으로 나아가기
        GetComponent<Rigidbody>().velocity = Player.transform.forward*movespeed;
        Managers.Resource.Destroy(gameObject, 15f);
        Debug.Log("미사일 발사하는 파티클, 쉐이더 필요");
       
    }
    private void OnTriggerEnter(Collider other)
    {

        //총알 나가는데 하나 더 나가는 느낌으로 ㄱㄱ
        if (!isDectedEnemy && other.CompareTag("Monster"))
        {
            //isDectedEnemy = true;
            //myTargetEnemy = other.gameObject;
            ////앞으로 가다가 적을 발견할 경우 그놈한테 다가가서 자폭 공격느낌
            //Vector3 movedir = other.transform.position - gameObject.transform.position;

            //gameObject.transform.Translate(movedir.normalized * movespeed * Time.deltaTime);

            //if ((other.transform.position - gameObject.transform.position).sqrMagnitude < 1.1f)
            //{
            //    Debug.Log("몬스터 의 IDmage 컴포넌트 가져와서 떄리기");
            //    Managers.Resource.Destroy(gameObject);
            //}

        }
     
    }

}
