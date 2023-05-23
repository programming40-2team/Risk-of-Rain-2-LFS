using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : BaseScene
{
    void Start()
    {
    

        Managers.Resource.LoadAllAsync<Object>("Asynchronous_Load", (key, count, totalCount) =>
        {
          //  Debug.Log($"{key} {count}/{totalCount}");

            if (count == totalCount)
            {
                Debug.Log("데이터 로딩 완료!");
               StartLoaded();
            }
        });
       
    }

    void StartLoaded()
    {
        Init();
        Managers.Data.Init();
        Managers.ItemInventory.init();
        Managers.UI.ShowSceneUI<MainUI>();
        Managers.UI.ShowSceneUI<MouseInteraction>();
        Managers.Resource.Instantiate("ItemContainer001");
    }


    protected override void Init()
    {
        base.Init();


    }
    public override void Clear()
    {
        
    }
}
