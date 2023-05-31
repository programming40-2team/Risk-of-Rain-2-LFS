using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item1017Skill : ItemPrimitiive
{
    [SerializeField]
    private float _detectorDistance=20f;
    Dictionary<int, GameObject> enemyTagert = new Dictionary<int, GameObject>();
    void Start()
    {
        gameObject.transform.position = Player.transform.position;
       GetComponent<Rigidbody>().velocity = Player.transform.forward * 15f; ;
    }

    //1. 적 몬스터 체력 깍이면 불러오기 -> 스킬 총 개수가 적어서 체계적인 관리가 힘듬
    //2. 플레이어가 공격할 때 투명한 물체를 발사하고 해당 물체가 맞아서 사라질 경우  투사체에서 이 함수 실행 -> Always로 


    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) //스킬들 전부 테그 가져오는 거 임시, 추후  컴포넌트를 가져오던가 변경 예정
        {
            if (enemyTagert.ContainsKey(other.GetHashCode()))
            {
                return;
            }
            else
            {
                enemyTagert.Add(other.GetHashCode(), other.gameObject);
            }

        }

    }
    private void OnDisable()
    {
        StopCoroutine(nameof(Item1017Skill_co));
    }
    private IEnumerator Item1017Skill_co()
    {
        while (true)
        {
            if (enemyTagert.Count != 0)
            {
                yield return new WaitForSeconds(2.0f);
            }
            else
            {
               foreach(var go in enemyTagert.Values)
                {
                    if ((go.transform.position - gameObject.transform.position).magnitude < _detectorDistance)
                    {
                        go.GetComponent<MeshRenderer>().enabled = true;
                    }
                }
            }
            yield return new WaitForSeconds(3.0f);
        }

    }
}
