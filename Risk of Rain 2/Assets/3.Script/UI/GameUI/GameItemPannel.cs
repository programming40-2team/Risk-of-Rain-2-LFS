using UnityEngine;

public class GameItemPannel : MonoBehaviour
{
    // Start is called before the first frame update
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

        foreach (var key in Managers.ItemInventory.PassiveItem.Keys)
        {
            GameItemImage ItemImage = Managers.UI.ShowGameUI<GameItemImage>();
            ItemImage.transform.SetParent(gameObject.transform);
            ItemImage.Itemcode = key;

        }

    }


}