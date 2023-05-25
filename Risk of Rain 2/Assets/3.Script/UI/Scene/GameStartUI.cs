using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using static Define;

public class GameStartUI : UI_Scene,IListener
{
    private bool isCharacterSelected;
    private int characterCode;
    #region UI오브젝트 관리
    enum EGameObjects
    {
        SelectDifficulty,

        ShowScriptButtonIsClickEffect,
        SkillScriptIsClickEffect,
        LoadIsClickEffect,

        AboutScript,
        AboutSkill,
        AboutLoad,

        PassiveReactPannel,
        M1SkillReactPannel,
        M2SkillReactPannel,
        ShiftSkillReactPannel,
        RSkillReactPannel,
        LoadPassiveSkill,
    }
    enum ETexts
    {
        DifficultyText,
        ExtendText,
        RelicsText,
        RelicsUnLockText,
        RightPannelBackButtonText,

        ShowScriptButtonText,
        SkillScriptText,
        LoadText,

        CharacterNameText,
        MoreDetailText1,
        MoreDetailText2,
        MoreDetailText3,
        MoreDetailText4,
       

        PassiveSkillTitle,
        PassiveSkillText,
        M1SkillTitle,
        M1SkillText,
        M2SkillTitle,
        M2SkillText,
        RSkillTitle,
        RSkillText,
        ShiftSkillTitle,
        ShiftSkillText,




        GameReadyButtonText,
    }
    enum EButtons
    {
        RightPannelBackButton,

        ShowScriptButton,
        SkillScriptButton,
        LoadButton,


        DifficultyEasy,
        DifficultyNormal,
        DifficultyHard,


        GameReadyButton,


        LoadShiftSkillButton_1,
        LoadShiftSkillButton_2,
        LoadM2SkillButton_1,
        LoadM2SkillButton_2,
        LoadRSkillButton_1,
        LoadRSkillButton_2,


        ExtendSubButton,
        RelicsSubButton,


    }
    enum EImages
    {
        BackGround,

        DiffidcultyEasyBackGround,
        DiffidcultyNormalBackGround,
        DiffidcultyHardBackGround,


        PassiveSkillImage,
        M1SkilIImage,
        M2SkilIImage,
        RSkilIImage,
        ShiftSkillImage,

        LoadPassiveSkillImage,
        LoadShiftSkillImage_1,
        LoadShiftSkillImage_2,


        LoadM2SkillImage_1,
        LoadM2SkillImage_2,
        LoadRSkillImage_1,
        LoadRSkillImage_2,




    }
    #endregion
    #region 난이도,캐릭터 설명 변경을 위한 Enum값

    enum ECharacterDetail
    {
        AboutScript,
        AboutSkill,
        AboutLoad,

    }
    #endregion
    void Start()
    {
        Init();
      
    }
    public override void Init()
    {
        base.Init();
        GetComponent<Canvas>().sortingOrder = (int)Define.SortingOrder.GameStartUI;
        Bind<GameObject>(typeof(EGameObjects));
        Bind<Button>(typeof(EButtons));
        Bind<TextMeshProUGUI>(typeof(ETexts));
        Bind<Image>(typeof(EImages));
        Managers.Event.AddListener(Define.EVENT_TYPE.SelectCharacter, this);

        //글자 관련 초기화
        InitText();
        //게임 오브젝트 관련 초기화, 효과 off
        InitGameObect();
        //버튼 오브젝트 관련 초기화
        InitButton();

        foreach (Transform transforom in Get<GameObject>((int)EGameObjects.SelectDifficulty).GetComponentInChildren<Transform>())
        {
            Managers.Resource.Destroy(transforom.gameObject);
        }
        for(int i = 0; i < 3; i++)
        {
            Diffidculty diff = Managers.UI.ShowSceneUI<Diffidculty>();
            diff.myDifficulty = (EDifficulty)i;
            diff.transform.SetParent(Get<GameObject>((int)EGameObjects.SelectDifficulty).transform);
        }
        //디폴트 이미지 효과 Easy
        // DifficultySelect(EDifficulty.Easy);

    }

    private void InitText()
    {
        GetText((int)ETexts.DifficultyText).text = $"난이도";
        GetText((int)ETexts.ExtendText).text = $"확장팩";
        GetText((int)ETexts.RelicsText).text = $"유물";

        GetText((int)ETexts.ShowScriptButtonText).text = $"개요";
        GetText((int)ETexts.SkillScriptText).text = "스킬";
        GetText((int)ETexts.LoadText).text = "장전";
                     
        GetText((int)ETexts.CharacterNameText).text = "";
        GetText((int)ETexts.MoreDetailText1).text = "";
        GetText((int)ETexts.MoreDetailText2).text = "";
        GetText((int)ETexts.MoreDetailText3).text = "";
        GetText((int)ETexts.MoreDetailText4).text = "";
        GetText((int)ETexts.PassiveSkillTitle).text="";
        GetText((int)ETexts.PassiveSkillText).text="";
        GetText((int)ETexts.M1SkillTitle).text="";
        GetText((int)ETexts.M1SkillText).text="";
        GetText((int)ETexts.M2SkillTitle).text="";
        GetText((int)ETexts.M2SkillText).text="";
        GetText((int)ETexts.RSkillTitle).text="";
        GetText((int)ETexts.RSkillText).text = "";
        GetText((int)ETexts.ShiftSkillText).text = "";
        GetText((int)ETexts.ShiftSkillTitle).text = "";
        GetText((int)ETexts.RelicsUnLockText).text = "이용가능한 유물이 없습니다.";




        GetText((int)ETexts.GameReadyButtonText).text = $"게임시작";
    }

    private void InitGameObect()
    {

        Get<GameObject>((int)EGameObjects. ShowScriptButtonIsClickEffect).SetActive(false);
        Get<GameObject>((int)EGameObjects. SkillScriptIsClickEffect).SetActive(false);
        Get<GameObject>((int)EGameObjects. LoadIsClickEffect).SetActive(false);
        Get<GameObject>((int)EGameObjects. AboutScript).SetActive(false);
        Get<GameObject>((int)EGameObjects. AboutSkill).SetActive(false);
        Get<GameObject>((int)EGameObjects.AboutLoad).SetActive(false);

        #region 스킬창 포인터 엔터 아웃시 나타나는 색상 이벤트
        Get<GameObject>((int)EGameObjects.PassiveReactPannel)
            .BindEvent((PointerEventData data) => SetColor(Get<GameObject>((int)EGameObjects.PassiveReactPannel), Color.white),Define.UIEvent.PointerEnter);
        Get<GameObject>((int)EGameObjects.M1SkillReactPannel)
              .BindEvent((PointerEventData data) => SetColor(Get<GameObject>((int)EGameObjects.M1SkillReactPannel), Color.white), Define.UIEvent.PointerEnter);
        Get<GameObject>((int)EGameObjects.M2SkillReactPannel)
              .BindEvent((PointerEventData data) => SetColor(Get<GameObject>((int)EGameObjects.M2SkillReactPannel), Color.white), Define.UIEvent.PointerEnter);
        Get<GameObject>((int)EGameObjects.ShiftSkillReactPannel)
              .BindEvent((PointerEventData data) => SetColor(Get<GameObject>((int)EGameObjects.ShiftSkillReactPannel), Color.white), Define.UIEvent.PointerEnter);
        Get<GameObject>((int)EGameObjects.RSkillReactPannel)
              .BindEvent((PointerEventData data) => SetColor(Get<GameObject>((int)EGameObjects.RSkillReactPannel), Color.white), Define.UIEvent.PointerEnter);
      

        Get<GameObject>((int)EGameObjects.PassiveReactPannel)
           .BindEvent((PointerEventData data) => SetColor(Get<GameObject>((int)EGameObjects.PassiveReactPannel), Color.white), Define.UIEvent.PointerExit);
        Get<GameObject>((int)EGameObjects.M1SkillReactPannel)
              .BindEvent((PointerEventData data) => SetColor(Get<GameObject>((int)EGameObjects.M1SkillReactPannel), Color.white), Define.UIEvent.PointerExit);
        Get<GameObject>((int)EGameObjects.M2SkillReactPannel)                     
              .BindEvent((PointerEventData data) => SetColor(Get<GameObject>((int)EGameObjects.M2SkillReactPannel), Color.white), Define.UIEvent.PointerExit);
        Get<GameObject>((int)EGameObjects.ShiftSkillReactPannel)                  
              .BindEvent((PointerEventData data) => SetColor(Get<GameObject>((int)EGameObjects.ShiftSkillReactPannel), Color.white), Define.UIEvent.PointerExit);
        Get<GameObject>((int)EGameObjects.RSkillReactPannel)                      
              .BindEvent((PointerEventData data) => SetColor(Get<GameObject>((int)EGameObjects.RSkillReactPannel), Color.white), Define.UIEvent.PointerExit);
       
        #endregion
    }

    private void SetColor(GameObject go,Color color)
    {
        if (go.GetComponent<Image>().color.a.Equals(1))
        {
            color.a = 0;
            go.GetComponent<Image>().color = color;
        }
        else
        {
            color.a = 1;
            go.GetComponent<Image>().color = color;
        }
    }

    private void InitButton()
    {
        GetButton((int)EButtons.RightPannelBackButton).gameObject
            .BindEvent((PointerEventData data) => ReturnToMain());
        GetButton((int)EButtons.GameReadyButton).gameObject
            .BindEvent((PointerEventData data) => GameStart());

        GetButton((int)EButtons.ShowScriptButton).gameObject
            .BindEvent((PointerEventData data) => DetaillCharacterScriptChange(ECharacterDetail.AboutScript));
        GetButton((int)EButtons.SkillScriptButton).gameObject
            .BindEvent((PointerEventData data) => DetaillCharacterScriptChange(ECharacterDetail.AboutSkill));
        GetButton((int)EButtons.LoadButton).gameObject
            .BindEvent((PointerEventData data) => DetaillCharacterScriptChange(ECharacterDetail.AboutLoad));

        GetButton((int)EButtons.ExtendSubButton).gameObject
            .BindEvent((PointerEventData data) => Managers.UI.ShowPopupUI<RelicExtendPopupUI>().MyType=RelicExtendPopupUI.EClickType.Extend);
        GetButton((int)EButtons.RelicsSubButton).gameObject
          .BindEvent((PointerEventData data) => Managers.UI.ShowPopupUI<RelicExtendPopupUI>().MyType = RelicExtendPopupUI.EClickType.Relic);
        
    }


    private void ReturnToMain()
    {
        Debug.Log("다시 게임 시작 누를 수 있는 메인 화면으로 돌아감 BGM 변경 시 여기!");
        Managers.Resource.Destroy(gameObject);

    }
    private void GameStart()
    {
        Debug.Log("게임화면 넘어가는 코드!");
    }
    private void DetaillCharacterScriptChange(ECharacterDetail ShowMenu)
    {
        if (!isCharacterSelected)
        {
            Debug.Log("캐릭터를 설정후 클릭해야 가능합니다.");
            return;
        }
        Get<GameObject>((int)EGameObjects.AboutLoad).gameObject.SetActive(false);
        Get<GameObject>((int)EGameObjects.AboutScript).gameObject.SetActive(false);
        Get<GameObject>((int)EGameObjects.AboutSkill).gameObject.SetActive(false);
        Get<GameObject>((int)EGameObjects.ShowScriptButtonIsClickEffect).gameObject.SetActive(false);
        Get<GameObject>((int)EGameObjects.SkillScriptIsClickEffect).gameObject.SetActive(false);
        Get<GameObject>((int)EGameObjects.LoadIsClickEffect).gameObject.SetActive(false);

        switch (ShowMenu)
        {
            case ECharacterDetail.AboutScript:
                Get<GameObject>((int)EGameObjects.AboutScript).gameObject.SetActive(true);
                Get<GameObject>((int)EGameObjects.ShowScriptButtonIsClickEffect).gameObject.SetActive(true);
                break;
            case ECharacterDetail.AboutSkill:
                Get<GameObject>((int)EGameObjects.AboutSkill).gameObject.SetActive(true);
                Get<GameObject>((int)EGameObjects.SkillScriptIsClickEffect).gameObject.SetActive(true);
                break;
            case ECharacterDetail.AboutLoad:
                Get<GameObject>((int)EGameObjects.AboutLoad).gameObject.SetActive(true);
                Get<GameObject>((int)EGameObjects.LoadIsClickEffect).gameObject.SetActive(true);
                break;
        }
    }
   


    //편하게 하려면 스킬 데이터 를 체계적으로 만들어야 하는데 귀찮아서ㅓㅓㅓㅓ...''
    private void ChangeSkillImage(int charcode)
    {
        //일부 캐릭터는 패시브가 있고 일부 캐릭터는 없네요 ---.. 미리 알았으면 편했는데;
        if (!charcode.Equals(7))
        {
            
            Get<GameObject>((int)EGameObjects.PassiveReactPannel).SetActive(false);
            Get<GameObject>((int)EGameObjects.LoadPassiveSkill).SetActive(false);
        }
        else
        {
            Get<GameObject>((int)EGameObjects.PassiveReactPannel).SetActive(true);
            Get<GameObject>((int)EGameObjects.LoadPassiveSkill).SetActive(true);
        }

        GetImage((int)EImages.PassiveSkillImage).sprite =Managers.Resource.LoadSprte(Managers.Data.CharacterDataDict[charcode].passiveskilliconpath);
       GetImage((int)EImages. M1SkilIImage          ).sprite=Managers.Resource.LoadSprte(Managers.Data.CharacterDataDict[charcode].m1skilliconpath);
       GetImage((int)EImages. M2SkilIImage          ).sprite=Managers.Resource.LoadSprte(Managers.Data.CharacterDataDict[charcode].m2skill_1iconpath);
       GetImage((int)EImages. RSkilIImage           ).sprite=Managers.Resource.LoadSprte(Managers.Data.CharacterDataDict[charcode].rskill_1iconpath);
       GetImage((int)EImages.ShiftSkillImage).sprite=Managers.Resource.LoadSprte(Managers.Data.CharacterDataDict[charcode].shiftskill_1iconpath);



        SetSkillImage(charcode);
    }
    private void SetSkillImage(int charcode)
    {
        GetImage((int)EImages.LoadPassiveSkillImage).sprite = Managers.Resource.LoadSprte(Managers.Data.CharacterDataDict[charcode].passiveskilliconpath);

        if (Managers.Data.CharacterDataDict[charcode].isshiftskill_1learn)
        {
            GetImage((int)EImages.LoadShiftSkillImage_1).sprite = Managers.Resource.LoadSprte(Managers.Data.CharacterDataDict[charcode].m1skilliconpath);
            
        }
        if (Managers.Data.CharacterDataDict[charcode].isshiftskill_2learn)
        {
            GetImage((int)EImages.LoadShiftSkillImage_2).sprite = Managers.Resource.LoadSprte(Managers.Data.CharacterDataDict[charcode].shiftskill_2iconpath);
        }
        if (Managers.Data.CharacterDataDict[charcode].ism2_1skilllearn)
        {
            GetImage((int)EImages.LoadM2SkillImage_1).sprite = Managers.Resource.LoadSprte(Managers.Data.CharacterDataDict[charcode].m2skill_1iconpath);
        }
        if (Managers.Data.CharacterDataDict[charcode].ism2_2skilllearn)
        {
            GetImage((int)EImages.LoadM2SkillImage_2).sprite = Managers.Resource.LoadSprte(Managers.Data.CharacterDataDict[charcode].m2skill_2iconpath);
        }
        if (Managers.Data.CharacterDataDict[charcode].isrskill_1learn)
        {
            GetImage((int)EImages.LoadRSkillImage_1).sprite = Managers.Resource.LoadSprte(Managers.Data.CharacterDataDict[charcode].rskill_1iconpath);
        }
        if (Managers.Data.CharacterDataDict[characterCode].isr_skill2learn)
        {
            GetImage((int)EImages.LoadRSkillImage_2).sprite = Managers.Resource.LoadSprte(Managers.Data.CharacterDataDict[charcode].r_skill2iconpath);
        }


    }


    private void SkillScriptButtonEvent()
    {

    }
    private void LoadButtonEvent()
    {
     
    }

    private void DesCribeChange(int charcode)
    {
      
        GetText((int)ETexts.CharacterNameText).text = $"{Managers.Data.CharacterDataDict[charcode].Name}";
        GetText((int)ETexts.MoreDetailText1).text = $"{Managers.Data.CharacterDataDict[charcode].script1}";
        GetText((int)ETexts.MoreDetailText2).text = $"{Managers.Data.CharacterDataDict[charcode].script2}";
        GetText((int)ETexts.MoreDetailText3).text = $"{Managers.Data.CharacterDataDict[charcode].script3}";
        GetText((int)ETexts.MoreDetailText4).text = $"{Managers.Data.CharacterDataDict[charcode].script4}";
        GetText((int)ETexts.PassiveSkillTitle).text = $"{Managers.Data.CharacterDataDict[charcode].passiveskill}";
        GetText((int)ETexts.PassiveSkillText).text = $"{Managers.Data.CharacterDataDict[charcode].passiveskillscript}";
        GetText((int)ETexts.M1SkillTitle).text = $"{Managers.Data.CharacterDataDict[charcode].m1skill}";
        GetText((int)ETexts.M1SkillText).text = $"{Managers.Data.CharacterDataDict[charcode].m1skillscript}";
        GetText((int)ETexts.M2SkillTitle).text = $"{Managers.Data.CharacterDataDict[charcode].m2skill_1}";
        GetText((int)ETexts.M2SkillText).text = $"{Managers.Data.CharacterDataDict[charcode].m2skill_1script}";
        GetText((int)ETexts.RSkillTitle).text = $"{Managers.Data.CharacterDataDict[charcode].rskill_1}";
        GetText((int)ETexts.RSkillText).text = $"{Managers.Data.CharacterDataDict[charcode].rskill_1script}";
        GetText((int)ETexts.ShiftSkillTitle).text= $"{Managers.Data.CharacterDataDict[charcode].shiftskill_1}";
        GetText((int)ETexts.ShiftSkillText).text= $"{Managers.Data.CharacterDataDict[charcode].shiftskill_1script}";


    }
    
    public void OnEvent(Define.EVENT_TYPE Event_Type, Component Sender, object Param = null)
    {
        switch (Event_Type)
        {
            case Define.EVENT_TYPE.SelectCharacter:
                isCharacterSelected = true;
                DetaillCharacterScriptChange(ECharacterDetail.AboutScript);
                if(Sender.TryGetComponent(out CharacterSelectButton CharBtn))
                {
                    characterCode = CharBtn.Charactercode;
                    DesCribeChange(characterCode);
                    ChangeSkillImage(characterCode);
                }
                break;
        }


 
    }
}
