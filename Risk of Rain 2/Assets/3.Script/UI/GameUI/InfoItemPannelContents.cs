using System.Linq;
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

        foreach (var key in Managers.ItemInventory.Items.Values.Where(s => s.Count >= 1))
        {
            ResultItemImage ItemImage = Managers.UI.ShowGameUI<ResultItemImage>();
            ItemImage.transform.SetParent(gameObject.transform);
            ItemImage._myitem = key;

        }



    }

}
