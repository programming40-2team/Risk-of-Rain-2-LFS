using System.Collections;
using UnityEngine;

public class BeetleQueen : Entity
{
    // TODO : 난이도에 따라 MaxHealth 증가시키기
    [SerializeField] private MonsterData _beetleQueenData;

    private GameObject _player;

    public ObjectPool AcidBallPool;
    public ObjectPool WardPool;
    public GameObject BombRange;

    public Animator BeetleQueenAnimator;
    private AudioSource _beetleQueenAudioSource;
    private AudioClip _hitSound;

    public bool IsRun = false;

    private MeshCollider _meshCollider;
    [SerializeField] private Material _material;

    [Header("Transforms")]
    [SerializeField] private Transform _beetleQueenMouthTransform;
    [SerializeField] private Transform _beetleQueenButtTransform;


    //private bool hasTarget
    //{
    //    get
    //    {
    //        if (targetEntity != null && !targetEntity.IsDeath)
    //        {
    //            return true;
    //        }

    //        return false;
    //    }
    //}
    private void Awake()
    {
        TryGetComponent(out BeetleQueenAnimator);
        _player = GameObject.FindGameObjectWithTag("Player");
        _beetleQueenMouthTransform = GameObject.FindGameObjectWithTag("BeetleQueenMouth").transform;
        _beetleQueenButtTransform = GameObject.FindGameObjectWithTag("BeetleQueenButt").transform;
        AcidBallPool = GameObject.Find("AcidBallPool").GetComponent<ObjectPool>();
        WardPool = GameObject.Find("WardPool").GetComponent<ObjectPool>();
        _meshCollider = FindObjectOfType<MeshCollider>();
    }

    protected override void OnEnable()
    {
        SetUp(_beetleQueenData);
        base.OnEnable();
        //Debug.Log("비틀퀸");
        //Debug.Log("Health : " + Health);
        //Debug.Log("IsDeath : " + IsDeath);
        //Debug.Log("Damage : " + Damage);
        //Debug.Log("MoveSpeed : " + MoveSpeed);
        //Debug.Log("Armor : " + Armor);
        //Debug.Log("MaxHealthAscent : " + MaxHealthAscent);
        //Debug.Log("DamageAscent : " + DamageAscent);
        //Debug.Log("HealthRegen : " + HealthRegen);
        //Debug.Log("HealthRegenAscent : " + HealthRegenAscent);
        Debug.Log(string.Format("{0}= {1} X {2}", MaxHealth, MaxHealthAscent, _difficulty));
        Managers.Event.PostNotification(Define.EVENT_TYPE.BossHpChange, this);

        _meshCollider.enabled = true;
    }

    public override void OnDamage(float damage)
    {
        if (!IsDeath)
        {
            //hitEffect.transform.SetPositionAndRotation(hitposition, Quaternion.LookRotation(hitnormal));
            //hitEffect.Play();
            //_beetleQueenAudio.PlayOneShot(hitSound);

            StopCoroutine(HitEffect_co());
            StartCoroutine(HitEffect_co());
            //Hp Slider 데미지 입을 떄 마다 갱신되도록 연동
        }

        base.OnDamage(damage);
        Managers.Event.PostNotification(Define.EVENT_TYPE.BossHpChange, this);
    }

    public override void Die()
    {
        base.Die();
        BeetleQueenAnimator.SetTrigger("Die");
        _meshCollider.enabled = false;
        // 보스 Destroy

        //보스 종료 시 텔레포트 이벤트 완료!
        Managers.Game.GameState = Define.EGameState.KillBoss;
    }

    private void SetUp(MonsterData data)
    {
        MaxHealth = data.MaxHealth;
        Damage = data.Damage;
        MoveSpeed = data.MoveSpeed;
        Armor = data.Amor;
        MaxHealthAscent = data.MaxHealthAscent;
        DamageAscent = data.DamageAscent;
        HealthRegen = data.HealthRegen;
        HealthRegenAscent = data.RegenAscent;

        Debug.Log(string.Format("비틀퀸 체력증가량 : {0}", MaxHealthAscent));
        //첫 생성 시 보스 Hp 조절을 위한 알림

    }

    /// <summary>
    /// 산성담즙 6개 부채꼴로 발사하는 스킬
    /// </summary>
    public void AcidBileSkill()
    {
        IsRun = true;
        Quaternion rot = Quaternion.LookRotation(_player.transform.position - _beetleQueenMouthTransform.position);
        for (int i = 0; i < 6; i++)
        {
            GameObject obj = AcidBallPool.GetObject();
            obj.transform.SetPositionAndRotation(_beetleQueenMouthTransform.position, Quaternion.Euler(0, -20f + 8 * i, 0) * rot);
            obj.GetComponent<AcidSkill>().Shoot_co();
        }
        IsRun = false;
    }

    /// <summary>
    /// 뒤꽁무니에서 구체 3개 뿅뿅뿅 발사하는 스킬
    /// </summary>
    public void WardSkill() // 체력 50% 미만
    {
        IsRun = true;
        StartCoroutine(CreateWard_co());
        IsRun = false;
    }

    /// <summary>
    /// 플레이어 위치에 시간차 범위 공격 하는 스킬
    /// </summary>
    public void RangeBombSkill() // 체력 25% 미만
    {
        IsRun = true;
        Vector3 pos;
        RaycastHit[] hits;
        Ray ray = new Ray(_player.transform.position, Vector3.down);

        hits = Physics.RaycastAll(ray);

        foreach (RaycastHit obj in hits)
        {
            if (obj.transform.gameObject.CompareTag("Ground"))
            {
                pos = obj.point;
                pos = new Vector3(pos.x, pos.y + 0.2f, pos.z);
                Instantiate(BombRange, pos, Quaternion.Euler(-90, 0, 0));
            }
        }
        IsRun = false;
    }

    private IEnumerator CreateWard_co()
    {
        Quaternion rot = Quaternion.LookRotation(_player.transform.position - _beetleQueenMouthTransform.position);
        WaitForSeconds wfs = new WaitForSeconds(0.3f);
        for (int i = 0; i < 3; i++)
        {
            GameObject obj = WardPool.GetObject();
            obj.transform.position = _beetleQueenButtTransform.position;
            yield return wfs;
        }
    }
    private IEnumerator HitEffect_co()
    {
        _material.SetFloat("_EmissionPower", 1);
        yield return new WaitForSeconds(0.1f);
        _material.SetFloat("_EmissionPower", 0);
    }
}
