using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

//도꺠비불 -> 용암생성
public class Item1008Skill : ItemPrimitiive
{
    private float damageCoolTime = 1.0f;
    private float damage;
    private bool IsExcute = false;

    //적 죽은 위치에 생성해야 하는데 적 위치를 어떻게 받아올 것인가??
    //세분화를 해야할 것인가?.. 생각할게 만구만

    private void Start()
    {
        damage = Player.GetComponent<PlayerStatus>().Damage
    * 3.5f * (Managers.ItemInventory.WhenActivePassiveItem[Managers.ItemInventory.PassiveItem[1008].WhenItemActive][1008].Count);
    }
    //장판딜 
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(Define.BossTag)) //지금은 테그로 비교하고 있으나, 컴포넌트를 가진 객체를 불러와야 함 
        {
            if (!IsExcute)
            {
                StopCoroutine(nameof(TakeDamage_co));
                StartCoroutine(nameof(TakeDamage_co), other);

            }
   

        }
    }


    private IEnumerator TakeDamage_co(Collider coll)
    {
        IsExcute = true;
        if(coll.TryGetComponent(out Entity entity))
        {
           entity.OnDamage(damageCoolTime);
        }
        else
        {
            Debug.Log($"{coll.gameObject.name}의 Entity를 찾지 못함");
        }
      
        yield return new WaitForSeconds(damageCoolTime);
        IsExcute = false;
    }
}
