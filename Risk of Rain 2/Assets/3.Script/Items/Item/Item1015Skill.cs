using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//서리유물
public class Item1015Skill : ItemPrimitiive
{
    private float damageCoolTime = 1.0f;
    private float damage;
    private bool IsExcute = false;
    private float prevMoveSpeed;
    private Vector3 prevTransformScale;
    //적 죽은 위치에 생성해야 하는데 적 위치를 어떻게 받아올 것인가??
    //세분화를 해야할 것인가?.. 생각할게 만구만

    private void Start()
    {
        damage = Player.GetComponent<PlayerStatus>().Damage
    * 3.5f * (Managers.ItemInventory.WhenActivePassiveItem[Managers.ItemInventory.PassiveItem[1015].WhenItemActive][1015].Count);
        prevTransformScale=gameObject.transform.localScale;

        gameObject.transform.localScale = new Vector3(
            prevTransformScale.x * (1 + 0.1f * Managers.ItemInventory.WhenActivePassiveItem[Managers.ItemInventory.PassiveItem[1015].WhenItemActive][1015].Count),
            prevTransformScale.y,
            prevTransformScale.z * (1 + 0.1f * Managers.ItemInventory.WhenActivePassiveItem[Managers.ItemInventory.PassiveItem[1015].WhenItemActive][1015].Count));

    }
    //장판딜 
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Monster")) //지금은 테그로 비교하고 있으나, 컴포넌트를 가진 객체를 불러와야 함 
        {
            //이속은 상시 - 데미지는 1초마다 
            prevMoveSpeed = other.GetComponent<Entity>().MoveSpeed;
            other.GetComponent<Entity>().MoveSpeed = prevMoveSpeed * 0.8f;
            if (!IsExcute)
            {
                StopCoroutine(nameof(TakeDamage_co));
                StartCoroutine(nameof(TakeDamage_co), other);

            }


        }
    }
    private void Update()
    {
        gameObject.transform.position = Player.transform.position;
    }
    private void OnTriggerExit(Collider other)
    {
        other.GetComponent<Entity>().MoveSpeed = prevMoveSpeed;
    }

    private IEnumerator TakeDamage_co(Collider coll)
    {
        IsExcute = true;
        coll.GetComponent<Entity>().OnDamage(damage);
        yield return new WaitForSeconds(damageCoolTime);
        IsExcute = false;
    }
}
