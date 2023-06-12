using System.Collections;
using UnityEngine;

public class Item1007SkillComponent : ItemPrimitiive
{
    private float damage;
    [SerializeField]
    private GameObject myTargetEnemy;
    Vector3 Objectposition;
    // GameItemImage Target;
    //유도 미사일 아님!

    private float lerpTime = 5f;
    float currentLerpTime;

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

        Init();
        myTargetEnemy = FindClosestEnemy();

        damage = Player.GetComponent<PlayerStatus>().Damage
            * 3 * (Managers.ItemInventory.Items[1007].Count);

        if (myTargetEnemy == null)
        {
            Objectposition = gameObject.SetRandomPositionSphere(0, 10, 3);
        }
        else
        {
            Objectposition = myTargetEnemy.transform.position;
        }
        StartCoroutine(nameof(LerpTransform_co));
    }
    private void OnDisable()
    {
        StopCoroutine(nameof(LerpTransform_co));
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.TryGetComponent(out Entity entity) && !other.CompareTag("Player"))
        {
            transform.position = other.transform.position;

            entity.OnDamage(damage);
            ShowDamageUI(entity.gameObject, damage, Define.EDamageType.Item);
            Managers.Resource.Destroy(gameObject);
        }
        else
        {
            Managers.Resource.Destroy(gameObject);
        }

    }

    IEnumerator LerpTransform_co()
    {
        float timeElapsed = 0;


        while (timeElapsed < lerpTime)
        {
            float percent = timeElapsed / lerpTime;
            transform.position = Vector3.Lerp(transform.position, Objectposition, percent);
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = Objectposition;

        Managers.Resource.Destroy(gameObject);
    }
}