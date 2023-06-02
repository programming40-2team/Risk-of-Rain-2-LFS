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
    private void Awake()
    {
        Bind<TextMeshProUGUI>(typeof(Texts));
    }
    public override void Init()
    {
    }

    // Start is called before the first frame update
    void OnEnable()
    {
        moveSpeed = 2.0f;
        alphaSpeed = 2.0f;
        destroyTime = 2.0f;
        alpha = GetText((int)Texts.DamageText).color;
        GetText((int)Texts.DamageText).text=$"{Damage}";

        StartCoroutine(nameof(Damage_Effect_co));

    }

    private IEnumerator Damage_Effect_co()
    {
        float deleteTime = 2f;
        float currentTime = 0f;
        while (true)
        {
            transform.Translate(new Vector3(0, moveSpeed * Time.deltaTime, 0)); // 텍스트 위치

            alpha.a = Mathf.Lerp(alpha.a, 0, Time.deltaTime * alphaSpeed); // 텍스트 알파값
            GetText((int)Texts.DamageText).color = alpha;
            currentTime += Time.deltaTime;
            yield return null;
            if (currentTime > deleteTime)
            {
                Managers.Resource.Destroy(gameObject);
            }
        }
    }
    private void OnDisable()
    {
        StopCoroutine(nameof(Damage_Effect_co));
    }
}
