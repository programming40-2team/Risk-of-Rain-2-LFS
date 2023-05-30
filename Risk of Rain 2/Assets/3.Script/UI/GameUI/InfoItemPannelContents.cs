using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoItemPannelContents : MonoBehaviour
{
    private void OnEnable()
    {
        Init();
    }
    public void Init()
    {

        foreach (Transform transforom in gameObject.GetComponentInChildren<Transform>())
        {
            Managers.Resource.Destroy(transforom.gameObject);
        }

        foreach (var key in Managers.ItemInventory.Items.Keys)
        {
            GameItemImage ItemImage = Managers.UI.ShowGameUI<GameItemImage>();
            ItemImage.transform.SetParent(gameObject.transform);
            ItemImage.Itemcode = key;
            ItemImage.SetItemGameUI(key);
        }

    }

}
