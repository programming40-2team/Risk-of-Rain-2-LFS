using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBullet : PrimitiveActiveItem
{
    private float _moveSpeed = 15f;
    GameObject Target;
    // Start is called before the first frame update
    void Start()
    {
        base.Init();
       
        Target = GameObject.FindGameObjectWithTag("Monster");
        Rigidbody rigid = GetComponent<Rigidbody>();
        if (Target == null)
        {
            rigid.velocity = _moveSpeed * Player.transform.forward;
        }
        else
        {
            rigid.velocity = _moveSpeed * (Target.transform.position - gameObject.transform.position).normalized;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Monster"))
        {
            other.GetComponent<Entity>().OnDamage(10);
            Managers.Resource.Destroy(gameObject);
        }
        else if (other.gameObject.layer == 1 << (int)Define.LayerMask.Enviroment)
        {
            Managers.Resource.Destroy(gameObject);
        }
    }
    private void FixedUpdate()
    {
        if ((gameObject.transform.position - Player.transform.position).sqrMagnitude > 1000f)
        {
            Managers.Resource.Destroy(gameObject);
        }
    }
}
