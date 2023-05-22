using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LogBook : UI_Scene,IListener
{
    //ㅈㅅ 고민중 프로퍼티 public으로 만들지.. 아직 설계가 ..좀!
    //LogBook과 DetailLogBook의 의존성?.. 최소화 노력중
    private int Iteminfocode { get; set; } = -1;
    private int enivinfocode { get; set; } = -1;
    private int monsterinfocode { get; set; } = -1;
    private int Characterinfocode { get; set; } = -1;
    private bool iseverclicked ;
    private DetailInLogBook detailInLogBook;
    public  static Define.ECurrentClickType ClickType { get; private set; }=Define.ECurrentClickType.None;
    private Color subMenuImagePrevColor;
    private Color subMenuButtonPrevColor;
    enum ETexts
    {
        ItemAndEquipText,
        MonsterText,
        EnvironmentText,
        CharacterText,
        LogBookTitleText,
        LogBookSubText,

        IneventoryDescirbeTitleBackGroundText,
        IneventoryDescirbeTitleText,
        IneventoryDescirbeText,
        IneventoryIsAquireText,

    }
    enum EButtons
    {
        ItemAndEquip,
        Monster,
        Environment,
        Character,
        BackButton,
    }
    enum EGameObjects
    {
        ItemIneventoryPannel,
        MonsterIneventoryPannel,
        EnvionmentIneventoryPannel,
        CharacterIneventoryPannel,

    }
    enum EImages
    {
        ItemAndEquipColor,
        MonsterColor,
        EnvironmentColor,
        CharacterColor,

    }
    public override void Init()
    {
        base.Init();
      
        gameObject.GetComponent<Canvas>().sortingOrder = (int)Define.SortingOrder.LogBookUI;
        Bind<TextMeshProUGUI>(typeof(ETexts));
        Bind<Button>(typeof(EButtons));
        Bind<GameObject>(typeof(EGameObjects));
        Bind<Image>(typeof(EImages));

        subMenuImagePrevColor = GetImage((int)EImages.CharacterColor).color;
        subMenuButtonPrevColor = GetButton((int)EButtons.ItemAndEquip).GetComponent<Image>().color;
        Managers.Event.AddListener(Define.EVENT_TYPE.LogBookItem, this);
        Managers.Event.AddListener(Define.EVENT_TYPE.ClickLogBookDetail, this);

        //각 버튼 별 클릭 이벤트
        GetButton((int)EButtons.ItemAndEquip).gameObject
            .BindEvent((PointerEventData data) => ItemAndEquipClickEvent());
        GetButton((int)EButtons.Character).gameObject
            .BindEvent((PointerEventData data) => CharacterClickEvent());
        GetButton((int)EButtons.Environment).gameObject
            .BindEvent((PointerEventData data) => EnvironmentClickEvent());
        GetButton((int)EButtons.Monster).gameObject
            .BindEvent((PointerEventData data) => MonsterClickEvent());
        GetButton((int)EButtons.BackButton).gameObject
            .BindEvent((PointerEventData data) => BackButtonEvent());
        //각 버튼들 포인터 In Out 칼라 변경 이벤트
        GetButton((int)EButtons.ItemAndEquip).gameObject
            .BindEvent((PointerEventData data) => GetImage((int)EImages.ItemAndEquipColor).color=Color.yellow,Define.UIEvent.PointerEnter);
        GetButton((int)EButtons.ItemAndEquip).gameObject
           .BindEvent((PointerEventData data) => GetImage((int)EImages.ItemAndEquipColor).color = subMenuImagePrevColor, Define.UIEvent.PointerExit);
        GetButton((int)EButtons.Monster).gameObject
         .BindEvent((PointerEventData data) => GetImage((int)EImages.MonsterColor).color = Color.yellow, Define.UIEvent.PointerEnter);
        GetButton((int)EButtons.Monster).gameObject
           .BindEvent((PointerEventData data) => GetImage((int)EImages.MonsterColor).color = subMenuImagePrevColor, Define.UIEvent.PointerExit);
        GetButton((int)EButtons.Environment).gameObject
                 .BindEvent((PointerEventData data) => GetImage((int)EImages.EnvironmentColor).color = Color.yellow, Define.UIEvent.PointerEnter);
        GetButton((int)EButtons.Environment).gameObject
           .BindEvent((PointerEventData data) => GetImage((int)EImages.EnvironmentColor).color = subMenuImagePrevColor, Define.UIEvent.PointerExit);
        GetButton((int)EButtons.Character).gameObject
                 .BindEvent((PointerEventData data) => GetImage((int)EImages.CharacterColor).color = Color.yellow, Define.UIEvent.PointerEnter);
        GetButton((int)EButtons.Character).gameObject
           .BindEvent((PointerEventData data) => GetImage((int)EImages.CharacterColor).color = subMenuImagePrevColor, Define.UIEvent.PointerExit);




        UIInit();


        #region 팝업 로그북
        {
            detailInLogBook = Managers.UI.ShowPopupUI<DetailInLogBook>();
            detailInLogBook.gameObject.SetActive(false);
        }
        #endregion

    }
    private void BackButtonEvent()
    {
        Managers.UI.ClosePopupUI(detailInLogBook);
        Managers.Resource.Destroy(gameObject);
        ClickType = Define.ECurrentClickType.None;
    }
    private void UIInit()
    {
        GetText((int)ETexts.ItemAndEquipText).text = "아이템과 장비";
        GetText((int)ETexts.MonsterText).text = "몬스터";
        GetText((int)ETexts.EnvironmentText).text = "환경";
        GetText((int)ETexts.CharacterText).text = "생존자";
        GetText((int)ETexts.LogBookSubText).text = "원하는 메뉴를 선택해 주세요!";
        GetText((int)ETexts.IneventoryDescirbeTitleBackGroundText).text = "";
        GetText((int)ETexts.IneventoryDescirbeTitleText).text = "";
        GetText((int)ETexts.IneventoryDescirbeText).text = "";
        GetText((int)ETexts.IneventoryIsAquireText).text = "";

        Get<GameObject>((int)EGameObjects.ItemIneventoryPannel).SetActive(false);
        Get<GameObject>((int)EGameObjects.EnvionmentIneventoryPannel).SetActive(false);
        Get<GameObject>((int)EGameObjects.CharacterIneventoryPannel).SetActive(false);
        Get<GameObject>((int)EGameObjects.MonsterIneventoryPannel).SetActive(false);
    }
    void Start()
    {
        Init();
    }
    private void ItemAndEquipClickEvent()
    {
        SelectMenu(Define.ECurrentClickType.ItemAndEquip);
        GetText((int)ETexts.LogBookSubText).text = "아이템과 장비";

    }
    private void MonsterClickEvent()
    {
        SelectMenu(Define.ECurrentClickType.Monster);
        GetText((int)ETexts.LogBookSubText).text = "몬스터";
    }
    private void EnvironmentClickEvent()
    {
        SelectMenu(Define.ECurrentClickType.Enviroment);
        GetText((int)ETexts.LogBookSubText).text = "환경";

    }
    private void CharacterClickEvent()
    {
        SelectMenu(Define.ECurrentClickType.Character);
        GetText((int)ETexts.LogBookSubText).text = "생존자";

    }




    private void SetText()
    {
        switch (ClickType)
        {
            case Define.ECurrentClickType.ItemAndEquip:
                GetText((int)ETexts.IneventoryDescirbeTitleBackGroundText).text
                    = $"<mark =#8A2BE2>[{Managers.Data.ItemDataDict[Iteminfocode].itemname}]</mark>";
                GetText((int)ETexts.IneventoryDescirbeTitleText).text
                    = $"[{Managers.Data.ItemDataDict[Iteminfocode].itemname}]";
                GetText((int)ETexts.IneventoryDescirbeText).text
                    = $"{Managers.Data.ItemDataDict[Iteminfocode].explanation}";
             
                GetText((int)ETexts.IneventoryIsAquireText).text = $"획득 여부 : {ConvertToSTring(Managers.Data.ItemDataDict[Iteminfocode].isHaveHad)}";
                break;
            case Define.ECurrentClickType.Monster:

               //각 도감별 코드에 따른 데이터 확인
                break;
            case Define.ECurrentClickType.Character:
                GetText((int)ETexts.IneventoryDescirbeTitleBackGroundText).text
                  = $"<mark =#8A2BE2>[{Managers.Data.CharacterDataDict[Characterinfocode].Name}]</mark>";
                GetText((int)ETexts.IneventoryDescirbeTitleText).text
                    = $"[{Managers.Data.CharacterDataDict[Characterinfocode].Name}]";
                GetText((int)ETexts.IneventoryDescirbeText).text
                    = $"{Managers.Data.CharacterDataDict[Characterinfocode].unlockscript2}";
                GetText((int)ETexts.IneventoryIsAquireText).text = $"획득 여부 : {ConvertToSTring(Managers.Data.CharacterDataDict[Characterinfocode].isActive)}";
                //각 도감별 코드에 따른 데이터 확인
                break;
            case Define.ECurrentClickType.Enviroment:

                //각 도감별 코드에 따른 데이터 확인
                break;
        }
    }
    private void SelectMenu(Define.ECurrentClickType _clickType)
    {
        Get<GameObject>((int)EGameObjects.ItemIneventoryPannel).SetActive(false);
        Get<GameObject>((int)EGameObjects.EnvionmentIneventoryPannel).SetActive(false);
        Get<GameObject>((int)EGameObjects.CharacterIneventoryPannel).SetActive(false);
        Get<GameObject>((int)EGameObjects.MonsterIneventoryPannel).SetActive(false);
        GetButton((int)EButtons.ItemAndEquip).GetComponent<Image>().color = subMenuButtonPrevColor;
        GetButton((int)EButtons.Environment).GetComponent<Image>().color = subMenuButtonPrevColor;
        GetButton((int)EButtons.Character).GetComponent<Image>().color = subMenuButtonPrevColor;
        GetButton((int)EButtons.Monster).GetComponent<Image>().color = subMenuButtonPrevColor;

        switch (_clickType)
        {
            case Define.ECurrentClickType.ItemAndEquip:
                ClickType= Define.ECurrentClickType.ItemAndEquip;
                Get<GameObject>((int)EGameObjects.ItemIneventoryPannel).SetActive(true);
                GetButton((int)EButtons.ItemAndEquip).GetComponent<Image>().color = Color.yellow;
                break;
            case Define.ECurrentClickType.Monster:
                ClickType= Define.ECurrentClickType.Monster;
                Get<GameObject>((int)EGameObjects.MonsterIneventoryPannel).SetActive(true);
                GetButton((int)EButtons.Monster).GetComponent<Image>().color = Color.yellow;
                break;
            case Define.ECurrentClickType.Character:
                ClickType= Define.ECurrentClickType.Character;
                Get<GameObject>((int)EGameObjects.CharacterIneventoryPannel).SetActive(true);
                GetButton((int)EButtons.Character).GetComponent<Image>().color = Color.yellow;
                break;
            case Define.ECurrentClickType.Enviroment:
                ClickType= Define.ECurrentClickType.Enviroment;
                Get<GameObject>((int)EGameObjects.EnvionmentIneventoryPannel).SetActive(true);
                GetButton((int)EButtons.Environment).GetComponent<Image>().color = Color.yellow;
                break;
        }
    }

    public void OnEvent(Define.EVENT_TYPE Event_Type, Component Sender, object Param = null)
    {
        //이벤트 처리 과정 이벤트 타입에 따라서 처리
        switch (Event_Type)
        {
            case Define.EVENT_TYPE.LogBookItem:
                iseverclicked = true;
                if (Sender.TryGetComponent(out ItemButton itemButton))
                {
                    Iteminfocode = itemButton.Itemcode;
                }
                else if (Sender.TryGetComponent(out InvenCharacterButton CharButton))
                {
                    Characterinfocode = CharButton.Charactercode;
                }

                SetText();
                break;
            case Define.EVENT_TYPE.ClickLogBookDetail:
                Debug.Log("ClickLogBookDetail 이벤트 발송");
                detailInLogBook.gameObject.SetActive(true);
                if (Sender.TryGetComponent(out ItemButton itemButtons))
                {
                    detailInLogBook.SpecialCode = itemButtons.Itemcode;
                }
                else if (Sender.TryGetComponent(out InvenCharacterButton CharButton))
                {
                    detailInLogBook.SpecialCode = CharButton.Charactercode;
                }
       
                break;
        }
   
    }

    private string ConvertToSTring(bool result)
    {
        return result ? "보유" : "미보유";
    }



}
