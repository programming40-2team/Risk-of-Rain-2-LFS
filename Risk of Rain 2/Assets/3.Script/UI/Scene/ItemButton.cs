using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemButton : UI_Scene
{
    public int Itemcode = -1;
    Color prevClickEffectcolor;
    enum EImages
    {
        ItemImage,
        ItemBackGround,


    }
    enum EGameObjects
    {
        ClickEffect,


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

        prevClickEffectcolor = Get<GameObject>((int)EGameObjects.ClickEffect).GetComponent<Image>().color;
        gameObject.GetComponent<Image>().enabled = true;
        Get<GameObject>((int)EGameObjects.ClickEffect).SetActive(false);


        GetImage((int)EImages.ItemImage).sprite = Managers.Resource.LoadSprte(Managers.Data.ItemDataDict[Itemcode].iconkey);
        gameObject.BindEvent((PointerEventData data) => ItemButtonClick());
        gameObject.BindEvent((PointerEventData data) => ItemButtonPointerEnter(),Define.UIEvent.PointerEnter);
        gameObject.BindEvent((PointerEventData data) => ItemButtonPointerExit(),Define.UIEvent.PointerExit);

        SetImage(Itemcode);
    }

    private void ItemButtonClick()
    {
        Debug.Log("아이템 상세 정보 창 출력!");
    }


    private void ItemButtonPointerEnter()
    {
        Get<GameObject>((int)EGameObjects.ClickEffect).SetActive(true);
        if (!Managers.Data.ItemDataDict[Itemcode].isHaveHad)
        {
            Color tempcolor = Color.red;
            tempcolor.a = 0.5f;
            Get<GameObject>((int)EGameObjects.ClickEffect).GetComponent<Image>().color = tempcolor;
        }
        Managers.Event.PostNotification(Define.EVENT_TYPE.LogBookItem, this);
    }
    private void ItemButtonPointerExit()
    {
        Get<GameObject>((int)EGameObjects.ClickEffect).SetActive(false);
        if (!Managers.Data.ItemDataDict[Itemcode].isHaveHad)
        {
            Get<GameObject>((int)EGameObjects.ClickEffect).GetComponent<Image>().color = prevClickEffectcolor;
        }
    }
    private void SetImage(int itemcode)
    {
        GetImage((int)EImages.ItemImage).sprite =
            Managers.Resource.LoadSprte(Managers.Data.ItemDataDict[itemcode].iconkey);
    }

}
