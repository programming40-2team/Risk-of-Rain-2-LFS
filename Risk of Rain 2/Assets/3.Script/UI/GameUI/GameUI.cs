using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : UI_Scene,IListener
{

    Color PrevSkillFillImageColor;
    string isnotActiveTeleport = "<b><color=#FF0000>텔레포터<u>(_)</u></color></b>를 찾아서 가동하십시오";
    string isActtiveTeleport = "보스를 처치하십시오!";
    string doneTeleporyEvent = "텔리포트로 들어가주십시오";

    #region UI기본 요소들 Bind
    enum Sliders
    {
        SkillM1,
        SkillM2,
        SkillShift,
        SkillR,
        SkillQ,

        ExpSlider,
        PlayerHpSlider,
        BossHpSlider,

    }
    enum Images
    {
        SkillM1FillImage,
        SkillM2FillImage,
        SkillShiftFillImage,
        SkillRFillImage,
        SkillQFillImage,

        StageImage,

        TeleCheckFalse,
        TeleCheckTrue,

    }
    enum Texts
    {
        SkillM1CoolTime,
        SkillM2CoolTime,
        SkillShiftCoolTime,
        SkillRCoolTime,
        SkillQCoolTime,


        GoldText,
        TimeText,
        StageNumber,
        StageLevel,

        ObjectContents,
        PlayerLevelText,

        PlayerHpText,
        BossHpText,

    }
    enum GameObjects
    {
        GameItemPannel,
        DifficultyBackground,

        DifficultyCompass,

        BossPannel,
    }
    #endregion
    public override void Init()
    {
        base.Init();
        Managers.Game.PlayingTIme = Time.time;
        Bind<GameObject>(typeof(GameObjects));
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<Slider>(typeof(Sliders));
        Bind<Image>(typeof(Images));

        PrevSkillFillImageColor = GetImage((int)Images.SkillRFillImage).color;
        Get<GameObject>((int)GameObjects.DifficultyBackground).GetComponent<Rigidbody2D>()
            .AddForce(new Vector2(-50f, 0));
        Get<GameObject>((int)GameObjects.BossPannel).SetActive(false);

        #region  이벤트 연동
        Managers.Event.DifficultyChange -= DifficultyImageChagngeEvent;
        Managers.Event.DifficultyChange += DifficultyImageChagngeEvent;
        Managers.Event.GoldChange -= GoldChangeEvent;
        Managers.Event.GoldChange += GoldChangeEvent;



        Managers.Event.AddListener(Define.EVENT_TYPE.PlayerHpChange, this);
        Managers.Event.AddListener(Define.EVENT_TYPE.BossHpChange, this);
        Managers.Event.AddListener(Define.EVENT_TYPE.PlayerExpChange, this);
        Managers.Event.AddListener(Define.EVENT_TYPE.PlayerUseSkill, this);

        #endregion
        InitTexts();
        InitImage();
        InitSlider();

    }
    void Start()
    {
        Init();
        
    }
    private void InitTexts()
    {
        GetText((int)Texts.SkillM1CoolTime).text = "";
        GetText((int)Texts.SkillM2CoolTime).text = "";
        GetText((int)Texts.SkillShiftCoolTime).text = "";
        GetText((int)Texts.SkillRCoolTime).text = "";
        GetText((int)Texts.SkillQCoolTime).text = "";

        GetText((int)Texts.GoldText).text = $"{0}";
        GetText((int)Texts.StageNumber).text = $"스테이지 {1}";
        GetText((int)Texts.StageLevel).text = $"레벨. {1}";
        GetText((int)Texts.ObjectContents).text = $"{isnotActiveTeleport}";
        GetText((int)Texts.PlayerLevelText).text = $"{1}";


    }
    private void FixedUpdate()
    {
        float time = Time.time - Managers.Game.PlayingTIme;
        int minutes = (int)(time / 60); // 분
        int seconds = (int)(time % 60); // 초

        GetText((int)Texts.TimeText).text = $"{minutes:00}:{seconds:00}";
    }


    // 상호작용을 누구와 할것인가에 따라 이벤트 구현방식 다르게 할예정?..
    //아무래도 플레이어와 직접 하는게 좋을듯
    private void SetCoolTime()
    {
    }
    private void InitSlider()
    {

        Get<Slider>((int)Sliders.SkillM1).value = 1;
        Get<Slider>((int)Sliders.SkillM2).value = 1;
        Get<Slider>((int)Sliders.SkillShift).value = 1;
        Get<Slider>((int)Sliders.SkillQ).value = 1;
        Get<Slider>((int)Sliders.SkillR).value = 1;
        Get<Slider>((int)Sliders.PlayerHpSlider).value = 1;
        Get<Slider>((int)Sliders.ExpSlider).value = 1;
        Get<Slider>((int)Sliders.BossHpSlider).value = 1;
    }
    private void InitImage()
    {
        GetImage((int)Images.SkillM1FillImage).color = PrevSkillFillImageColor;
        GetImage((int)Images.SkillM2FillImage).color = PrevSkillFillImageColor;
        GetImage((int)Images.SkillShiftFillImage).color = PrevSkillFillImageColor;
        GetImage((int)Images.SkillRFillImage).color = PrevSkillFillImageColor;
        GetImage((int)Images.SkillQFillImage).color = PrevSkillFillImageColor;

        GetImage((int)Images.TeleCheckTrue).enabled = false;
    }
    private void DifficultyImageChagngeEvent()
    {
        GetImage((int)Images.StageImage).sprite = Managers.Resource.LoadSprte($"Difficultyicon{(int)Managers.Game.Difficulty+1}");
    }
    private void EventOfSkill()
    {
        //Get<Slider>((int)Sliders.SkillM1).value = (float)현재 남은시간/ 스킬쿨타임;
        //Get<Slider>((int)Sliders.SkillM2).value = (float)현재 남은시간/ 스킬쿨타임;
        //Get<Slider>((int)Sliders.SkillShift).value = (float)현재 남은시간/ 스킬쿨타임;
        //Get<Slider>((int)Sliders.SkillQ).value = (float)현재 남은시간/ 스킬쿨타임;
        //Get<Slider>((int)Sliders.SkillR).value = (float)현재 남은시간/ 스킬쿨타임;

    }
    //여기는 한번 더 머지  받으면 (플레이어 경험치, Hp 에 따라서 이벤트를 연동시켜줄 예정) 
    private void EventOfPlayerHp()
    {
       // Get<Slider>((int)Sliders.PlayerHpSlider).value = 1;
       // Get<Slider>((int)Sliders.ExpSlider).value = 1;
    }
    private void EventOfPlayerExp()
    {

    }
    private void EventOfBossHp()
    {
        if (!Get<GameObject>((int)GameObjects.BossPannel).activeSelf)
        {
            Get<GameObject>((int)GameObjects.BossPannel).SetActive(true);
        }
      
    }
    private void GoldChangeEvent(int gold)
    {
        GetText((int)Texts.GoldText).text = $"{gold}";
    }

    public void OnEvent(Define.EVENT_TYPE Event_Type, Component Sender, object Param = null)
    {
       switch(Event_Type)
        {
            case Define.EVENT_TYPE.PlayerHpChange:
                EventOfPlayerHp();
                break;
            case Define.EVENT_TYPE.BossHpChange:
                EventOfBossHp();
                break;
            case Define.EVENT_TYPE.PlayerExpChange:
                EventOfPlayerExp();
                break;
            case Define.EVENT_TYPE.PlayerUseSkill:
                EventOfSkill();
                break;

        }
    }
}
