using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DamageUI : UI_Base
{
    private float moveSpeed;
    private float alphaSpeed;
    private float destroyTime;
    private Color alpha;
    public int Damage;
    enum Texts
    {
        DamageText,
    }
    public override void Init()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = 2.0f;
        alphaSpeed = 2.0f;
        destroyTime = 2.0f;
        Bind<TextMeshProUGUI>(typeof(Texts));
        alpha = GetText((int)Texts.DamageText).color;
        GetText((int)Texts.DamageText).text=$"{Damage}";

        //오브젝트풀 해야함 나중에 디테일
        Managers.Resource.Destroy(gameObject, destroyTime);

    }

    void Update()
    {
        transform.Translate(new Vector3(0, moveSpeed * Time.deltaTime, 0)); // 텍스트 위치

        alpha.a = Mathf.Lerp(alpha.a, 0, Time.deltaTime * alphaSpeed); // 텍스트 알파값
        GetText((int)Texts.DamageText).color = alpha;
    }
}
