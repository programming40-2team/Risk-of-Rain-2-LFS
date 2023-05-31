using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterHpBar : UI_Base
{
    enum Sliders
    {
        HpBar,
    }
    public override void Init()
    {
       

    }

    // Start is called before the first frame update
    void Start()
    {
        Bind<Slider>(typeof(Sliders));
        Debug.Log("몬스터 체력 컴포넌트 가져와서 value 값해줘야함");
    }

    void Update()
    {
        Transform parent = transform.parent;
        transform.position=parent.position+Vector3.up*(parent.GetComponent<Collider>().bounds.size.y);

        //부모의 체력을 끌고와서 업데이트 해주면됨
    }
}
