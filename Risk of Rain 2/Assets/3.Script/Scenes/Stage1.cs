using UnityEngine;

public class Stage1 : BaseScene
{
   
    // Start is called before the first frame update
    void Start()
    {
        Managers.Resource.LoadAllAsync<Object>("Asynchronous_Load", (key, count, totalCount) =>
        {
            //  Debug.Log($"{key} {count}/{totalCount}");

            if (count == totalCount)
            {
                Debug.Log("임시 코드 !");
                Debug.Log("추후 이부분 StartLoaded 제거해야함");
                Debug.Log("        Managers.UI.ShowGameUI<GameUI>(); 넣어줘야함");
                StartLoaded();
            }
        });
        SceneType = Define.Scene.Stage1;

    }
    void StartLoaded()
    {
        Init();
        Managers.Data.Init();
        Managers.ItemInventory.init();
        Managers.ItemApply.Init();
        Managers.UI.ShowGameUI<GameUI>();

        Managers.Game.Gold = 9900;
    }
    protected override void Init()
    {
        base.Init();


    }

    public override void Clear()
    {
    }
}
