using UnityEngine;

public class Stage1 : BaseScene
{
    private void Awake()
    {

        SceneType = Define.Scene.Stage1;
        Managers.UI.ShowGameUI<GameUI>();
    }
  
  
    protected override void Init()
    {
        base.Init();


    }

    public override void Clear()
    {
    }
}
