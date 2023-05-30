using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MouseInteraction : UI_Scene, IListener
{
    public int SpecialCode;
    [SerializeField] private Texture2D mouseCursorImage;
    RectTransform MouseFakeImage;
    enum EInteractionType
    {
        Skill,
        Character,
        Difficulty,
    }
    enum EImages
    {
        MouseCursorImage,

    }
    enum EGameObjects
    {
        RightPannel,
        LeftPannel,
        RightBackGroundPannel,
        LeftBackGroundPannel,

    }
    enum ETexts
    {
        RightContentsTitleText,
        RightTitleText,
        LeftTitleText,
        LeftContentsTitleText,
    }
    public override void Init()
    {
        base.Init();
        //마우스 기존 커서 활성/비활성
        Cursor.visible = true;
        GetComponent<Canvas>().sortingOrder = (int)Define.SortingOrder.MouseInteraction;
        Bind<Image>(typeof(EImages));
        Bind<TextMeshProUGUI>(typeof(ETexts));
        Bind<GameObject>(typeof(EGameObjects));
        MouseFakeImage = GetImage((int)EImages.MouseCursorImage).GetComponent<RectTransform>();
        Get<GameObject>((int)EGameObjects.LeftPannel).SetActive(false);
        Get<GameObject>((int)EGameObjects.RightPannel).SetActive(false);

        //이벤트 연동...
        Managers.Event.AddListener(Define.EVENT_TYPE.MousePointerEnter, this);
        Managers.Event.AddListener(Define.EVENT_TYPE.MousePointerExit, this);


        Cursor.SetCursor(mouseCursorImage, Vector2.zero, CursorMode.Auto);
        GetImage((int)EImages.MouseCursorImage).enabled = false;

    }
    void Start()
    {
        Init();
    }

    void Update()
    {
        MouseFakeImage.anchoredPosition = Input.mousePosition;
    }
    private void ReInit()
    {
        Get<GameObject>((int)EGameObjects.LeftPannel).SetActive(false);
        Get<GameObject>((int)EGameObjects.RightPannel).SetActive(false);
    }
    private void ActivePannel(EInteractionType myInteractionType)
    {
        //들어오는 타입
        //1. 캐릭터 활성 조건( 오른쪽 생성 ) 
        //2. 스킬( 오른쪽 생성 )
        //3. 난이도( 왼쪽 생성 )
        switch (myInteractionType)
        {
            case EInteractionType.Skill:
                Get<GameObject>((int)EGameObjects.RightPannel).SetActive(true);
                break;
            case EInteractionType.Character:
                Get<GameObject>((int)EGameObjects.RightPannel).SetActive(true);
                break;
            case EInteractionType.Difficulty:
                Get<GameObject>((int)EGameObjects.LeftPannel).SetActive(true);
                break;
        }

    }

    public void OnEvent(Define.EVENT_TYPE Event_Type, Component Sender, object Param = null)
    {
        switch (Event_Type)
        {
            case Define.EVENT_TYPE.MousePointerEnter:
                if (Sender.TryGetComponent(out Diffidculty diff))
                {
                    ActivePannel(EInteractionType.Difficulty);
                    switch (diff.myDifficulty)
                    {
                        case Define.EDifficulty.Easy:
                            GetText((int)ETexts.LeftTitleText).text = "<b><color=#006400>이슬비</color></b>";
                            GetText((int)ETexts.LeftContentsTitleText).text = "초보 플레이어를 위한 난이도입니다. 눈물 나고 이가 갈리는 고통이 간지러운 수준으로 약해집니다.";
                            Get<GameObject>((int)EGameObjects.LeftBackGroundPannel).GetComponent<Image>().color = Color.green;
                            break;
                        case Define.EDifficulty.Normal:
                            GetText((int)ETexts.LeftTitleText).text = "<b><color=#FF4500>폭풍우</color></b>";
                            GetText((int)ETexts.LeftContentsTitleText).text = "이 게임을 제작의 의도대로 플레이합니다! 강력한 적들을 상대로 실력을 시험하십시오.";
                            Get<GameObject>((int)EGameObjects.LeftBackGroundPannel).GetComponent<Image>().color = Color.yellow;
                            break;
                        case Define.EDifficulty.Hard:
                            GetText((int)ETexts.LeftTitleText).text = "<b><color=#FF1493>몬순</color></b>";
                            GetText((int)ETexts.LeftContentsTitleText).text = "하드코어 플레이어를 위한 난이도입니다. 가는 곳마다 고통과 공포가 덮쳐 올 것입니다. 죽음을 각오하십시오.";
                            Get<GameObject>((int)EGameObjects.LeftBackGroundPannel).GetComponent<Image>().color = Color.magenta;
                            break;
                    }
                }
                else if (Sender.TryGetComponent(out CharacterSelectButton characterSelect))
                {
                    ActivePannel(EInteractionType.Character);
                    GetText((int)ETexts.RightTitleText).text = Managers.Data.CharacterDataDict[characterSelect.Charactercode].Name;
                    //캐릭터 보유 미보유 여부에 따라 달르게 출력
                    if (Managers.Data.CharacterDataDict[characterSelect.Charactercode].isActive)
                    {
                        GetText((int)ETexts.RightContentsTitleText).text = Managers.Data.CharacterDataDict[characterSelect.Charactercode].script1;
                    }
                    else
                    {
                        GetText((int)ETexts.RightContentsTitleText).text = Managers.Data.CharacterDataDict[characterSelect.Charactercode].unlockscript2;
                    }

                }
                else if (Sender.TryGetComponent(out LoadSkillTempo Tempo))
                {
                    ActivePannel(EInteractionType.Skill);
                    GetText((int)ETexts.RightTitleText).text = Tempo.skillTitle;
                    GetText((int)ETexts.RightContentsTitleText).text = Tempo.skillContents;
                }
                break;
            case Define.EVENT_TYPE.MousePointerExit:
                ReInit();
                break;
        }


    }
}
