using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class GameItemPannel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }
    //먹을 떄 마다 삭제 재생성 or 현재 아이템 개수에 따라 부모 자식 위치 스왑
    public void Init()
    {
        foreach (Transform transforom in gameObject.GetComponentInChildren<Transform>())
        {
            Managers.Resource.Destroy(transforom.gameObject);
        }

        foreach (var key in Managers.ItemInventory.Items.Keys)
        {
            GameItemImage ItemImage = Managers.UI.ShowSceneUI<GameItemImage>();
            ItemImage.transform.SetParent(gameObject.transform);
            ItemImage.Itemcode = key;

        }

    }



}
