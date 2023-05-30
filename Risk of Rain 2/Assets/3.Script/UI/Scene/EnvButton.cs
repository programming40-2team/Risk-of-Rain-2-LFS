using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EnvButton : UI_Scene
{
    public int EnvCode = -1;
    private void Start()
    {
        Init();
    }
    public override void Init()
    {
        base.Init();
        GetComponent<Canvas>().sortingOrder = (int)Define.SortingOrder.CharacterSelectButton;
        gameObject.BindEvent((PointerEventData data) => EnvButtonPointerEnter(), Define.UIEvent.PointerEnter);
        gameObject.BindEvent((PointerEventData data) => EnvButtonPointerExit(), Define.UIEvent.PointerExit);
        gameObject.BindEvent((PointerEventData data) => EnvButtonPointerClick());


        SetImage();
    }
    private void SetImage()
    {
        gameObject.GetComponent<Image>().sprite
            = Managers.Resource.LoadSprte(Managers.Data.EnvDataDict[EnvCode].imagekey);
    }
    private void EnvButtonPointerEnter()
    {
        Managers.Event.PostNotification(Define.EVENT_TYPE.LogBookItem, this);
    }
    private void EnvButtonPointerExit()
    {

    }
    private void EnvButtonPointerClick()
    {
        //Managers.Event.PostNotification(Define.EVENT_TYPE.ClickLogBookDetail, this);
    }
}
