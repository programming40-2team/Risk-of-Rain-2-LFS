using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemIneventoryPannel : MonoBehaviour
{
    void Start()
    {
        Init();
    }
    public void Init()
    {
        foreach (Transform transforom in gameObject.GetComponentInChildren<Transform>())
        {
            Managers.Resource.Destroy(transforom.gameObject);
        }
        foreach(int i in Managers.Data.ItemDataDict.Keys)
        {
            ItemButton item = Managers.UI.ShowSceneUI<ItemButton>();
            item.transform.SetParent(gameObject.transform);
            item.Itemcode = i;
        }

    }
}
