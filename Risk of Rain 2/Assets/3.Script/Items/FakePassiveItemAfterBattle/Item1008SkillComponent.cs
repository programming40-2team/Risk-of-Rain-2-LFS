using System.Collections;
using UnityEngine;

public class Item1008SkillComponent : ItemPrimitiive
{
    private float damageCoolTime = 1.5f;
    private float damage;
    private bool IsExcute = false;
    [SerializeField] float deleteTime = 4.0f;
    //적 죽은 위치에 생성해야 하는데 적 위치를 어떻게 받아올 것인가??
    //세분화를 해야할 것인가?.. 생각할게 만구만

    private void Awake()
    {

        Init();
    }
    public override void Init()
    {
        base.Init();

    }
    private void OnEnable()
    {
        damage = _playerStatus.Damage
* 3.5f * Managers.ItemInventory.Items[1008].Count;
        Managers.Resource.Destroy(gameObject, 4.0f);
    }
    private IEnumerator DeleteSkill_co()
    {
        yield return new WaitForSeconds(deleteTime);
        Managers.Resource.Destroy(gameObject);
    }
    //장판딜 
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
            entity.OnDamage(damage);
            ShowDamageUI(coll.gameObject, damage, Define.EDamageType.Item);
        }
        else
        {
            Debug.Log($"{coll.gameObject.name}의 Entity를 찾지 못함");
        }

        yield return new WaitForSeconds(damageCoolTime);
        IsExcute = false;
    }
}
