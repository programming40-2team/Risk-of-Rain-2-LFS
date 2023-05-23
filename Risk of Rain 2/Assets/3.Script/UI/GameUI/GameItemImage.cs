using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

public class GameItemImage : UI_Scene
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
        gameObject.SetActive(false);
        Managers.Event.AddItem -= SetItemGameUI;
        Managers.Event.AddItem += SetItemGameUI;
    }

    void Start()
    {
        Init();
    }

    private void SetItemGameUI(int _Itemcode)
    {
        if (Itemcode == _Itemcode)
        {
            gameObject.SetActive(true);
            GetText((int)ETexts.ItemCount).text = $"x{Managers.Data.ItemDataDict[_Itemcode].count}";
        }

    }


}
