using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class MouseInteraction : UI_Scene,IListener
{
    Image MouseCursor;
    public int SpecialCode;

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
        SetMouseCursor();

        Get<GameObject>((int)EGameObjects.LeftPannel).SetActive(false);
        Get<GameObject>((int)EGameObjects.RightPannel).SetActive(false);
        Managers.Event.AddListener(Define.EVENT_TYPE.MousePointerEnter, this);
        Managers.Event.AddListener(Define.EVENT_TYPE.MousePointerExit, this);

        //이벤트 연동...

    }
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }
    private void SetMouseCursor()
    {
        MouseCursor = GetImage((int)EImages.MouseCursorImage);
        MouseCursor.GetComponent<Image>().raycastTarget = false;
        MouseCursor.transform.localScale = 5* Vector3.one;
        MouseCursor.GetComponent<RectTransform>().pivot = new Vector2(1.8f, 1f);
    }
    // Update is called once per frame
    void Update()
    {
        MouseCursor.transform.position = Input.mousePosition;
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
                            GetText((int)ETexts.LeftTitleText).text = "이슬비";
                            GetText((int)ETexts.LeftContentsTitleText).text = "초보 플레이어를 위한 난이도입니다. 눈물 나고 이가 갈리는 고통이 간지러운 수준으로 약해집니다.";
                            break;
                        case Define.EDifficulty.Normal:
                            GetText((int)ETexts.LeftTitleText).text = "폭풍우";
                            GetText((int)ETexts.LeftContentsTitleText).text = "이 게임을 제작의 의도대로 플레이합니다! 강력한 적들을 상대로 실력을 시험하십시오.";
                            break;
                        case Define.EDifficulty.Hard:
                            GetText((int)ETexts.LeftTitleText).text = "몬순";
                            GetText((int)ETexts.LeftContentsTitleText).text = "하드코어 플레이어를 위한 난이도입니다. 가는 곳마다 고통과 공포가 덮쳐 올 것입니다. 죽음을 각오하십시오.";
                            break;
                    }
                }
                else if(Sender.TryGetComponent(out CharacterSelectButton characterSelect))
                {
                    ActivePannel(EInteractionType.Character);
                    GetText((int)ETexts.RightTitleText).text = Managers.Data.CharacterDataDict[characterSelect.Charactercode].Name;
                    GetText((int)ETexts.RightContentsTitleText).text = Managers.Data.CharacterDataDict[characterSelect.Charactercode].unlockscript2;
                }
                else if(Sender.TryGetComponent(out LoadSkillTempo Tempo))
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
