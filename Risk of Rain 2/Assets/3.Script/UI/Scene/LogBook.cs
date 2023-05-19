using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class LogBook : UI_Scene,IListener
{
    private int iteminfocode = -1;
    private bool iseverclicked;

    private ECurrentClickType ClickType;
    enum ECurrentClickType
    {
        ItemAndEquip,
        Monster,
        Survivor,
        Enviroment,

    }
    enum ETexts
    {
        ItemAndEquipText,
        MonsterText,
        EnvironmentText,
        SurvivorText,
        LogBookTitleText,
        LogBookSubText,

        IneventoryDescirbeTitleBackGroundText,
        IneventoryDescirbeTitleText,
        IneventoryDescirbeText,

    }
    enum EButtons
    {
        ItemAndEquip,
        Monster,
        Environment,
        Survivor,

    }
    public override void Init()
    {
        base.Init();
        gameObject.GetComponent<Canvas>().sortingOrder = (int)Define.SortingOrder.LogBookUI;
        Bind<TextMeshProUGUI>(typeof(ETexts));
        Bind<Button>(typeof(EButtons));
        Managers.Event.AddListener(Define.EVENT_TYPE.LogBookItem, this);
        TextInit();
        GetButton((int)EButtons.ItemAndEquip).gameObject
            .BindEvent((PointerEventData data) => ItemAndEquipClickEvent());
        GetButton((int)EButtons.Survivor).gameObject
            .BindEvent((PointerEventData data) => SurvivorClickEvent());
        GetButton((int)EButtons.Environment).gameObject
            .BindEvent((PointerEventData data) => EnvironmentClickEvent());
        GetButton((int)EButtons.Monster).gameObject
            .BindEvent((PointerEventData data) => MonsterClickEvent());

    }
    private void TextInit()
    {
        GetText((int)ETexts.ItemAndEquipText).text = "아이템과 장비";
        GetText((int)ETexts.MonsterText).text = "몬스터";
        GetText((int)ETexts.EnvironmentText).text = "환경";
        GetText((int)ETexts.SurvivorText).text = "생존자";
        GetText((int)ETexts.LogBookSubText).text = "원하는 메뉴를 선택해 주세요!";
        GetText((int)ETexts.IneventoryDescirbeTitleBackGroundText).text = "";
        GetText((int)ETexts.IneventoryDescirbeTitleText).text = "";
        GetText((int)ETexts.IneventoryDescirbeText).text = "";
    }
    void Start()
    {
        Init();
    }
    private void ItemAndEquipClickEvent()
    {
        
    }
    private void MonsterClickEvent()
    {

    }
    private void EnvironmentClickEvent()
    {

    }
    private void SurvivorClickEvent()
    {

    }




    private void SetText()
    {
        switch (ClickType)
        {
            case ECurrentClickType.ItemAndEquip:
                GetText((int)ETexts.IneventoryDescirbeTitleBackGroundText).text
                    = $"<mark =#8A2BE2>[{Managers.Data.ItemDataDict[iteminfocode].itemname}]</mark>";
                GetText((int)ETexts.IneventoryDescirbeTitleText).text
                    = $"[{Managers.Data.ItemDataDict[iteminfocode].itemname}]";
                GetText((int)ETexts.IneventoryDescirbeText).text
                    = $"{Managers.Data.ItemDataDict[iteminfocode].explanation}";
                GetText((int)ETexts.LogBookSubText).text = "아이템";
                break;
            case ECurrentClickType.Monster:
                break;
            case ECurrentClickType.Survivor:
                break;
            case ECurrentClickType.Enviroment:
                break;
        }
    }

    public void OnEvent(Define.EVENT_TYPE Event_Type, Component Sender, object Param = null)
    {
        iseverclicked = true;
        iteminfocode= Sender.GetComponent<ItemButton>().Itemcode;
        SetText();
    }




}
