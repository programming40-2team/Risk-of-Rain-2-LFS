using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static RelicExtendPopupUI;

public class InvenMonsterButton : UI_Scene
{

    public int MonsterCode = -1;
    enum Images
    {
        MonsterImage,
        IsHaveCharacter
    }
    enum GameObjects
    {
        Monster_RectImage_Image,

    }
    private void Start()
    {
        Init();
    }
    public override void Init()
    {
        base.Init();
        GetComponent<Canvas>().sortingOrder = (int)Define.SortingOrder.CharacterSelectButton;
        Bind<Image>(typeof(Images));
        Bind<GameObject>(typeof(GameObjects));
        Get<GameObject>((int)GameObjects.Monster_RectImage_Image).SetActive(false);

        gameObject.BindEvent((PointerEventData data) => MoncterButtonPointerEnter(), Define.UIEvent.PointerEnter);
        gameObject.BindEvent((PointerEventData data) => MonsterButtonPointerExit(), Define.UIEvent.PointerExit);
        gameObject.BindEvent((PointerEventData data) => MonsterButtonClcik());
 
        SetImage();
    }
    private void SetImage()
    {
        GetImage((int)Images.MonsterImage).sprite = Managers.Resource.LoadSprte($"{MonsterCode}");

    }
    private void MonsterButtonClcik()
    {
        Managers.Event.PostNotification(Define.EVENT_TYPE.ClickLogBookDetail, this);

    }
    private void MoncterButtonPointerEnter()
    {

        Managers.Event.PostNotification(Define.EVENT_TYPE.LogBookItem, this);
        Get<GameObject>((int)GameObjects.Monster_RectImage_Image).SetActive(true);
        GetImage((int)Images.IsHaveCharacter).GetComponent<Image>().enabled = true;
    }
    private void MonsterButtonPointerExit()
    {
        Get<GameObject>((int)GameObjects.Monster_RectImage_Image).SetActive(false);
        GetImage((int)Images.IsHaveCharacter).GetComponent<Image>().enabled = false;
    }


}
