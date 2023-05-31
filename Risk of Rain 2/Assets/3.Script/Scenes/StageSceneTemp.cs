using UnityEngine;

public class StageSceneTemp : BaseScene
{
    // Start is called before the first frame update
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
