using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    protected PlayerStatus _playerStatus;
    protected Rigidbody _projectileRigidbody;
    protected ObjectPool _projectileObjectPool;
    protected Entity _entity;
    protected string _projectilePoolName;
    protected float _projectileSpeed;
    protected float _damage;
    protected float _damageCoefficient;
    protected float _criticalChance;
    private void Awake()
    {
        _playerStatus = FindObjectOfType<PlayerStatus>();
        InitializeProjectile();
    }

    protected virtual void OnEnable()
    {
        _damage = _playerStatus.Damage;
    }

    /// <summary>
    /// 자식 클래스에서 투사체 풀 이름 지정해줘야함
    /// </summary>
    protected virtual void InitializeProjectile()
    {
        TryGetComponent(out _projectileRigidbody);
        FindObjectPool();
    }

    /// <summary>
    /// 총알이 바라보는 방향으로 발사
    /// </summary>
    public virtual void ShootForward()
    {
        _projectileRigidbody.velocity = this.transform.forward * _projectileSpeed;
    }
    protected void FindObjectPool()
    {
        _projectileObjectPool = GameObject.Find(_projectilePoolName).GetComponent<ObjectPool>();
    }
    protected void PrintDamage(GameObject TargetObject,Define.EDamageType DamageType=Define.EDamageType.Nomal)
    {
        DamageUI _damageUI = Managers.UI.MakeWorldSpaceUI<DamageUI>();
        _damageUI.transform.SetParent(TargetObject.transform);
        _damageUI.transform.localPosition = Vector3.zero;
        _damageUI.SetDamage(_damage);
        _damageUI.Excute();

        switch (DamageType)
        {
            case Define.EDamageType.Nomal:
                break;
            case Define.EDamageType.Cirtical:
                _damageUI.SetColor(Color.red);
                break;
            case Define.EDamageType.Item:
                _damageUI.SetColor(Color.blue);
                break;
            case Define.EDamageType.Heal:
                _damageUI.SetColor(Color.green);
                break;
        }
    }
}
