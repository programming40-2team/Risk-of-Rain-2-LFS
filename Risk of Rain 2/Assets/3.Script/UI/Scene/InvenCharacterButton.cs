using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InvenCharacterButton : UI_Scene
{

    public int Charactercode = -1;
    private Color prevColor;
    enum EImages
    {
        CharacterImage,
        IsHaveCharacter,
        Character_RectImage,
    }
    enum EGameObjects
    {
        Character_RectImage_Image,

    }
    void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();
        GetComponent<Canvas>().sortingOrder = (int)Define.SortingOrder.CharacterSelectButton;
        Bind<Image>(typeof(EImages));
        Bind<GameObject>(typeof(EGameObjects));
        Get<GameObject>((int)EGameObjects.Character_RectImage_Image).SetActive(false);

        gameObject.BindEvent((PointerEventData data) => CharcterButtonPointerEnter(), Define.UIEvent.PointerEnter);
        gameObject.BindEvent((PointerEventData data) => CharcterButtonPointerExit(), Define.UIEvent.PointerExit);
        gameObject.BindEvent((PointerEventData data) => CharcterButtonClcik());
        prevColor = GetImage((int)EImages.IsHaveCharacter).GetComponent<Image>().color;
        SetImage();
    }

    private void SetImage()
    {
        GetImage((int)EImages.CharacterImage).sprite = Managers.Resource.LoadSprte(Managers.Data.CharacterDataDict[Charactercode].iconkey);

        if (Managers.Data.CharacterDataDict[Charactercode].isActive)
        {
            GetImage((int)EImages.CharacterImage).color= Color.white;
        }
    }
   private void CharcterButtonClcik()
    {
        Managers.Event.PostNotification(Define.EVENT_TYPE.ClickLogBookDetail, this);

    }
    private void CharcterButtonPointerEnter()
    {

        Managers.Event.PostNotification(Define.EVENT_TYPE.LogBookItem, this);
        Debug.Log("도감에서 캐릭터 마우스 포인터 들어오면 음악을 넣으실 껀가요??");
        Get<GameObject>((int)EGameObjects.Character_RectImage_Image).SetActive(true);

        if (Managers.Data.CharacterDataDict[Charactercode].isActive)
        {
            GetImage((int)EImages.IsHaveCharacter).GetComponent<Image>().color = Color.yellow;
        }
        else
        {
            GetImage((int)EImages.IsHaveCharacter).GetComponent<Image>().color = Color.red;
        }

    }
    private void CharcterButtonPointerExit()
    {
        Get<GameObject>((int)EGameObjects.Character_RectImage_Image).SetActive(false);
        GetImage((int)EImages.IsHaveCharacter).GetComponent<Image>().color = prevColor;
    }



}