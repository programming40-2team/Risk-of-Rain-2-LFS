using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class GameStartUI : UI_Scene,IListener
{
    private bool isCharacterSelected;
    private int characterCode;
    enum GameObjects
    {
        DifficultyEasySelectEffect,
        DifficultyNormalSelectEffect,
        DifficultyHardSelectEffect,

        SpecialFeatureIsClickEffect,
        SkillScriptIsClickEffect,
        LoadIsClickEffect,

        AboutScript,
        AboutSkill,
        AboutLoad,
    }
    enum Texts
    {
        DifficultyText,
        ExtendText,
        RelicsText,
        RightPannelBackButtonText,

        SpecialFeatureText,
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
        ShiftSkilllText,




        GameReadyButtonText,
    }
    enum Buttons
    {
        RightPannelBackButton,

        SpecialFeatureButton,
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



    }
    enum Images
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
    enum Difficulty
    {
        Easy,
        Normal,
        Hard,
    }

    void Start()
    {
        Init();
      
    }
    public override void Init()
    {
        base.Init();
        GetComponent<Canvas>().sortingOrder = 5;
        Bind<GameObject>(typeof(GameObjects));
        Bind<Button>(typeof(Buttons));
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<Image>(typeof(Images));
        Managers.Event.AddListener(Define.EVENT_TYPE.SelectCharacter, this);
        //글자 관련 초기화
        InitText();
        //게임 오브젝트 관련 초기화, 효과 off
        InitGameObect();
        //버튼 오브젝트 관련 초기화
        InitButton();









        //디폴트 이미지 효과 Easy
        DifficultyEffectChange(Difficulty.Easy);

    }

    private void InitText()
    {
        GetText((int)Texts.DifficultyText).text = $"난이도";
        GetText((int)Texts.ExtendText).text = $"확장팩";
        GetText((int)Texts.RelicsText).text = $"유물";

        GetText((int)Texts.SpecialFeatureText).text = $"개요";
        GetText((int)Texts.SkillScriptText).text = "스킬";
        GetText((int)Texts.LoadText).text = "장전";

        GetText((int)Texts.CharacterNameText).text = "";
        GetText((int)Texts.MoreDetailText1).text = "";
        GetText((int)Texts.MoreDetailText2).text = "";
        GetText((int)Texts.MoreDetailText3).text = "";
        GetText((int)Texts.MoreDetailText4).text = "";
        GetText((int)Texts.PassiveSkillTitle).text="";
        GetText((int)Texts.PassiveSkillText).text="";
        GetText((int)Texts.M1SkillTitle).text="";
        GetText((int)Texts.M1SkillText).text="";
        GetText((int)Texts.M2SkillTitle).text="";
        GetText((int)Texts.M2SkillText).text="";
        GetText((int)Texts.RSkillTitle).text="";
        GetText((int)Texts.RSkillText).text = "";
        GetText((int)Texts.ShiftSkilllText).text = "";
        GetText((int)Texts.ShiftSkillTitle).text = "";





        GetText((int)Texts.GameReadyButtonText).text = $"게임시작";
    }

    private void InitGameObect()
    {
        Get<GameObject>((int)GameObjects. DifficultyEasySelectEffect)       .gameObject.SetActive(false);
        Get<GameObject>((int)GameObjects. DifficultyNormalSelectEffect)     .gameObject.SetActive(false);
        Get<GameObject>((int)GameObjects. DifficultyHardSelectEffect)       .gameObject.SetActive(false);
        Get<GameObject>((int)GameObjects. SpecialFeatureIsClickEffect)      .gameObject.SetActive(false);
        Get<GameObject>((int)GameObjects. SkillScriptIsClickEffect)         .gameObject.SetActive(false);
        Get<GameObject>((int)GameObjects. LoadIsClickEffect)                .gameObject.SetActive(false);
        Get<GameObject>((int)GameObjects. AboutScript)                      .gameObject.SetActive(false);
        Get<GameObject>((int)GameObjects. AboutSkill)                       .gameObject.SetActive(false);
        Get<GameObject>((int)GameObjects. AboutLoad)                        .gameObject.SetActive(false);


    }

    private void InitButton()
    {
        GetButton((int)Buttons.RightPannelBackButton).gameObject
            .BindEvent((PointerEventData data) => Managers.Resource.Destroy(gameObject));
        GetButton((int)Buttons.GameReadyButton).gameObject
            .BindEvent((PointerEventData data) => Debug.Log("게임화면 넘어가는 코드!"));

        GetButton((int)Buttons.DifficultyEasy).gameObject
           .BindEvent((PointerEventData data) => DifficultyEffectChange(Difficulty.Easy));
        GetButton((int)Buttons.DifficultyNormal).gameObject
        .BindEvent((PointerEventData data) => DifficultyEffectChange(Difficulty.Normal));
        GetButton((int)Buttons.DifficultyHard).gameObject
        .BindEvent((PointerEventData data) => DifficultyEffectChange(Difficulty.Hard));


        //   GetButton((int)Buttons.SpecialFeatureButton).gameObject
        //       .BindEvent((PointerEventData data) => )
    }


    private void DifficultyEffectChange(Difficulty difficulty)
    {
        Get<GameObject>((int)GameObjects.DifficultyEasySelectEffect).gameObject.SetActive(false);
        Get<GameObject>((int)GameObjects.DifficultyNormalSelectEffect).gameObject.SetActive(false);
        Get<GameObject>((int)GameObjects.DifficultyHardSelectEffect).gameObject.SetActive(false);

        switch (difficulty)
        {
            case Difficulty.Easy:
                Get<GameObject>((int)GameObjects.DifficultyEasySelectEffect).gameObject.SetActive(true);
                break;
            case Difficulty.Normal:
                Get<GameObject>((int)GameObjects.DifficultyNormalSelectEffect).gameObject.SetActive(true);
                break;
            case Difficulty.Hard:
                Get<GameObject>((int)GameObjects.DifficultyHardSelectEffect).gameObject.SetActive(true);
                break;
        }
        Debug.Log("게임 시작 난이도 관련 설정은 여기서!");


    }
    private void ChangeImage(int charcode)
    {
        GetImage((int)Images.PassiveSkillImage).sprite = Managers.Resource.LoadSprte(Managers.Data.CharacterDataDict[charcode].passiveskilliconpath);
       GetImage((int)Images. M1SkilIImage          ).sprite=Managers.Resource.LoadSprte(Managers.Data.CharacterDataDict[charcode].m1skilliconpath);
       GetImage((int)Images. M2SkilIImage          ).sprite=Managers.Resource.LoadSprte(Managers.Data.CharacterDataDict[charcode].m2skill_1iconpath);
       GetImage((int)Images. RSkilIImage           ).sprite=Managers.Resource.LoadSprte(Managers.Data.CharacterDataDict[charcode].rskill_1iconpath);
       GetImage((int)Images.ShiftSkillImage).sprite=Managers.Resource.LoadSprte(Managers.Data.CharacterDataDict[charcode].shiftskill_1iconpath);
       GetImage((int)Images. LoadPassiveSkillImage ).sprite=Managers.Resource.LoadSprte(Managers.Data.CharacterDataDict[charcode].passiveskilliconpath);
       GetImage((int)Images. LoadShiftSkillImage_1 ).sprite=Managers.Resource.LoadSprte(Managers.Data.CharacterDataDict[charcode].shiftskill_1iconpath);
       GetImage((int)Images. LoadShiftSkillImage_2 ).sprite=Managers.Resource.LoadSprte(Managers.Data.CharacterDataDict[charcode].shiftskill_2iconpath);
       GetImage((int)Images. LoadM2SkillImage_1    ).sprite=Managers.Resource.LoadSprte(Managers.Data.CharacterDataDict[charcode].m2skill_1iconpath);
       GetImage((int)Images. LoadM2SkillImage_2    ).sprite=Managers.Resource.LoadSprte(Managers.Data.CharacterDataDict[charcode].m2skill_2iconpath);
       GetImage((int)Images. LoadRSkillImage_1     ).sprite=Managers.Resource.LoadSprte(Managers.Data.CharacterDataDict[charcode].rskill_1iconpath);
       GetImage((int)Images. LoadRSkillImage_2     ).sprite= Managers.Resource.LoadSprte(Managers.Data.CharacterDataDict[charcode].r_skill2iconpath);
    }

    private void SkillScriptButtonEvent()
    {

    }
    private void LoadButtonEvent()
    {
     
    }

    private void DesCribeChange(int charcode)
    {
        Debug.Log("나중에 마우스 캔버스 한테도 이벤트 발송해 줘야합니다.");
        GetText((int)Texts.CharacterNameText).text = $"{Managers.Data.CharacterDataDict[charcode].Name}";
        GetText((int)Texts.MoreDetailText1).text = $"{Managers.Data.CharacterDataDict[charcode].script1}";
        GetText((int)Texts.MoreDetailText2).text = $"{Managers.Data.CharacterDataDict[charcode].script2}";
        GetText((int)Texts.MoreDetailText3).text = $"{Managers.Data.CharacterDataDict[charcode].script3}";
        GetText((int)Texts.MoreDetailText4).text = $"{Managers.Data.CharacterDataDict[charcode].script4}";
        GetText((int)Texts.PassiveSkillTitle).text = $"{Managers.Data.CharacterDataDict[charcode].passiveskill}";
        GetText((int)Texts.PassiveSkillText).text = $"{Managers.Data.CharacterDataDict[charcode].passiveskillscript}";
        GetText((int)Texts.M1SkillTitle).text = $"{Managers.Data.CharacterDataDict[charcode].m1skill}";
        GetText((int)Texts.M1SkillText).text = $"{Managers.Data.CharacterDataDict[charcode].m1skillscript}";
        GetText((int)Texts.M2SkillTitle).text = $"{Managers.Data.CharacterDataDict[charcode].m2skill_1}";
        GetText((int)Texts.M2SkillText).text = $"{Managers.Data.CharacterDataDict[charcode].m2skill_1script}";
        GetText((int)Texts.RSkillTitle).text = $"{Managers.Data.CharacterDataDict[charcode].rskill_1}";
        GetText((int)Texts.RSkillText).text = $"{Managers.Data.CharacterDataDict[charcode].rskill_1script}";
        GetText((int)Texts.ShiftSkillTitle).text= $"{Managers.Data.CharacterDataDict[charcode].shiftskill_1}";
        GetText((int)Texts.ShiftSkilllText).text= $"{Managers.Data.CharacterDataDict[charcode].shiftskill_1script}";


    }
    
    public void OnEvent(Define.EVENT_TYPE Event_Type, Component Sender, object Param = null)
    {   
        isCharacterSelected=true;
        characterCode=Sender.GetComponent<CharacterSelectButton>().Charactercode;

        DesCribeChange(characterCode);
        ChangeImage(characterCode);

        DesCribeChange(characterCode);
    }
}
