using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item1014SkillComponent : ItemPrimitiive
{
    //  List<GameObject> Enemys;
    private float damage;
    float movespeed = 10.0f;
    [SerializeField]
    private GameObject myTargetEnemy;
    private float rotateSpeed = 800.0f;

    private Entity enemyEntity;

    private void FindEnemy()
    {
        myTargetEnemy =  GameObject.FindGameObjectWithTag("Monster");
        enemyEntity = myTargetEnemy.GetComponent<Entity>();
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().velocity = movespeed * (myTargetEnemy.transform.position - gameObject.transform.position).normalized;
    }


    public override void Init()
    {
        base.Init();
        damage = Player.GetComponent<PlayerStatus>().Damage
          * 1.5f * Managers.ItemInventory.Items[1014].Count;

    }
    private void Start()
    {

        Init();
        FindEnemy();    
    }


    private void FixedUpdate()
    {
        transform.RotateAround(transform.position, Vector3.forward, rotateSpeed * Time.deltaTime);
        if ((myTargetEnemy.transform.position - gameObject.transform.position).sqrMagnitude < 10f)
        {
            enemyEntity.OnDamage(damage);
            Managers.Resource.Destroy(gameObject);
        }
        else if(((myTargetEnemy.transform.position - gameObject.transform.position).sqrMagnitude > 100f))
        {
            FindEnemy();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 1 << 6)
        {
            FindEnemy();
        }
    }

}
