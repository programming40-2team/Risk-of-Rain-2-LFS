using UnityEngine;

public class UI_Scene : UI_Base
{
    public override void Init()
    {
        Managers.UI.SetCanvas(gameObject, false);
        GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceCamera;
        GetComponent<Canvas>().worldCamera = Camera.main;
    }
}
