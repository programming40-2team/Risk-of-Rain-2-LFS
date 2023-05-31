using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item1016Skill : ItemPrimitiive
{
    // Start is called before the first frame update
    private float damage;
    private bool IsExcute = false;
    private float damageCoolTime = 1.0f;
    void Start()
    {
        Init();
    }
    public override void Init()
    {
        base.Init();
        damage = Player.GetComponent<PlayerStatus>().Damage
* 5.5f * (Managers.ItemInventory.WhenActivePassiveItem[Managers.ItemInventory.PassiveItem[1016].WhenItemActive][1016].Count);
        Managers.Resource.Destroy(gameObject,2.0f);
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Player")) //지금은 테그로 비교하고 있으나, 컴포넌트를 가진 객체를 불러와야 함 
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
        if (coll.TryGetComponent(out Entity entity))
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
