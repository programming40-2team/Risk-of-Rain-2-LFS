using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameItemImage : UI_Game
{
    public int Itemcode = -1;

    enum ETexts
    {
        ItemCount
    }
    public override void Init()
    {
        base.Init();
        Bind<TextMeshProUGUI>(typeof(ETexts));
        GetText((int)ETexts.ItemCount).text = $"x{Managers.Data.ItemDataDict[Itemcode].count}";
        GetComponent<Image>().sprite = Managers.Resource.LoadSprte($"{Managers.Data.ItemDataDict[Itemcode].iconkey}");
        Debug.Log(transform.parent.name);
        Managers.Event.AddItem -= SetItemGameUI;
        Managers.Event.AddItem += SetItemGameUI;

        gameObject.SetActive(false);
    }

    void Start()
    {
        Init();
    }
    public void SetItemGameUI(int _Itemcode)
    {
        if (Itemcode == _Itemcode)
        {
            gameObject.SetActive(true);
            GetText((int)ETexts.ItemCount).text = $"x{Managers.ItemInventory.Items[_Itemcode].Count}";
        }

    }


}
