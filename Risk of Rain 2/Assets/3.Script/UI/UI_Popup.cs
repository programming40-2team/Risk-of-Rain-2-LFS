using UnityEngine;

public class UI_Popup : UI_Base
{
    public override void Init()
    {
        Managers.UI.SetCanvas(gameObject, true);
        GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceCamera;
        GetComponent<Canvas>().worldCamera = Camera.main;
    }

    public virtual void ClosePopupUI()
    {
        Managers.UI.ClosePopupUI(this);
    }
}
