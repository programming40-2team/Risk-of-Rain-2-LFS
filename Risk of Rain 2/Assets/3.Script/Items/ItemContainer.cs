using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemContainer : MonoBehaviour
{
    public int price;

    private void Start()
    {
        price = Random.Range(25, 50);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("UI 띄우자 ");
            Debug.Log("상호작용 키를 누르면 아이템을 획득 이 아니라 생성 ");
            Debug.Log("상자로부터 랜덤 위치 생성 인데, 나중에 영일이가 이펙트로 생성된 지점 과 박스 오브젝트 점을 뭐" +
                "포물선으로 연결해서 잘 해주지 않을까? ");
            Debug.Log("아이템은 생성되고, 독자적인 Collider를 가지고 있어서 해당 아이템 Collider에 들어가면 아이템을 획득");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        
    }

}
