using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : BaseScene
{
    void Start()
    {
        base.Init();

        Managers.Resource.LoadAllAsync<Object>("Asynchronous_Load", (key, count, totalCount) =>
        {
            Debug.Log($"{key} {count}/{totalCount}");

            if (count == totalCount)
            {
               StartLoaded();
            }
        });
    }

    void StartLoaded()
    {
        Managers.Data.Init();
        Managers.ItemInventory.init();
    }


    protected override void Init()
    {
        base.Init();

    }
    public override void Clear()
    {
        
    }
}
