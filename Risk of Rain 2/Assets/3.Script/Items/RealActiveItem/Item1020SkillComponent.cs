using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item1020SkillComponent : ItemPrimitiive
{
    private float damage;
    [SerializeField]
    private GameObject myTargetEnemy;

    Vector3 TargetPosition;
    // GameItemImage Target;
    //¿Øµµ πÃªÁ¿œ æ∆¥‘!

    private float lerpTime = 5f;
    float currentLerpTime;
    [SerializeField] 
    private GameObject _particleObject;
    private MeshRenderer _myMeshRender;

    private void Awake()
    {
        Init();
        _myMeshRender=GetComponentInChildren<MeshRenderer>();
    }
    public override void Init()
    {
        base.Init();
        damage = Player.GetComponent<PlayerStatus>().Damage
            * 1 * (Managers.ItemInventory.Items[1020].Count);

    }
    private void OnEnable()
    {

        GetComponent<Rigidbody>().velocity = Vector3.zero;
        _myMeshRender.gameObject.SetActive(true);
        myTargetEnemy = GameObject.FindGameObjectWithTag("Monster");
        if(myTargetEnemy!= null )
        {
            gameObject.SetRandomPositionSphere(10, 14, 7, myTargetEnemy.transform);
        }
        else
        {
            gameObject.SetRandomPositionSphere(10, 14, 7, Player.transform);
        }
        _particleObject.SetActive(false);
        StartCoroutine(nameof(GotoGround_co));

    }
    private void OnDisable()
    {
        StopCoroutine(nameof(GotoGround_co));
        _particleObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.TryGetComponent(out Entity entity))
        {
            entity.OnDamage(damage);
            ShowDamageUI(entity.gameObject, damage, Define.EDamageType.Item);
            Debug.Log("ªßæﬂ");
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            _particleObject.SetActive(true);
            _myMeshRender.gameObject.SetActive(false);
        }
        else
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            _particleObject.SetActive(true);
            _myMeshRender.gameObject.SetActive(false);
        }

    }

    IEnumerator GotoGround_co()
    {
        yield return new WaitForSeconds(1.5f);
        GetComponent<Rigidbody>().velocity = 50 * Vector3.down;
        yield return new WaitForSeconds(2.5f);
        Managers.Resource.Destroy(gameObject);
    }



}
