using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameUI : UI_Game, IListener
{

    Color PrevSkillFillImageColor;
    string isnotActiveTeleport = "<b><color=#FF0000>텔레포터<u>(_)</u></color></b>를 찾아서 가동하십시오";
    string isActtiveTeleport = "보스를 처치하십시오!";
    string doneTeleporyEvent = "텔리포트로 들어가주십시오";
    public float RunTime = 0f;
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
        InteractionKeyText,
        InteractionContentsText

    }
    enum GameObjects
    {
        GameItemPannel,
        DifficultyBackground,

        DifficultyCompass,

        BagItemPannel,
        EquipPannel,
        BossPannel,
        BagPannel,
        EscPannel,
        InteractionPannel,

    }
    enum Buttons
    {
        ContinueButton,
        ReturnToMenuButton,
        QuitButton,
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
        Bind<Button>(typeof(Buttons));

        PrevSkillFillImageColor = GetImage((int)Images.SkillRFillImage).color;

        #region  이벤트 연동
        Managers.Event.DifficultyChange -= DifficultyImageChagngeEvent;
        Managers.Event.DifficultyChange += DifficultyImageChagngeEvent;
        Managers.Event.GoldChange -= GoldChangeEvent;
        Managers.Event.GoldChange += GoldChangeEvent;
        Managers.Event.EquipItemChange -= EquipChangeEvent;
        Managers.Event.EquipItemChange += EquipChangeEvent;


        Managers.Event.AddListener(Define.EVENT_TYPE.PlayerHpChange, this);
        Managers.Event.AddListener(Define.EVENT_TYPE.BossHpChange, this);
        Managers.Event.AddListener(Define.EVENT_TYPE.PlayerExpChange, this);
        Managers.Event.AddListener(Define.EVENT_TYPE.PlayerUseSkill, this);
        Managers.Event.AddListener(Define.EVENT_TYPE.PlayerInteractionIn, this);
        Managers.Event.AddListener(Define.EVENT_TYPE.PlayerInteractionOut, this);

        #endregion
        InitTexts();
        InitImage();
        InitSlider();

        InitButton();
        InitGameObjects();

    }
    void Start()
    {
        Init();

    }

    public void InitGameObjects()
    {
        Get<GameObject>((int)GameObjects.DifficultyBackground).GetComponent<Rigidbody2D>()
       .velocity = 5 * Vector2.left;

        Get<GameObject>((int)GameObjects.BagItemPannel).SetActive(true);
        Get<GameObject>((int)GameObjects.BossPannel).SetActive(false);
        Get<GameObject>((int)GameObjects.EscPannel).SetActive(false);
        Get<GameObject>((int)GameObjects.InteractionPannel).SetActive(false);
        Get<GameObject>((int)GameObjects.BagPannel).SetActive(false);
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
        RunTime = Time.time - Managers.Game.PlayingTIme;
        int minutes = (int)(RunTime / 60); // 분
        int seconds = (int)(RunTime % 60); // 초

        GetText((int)Texts.TimeText).text = $"{minutes:00}:{seconds:00}";


    }
    private void Update()
    {


        //E 버튼 누르면 활성/비활성
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Get<GameObject>((int)GameObjects.BagPannel).SetActive(true);
        }
        if (Input.GetKeyUp(KeyCode.Tab))
        {
            Get<GameObject>((int)GameObjects.BagPannel).SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("마우스 인풋 뺏기 카메라 못움직이게");
            Get<GameObject>((int)GameObjects.EscPannel).SetActive(true);
            Time.timeScale = 0f;
        }
    }

    // 상호작용을 누구와 할것인가에 따라 이벤트 구현방식 다르게 할예정?..
    //아무래도 플레이어와 직접 하는게 좋을듯
    private void SetCoolTime()
    {
    }
    private void InitButton()
    {
        GetButton((int)Buttons.ReturnToMenuButton).gameObject
            .BindEvent((PointerEventData data) => Debug.Log("씬 전환"));
        GetButton((int)Buttons.ContinueButton).gameObject
           .BindEvent((PointerEventData data) => ResumeGame());
        GetButton((int)Buttons.QuitButton).gameObject
          .BindEvent((PointerEventData data) => Debug.Log("게임종료"));
    }
    private void ResumeGame()
    {
        Debug.Log("마우스 인풋 다시 주기 카메라 움직이게");
        Get<GameObject>((int)GameObjects.EscPannel).SetActive(false);
        Time.timeScale = 1f;
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
    private void DifficultyImageChagngeEvent(int _)
    {
        GetImage((int)Images.StageImage).sprite = Managers.Resource.LoadSprte($"Difficultyicon{(int)Managers.Game.Difficulty + 1}");
        Debug.Log("만약 난이도 에 따라서 시작 Sprite를 다르게 하고 싶으면 여기에 추가적인설정");
        // (int)Manager.Game.Difficulty++1 + [ 난이도 보정값] 하면 될듯
    }
    private void EventOfSkill()
    {
        //Get<Slider>((int)Sliders.SkillM1).value = (float)현재 남은시간/ 스킬쿨타임;
        //Get<Slider>((int)Sliders.SkillM2).value = (float)현재 남은시간/ 스킬쿨타임;
        //Get<Slider>((int)Sliders.SkillShift).value = (float)현재 남은시간/ 스킬쿨타임;

        //Get<Slider>((int)Sliders.SkillR).value = (float)현재 남은시간/ 스킬쿨타임;
        //if (Get<Slider>((int)Sliders.SkillQ).value.CompareTo(0.95) > 0)
        //{
        //    Color color = Color.white;
        //    color.a = 0;
        //    GetImage((int)Images.SkillQFillImage).color = color;
        //}
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
    private void EquipChangeEvent(int itemcode)
    {
        //추후 스킬 클래스 혹은 스킬에서 남은시간 -> 스킬 쿨타임으로 초기화
        // 전체시간 - 재고있는 시간 -> 남은 시간 표현할 수 있을듯
        // //Get<Slider>((int)Sliders.SkillQ).value = (float)현재 남은시간/ 스킬쿨타임;

        //스킬 쿨타임은 FixedUpadte에서 처리할 예정

        Get<Slider>((int)Sliders.SkillQ).GetComponent<Image>()
             .sprite = Managers.Resource.LoadSprte($"{Managers.Data.ItemDataDict[itemcode].iconkey}");
        Get<GameObject>((int)GameObjects.EquipPannel).GetComponent<Image>()
            .sprite = Managers.Resource.LoadSprte($"{Managers.Data.ItemDataDict[itemcode].iconkey}");

    }
    private void InteractionInTextChangeEvent(Component _Sender)
    {
        Get<GameObject>((int)GameObjects.InteractionPannel).SetActive(true);
        if (_Sender.TryGetComponent(out ItemContainer _itemCOntainer))
        {
            GetText((int)Texts.InteractionKeyText).text = "E";
            if (Managers.Game.Gold > _itemCOntainer.Itemprice)
            {
                GetText((int)Texts.InteractionContentsText).text = $"상자 열기($<color=#FFD700>{_itemCOntainer.Itemprice}</color>)";
            }
            else
            {
                GetText((int)Texts.InteractionContentsText).text = $"상자 열기($<color=#FF0000>{_itemCOntainer.Itemprice}</color>)";
            }
            //이런식으로 처리 하지만 결국 그 가격에 따라 아이템을 사는 (상자를 여는 행위) 자체는 상자 ItemContainer에서 진행
            // 다른 상호작용 키들도 마찬가지 여기는 UI만 관리하고 동작들은 해당 class 내에서 처리합시다.!!
        }
    }
    private void InteractionOutEvent()
    {
        Get<GameObject>((int)GameObjects.InteractionPannel).SetActive(false);
    }
    public void OnEvent(Define.EVENT_TYPE Event_Type, Component Sender, object Param = null)
    {
        switch (Event_Type)
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
            case Define.EVENT_TYPE.EnemyHpChange:
                //적체력 변화 SLider는 여기 보다는... 그냥 적 스크립트에서 처리하도록 합시다.
                break;
            case Define.EVENT_TYPE.PlayerInteractionIn:
                InteractionInTextChangeEvent(Sender);
                //위와 같은 방식으로 Sender의 컴포넌트 ( 아이템, 비상정 탈출, 텔레포트 ) 등에 따라 Text 다르게 출력 
                break;
            case Define.EVENT_TYPE.PlayerInteractionOut:
                InteractionOutEvent();
                //위와 같은 방식으로 Sender의 컴포넌트 ( 아이템, 비상정 탈출, 텔레포트 ) 등에 따라 Text 다르게 출력 
                break;

        }
    }

}
