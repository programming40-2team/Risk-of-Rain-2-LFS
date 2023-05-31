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
            myTargetEnemy = GameObject.FindGameObjectWithTag(Define.BossTag);
            if (myTargetEnemy == null)
            {

                return;
            }
        }

        damage = Player.GetComponent<PlayerStatus>().Damage
            * 3 * (Managers.ItemInventory.WhenActivePassiveItem[Managers.ItemInventory.PassiveItem[1007].WhenItemActive][1007].Count);

        if (myTargetEnemy == null)
        {
            //플레이어 방향으로 앞으로 나아가기
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().velocity = Player.transform.forward * movespeed;
        }
        else
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;

            GetComponent<Rigidbody>().velocity = movespeed * myTargetEnemy.transform.position;
        }

        Managers.Resource.Destroy(gameObject, 15f);
        Debug.Log("미사일 발사하는 파티클, 쉐이더 필요");
       
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Monster")||other.CompareTag(Define.BossTag))
        {
            if(other.TryGetComponent(out Entity entity))
            {
                entity.OnDamage(damage);
            }
            else
            {
                Debug.Log($"{other.gameObject.name} 의 Entity를 가져오지 못함");
            }

        }
    }

}
