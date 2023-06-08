using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResultItemImage : UI_Game
{
    public Item _myitem;
    enum ETexts
    {
        ItemCount
    }
    public override void Init()
    {
        base.Init();
        base.Init();
        Bind<TextMeshProUGUI>(typeof(ETexts));
        SetItemGameUI(_myitem);
    }
    // Start is called before the first frame update
    void Start()
    {
        Init();   
    }
    public void SetItemGameUI(Item _Item)
    {
        if (_Item == null) return;
        else 
        {
            gameObject.SetActive(true);
            GetText((int)ETexts.ItemCount).text = $"x{_myitem.Count}";
            GetComponent<Image>().sprite = Managers.Resource.LoadSprte($"{_myitem.ItemIconKey}");
        }

    }
}
