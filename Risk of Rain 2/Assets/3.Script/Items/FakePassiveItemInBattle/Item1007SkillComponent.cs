using System.Collections;
using System.Collections.Generic;
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
    public override void Init()
    {
        base.Init();
        myTargetEnemy = GameObject.FindGameObjectWithTag("Monster");
       
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
    }
    private void OnEnable()
    {

        Init();
        StartCoroutine(nameof(LerpTransform_co));
       
    }
    private void OnDisable()
    {
        StopCoroutine(nameof(LerpTransform_co));
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            if (other.TryGetComponent(out Entity entity))
            {
                entity.OnDamage(damage);
                Managers.Resource.Destroy(gameObject);
            }
         
        }
        else if (other.gameObject.layer == 1 << (int)Define.LayerMask.Enviroment)
        {
            Managers.Resource.Destroy(gameObject);
        }

    }

    IEnumerator LerpTransform_co()
    {
        float timeElapsed = 0;
        while (timeElapsed < lerpTime)
        {
            transform.position = Vector3.Lerp(transform.position, Objectposition, timeElapsed / lerpTime);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = Objectposition;

        if ((transform.position - Objectposition).sqrMagnitude < 0.7f)
        {
            Managers.Resource.Destroy(gameObject);
        }
    }
}
