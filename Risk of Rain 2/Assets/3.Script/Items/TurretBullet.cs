using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBullet : MonoBehaviour
{
    private float _moveSpeed = 15f;
    GameObject Target;
    GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
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
        if (other.TryGetComponent(out Entity entity)&&!other.CompareTag("Player"))
        {
            other.GetComponent<Entity>().OnDamage(15);
            DamageUI _damageUI = Managers.UI.MakeWorldSpaceUI<DamageUI>();
            _damageUI.transform.SetParent(Target.transform);
            _damageUI.transform.localPosition = Vector3.zero;
            _damageUI.SetDamage(15);
            _damageUI.Excute();
            _damageUI.SetColor(Color.blue);
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
