using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item1003SkillComponent : ItemPrimitiive
{
    private float movespeed = 5f;

    [SerializeField] private float SkillDectDistance = 8f;
    private void Awake()
    {
        base.Init();

    }

    private void OnEnable()
    {
        StartCoroutine(nameof(ItemSpawnTime_co));
    }
    private IEnumerator ItemSpawnTime_co()
    {
        yield return new WaitForSeconds(7.0f);
        Managers.Resource.Destroy(gameObject);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            //일정 거리 안에 있을 경우에만 체력 팩이 따라가도록 ==> 콜라이더를 기ㅣ본적으로 크게 잡고
            // 범위 안에 들어왔을 때 거리를 감지하여 계산을 실행 -> 가까우면 다가가서  먹힘 ->
            // 멀면 먹히지 않고 그냥 가만히 아무것도 안함
            //거리 확인
            if ((other.transform.position - gameObject.transform.position).magnitude < SkillDectDistance)
            {
                //이동
                Vector3 movedir = Player.transform.position - gameObject.transform.position;
                gameObject.transform.Translate(movedir.normalized * movespeed * Time.deltaTime);

                if ((other.transform.position - gameObject.transform.position).magnitude < SkillDectDistance * 0.3f)
                {
                    //힐 동작
                    other.GetComponent<PlayerStatus>().OnHeal(8 +
                   other.GetComponent<PlayerStatus>().MaxHealth
                          * 0.02f * Managers.ItemInventory.Items[1003].Count);
                    Managers.Resource.Destroy(gameObject);
                    Debug.Log("회복 키트 먹으면 나타날 이펙트");
                }

            }
            else
            {
                //거리가 먼 경우

            }

        }
    }
}
