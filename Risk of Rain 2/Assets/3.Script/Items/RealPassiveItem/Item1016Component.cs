using System.Collections;
using UnityEngine;

public class Item1016Component : ItemPrimitiive
{
    // Start is called before the first frame update
    private float _damage;
    private bool _isTakeDamageable = false;
    private float damageCoolTime = 1.0f;


    private void Awake()
    {
        Init();

    }
    private void OnEnable()
    {
        gameObject.transform.position = Player.transform.position + Vector3.up * 0.1f;
        SetDamage(Managers.ItemInventory.Items[1016].Count);
        // gameObject.SetRandomPositionSphere(1, 1, -Player.GetComponent<Collider>().bounds.size.y-1f, Player.transform);
        StartCoroutine(nameof(JumpingStart_co));
    }

    public override void Init()
    {

        base.Init();

    }

    public void SetDamage(int Count)
    {
        _damage = _playerStatus.Damage * 5.5f * Count;
    }
    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Player") && other.TryGetComponent(out Entity entity)) //지금은 테그로 비교하고 있으나, 컴포넌트를 가진 객체를 불러와야 함 
        {
            if (!_isTakeDamageable)
            {
                StopCoroutine(nameof(TakeDamage_co));
                StartCoroutine(nameof(TakeDamage_co), entity);

            }


        }
    }
    private IEnumerator TakeDamage_co(Entity entity)
    {
        _isTakeDamageable = true;

        ShowDamageUI(entity.gameObject, _damage, Define.EDamageType.Item);
        entity.OnDamage(_damage);

        yield return new WaitForSeconds(damageCoolTime);
        _isTakeDamageable = false;
    }

    private IEnumerator JumpingStart_co()
    {
        yield return new WaitForSeconds(1.2f);
        Managers.Resource.Destroy(gameObject);

    }
}
