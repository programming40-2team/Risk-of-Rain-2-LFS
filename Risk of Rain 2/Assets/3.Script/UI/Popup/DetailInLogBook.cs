using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DetailInLogBook : UI_Popup
{

    private int _specialCode;
    public int SpecialCode
    {
        get
        {
            return _specialCode;
        }
        set
        {
            _specialCode = value;
            Setting();
        }
    }
    private void Awake()
    {
        Init();
    }
    public override void Init()
    {
        base.Init();
        Bind<GameObject>(typeof(EGameObjects));
        Bind<Button>(typeof(EButtons));
        Bind<TextMeshProUGUI>(typeof(ETexts));
        GetButton((int)EButtons.BackButton).gameObject
            .BindEvent((PointerEventData data) => gameObject.SetActive(false));

        GetComponent<Canvas>().sortingOrder = (int)Define.SortingOrder.DetailInLogBook;
        Setting();
    }
    enum EGameObjects
    {
        ObjectSpawnPosition,

    }
    enum ETexts
    {
        TItileTitleText,
        TitlieContentsText,
        ScriptTitleText,
        ScriptContentsText,
        FindCountText,
        FindMaxCountTitleText,
        InformationTitleText,
        InformationContentsText,


    }
    enum EButtons
    {
        BackButton
    }
    private void Setting()
    {

        if (SpecialCode.Equals(-1))
        {
            return;
        }
        if (LogBook.ClickType.Equals(Define.ECurrentClickType.None))
        {
            return;
        }

        foreach (Transform transforom in Get<GameObject>((int)EGameObjects.ObjectSpawnPosition).GetComponentInChildren<Transform>())
        {
            Managers.Resource.Destroy(transforom.gameObject);
        }
        //최적화 방지를 위해 이 팝업 창은 미리 생성해두고, Enable할때 마다 이벤트로 텍스트  설정..
        //다른 UI들도 시작과 동시에 미리 만들어 놓으면 최적화 가능한데.. FPS 나쁘지 않아서 일단 냅두었습니다.
        SetText();
        SetModel();
    }

    private void SetText()
    {

        switch (LogBook.ClickType)
        {
            case Define.ECurrentClickType.ItemAndEquip:
                GetText((int)ETexts.TItileTitleText).text = $"{Managers.Data.ItemDataDict[SpecialCode].itemname}";
                GetText((int)ETexts.TitlieContentsText).text = "아이템과 장비";
                GetText((int)ETexts.ScriptTitleText).text = $"설명 : ";
                GetText((int)ETexts.ScriptContentsText).text = $" {Managers.Data.ItemDataDict[SpecialCode].explanation}";
                GetText((int)ETexts.FindCountText).text = $"발견함 : {0}";
                GetText((int)ETexts.FindMaxCountTitleText).text = $"최고중첩 : {0}";
                GetText((int)ETexts.InformationTitleText).text = "정보";
                GetText((int)ETexts.InformationContentsText).text = $"{Managers.Data.ItemDataDict[SpecialCode].explanation}\n" +
                    $"발견 가능 지역 : {Managers.Data.EnvDataDict[Random.Range(100, 107)].enviromentname}\n";
        
                break;
            case Define.ECurrentClickType.Monster:
                GetText((int)ETexts.TItileTitleText).text = $"{Managers.Data.MonData[SpecialCode].name}";
                GetText((int)ETexts.TitlieContentsText).text = "몬스터";
                GetText((int)ETexts.ScriptTitleText).text = $"설명 : ";
                GetText((int)ETexts.ScriptContentsText).text = $" 체력 : {Managers.Data.MonData[SpecialCode].maxhealth} \n공격 : {Managers.Data.MonData[SpecialCode].attack}\n 속도 : {Managers.Data.MonData[SpecialCode].speed}\n 방어 : {Managers.Data.MonData[SpecialCode].armor}" +
                    $"";
                GetText((int)ETexts.FindCountText).text = $"플레이어 처치 : {0}";
                GetText((int)ETexts.FindMaxCountTitleText).text = $"처치 당한 횟수 : {0}";
                GetText((int)ETexts.InformationTitleText).text = "나무위키 정보";
                GetText((int)ETexts.InformationContentsText).text = $"{Managers.Data.MonData[SpecialCode].script}";
                break;
            case Define.ECurrentClickType.Character:
                GetText((int)ETexts.TItileTitleText).text = $"{Managers.Data.CharacterDataDict[SpecialCode].Name}";
                GetText((int)ETexts.TitlieContentsText).text = "생존자";
                GetText((int)ETexts.ScriptTitleText).text = $"설명 : ";
                GetText((int)ETexts.ScriptContentsText).text = $" {Managers.Data.CharacterDataDict[SpecialCode].script1}";
                GetText((int)ETexts.FindCountText).text = $"몬스터 처치 : {0}";
                GetText((int)ETexts.FindMaxCountTitleText).text = $"최대 몬스터 처치 : {0}";
                GetText((int)ETexts.InformationTitleText).text = "정보";
                GetText((int)ETexts.InformationContentsText).text = $"{Managers.Data.CharacterDataDict[SpecialCode].script1}\n{Managers.Data.CharacterDataDict[SpecialCode].script2}\n{Managers.Data.CharacterDataDict[SpecialCode].script3}\n{Managers.Data.CharacterDataDict[SpecialCode].script4}";
                break;
            case Define.ECurrentClickType.Enviroment:
                break;
        }
    }
    private void SetModel()
    {
        switch (LogBook.ClickType)
        {
            case Define.ECurrentClickType.ItemAndEquip:
                GameObject item = Managers.Resource.Instantiate($"item{SpecialCode}", Get<GameObject>((int)EGameObjects.ObjectSpawnPosition).transform);
                item.GetOrAddComponent<UIItemController>();
                break;
            case Define.ECurrentClickType.Monster:
                GameObject monster = Managers.Resource.Instantiate($"{SpecialCode}Model", Get<GameObject>((int)EGameObjects.ObjectSpawnPosition).transform);
                monster.GetOrAddComponent<UIItemController>();
                break;
            case Define.ECurrentClickType.Character:
                if (!(SpecialCode.Equals(7) || SpecialCode.Equals(1)))
                {
                    return;
                }
                if (SpecialCode.Equals(1))
                {
                    GameObject character = Managers.Resource.Instantiate($"Commando", Get<GameObject>((int)EGameObjects.ObjectSpawnPosition).transform);
                    character.GetOrAddComponent<UIItemController>();

                }
                else if (SpecialCode.Equals(7))
                {
                    GameObject character = Managers.Resource.Instantiate($"Merc", Get<GameObject>((int)EGameObjects.ObjectSpawnPosition).transform);
                    character.GetOrAddComponent<UIItemController>();
                }
                break;
            case Define.ECurrentClickType.Enviroment:
                break;
            case Define.ECurrentClickType.None:
                break;
        }
    }



}
