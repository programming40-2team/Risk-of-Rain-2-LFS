using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DetailInLogBook : UI_Popup
{
  
    public int specialCode = -1;
    private void Start()
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
            .BindEvent((PointerEventData data) => { gameObject.SetActive(false); });

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
    private void OnEnable()
    {
        
        Setting();
    }
    private void Setting()
    {
       
        if (specialCode.Equals(-1))
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
                GetText((int)ETexts.TItileTitleText).text = $"{Managers.Data.ItemDataDict[specialCode].itemname}";
                GetText((int)ETexts.TitlieContentsText).text = "아이템과 장비";
                GetText((int)ETexts.ScriptTitleText).text = $"설명 : ";
                GetText((int)ETexts.ScriptContentsText).text = $" {Managers.Data.ItemDataDict[specialCode].explanation}";
                GetText((int)ETexts.FindCountText).text = $"발견함 : {1}";
                GetText((int)ETexts.FindMaxCountTitleText).text = $"최고중첩 : {1}";
                GetText((int)ETexts.InformationTitleText).text = "정보";
                GetText((int)ETexts.InformationContentsText).text = "아이템에 대한 디테일한 설명!";
                break;
            case Define.ECurrentClickType.Monster:

                break;
            case Define.ECurrentClickType.Character:
                GetText((int)ETexts.TItileTitleText).text = $"{Managers.Data.CharacterDataDict[specialCode].Name}";
                GetText((int)ETexts.TitlieContentsText).text = "생존자";
                GetText((int)ETexts.ScriptTitleText).text = $"설명 : ";
                GetText((int)ETexts.ScriptContentsText).text = $" {Managers.Data.CharacterDataDict[specialCode].script1}";
                GetText((int)ETexts.FindCountText).text = $"몬스터 처치 : {1}";
                GetText((int)ETexts.FindMaxCountTitleText).text = $"최대 몬스터 처치 : {1}";
                GetText((int)ETexts.InformationTitleText).text = "정보";
                GetText((int)ETexts.InformationContentsText).text = "캐릭터에 대한 디테일한 설명!";
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
               GameObject go= Managers.Resource.Instantiate($"item{Managers.Data.ItemDataDict[specialCode].itemcode}", Get<GameObject>((int)EGameObjects.ObjectSpawnPosition).transform);
                go.transform.localScale = Vector3.one * 22000f;
                go.transform.localPosition = new Vector3(0, 0 ,- 100);
                break;
            case Define.ECurrentClickType.Monster:
                break;
            case Define.ECurrentClickType.Character:
                Debug.Log("추후 캐릭터 모델링 완성되면 완성된 캐릭터만 모델 추가");
                //Managers.Resource.Instantiate($"item{Managers.Data.CharacterDataDict[specialCode].}", Get<GameObject>((int)EGameObjects.ObjectSpawnPosition).transform);

                break;
            case Define.ECurrentClickType.Enviroment:
                break;
            case Define.ECurrentClickType.None:
                break;
        }
    }



}
