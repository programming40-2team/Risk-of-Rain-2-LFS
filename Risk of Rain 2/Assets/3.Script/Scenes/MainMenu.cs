public class MainMenu : BaseScene
{
    void Start()
    {
        SceneType = Define.Scene.MainMenu;

        //음.. 모든 Addserable의 라벨을 Asynchronous_Load 로 할필요 없고,
        //다음 씬으로 넘어갈 때 나,, 뭐 이런식으로 리소스 그때마다 로딩 해도 되는데
        //프로젝트가 작기도 하고 하니까 일단 여기서는 한번에 모든 것을 로드 
        //Managers.Resource.LoadAllAsync<Object>("Asynchronous_Load", (key, count, totalCount) =>
        //{
        //    //  Debug.Log($"{key} {count}/{totalCount}");

        //    if (count == totalCount)
        //    {
        //        Debug.Log("데이터 로딩 완료!");
        //        StartLoaded();
        //    }
        //});

        Init();
        Managers.Data.Init();
        Managers.ItemInventory.init();
        Managers.ItemApply.Init();
        Managers.UI.ShowSceneUI<MainUI>();
        Managers.UI.ShowSceneUI<MouseInteraction>();

    }

    void StartLoaded()
    {
        Init();
        Managers.Data.Init();
        Managers.ItemInventory.init();
        Managers.ItemApply.Init();
        Managers.UI.ShowSceneUI<MainUI>();
        Managers.UI.ShowSceneUI<MouseInteraction>();

    }


    protected override void Init()
    {
        base.Init();


    }
    public override void Clear()
    {

    }
}
