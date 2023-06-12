using UnityEngine;
using UnityEngine.UI;

public class MonsterHpBar : UI_Base
{
    Entity _myentity;
    [SerializeField]
    private float _yoffset = 1.2f;
    enum Sliders
    {
        HpBar,
    }

    private void Awake()
    {
        Init();
    }
    public override void Init()
    {
        Bind<Slider>(typeof(Sliders));
        Debug.Log("몬스터 체력 컴포넌트 가져와서 value 값해줘야함");

    }

    private void OnEnable()
    {
        if (gameObject.transform.root.TryGetComponent(out Entity entity))
        {
            _myentity = entity;
        }
        else
        {
            Debug.Log($"{gameObject.transform.root} 에서 Entity를 가져올 수 없음");
        }

    }

    void Update()
    {
        Transform parent = transform.parent;
        transform.position = parent.position + Vector3.up * (parent.GetComponent<Collider>().bounds.size.y + _yoffset);
        Get<Slider>((int)Sliders.HpBar).value = _myentity.Health / _myentity.MaxHealth;
        //부모의 체력을 끌고와서 업데이트 해주면됨
    }
}
